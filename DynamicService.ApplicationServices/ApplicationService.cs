namespace DynamicService.ApplicationServices
{
    public class ApplicationService : IApplicationService
    {
        public string Hello(string name)
        {
            return "Hello " + name;
        }
    }
}
