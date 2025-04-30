using System.Reflection;

namespace ReflectorKG.Classes;

/*
     AppInfo
        returns title, copyright, version, etc of Application.
*/

internal class AppInfo
{
    /*
        AppInfo -> Title
    */

    public static string Title
    {
        get
        {
            var title = (AssemblyTitleAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyTitleAttribute));

            if (title != null && !string.IsNullOrEmpty(title.Title))
                return title.Title;

            return string.Empty;
        }
    }

    /*
        AppInfo -> Description
    */

    public static string Description
    {
        get
        {
            var desc = (AssemblyDescriptionAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyDescriptionAttribute));

            if (desc != null && !string.IsNullOrEmpty(desc.Description))
                return desc.Description;

            return string.Empty;
        }
    }

    /*
        AppInfo -> Author
    */

    public static string Trademark
    {
        get
        {
            var tm = (AssemblyTrademarkAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyTrademarkAttribute));

            if (tm != null && !string.IsNullOrEmpty(tm.Trademark))
                return tm.Trademark;

            return string.Empty;
        }
    }

    /*
        AppInfo -> Company
    */

    public static string Company
    {
        get
        {
            var comp = (AssemblyCompanyAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyCompanyAttribute));

            if (comp != null && !string.IsNullOrEmpty(comp.Company))
                return comp.Company;

            return string.Empty;
        }
    }

    /*
        AppInfo -> Copyright
    */

    public static string Copyright
    {
        get
        {
            var cr = (AssemblyCopyrightAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyCopyrightAttribute));

            if (cr != null && !string.IsNullOrEmpty(cr.Copyright))
                return cr.Copyright;

            return string.Empty;
        }
    }

    /*
        AppInfo -> Version
    */

    public static string Version
    {
        get
        {
            var _ver = Assembly.GetExecutingAssembly().GetName().Version;
            var ver  = $"{_ver.Major}.{_ver.Minor}.{_ver.Build}.{_ver.Revision}";

            if (ver != null && !string.IsNullOrEmpty(ver))
                return ver;

            return string.Empty;
        }
    }
}