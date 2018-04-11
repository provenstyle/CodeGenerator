namespace CodeGenerator.Features
{
    public class GeneratorConfig
    {
        public static string FileName = "generatorConfig.json";

        public bool              Replace           { get; set; }
        public string            TemplateFolder    { get; set; }
        public ApplicationConfig ApplicationConfig { get; set; }
        public string[]          EntityNames       { get; set; }
    }
}