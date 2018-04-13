namespace MultiTenant.Models
{
    public class AppTenant
    {
        public string AppName { get; set; }
        public string[] Hostnames { get; set; }
        public string Theme { get; set; }
    }
}