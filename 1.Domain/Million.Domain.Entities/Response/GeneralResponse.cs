namespace Million.Domain.Entities.Response
{
    using Newtonsoft.Json;

    public class GeneralResponse
    {
        public bool isSuccess { get; set; }

        public string message { get; set; }

        public object result { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}