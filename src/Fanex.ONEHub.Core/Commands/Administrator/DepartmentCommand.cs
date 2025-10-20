namespace Fanex.Affiliate.Admin.Core.Commands.Administrator
{
    public class DepartmentCommand : BaseNonQueryCommand
    {
        private readonly string SpKey;

        public DepartmentCommand(string spKey)
        {
            SpKey = spKey;
        }
        public override string GetSettingKey() => SpKey;

        public override bool IsValid() => !string.IsNullOrWhiteSpace(SpKey);

        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public string GroupDesc { get; set; }
    }
}
