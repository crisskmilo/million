using System;

namespace Million.Domain.Entities.Dto.Transversal
{
    public class DailyForecastDto
    {
        public DateTime Date { get; set; }

        public double MinTemperature { get; set; }

        public double MaxTemperature { get; set; }
    }
}
