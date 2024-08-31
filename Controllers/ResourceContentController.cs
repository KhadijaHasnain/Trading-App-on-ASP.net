using BIGQXWebsite.Helpers;
using BIGQXWebsite.Models;
using BIGQXWebsite.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BIGQXWebsite.Controllers
{
  [Route("{culture=en}/Admin/{controller}/{action}/{id?}")]
  public class ResourceContentController : Controller
  {
    private readonly IResourceContent _model;
    private readonly Language _language;
    public ResourceContentController(IResourceContent model)
    {
      _model = model;
      _language = LocalizationHelper.GetLanguage();
    }

    #region Actions
    public IActionResult UpdatePartial(int id, ResourceTypeEnum? type, ResourcePosition? position)
    {
      ViewData["Title"] = "Update";
      try
      {
        var model = _model.Get(id, _language);
        if (model == null)
        {
          model = new ResourceContent();
          model.Language = _language;
          model.Position = position ?? ResourcePosition.NotSet;
          model.Type = type ?? ResourceTypeEnum.Text;
        }
        var partialName = "_Update" + model.Type.ToString();

        return PartialView(partialName, model);
      }
      catch (Exception ex)
      {
        return BadRequest();
      }
    }

    [HttpPost]
    public IActionResult Update([FromForm] ResourceContent model)
    {
      try
      {
        _model.AddOrUpdate(model);
        return Ok(model);
      }
      catch
      {
        return BadRequest();
      }
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
      if (id != 0)
      {
        _model.Remove(id, _language);
        return Ok();
      }
      return BadRequest();
    }
    #endregion

  }

}
