namespace DexExam.Application.DTOs.Reading
{
    public class ReadingResponseDto
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double BatteryLevel { get; set; }
    }
}