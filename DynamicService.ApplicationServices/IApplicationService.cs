using DynamicService.ApplicationServices.Models;

namespace DynamicService.ApplicationServices
{
    public interface IApplicationService
    {
        string Hello(string name);
        TestModel CreateTestModel(TestModel model);
        void TestVoid(string name);
    }
}
