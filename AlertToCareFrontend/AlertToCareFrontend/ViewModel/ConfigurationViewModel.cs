using AlertToCareFrontend.Models;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AlertToCareFrontend.ViewModel
{
    class ConfigurationViewModel:BaseViewModel
    {
        #region Fields
        Models.IcuDataModel icuDataModel;
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

            icuDataModel = new Models.IcuDataModel();
            
            AddIcuCommand = new Command.DelegateCommandClass(AddIcuWrapper, CanExecuteWrapper);

            UpdateIcuCommand = new Command.DelegateCommandClass(UpdateIcuWrapper, CanExecuteWrapper);

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
        public async void AddIcu()
        {
            // Logic to call backend api to add icu details
            IcuDataModel newIcu = new IcuDataModel(IcuId, TotalNoOfBeds, Layout);

            IcuDataModel response = await PostOrPutIcuData(newIcu, true);
            // Set success/failure message
            if (!String.IsNullOrEmpty(response.IcuId))
            {
                Message = "Add request successful: " + response.IcuId + ", " + response.TotalNoOfBeds.ToString() + ", " + response.Layout;
            }
            else
            {
                Message = "Add request failed!";
            }
        }

        public static async Task<IcuDataModel> PostOrPutIcuData(IcuDataModel requestObj, bool isPostRequest)
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
                    if (isPostRequest)
                        response = await client.PostAsJsonAsync("api/IcuData/", requestObj).ConfigureAwait(false);
                    else
                        response = await client.PutAsJsonAsync("api/IcuData/" + requestObj.IcuId, requestObj).ConfigureAwait(false);

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
        public async void UpdateIcu()
        {
            // Logic to call backend api to update icu details
            IcuDataModel newIcu = new IcuDataModel(IcuId, TotalNoOfBeds, Layout);
            
            IcuDataModel response = await PostOrPutIcuData(newIcu, false);
            // Set success/failure message
            if (!String.IsNullOrEmpty(response.IcuId))
            {
                Message = "Update request successful: " + response.IcuId + ", " + response.TotalNoOfBeds.ToString() + ", " + response.Layout;
            }
            else
            {
                Message = "Update request failed!";
            }
        }
        #endregion
        #region Commands
        public ICommand AddIcuCommand
        {
            get;
            set;
        }
        public ICommand UpdateIcuCommand
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
        void UpdateIcuWrapper(object parameter)
        {
            this.UpdateIcu();
        }
        bool CanExecuteWrapper(object parameter)
        {
            return true;
        }
        #endregion
    }
}
