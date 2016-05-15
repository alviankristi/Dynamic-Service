using DynamicService.ApplicationServices.Models;

namespace DynamicService.ApplicationServices
{
    public class ApplicationService : IApplicationService
    {
        public string Hello(string name)
        {
            return "Hello " + name;
        }

        public TestModel CreateTestModel(TestModel model)
        {
            return model;
        }
    }
}
