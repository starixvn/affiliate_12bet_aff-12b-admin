namespace Fanex.Affiliate.Admin.Core.Commands.LandingPage
{
    public abstract class BaseModuleMediaCommand : BaseNonQueryCommand
    {
        public int ModuleId { get; set; }

        public string MediaName { get; set; }

        public string FileName { get; set; }

        public string Size { get; set; }
    }
}
