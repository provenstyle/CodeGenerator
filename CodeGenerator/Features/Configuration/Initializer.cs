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
                       DataDomain      = $"I{appName}Domain",
                       ngApp           = appName.LowerFirstLetter()
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
