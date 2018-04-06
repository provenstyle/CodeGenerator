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
            if (args.Length > 0 && args[0] == "init")
            {
                var name = (args.Length > 1)
                               ? args[1]
                               : "MyApp";
                new Initializer().Init(name);
                return;
            }

            try
            {
                Generator.Run(GetConfig());
                Console.WriteLine("Generated Entities");
            }
            catch(ConfigurationErrorsException ex)
            {
                Console.WriteLine("Configuration Error:");
                Console.WriteLine($"    {ex.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        private static GeneratorConfig GetConfig()
        {
            var configFile = GeneratorConfig.FileName;
            var path = Path.Combine(Environment.CurrentDirectory, configFile);
            if(!File.Exists(path))
                throw new ConfigurationErrorsException($"{configFile} not found.");

            var json = File.ReadAllText(path);
            var config = JsonConvert.DeserializeObject<GeneratorConfig>(json);

            var templateDirectory = config.ApplicationConfig.TemplateFolder;
            if(!Directory.Exists(templateDirectory))
                throw new ConfigurationErrorsException($"{templateDirectory} not found.");

            return config;
        }
    }

}
