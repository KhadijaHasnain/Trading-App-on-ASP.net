using BIGQXWebsite.Data;
using BIGQXWebsite.Helpers;
using BIGQXWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace BIGQXWebsite.Services
{
  public class ResourceContentService : IResourceContent
  {
    private readonly ApplicationDbContext _db;
    private readonly Language _language;
    private readonly List<ResourceContent> _resources;
    public ResourceContentService(ApplicationDbContext context)
    {
      _db = context;
      _language = LocalizationHelper.GetLanguage();
      _resources = _db.Resource.AsNoTracking().Where(x => x.Language == _language).ToList();
    }

    public ResourceContent AddOrUpdate(ResourceContent resource)
    {
      if (_db.Resource.Any(x => x.Id == resource.Id && x.Language == resource.Language))
      {
        _db.Resource.Update(resource);
      }
      else
      {
        _db.Resource.Add(resource);
      }
      Save();
      return resource;
    }

    public ResourceContent Get(int id, Language language)
    {
      return _db.Resource.FirstOrDefault(x => x.Id == id && x.Language == language);
    }

    public IQueryable<ResourceContent> Get(Language language)
    {
      return _db.Resource.Where(x => x.Language == language);
    }

    public IQueryable<ResourceContent> Get(List<ResourcePosition> positions)
    {
      return _db.Resource.Where(x => x.Language == _language && positions.Contains(x.Position));
    }

    public IQueryable<ResourceContent> Get(Language language, List<ResourcePosition> positions)
    {
      return _db.Resource.Where(x => x.Language == language && positions.Contains(x.Position));
    }

    public IQueryable<ResourceContent> GetAll()
    {
      return _db.Resource.Where(x => x.Language == _language);
    }

    public List<ResourceContent> GetList(Language language)
    {
      return _resources;
    }

    public List<ResourceContent> GetList()
    {
      return _resources;
    }

    public void Remove(int id, Language language)
    {
      var resource = _db.Resource.FirstOrDefault(x => x.Id == id && x.Language == language);
      if (resource != null)
      {
        _db.Resource.Remove(resource);
        Save();
      }
      else
      {
        throw new NullReferenceException();
      }
    }

    public string GetCurrentLanguageByIds(params int[] id)
    {
      return string.Join(" ", _resources.Where(x => id.Contains(x.Id)).Select(x => x.Text));
    }


    private void Save()
    {
      _db.SaveChanges();
    }
  }

  public interface IResourceContent
  {
    ResourceContent AddOrUpdate(ResourceContent resource);
    ResourceContent Get(int id, Language language);
    IQueryable<ResourceContent> Get(Language language);
    string GetCurrentLanguageByIds(params int[] id);
    List<ResourceContent> GetList(Language language);
    IQueryable<ResourceContent> Get(List<ResourcePosition> positions);
    IQueryable<ResourceContent> GetAll();
    IQueryable<ResourceContent> Get(Language language, List<ResourcePosition> positions);
    void Remove(int id, Language language);
    List<ResourceContent> GetList();
  }
}
