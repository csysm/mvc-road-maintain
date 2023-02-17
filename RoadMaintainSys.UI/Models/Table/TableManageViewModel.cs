using System.ComponentModel.DataAnnotations;

namespace RoadMaintainSys.UI.Models
{
    public class TableManageViewModel
    {
        //1
        [Display(Name = "扣分原因")]
        [Required(ErrorMessage = "请填写扣分原因")]
        public string Reason1 { get; set; }

        [Display(Name = "评价得分")]
        [Required(ErrorMessage = "请填写评价得分")]
        [Range(0, 3, ErrorMessage = "得分只能在{1}和{2}之间")]
        public double FinalScore1 { get; set; }

        [Display(Name = "备注")]
        public string? Remark1 { get; set; }


        //2
        [Display(Name = "扣分原因")]
        [Required(ErrorMessage = "请填写扣分原因")]
        public string Reason2 { get; set; }

        [Display(Name = "评价得分")]
        [Required(ErrorMessage = "请填写评价得分")]
        [Range(0, 9, ErrorMessage = "得分只能在{1}和{2}之间")]
        public double FinalScore2 { get; set; }

        [Display(Name = "备注")]
        public string? Remark2 { get; set; }


        //3
        [Display(Name = "扣分原因")]
        [Required(ErrorMessage = "请填写扣分原因")]
        public string Reason3 { get; set; }

        [Display(Name = "评价得分")]
        [Required(ErrorMessage = "请填写评价得分")]
        [Range(0, 3, ErrorMessage = "得分只能在{1}和{2}之间")]
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
        [Range(0, 4, ErrorMessage = "得分只能在{1}和{2}之间")]
        public double FinalScore5 { get; set; }

        [Display(Name = "备注")]
        public string? Remark5 { get; set; }

        //6
        [Display(Name = "扣分原因")]
        [Required(ErrorMessage = "请填写扣分原因")]
        public string Reason6 { get; set; }

        [Display(Name = "评价得分")]
        [Required(ErrorMessage = "请填写评价得分")]
        [Range(0, 3, ErrorMessage = "得分只能在{1}和{2}之间")]
        public double FinalScore6 { get; set; }

        [Display(Name = "备注")]
        public string? Remark6 { get; set; }

        //7
        [Display(Name = "扣分原因")]
        [Required(ErrorMessage = "请填写扣分原因")]
        public string Reason7 { get; set; }

        [Display(Name = "评价得分")]
        [Required(ErrorMessage = "请填写评价得分")]
        [Range(0, 1, ErrorMessage = "得分只能在{1}和{2}之间")]
        public double FinalScore7 { get; set; }

        [Display(Name = "备注")]
        public string? Remark7 { get; set; }

        //8
        [Display(Name = "扣分原因")]
        [Required(ErrorMessage = "请填写扣分原因")]
        public string Reason8 { get; set; }

        [Display(Name = "评价得分")]
        [Required(ErrorMessage = "请填写评价得分")]
        [Range(0,1, ErrorMessage = "得分只能在{1}和{2}之间")]
        public double FinalScore8 { get; set; }

        [Display(Name = "备注")]
        public string? Remark8 { get; set; }

        //9
        [Display(Name = "扣分原因")]
        [Required(ErrorMessage = "请填写扣分原因")]
        public string Reason9 { get; set; }

        [Display(Name = "评价得分")]
        [Required(ErrorMessage = "请填写评价得分")]
        [Range(0, 3, ErrorMessage = "得分只能在{1}和{2}之间")]
        public double FinalScore9 { get; set; }

        [Display(Name = "备注")]
        public string? Remark9 { get; set; }
    }
}
