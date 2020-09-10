using System;
using System.ComponentModel.DataAnnotations;

namespace IL.DTO.Core.ReportDTO
{
    public class CommonReport
    {
        [Required]
        public string FromDate { get; set; }
        [Required]
        public string ToDate { get; set; }
        [Required]
        public int OutletId { get; set; }
    }
}
