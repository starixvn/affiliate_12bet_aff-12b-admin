namespace Fanex.Affiliate.User.Core.Utilities
{
    public interface IAppSetting
    {
        string GetValue(string key);

        string Environment { get; }

        bool IsLocal { get; }
    }
}
