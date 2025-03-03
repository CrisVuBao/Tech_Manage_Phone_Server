using Gemini.NET;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Enums;
using Newtonsoft.Json.Linq;
using Tech_Manage_Server.Application.Helpers;

namespace Tech_Manage_Server.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class GeminiController : ControllerBase
    {
        public GeminiController() { }

        [HttpPost("GenerateChat")]
        public async Task<ActionResult> GenerateChat(AIChatRequest aiChatRes)
        {
            var generator = new Generator("AIzaSyCnAepmeJQIWdabakaVBzZaRLvmIIAj-l4");

            // build an API request
            var apiRequest = new ApiRequestBuilder()
                .WithPrompt(aiChatRes.Text)
                .WithDefaultGenerationConfig(temperature: 0.7F, responseMimeType: ResponseMimeType.Json)
                .DisableAllSafetySettings()
                .EnableGrounding()
                .Build();

            // tạo nội dung với mô hình ổn định mới nhất
            var modelVersion = Generator.GetLatestStableModelVersion();
            var response = await generator.GenerateContentAsync(apiRequest, modelVersion);

            // in kết quả
            var res = response.Result;

            return Ok(res);
        }
    }
}
