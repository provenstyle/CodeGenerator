namespace CodeGenerator
{
    using System;
    using Features;

    class Program
    {
        static void Main(string[] args)
        {
            var applicationConfig = new ApplicationConfig
            {
                SolutionFolder  = @"c:\dev\BibleTraining",
                TemplateFolder  = @"c:\dev\BibleTraining\templates",
                ApplicationName = "BibleTraining",
                DataDomain      = "IBibleTrainingDomain",
                ngApp           = "bt"
            };

            var projectsToUpdate = new []
            {
                @"C:\dev\BibleTraining\src\BibleTraining\BibleTraining.csproj",
                @"C:\dev\BibleTraining\src\BibleTraining.Api\BibleTraining.Api.csproj",
                @"C:\dev\BibleTraining\src\BibleTraining.Migrations\BibleTraining.Migrations.csproj",
                @"C:\dev\BibleTraining\src\BibleTraining.Web.UI\BibleTraining.Web.UI.csproj",
                @"C:\dev\BibleTraining\test\UnitTests\UnitTests.csproj",
                @"C:\dev\BibleTraining\test\IntegrationTests\IntegrationTests.csproj"
            };

            var entityNames = new[]
            {
                "Address",
                "AddressType",
                "Email",
                "EmailType",
                //"Person",
                "Phone",
                "PhoneType",
            };

            Generator.Run(applicationConfig, entityNames, projectsToUpdate);

            Console.WriteLine("Generated Entities: Press any key to exit");
            Console.ReadKey();
        }
    }
}
