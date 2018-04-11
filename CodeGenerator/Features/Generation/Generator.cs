namespace CodeGenerator.Features.Generation
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Pluralization;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;

    public static class Generator
    {
        public static void Run()
        {
            var configFiles = Files
                .GetFiles(Environment.CurrentDirectory, GeneratorConfig.FileName)
                .ToArray();

            foreach (var configFile in configFiles)
            {
                var templateDirectory = new DirectoryInfo(configFile).Parent;
                if(templateDirectory == null) continue;

                var config = GetConfig(configFile);
                config.TemplateFolder = templateDirectory.FullName;
                ProcessTemplates(config);
            }
        }

        private static GeneratorConfig GetConfig(string path)
        {
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<GeneratorConfig>(json);
        }

        private static void ProcessTemplates(GeneratorConfig config)
        {
            var pluralization = new EnglishPluralizationService();

            foreach (var entity in config.EntityNames)
            {
                var plural = pluralization.Pluralize(entity);

                var keys = new Dictionary<string, string>
                {
                    {"$ApplicationName$", config.ApplicationConfig.ApplicationName},
                    {"$ngApp$",           config.ApplicationConfig.ngApp},
                    {"$DataDomain$",      config.ApplicationConfig.DataDomain},

                    {"$Entity$",                entity.UpperFirstLetter()},
                    {"$EntityPlural$",          plural.UpperFirstLetter()},
                    {"$entityLowercase$",       entity.LowerFirstLetter()},
                    {"$entityPluralLowercase$", plural.LowerFirstLetter()}
                };

                var files = new TemplateProcessor(config, keys).Run();
                if(files.Any())
                    new ProjectUpdater().Run(files);
            }
        }
    }
}
