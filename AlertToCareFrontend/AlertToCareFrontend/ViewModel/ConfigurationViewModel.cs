using AlertToCareFrontend.Models;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AlertToCareFrontend.ViewModel
{
    class ConfigurationViewModel:BaseViewModel
    {
        #region Fields
        IcuDataModel icuDataModel;
        ObservableCollection<string> layouts;
        string message;
        #endregion

        #region Initializers
        public ConfigurationViewModel()
        {
            Layouts = new ObservableCollection<string>()
            {
                "L Shape",
                "Parallel"
            };

            icuDataModel = new IcuDataModel();
            
            AddIcuCommand = new Command.DelegateCommandClass(AddIcuWrapper, CanExecuteWrapper);

        }
        #endregion
        #region Properties
        public string IcuId
        {
            get { return icuDataModel.IcuId; }
            set
            {
                if (value != icuDataModel.IcuId)
                {
                    icuDataModel.IcuId = value;
                    OnPropertyChanged();
                }
            }
        }
        public int TotalNoOfBeds
        {
            get { return icuDataModel.TotalNoOfBeds; }
            set
            {
                if (value != icuDataModel.TotalNoOfBeds)
                {
                    icuDataModel.TotalNoOfBeds = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Layout
        {
            get { return icuDataModel.Layout; }
            set
            {
                if (value != icuDataModel.Layout)
                {
                    icuDataModel.Layout = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<string> Layouts
        {
            get { return layouts; }
            set 
            {
                if (value != layouts) { 
                    layouts = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Message
        {
            get { return message; }
            set
            {
                if (value != message)
                {
                    message = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Logic
        private void AddBeds(IcuDataModel icu)
        {
            for (int bedIndex = 1; bedIndex <= icu.TotalNoOfBeds; bedIndex++)
            {
                // Add new bed
                string bedId = icu.IcuId + "Bed" + bedIndex;
                BedDataModel bed = new BedDataModel(bedId);
                _ = PostBedData(bed);
            }
        }
        
        public void AddIcu()
        {
            // Logic to call backend api to add icu details
            IcuDataModel newIcu = new IcuDataModel(IcuId, TotalNoOfBeds, Layout);

            IcuDataModel savedIcuData = PostIcuData(newIcu).Result;

            // Update settings
            Properties.Settings.Default.currentIcuId = savedIcuData.IcuId;
            Properties.Settings.Default.Save();

            // Add beds for icu
            AddBeds(savedIcuData);

            // Set success/failure message
            Message = String.IsNullOrEmpty(savedIcuData.IcuId) ? "ICU Configuration failed!" : "ICU Configuration successful!";
        }

        public static async Task<BedDataModel> PostBedData(BedDataModel requestObj)
        {
            // Initialization.  
            BedDataModel responseObj = new BedDataModel();

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

                    // HTTP POST  
                    response = await client.PostAsJsonAsync("api/BedData/", requestObj).ConfigureAwait(false);
                    
                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        responseObj = JsonConvert.DeserializeObject<BedDataModel>(result);

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

        public static async Task<IcuDataModel> PostIcuData(IcuDataModel requestObj)
        {
            // Initialization.  
            IcuDataModel responseObj = new IcuDataModel();

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

                    // HTTP POST  
                    response = await client.PostAsJsonAsync("api/IcuData/", requestObj).ConfigureAwait(false);

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        responseObj = JsonConvert.DeserializeObject<IcuDataModel>(result);

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
        #region Commands
        public ICommand AddIcuCommand
        {
            get;
            set;
        }
        
        #endregion

        #region Command helper Methods
        void AddIcuWrapper(object parameter)
        {
            this.AddIcu();
        }
        bool CanExecuteWrapper(object parameter)
        {
            return true;
        }
        #endregion
    }
}
