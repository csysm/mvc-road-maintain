using System.ComponentModel.DataAnnotations;

namespace RoadMaintainSys.UI.Models
{
    public class AddNewsViewModel
    {
        [Required(ErrorMessage ="请填写新闻标题")]
        [Display(Name ="标题:")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = "标题字数不能超过50字")]
        public string Title { get; set; }

        [Required(ErrorMessage = "请输入新闻内容")]
        [Display(Name = "内容:")]
        [StringLength(500, MinimumLength = 0,ErrorMessage ="内容字数不能超过500字")]
        public string Content { get; set; }
    }
}
