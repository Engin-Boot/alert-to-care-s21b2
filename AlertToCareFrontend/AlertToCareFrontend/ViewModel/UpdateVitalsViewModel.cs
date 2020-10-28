using AlertToCareFrontend.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AlertToCareFrontend.ViewModel
{
    class UpdateVitalsViewModel:BaseViewModel
    {
        #region Fields
        VitalsDataModel vitalsDataModel;
        string message;
        #endregion

        #region Intializer
        public UpdateVitalsViewModel(string patientId, string bedId)
        {
            vitalsDataModel = new VitalsDataModel(patientId, bedId);

            Message = "";

            UpdateVitalsCommand = new Command.DelegateCommandClass(UpdateVitalsWrapper, CanExecuteWrapper);
        }
        #endregion

        #region Properties

        public string PatientId
        {
            get { return vitalsDataModel.PatientId; }
            set
            {
                if (value != vitalsDataModel.PatientId)
                {
                    vitalsDataModel.PatientId = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PatientBedId
        {
            get { return vitalsDataModel.PatientBedId; }
            set
            {
                if (value != vitalsDataModel.PatientBedId)
                {
                    vitalsDataModel.PatientBedId = value;
                    OnPropertyChanged();
                }
            }
        }

        public float Bpm
        {
            get { return vitalsDataModel.Bpm; }
            set
            {
                if (value != vitalsDataModel.Bpm)
                {
                    vitalsDataModel.Bpm = value;
                    OnPropertyChanged();
                }
            }
        }

        public float Spo2
        {
            get { return vitalsDataModel.Spo2; }
            set
            {
                if (value != vitalsDataModel.Spo2)
                {
                    vitalsDataModel.Spo2 = value;
                    OnPropertyChanged();
                }
            }
        }
        public float RespRate
        {
            get { return vitalsDataModel.RespRate; }
            set
            {
                if (value != vitalsDataModel.RespRate)
                {
                    vitalsDataModel.RespRate = value;
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
        void UpdateVitals()
        {
            try
            {
                _ = PutVitalData(vitalsDataModel).Result;
                Message = "Vitals update successful!";
            }
            catch(ArgumentException exception)
            {
                Message = exception.Message;
            }
        }
        public static async Task<VitalsDataModel> PutVitalData(VitalsDataModel requestObj)
        {
            // Initialization.  
            VitalsDataModel responseObj = new VitalsDataModel();

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

                response = await client.PutAsJsonAsync("api/VitalData/" + requestObj.PatientId, requestObj).ConfigureAwait(false);

                // Verification  
                if (response.IsSuccessStatusCode)
                {
                    // Reading Response.  
                    string result = response.Content.ReadAsStringAsync().Result;
                    responseObj = JsonConvert.DeserializeObject<VitalsDataModel>(result);

                    // Releasing.  
                    response.Dispose();
                }
                else
                {
                    // Reading Response.  
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new ArgumentException(result);
                }
            }

            return responseObj;
        }

        #endregion

        #region Commands
        public ICommand UpdateVitalsCommand
        {
            get;
            set;
        }

        #endregion

        #region CommandHelpers
        void UpdateVitalsWrapper(object parameter)
        {
            UpdateVitals();
        }

        bool CanExecuteWrapper(object parameter)
        {
            return true;
        }
        #endregion
    }
}
