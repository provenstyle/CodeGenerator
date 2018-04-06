namespace CodeGenerator.Features
{
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Pluralization;
    using System.Linq;

    public static class Generator
    {
        public static void Run(GeneratorConfig config)
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

                var files = new TemplateProcessor(config.ApplicationConfig, keys).Run();
                if(files.Any())
                    new ProjectUpdater().Run(files, config.Projects);
            }
        }
    }
}
