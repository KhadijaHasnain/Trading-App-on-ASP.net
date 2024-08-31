using BIGQXWebsite.Services;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BIGQXWebsite.Helpers
{
  [HtmlTargetElement(Attributes = "resource")]
  public class ResourceDb : TagHelper
  {
    private readonly IResourceContent _resorce;
    public string Resource { get; set; }

    public ResourceDb(IResourceContent resource)
    {
      _resorce = resource;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      var resources = _resorce.GetList();
      var resource = resources.FirstOrNew(int.Parse(Resource));
      if (!string.IsNullOrWhiteSpace(resource.Text))
      {
        if (context.TagName == "img")
        {
          if (context.AllAttributes.Where(x => x.Name == "b-width").Any())
          {
            resource.Text += "?width=" + context.AllAttributes.FirstOrDefault(x => x.Name == "b-width").Value;
          }
          output.Attributes.SetAttribute("src", resource.Text);
        }
        else if (resource.Text != "-")
        {
          output.Content.SetHtmlContent(resource.Text);
        }
        else
        {
          output.Content.SetHtmlContent("");
        }
      }
      output.Attributes.SetAttribute("resource", Resource);
    }

  }
}
