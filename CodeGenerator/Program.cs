namespace CodeGenerator
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            new Generator("c:/dev/BibleTraining/templates", "c:/dev", new Dictionary<string, string>
            {
                //Application
                {"$ApplicationName$",       "BibleTraining"},
                {"$ngApp$",                 "bt"},
                {"$DataDomain$",            "IBibleTrainingDomain"},

                //Entity
                {"$Entity$",                "Baz"},
                {"$EntityPlural$",          "Bazs"},
                {"$entityLowercase$",       "baz"},
                {"$entityPluralLowercase$", "bazs"}

            }).Generate();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
