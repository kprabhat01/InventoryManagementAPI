using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IL.DTO.Core.CommonDTO;
namespace IL.DTO.Core.PODTO
{
    public class PORequestDTO
    {
        public int Id { get; set; }
        [Required]
        public int FromOutletId { get; set; }
        [Required]
        public int ToOutletId { get; set; }
        public System.DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }

        public int StatusId { get; set; } = 2;
        [Required]
        public List<ItemQty> POItems { get; set; }
    }

    public class POReturnDTO
    {
        public int Id { get; set; }
        public CommonDTO.CommonDTO FromStore { get; set; }
        public CommonDTO.CommonDTO ToStore { get; set; }
        public CommonDTO.CommonDTO Status { get; set; }
        public string Username { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<POItemReturnDTO> ItemDetail { get; set; }
    }
    public class POItemReturnDTO
    {
        public int Id { get; set; }
        public CommonDTO.CommonDTO Item { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public CommonDTO.CommonDTO Unit { get; set; }
    }
    public class ItemQty
    {
        [Required]
        public Nullable<int> itemid { get; set; }
        [Required]
        public decimal quantity { get; set; }
    }
    public class PostedItem
    {
        public int ItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
    }
    public class ProcessPODTO
    {
        [Required]
        public int Id { get; set; }
        public int StatusID { get; set; } = 3;
        [Required]
        public List<PostedItem> Items { get; set; }
        [Required]
        public string Username { get; set; }

    }
    public class TransferPORequest
    {
        [Required]
        public int FromOutletId { get; set; }
        [Required]
        public int ToOutletId { get; set; }
        [Required]
        public int PrId { get; set; }
        [Required]
        public string Username { get; set; }
    }
}
