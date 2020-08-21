namespace IL.DTO.Core.CommonDTO
{
    public class CommonDTO
    {
        public int Id { get; set; }
        public string NormalizeName { get; set; }

    }
    public class CommonOutletDTO
    {
        public int Id { get; set; }
        public string NormalizeName { get; set; }
        public decimal IncrementPercentage { get; set; }
    }
}
