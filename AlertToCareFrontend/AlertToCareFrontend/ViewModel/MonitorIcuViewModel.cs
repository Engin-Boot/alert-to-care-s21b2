using AlertToCareFrontend.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AlertToCareFrontend.ViewModel
{
    class MonitorIcuViewModel:BaseViewModel
    {
        #region Fields
        #endregion

        #region Intialisers
        public MonitorIcuViewModel()
        {
            GetAllBedsForIcu();
        }
        #endregion

        #region Properties
        public ObservableCollection<BedDataModel> BedDataModelList { get; set; } = new ObservableCollection<BedDataModel>();
        #endregion

        #region Logic

        private int GetBedNumFromBedId(string bedId)
        {
            string bedNum = bedId.Replace(Properties.Settings.Default.currentIcuId, "").Replace("Bed", "");
            return int.Parse(bedNum);
        }

        private void GetAllBedsForIcu()
        {
            string icuId = Properties.Settings.Default.currentIcuId;

            if (icuId == null)
                return;

            List<BedDataModel> beds = GetBeds(icuId).Result.ToList();

            beds.Sort((bedOne, bedTwo)=> GetBedNumFromBedId(bedOne.BedId).CompareTo(GetBedNumFromBedId(bedTwo.BedId)));

            if (beds != null)
            {
                foreach (BedDataModel bed in beds)
                {
                    BedDataModelList.Add(bed);
                }
            }
        }

        private static async Task<IEnumerable<BedDataModel>> GetBeds(string icuId)
        {
            // Initialization.  
            IEnumerable<BedDataModel> responseObj = new List<BedDataModel>();

            try
            {
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.  
                    client.BaseAddress = new Uri("http://localhost:5000/");

                    // Setting content type.  
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP GET
                    var url = "api/BedData/GetBedsByIcuId/" + icuId;
                    response = await client.GetAsync(url).ConfigureAwait(false);

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        responseObj = JsonConvert.DeserializeObject<IEnumerable<BedDataModel>>(result);

                        // Releasing.  
                        response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return responseObj;
        }

        #endregion
    }
}
