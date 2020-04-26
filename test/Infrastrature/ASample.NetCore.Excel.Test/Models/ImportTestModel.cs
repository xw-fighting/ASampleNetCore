using System.ComponentModel.DataAnnotations;

namespace ASample.NetCore.Excel.Test.Models
{
    public class ImportTestModel
    {

        [Display(Name = "姓名")]
        public string PassengerName { get; set; }

        [Display(Name = "英文名")]
        public string PassengerEngName { get; set; }

        [Display(Name = "旅客类型")]
        public string PassengerType { get; set; }

        [Display(Name = "性别")]
        public string Sex { get; set; }

        [Display(Name = "联系电话")]
        public string Telphone { get; set; }

        [Display(Name = "证件类型")]
        public string CertType { get; set; }

        [Display(Name = "证件号")]
        public string CertNumber { get; set; }

        [Display(Name = "舱位")]
        public string Class { get; set; }

        [Display(Name = "票号")]
        public string TicketNumber { get; set; }
    }
}
