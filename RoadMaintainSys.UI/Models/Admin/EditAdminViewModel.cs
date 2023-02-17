using System.ComponentModel.DataAnnotations;

namespace RoadMaintainSys.UI.Models
{
    public class EditAdminViewModel
    {
        //public long AdminId { get; set; }

        //public string CurrentAdminName { get; set; }

        [Required(ErrorMessage = "请输入新用户名")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "新用户名必须多于1个字符少于10个字符")]
        [Display(Name = "新用户名:")]
        public string NewAdminName { get; set; }


        [Required(ErrorMessage = "请输入当前密码")]
        [StringLength(20)]
        [Display(Name = "当前密码:")]
        public string CurrentPassword { get; set; }


        [Required(ErrorMessage = "请输入新密码")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "新密码必须多于6个字符少于20个字符")]
        [Display(Name = "新密码:")]
        public string NewPassword { get; set; }
    }
}
