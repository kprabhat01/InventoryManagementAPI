using System;
using System.ComponentModel.DataAnnotations;

namespace IL.DTO.Core.ItemDTO
{
    public class ItemDTO
    {
        public int ItemId { get; set; }
        public string NormalizeName { get; set; }
        public CommonDTO.CommonDTO Units { get; set; }
    }

    public class ItemDetailDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        public string Comment { get; set; }
        [Required]
        public int UnitId { get; set; }
    }

    public class ItemStockDTO
    {
        public int ItemId { get; set; }
        public string NormalizeName { get; set; }
        public decimal Quantity { get; set; }
        public CommonDTO.CommonDTO Units { get; set; }
        public DateTime LastUpdated { get; set; }
    }
    public class ItemOutWardStockDTO
    {        
        public int ItemId { get; set; }
        public int OutletId { get; set; }
        public int MovementId { get; set; }
        public string UserName { get; set; }
        public decimal Quantity { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Comment { get; set; }
        public decimal Rate { get; set; }
    }
}
