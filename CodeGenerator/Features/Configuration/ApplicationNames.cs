namespace CodeGenerator.Features
{
    public class ApplicationConfig
    {
        public bool   Replace         { get; set; }
        public string TemplateFolder  { get; set; }

        public string ApplicationName { get; set; }
        public string ngApp           { get; set; }
        public string DataDomain      { get; set; }
    }
}