namespace CodeGenerator
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            var application = new Dictionary<string, string>
            {
                {"$ApplicationName$", "BibleTraining"},
                {"$ngApp$",           "bt"},
                {"$DataDomain$",      "IBibleTrainingDomain"}
            };

            var projects = new []
            {
                @"C:\dev\BibleTraining\src\BibleTraining\BibleTraining.csproj",
                @"C:\dev\BibleTraining\src\BibleTraining.Api\BibleTraining.Api.csproj",
                @"C:\dev\BibleTraining\src\BibleTraining.Migrations\BibleTraining.Migrations.csproj",
                @"C:\dev\BibleTraining\src\BibleTraining.Web.UI\BibleTraining.Web.UI.csproj",
                @"C:\dev\BibleTraining\test\BibleTraining.Test\BibleTraining.Test.csproj"
            };

            var entities = new [] {
                new Dictionary<string, string>
                {
                    {"$Entity$",                "EmailType"},
                    {"$EntityPlural$",          "EmailTypes"},
                    {"$entityLowercase$",       "emailType"},
                    {"$entityPluralLowercase$", "emailTypes"}
                },
                new Dictionary<string, string>
                {
                    {"$Entity$",                "AddressType"},
                    {"$EntityPlural$",          "AddressTypes"},
                    {"$entityLowercase$",       "addressType"},
                    {"$entityPluralLowercase$", "addressTypes"}
                }
            };

            Generate(application, entities, projects);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        public static void Generate(
            Dictionary<string, string> applicationKeys,
            Dictionary<string, string>[] entities,
            string[] projects)
        {
            foreach (var entityKeys in entities)
            {
                var files = new Generator(@"c:\dev\BibleTraining\templates", @"c:\dev", applicationKeys, entityKeys).Generate();
                new ProjectUpdater().Update(files, projects);
            }
        }
    }
}
