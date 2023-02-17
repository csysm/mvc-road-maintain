using RoadMaintainSys.Entities;
using System.ComponentModel.DataAnnotations;

namespace RoadMaintainSys.UI.Models
{
    public class TableCheckViewModel
    {

        //
        [Display(Name = "扣分原因")]
        [Required(ErrorMessage = "请填写扣分原因")]
        public string Reason1 { get; set; }

        [Display(Name = "评价得分")]
        [Required(ErrorMessage = "请填写评价得分")]
        [Range(0, 5, ErrorMessage = "得分只能在{1}和{2}之间")]
        public double FinalScore1 { get; set; }

        [Display(Name = "备注")]
        public string? Remark1 { get; set; }

        //
        

        [Display(Name = "扣分原因")]
        [Required(ErrorMessage = "请填写扣分原因")]
        public string Reason2 { get; set; }

        [Display(Name = "评价得分")]
        [Required(ErrorMessage = "请填写评价得分")]
        [Range(0, 5, ErrorMessage = "得分只能在{1}和{2}之间")]
        public double FinalScore2 { get; set; }

        [Display(Name = "备注")]
        public string? Remark2 { get; set; }
        //


        [Display(Name = "扣分原因")]
        [Required(ErrorMessage = "请填写扣分原因")]
        public string Reason3 { get; set; }

        [Display(Name = "评价得分")]
        [Required(ErrorMessage = "请填写评价得分")]
        [Range(0, 5, ErrorMessage = "得分只能在{1}和{2}之间")]
        public double FinalScore3 { get; set; }

        [Display(Name = "备注")]
        public string? Remark3 { get; set; }

        //

        [Display(Name = "扣分原因")]
        [Required(ErrorMessage = "请填写扣分原因")]
        public string Reason4 { get; set; }

        [Display(Name = "评价得分")]
        [Required(ErrorMessage = "请填写评价得分")]
        [Range(0, 5, ErrorMessage = "得分只能在{1}和{2}之间")]
        public double FinalScore4 { get; set; }

        [Display(Name = "备注")]
        public string? Remark4 { get; set; }
    }
}
