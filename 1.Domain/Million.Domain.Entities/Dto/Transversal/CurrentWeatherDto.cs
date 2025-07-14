using System;

namespace Million.Domain.Entities.Dto.Transversal
{
    public class CurrentWeatherDto
    {
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public string Weather { get; set; }
        public string Description { get; set; }
    }
}
