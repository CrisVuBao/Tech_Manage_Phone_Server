using Domain.Interface;
using Tech_Manage_Server.DTOs.RepairModelDto;

namespace Tech_Manage_Server.Helpers
{
    public class CalculateTotalAmount
    {
        private readonly IUnitOfWork _unitOfWork;

        public CalculateTotalAmount(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public decimal CalTotalAmount(CreateRepairDto createRepairDto)
        //{
        //    decimal total = 0;

        //    // Tính tổng tiền cho các linh kiện
        //    if(createRepairDto != null && createRepairDto.RepairItems.Any())
        //    {
        //        foreach(var item in createRepairDto.RepairItems)
        //        {
        //            var inventory =  _unitOfWork.Inventories.GetInventoryByIdAsync(item.InventoryId).Result;
        //            if (inventory != null)
        //            {
        //                total += inventory.Price * item.Quantity;
        //            }
        //        }
        //    }

        //    return total;
        //}
    }
}
