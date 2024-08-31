using BIGQXWebsite.Models;

namespace BIGQXWebsite.Helpers
{
  public static class Helper
  {
    public static ResourceContent FirstOrNew(this IEnumerable<ResourceContent> resources, int id)
    {
      var resource = resources.FirstOrDefault(x => x.Id == id);
      if (resource == null)
      {
        resource = new ResourceContent();
        resource.Id = id;
        resource.Language = LocalizationHelper.GetLanguage();
        resource.Position = resource.Position;
      }
      return resource;
    }

    public static bool HasValue(this string str)
    {
      return !String.IsNullOrWhiteSpace(str);
    }

  }
}
