namespace DataMappingService.Models
{
    public class CarData
    {
        public string CarModel { get; set; }

        public int YearOfManufacture { get; set; }

        public EngineTypes EngineType { get; set; }

        public decimal? EngineVolume { get; set; }

        public string Color { get; set; }
    }
}
