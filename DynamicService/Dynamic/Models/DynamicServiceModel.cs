namespace DynamicService.Dynamic.Models
{
    public class DynamicServiceModel
    {
        public string Id { get; set; }
        public string ServiceName { get; set; }
        public object[] Parameter { get; set; }
        public string MethodName { get; set; }
    }
}