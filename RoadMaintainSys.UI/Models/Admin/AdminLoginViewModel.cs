using System.ComponentModel.DataAnnotations;

namespace RoadMaintainSys.UI.Models
{
    public class AdminLoginViewModel
    {
        [Required(ErrorMessage = "请输入登陆账号")]
        [StringLength(20)]
        [RegularExpression("^[0-9]{1,8}$", ErrorMessage = "账号格式不正确!")]
        [Display(Name = "账号:")]
        public string UserId { get; set;}

        [Required(ErrorMessage = "请输入密码")]
        [StringLength(20)]
        [Display(Name = "密码:")]
        public string Password { get; set;}
    }
}
