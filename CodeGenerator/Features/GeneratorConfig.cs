namespace CodeGenerator.Features
{
    public class GeneratorConfig
    {
        public ApplicationConfig ApplicationConfig { get; set; }
        public string[]          EntityNames       { get; set; }
        public string[]          Projects          { get; set; }
    }
}