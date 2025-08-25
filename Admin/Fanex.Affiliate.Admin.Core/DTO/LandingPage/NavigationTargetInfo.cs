namespace Fanex.Affiliate.Admin.Core.DTO.LandingPage
{
    public class NavigationTargetInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public bool IsExternal { get; set; }

        public bool RequiresAuthentication { get; set; }
    }
}
