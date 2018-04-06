namespace CodeGenerator
{
    using System;
    using System.Configuration;
    using System.IO;
    using Features;
    using Newtonsoft.Json;

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Generator.Run(GetConfig());
                Console.WriteLine("Generated Entities: Press any key to exit");
                Console.ReadKey();
            }
            catch(ConfigurationErrorsException ex)
            {
                Console.WriteLine("Configuration Error:");
                Console.Write($"    {ex.Message}");
            }
        }

        private static GeneratorConfig GetConfig()
        {
            var configFile = "generatorConfig.json";
            var path = Path.Combine(Environment.CurrentDirectory, configFile);
            if(!File.Exists(path))
                throw new ConfigurationErrorsException($"{configFile} not found.");

            var json = File.ReadAllText(path);
            var config = JsonConvert.DeserializeObject<GeneratorConfig>(json);

            var templateDirectory = config.ApplicationConfig.TemplateFolder;
            if(!Directory.Exists(templateDirectory))
                throw new ConfigurationErrorsException($"{configFile} not found.");

            return config;
        }
    }

}
