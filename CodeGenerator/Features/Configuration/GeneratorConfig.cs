namespace CodeGenerator.Features
{
    public class GeneratorConfig
    {
        public static string FileName = "generatorConfig.json";

        public ApplicationConfig ApplicationConfig { get; set; }
        public string[]          EntityNames       { get; set; }
        public string[]          Projects          { get; set; }
    }
}