using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace manahil.Models
{
    public class Cat_Department
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeptId { get; set; }

        public string ComId { get; set; }

        [Display(Name = "Department Code")]
        //[StringLength()]
        public string DeptCode { get; set; }

        [Display(Name = "Department Name")]
        [Required(ErrorMessage = "Please Provide Department Name")]
        [StringLength(25)]
        [DataType("NVARCHAR(25)")]
        //[Remote(action: "IsExist", controller: "Department")]
        public string DeptName { get; set; }

        [Display(Name = "Department Name Bangla")]
        [StringLength(25)]
        [DataType("NVARCHAR(25)")]
        public string DeptBangla { get; set; }

        [Display(Name = "SL No")]
        public short? Slno { get; set; }

        public string PcName { get; set; }
        [StringLength(128)]
        public string UserId { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
        public string UpdateByUserId { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DtInput { get; set; }


        //public virtual Cat_Company Com { get; set; }
        //public virtual ICollection<HrTempAttend> HrTempAttend { get; set; }
        //public virtual ICollection<HrTempAttendMonth> HrTempAttendMonth { get; set; }

    }
}
