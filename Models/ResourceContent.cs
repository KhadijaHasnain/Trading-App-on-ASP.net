using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BIGQXWebsite.Models
{
  public class ResourceContent
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public string Text { get; set; }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Language Language { get; set; }
    public ResourcePosition Position { get; set; }
    public ResourceTypeEnum Type { get; set; }
  }

  public enum ResourceTypeEnum : byte
  {
    Text = 1,
    Svg = 2,
    Html = 3,
    Image = 4,
    Url = 5
  }

  public enum ResourcePosition : byte
  {
    NotSet = 0,
    All = 1,
    Home = 2,
    About = 3,
    Contact = 4,
    Service = 5,
    Landing1 = 6
  }

  public enum Language : byte
  {
    [Display(Name = "ar")]
    NotSet = 0,
    [Display(Name = "en")]
    English = 1,
    [Display(Name = "ar")]
    Arabic = 2,
    [Display(Name = "tr")]
    Turkish = 3,
    [Display(Name = "fr")]
    French = 4,
    [Display(Name = "fa")]
    Persian = 5,
    [Display(Name = "ru")]
    Russian = 6,
    [Display(Name = "pt")]
    Portuguese = 7,
    [Display(Name = "es")]
    Spanish = 8,
  }
}
