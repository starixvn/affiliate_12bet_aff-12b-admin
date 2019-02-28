/// <summary>
/// Summary description for Language
/// </summary>
public static class Language
{
    private static string _english = "en-US";
    private static string _chinese = "zh-CN";
    // ... more language

    public static string ENGLISH
    {
        get
        {
            return _english;
        }
    }

    public static string CHINESE
    {
        get
        {
            return _chinese;
        }
    }
}
