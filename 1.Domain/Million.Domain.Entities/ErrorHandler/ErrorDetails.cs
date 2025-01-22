namespace Million.Domain.Entities.ErrorHandler
{
    using Newtonsoft.Json;

    public class ErrorDetails
    {
        [JsonProperty(PropertyName = "resultCode")]
        public string ResultCode { get; set; }

        [JsonProperty(PropertyName = "resultMsg")]
        public string ResultMsg { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}