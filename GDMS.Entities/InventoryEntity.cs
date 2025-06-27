using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.Entities
{
    public class InventoryEntity : BaseEntity
    {

        public int? Id { get; set; }
        public int UserId { get; set; }
        public string User_Token { get; set; }

        public string BatchId { get; set; }
        public string CartonNo { get; set; }
        public string DepartmentName { get; set; }
        public string BatchCreatedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string WarehouseEntryBy { get; set; }
        public string WarehouseApprovedBy { get; set; }
        public string BatchCreatedDate { get; set; }
        public string ApprovedDate { get; set; }
        public string RejectedDate { get; set; }
        public string WarehouseEntryDate { get; set; }
        public string WarehouseApprovedDate { get; set; }
        public string PickupDate { get; set; }
        public string WarehouseLocationUpdatedDate { get; set; }
        public string InventoryDate { get; set; }
        public string Status { get; set; }
        public string InventoryBy { get; set; }

        public string documents { get; set; }
        public string ItemLcoation { get; set; }
        public string warehouseName { get; set; }
        public string ReteionPeriod { get; set; }

        public string DocumentDetails { get; set; }

    }
}