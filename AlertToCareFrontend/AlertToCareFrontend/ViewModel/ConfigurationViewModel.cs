using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertToCareFrontend.ViewModel
{
    class ConfigurationViewModel
    {
        string icuId;
        int totalNoOfBeds;
        string layout;

        public string IcuId
        {
            get { return icuId; }
            set { icuId = value; }
        }
        public int TotalNoOfBeds
        {
            get { return totalNoOfBeds; }
            set { totalNoOfBeds = value; }
        }
        public string Layout
        {
            get { return layout; }
            set { layout = value; }
        }
    }
}
