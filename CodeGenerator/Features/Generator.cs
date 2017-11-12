namespace CodeGenerator.Features
{
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Pluralization;
    using System.Linq;

    public static class Generator
    {
        public static void Run(
            ApplicationConfig applicationConfig,
            string[]          entityNames,
            string[]          projects)
        {
            var pluralization = new EnglishPluralizationService();

            foreach (var entity in entityNames)
            {
                var plural = pluralization.Pluralize(entity);

                var keys = new Dictionary<string, string>
                {
                    {"$ApplicationName$", applicationConfig.ApplicationName},
                    {"$ngApp$",           applicationConfig.ngApp},
                    {"$DataDomain$",      applicationConfig.DataDomain},

                    {"$Entity$",                entity.UpperFirstLetter()},
                    {"$EntityPlural$",          plural.UpperFirstLetter()},
                    {"$entityLowercase$",       entity.LowerFirstLetter()},
                    {"$entityPluralLowercase$", plural.LowerFirstLetter()}
                };

                var files = new TemplateProcessor(applicationConfig, keys).Run();
                if(files.Any())
                    new ProjectUpdater().Run(files, projects);
            }
        }
    }
}
