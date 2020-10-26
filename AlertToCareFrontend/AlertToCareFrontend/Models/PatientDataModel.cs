using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertToCareFrontend.Models
{
    class PatientDataModel
    {
        public string PatientId { get; set; }
        public string BedId { get; set; }
        public string PatientName { get; set; }
        public int PatientAge { get; set; }
        public string Email { get; set; }
        public int ContactNo { get; set; }
        public string Address { get; set; }
    }
}
