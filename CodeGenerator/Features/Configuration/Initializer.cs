namespace CodeGenerator.Features
{
    using System;
    using System.IO;
    using Newtonsoft.Json;

    public class Initializer
    {
        public void Init(string appName)
        {
            appName = appName.UpperFirstLetter();

            var workingDirectory = Environment.CurrentDirectory;
            var configFile = Path.Combine(workingDirectory, GeneratorConfig.FileName);

            if (!File.Exists(configFile))
            {
                var config = new GeneratorConfig
                {
                    ApplicationConfig = new ApplicationConfig
                    {
                       ApplicationName = appName,
                       SolutionFolder  = workingDirectory,
                       TemplateFolder  = $"{workingDirectory}\\templates",
                       DataDomain      = $"I{appName}Domain",
                       ngApp           = appName.LowerFirstLetter()
                    },
                    Projects = new []
                    {
                        $"{workingDirectory}\\src\\{appName}\\{appName}.csproj",
                        $"{workingDirectory}\\src\\{appName}.Api\\{appName}.Api.csproj",
                        $"{workingDirectory}\\src\\{appName}.Migrations\\{appName}.Migrations.csproj",
                        $"{workingDirectory}\\src\\{appName}.Web.UI\\{appName}.Web.UI.csproj",
                        $"{workingDirectory}\\test\\UnitTests\\UnitTests.csproj",
                        $"{workingDirectory}\\test\\IntegrationTests\\IntegrationTests.csproj"
                    },
                    EntityNames = new []
                    {
                        "Foo",
                        "Bar",
                        "Baz"
                    }
                };
                var json = JsonConvert.SerializeObject(config, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });
                File.WriteAllText(configFile, json);
            }
        }
    }
}
