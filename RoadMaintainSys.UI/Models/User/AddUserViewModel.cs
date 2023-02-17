using System.ComponentModel.DataAnnotations;

namespace RoadMaintainSys.UI.Models
{
    public class AddUserViewModel
    {
        [Required]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "用户名必须多于1个字符且少于10个字符")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6,ErrorMessage ="密码长度必须大于6个字符且小于20个字符")]
        [Display(Name = "密码")]
        public string Password { get; set; }
    }
}
