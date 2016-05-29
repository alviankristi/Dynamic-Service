using System;
using DynamicService.ApplicationServices.Models;

namespace DynamicService.ApplicationServices
{
    public class ApplicationService : IApplicationService
    {
        public string Hello(string name)
        {
            return "Hello " + name;
        }

        public void TestVoid(string name)
        {
            Console.WriteLine("I'm void");
        }

        public string NameAndNumber(string name, int number)
        {
            return name + " & " + number;
        }

        public string NameNumberAndDate(string name, int number, DateTime date)
        {
            return name + " & " + number + " & " + date;
        }

        public TestModel CreateTestModel(TestModel model)
        {
            return model;
        }
    }
}
