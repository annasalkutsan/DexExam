namespace DexExam.Application.DTOs.Sensor
{
    public class SensorResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string GeoCoordinates { get; set; }
        public string PhotoUrl { get; set; }
        public double MinThreshold { get; set; }
        public double MaxThreshold { get; set; }
        public double BatteryLevel { get; set; }
    }
}