using System.Resources;

namespace EcosferaBlazor.Auth.Application.Common.Helper;
public static class ConstantStringLocalizer
{
    public const string CONSTANTSTRINGRESOURCEID = "EcosferaBlazor.Auth.Application.Resources.Constants.ConstantString";
    private static readonly ResourceManager rm;
    static ConstantStringLocalizer() {
        rm = new ResourceManager(CONSTANTSTRINGRESOURCEID, typeof(ConstantStringLocalizer).Assembly);
    }
    public static string Localize(string key)
    {
        return rm.GetString(key, CultureInfo.CurrentCulture) ?? key;
    }
}
