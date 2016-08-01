using System.Globalization;
using System.Linq;
using System.Text;

namespace DynamicService.Dynamic.JavascriptGenerator
{
    public class AngularScriptGenerator : IScriptGenerator
    {
        public string GenerateScript()
        {
            var template = new StringBuilder();
            template.AppendLine("var module = angular.module('dynamicService',[])");
            var services = DynamicApiBuilderManager.GetAll();
            foreach (var dynamicServiceInfo in services)
            {
                template.AppendLine("module.service(\"" + ToCamelCase(dynamicServiceInfo.ServiceName.Substring(1))+ "\",");
                template.AppendLine("\t function($http){");
                dynamicServiceInfo.MethodServices.ForEach(method =>
                {
                    template.AppendFormat("\t \t this.{0}=function({1}){{ \n", ToCamelCase(method.Name) , SetParamater(method.Parameters.Any()));
                    template.AppendLine("\t \t \t var data = {");
                    template.AppendFormat("\t \t \t \t  ServiceName: \"{0}\", \n", dynamicServiceInfo.ServiceFullName);
                    template.AppendFormat("\t \t \t \t  MethodName: \"{0}\", \n", method.Name);
                    template.AppendFormat("\t \t \t \t  Id: \"{0}\", \n", dynamicServiceInfo.Id);
                    template.AppendFormat("\t \t \t \t  Parameter: {0}, \n", SetData(method.Parameters.Any()));
                    template.AppendLine("\t \t \t }");
                    template.AppendLine("\t \t \t return $http({");
                    template.AppendLine("\t \t \t \t method:\"POST\",");
                    template.AppendLine("\t \t \t \t url:\"/DynamicService/Post/\",");
                    template.AppendLine("\t \t \t \t data: JSON.stringify(data),");
                    template.AppendLine("\t \t \t \t contentType: \"application/x-www-form-urlencoded\",");
                    template.AppendLine("\t \t \t \t dataType: \"json\",");
                    template.AppendLine("\t \t \t });");
                    template.AppendLine("\t \t }");
                });
                template.AppendLine("});\n\n");
            }
            return template.ToString();
        }


        private string SetParamater(bool anyParamters)
        {
            return anyParamters ? "model" : string.Empty;
        }

        private string SetData(bool anyParamters)
        {
            return anyParamters ? "model" : "{}";
        }

        public static string ToCamelCase( string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            if (str.Length == 1)
            {
                return str.ToLower(CultureInfo.InvariantCulture);
            }

            return char.ToLower(str[0], CultureInfo.InvariantCulture) + str.Substring(1);
        }
    }
}