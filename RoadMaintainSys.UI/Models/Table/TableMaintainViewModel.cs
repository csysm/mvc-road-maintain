using System.ComponentModel.DataAnnotations;

namespace RoadMaintainSys.UI.Models
{
    public class TableMaintainViewModel
    {
        //1
        [Display(Name = "扣分原因")]
        [Required(ErrorMessage = "请填写扣分原因")]
        public string Reason1 { get; set; }

        [Display(Name = "评价得分")]
        [Required(ErrorMessage = "请填写评价得分")]
        [Range(0, 12, ErrorMessage = "得分只能在{1}和{2}之间")]
        public double FinalScore1 { get; set; }

        [Display(Name = "备注")]
        public string? Remark1 { get; set; }


        //2
        [Display(Name = "扣分原因")]
        [Required(ErrorMessage = "请填写扣分原因")]
        public string Reason2 { get; set; }

        [Display(Name = "评价得分")]
        [Required(ErrorMessage = "请填写评价得分")]
        [Range(0, 12, ErrorMessage = "得分只能在{1}和{2}之间")]
        public double FinalScore2 { get; set; }

        [Display(Name = "备注")]
        public string? Remark2 { get; set; }


        //3
        [Display(Name = "扣分原因")]
        [Required(ErrorMessage = "请填写扣分原因")]
        public string Reason3 { get; set; }

        [Display(Name = "评价得分")]
        [Required(ErrorMessage = "请填写评价得分")]
        [Range(0, 10, ErrorMessage = "得分只能在{1}和{2}之间")]
        public double FinalScore3 { get; set; }

        [Display(Name = "备注")]
        public string? Remark3 { get; set; }

        //4
        [Display(Name = "扣分原因")]
        [Required(ErrorMessage = "请填写扣分原因")]
        public string Reason4 { get; set; }

        [Display(Name = "评价得分")]
        [Required(ErrorMessage = "请填写评价得分")]
        [Range(0, 3, ErrorMessage = "得分只能在{1}和{2}之间")]
        public double FinalScore4 { get; set; }

        [Display(Name = "备注")]
        public string? Remark4 { get; set; }

        //5
        [Display(Name = "扣分原因")]
        [Required(ErrorMessage = "请填写扣分原因")]
        public string Reason5 { get; set; }

        [Display(Name = "评价得分")]
        [Required(ErrorMessage = "请填写评价得分")]
        [Range(0, 3, ErrorMessage = "得分只能在{1}和{2}之间")]
        public double FinalScore5 { get; set; }

        [Display(Name = "备注")]
        public string? Remark5 { get; set; }

        //6
        [Display(Name = "扣分原因")]
        [Required(ErrorMessage = "请填写扣分原因")]
        public string Reason6 { get; set; }

        [Display(Name = "评价得分")]
        [Required(ErrorMessage = "请填写评价得分")]
        [Range(0, 8, ErrorMessage = "得分只能在{1}和{2}之间")]
        public double FinalScore6 { get; set; }

        [Display(Name = "备注")]
        public string? Remark6 { get; set; }

        //7
        [Display(Name = "扣分原因")]
        [Required(ErrorMessage = "请填写扣分原因")]
        public string Reason7 { get; set; }

        [Display(Name = "评价得分")]
        [Required(ErrorMessage = "请填写评价得分")]
        [Range(0, 2, ErrorMessage = "得分只能在{1}和{2}之间")]
        public double FinalScore7 { get; set; }

        [Display(Name = "备注")]
        public string? Remark7 { get; set; }
        
    }
}
