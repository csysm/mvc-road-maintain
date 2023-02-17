using System.ComponentModel.DataAnnotations;

namespace RoadMaintainSys.UI.Models
{
    public class AddAdminViewModel
    {
        [Required]
        [StringLength(20)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [StringLength(20,MinimumLength =6)]
        [Display(Name = "密码")]
        public string Password { get; set; }
    }
}
