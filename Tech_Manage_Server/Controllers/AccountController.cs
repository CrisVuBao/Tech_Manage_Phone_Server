using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tech_Manage_Server.DTOs.Auth;
using Tech_Manage_Server.DTOs.Auths;
using Tech_Manage_Server.Models;

namespace Tech_Manage_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Kiểm tra Email đã tồn tại
            var emailExists = await _userManager.FindByEmailAsync(model.Email);
            if (emailExists != null)
                return Conflict(new { message = "Email already exists!" });

            // Kiểm tra PhoneNumber đã tồn tại
            var phoneExists = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == model.PhoneNumber);
            if (phoneExists != null)
                return Conflict(new { message = "Phone number already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Email, // Sử dụng Email làm UserName
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            return Ok(new { message = "User created successfully!" });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ApplicationUser user = null;

            // Kiểm tra xem đầu vào là Email hay Số điện thoại
            if (new EmailAddressAttribute().IsValid(model.EmailOrPhoneNumber))
            {
                // Đầu vào là Email
                user = await _userManager.FindByEmailAsync(model.EmailOrPhoneNumber);
            }
            else
            {
                // Đầu vào có thể là Số điện thoại
                user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == model.EmailOrPhoneNumber);
            }

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Email),
            new Claim("FullName", user.FullName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

                // Thêm các claims về vai trò
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    model.EmailOrPhoneNumber,
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return Unauthorized(new { message = "Thông tin xác thực không hợp lệ!" });
        }


        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.UtcNow.AddHours(5),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

    }
}
