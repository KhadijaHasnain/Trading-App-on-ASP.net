using BIGQXWebsite.Models;
using System.Globalization;

namespace BIGQXWebsite.Helpers
{
  public static class LocalizationHelper
  {
    private static CultureInfo Info()
    {
      CultureInfo uiCultureInfo = Thread.CurrentThread.CurrentUICulture;
      //CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;

      return uiCultureInfo;
    }
    public static string GetCode()
    {
      return Info().Name;
    }

    public static string GetEnglishName()
    {
      return Info().EnglishName;
    }

    public static string GetNativeName()
    {
      return Info().NativeName;
    }

    public static Language GetLanguage()
    {
      var success = Enum.TryParse(Info().EnglishName, out Language language);
      return success ? language : Language.Arabic;
    }
  }
}
