namespace CodeGenerator
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            var files = new Generator(@"c:\dev\BibleTraining\templates", @"c:\dev", new Dictionary<string, string>
            {
                //Application
                {"$ApplicationName$",       "BibleTraining"},
                {"$ngApp$",                 "bt"},
                {"$DataDomain$",            "IBibleTrainingDomain"},

                //These world don't work Message, Action
                //Entity
                {"$Entity$",                "Pet"},
                {"$EntityPlural$",          "Pets"},
                {"$entityLowercase$",       "pet"},
                {"$entityPluralLowercase$", "pets"}

            }).Generate();

            new ProjectUpdater().Update(files, new []
            {
                @"C:\dev\BibleTraining\src\BibleTraining\BibleTraining.csproj",
                @"C:\dev\BibleTraining\src\BibleTraining.Api\BibleTraining.Api.csproj",
                @"C:\dev\BibleTraining\src\BibleTraining.Migrations\BibleTraining.Migrations.csproj",
                @"C:\dev\BibleTraining\src\BibleTraining.Web.UI\BibleTraining.Web.UI.csproj",
                @"C:\dev\BibleTraining\test\BibleTraining.Test\BibleTraining.Test.csproj"
            });

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
