using AlertToCareFrontend.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AlertToCareFrontend.ViewModel
{
    class BedViewModel:BaseViewModel
    {
        #region Fields
        BedDataModel bedDataModel;
        string message;
        string alertMessage;
        string alertMessageHistory;
        #endregion

        #region Initializers

        public BedViewModel(BedDataModel bed)
        {
            //bedDataModel = new BedDataModel(ref bed);
            bedDataModel = bed;

            AlertMessage = "";
            AlertMessageHistory = "";

            CheckPatientVitalsCommand = new Command.DelegateCommandClass(CheckPatientVitalsWrapper, CanExecuteWrapper);

            DischargePatientCommand = new Command.DelegateCommandClass(DischargePatientWrapper, CanExecuteWrapper);

            StopAlertCommand = new Command.DelegateCommandClass(StopAlertWrapper, CanExecuteWrapper);

            UndoAlertCommand = new Command.DelegateCommandClass(UndoAlertWrapper, CanExecuteWrapper);

        }
        #endregion

        #region Properties
        public string BedId
        {
            get { return bedDataModel.BedId; }
            set
            {
                if (value != bedDataModel.BedId)
                {
                    bedDataModel.BedId = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool BedStatus
        {
            get { return bedDataModel.BedStatus; }
            set
            {
                if (value != bedDataModel.BedStatus)
                {
                    bedDataModel.BedStatus = value;
                    OnPropertyChanged();
                }
            }
        }
        public string PatientId
        {
            get { return bedDataModel.PatientId; }
            set
            {
                if (value != bedDataModel.PatientId)
                {
                    bedDataModel.PatientId = value;
                    OnPropertyChanged();
                }
            }
        }

        public string IcuId
        {
            get { return bedDataModel.IcuId; }
            set
            {
                if (value != bedDataModel.IcuId)
                {
                    bedDataModel.IcuId = value;
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
        public string AlertMessage
        {
            get { return alertMessage; }
            set
            {
                if (value != alertMessage)
                {
                    alertMessage = value;
                    OnPropertyChanged();
                }
            }
        }
        public string AlertMessageHistory
        {
            get { return alertMessageHistory; }
            set
            {
                if (value != alertMessageHistory)
                {
                    alertMessageHistory = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Logic

        public static async Task<PatientDataModel> DeletePatientData(string patientId)
        {
            PatientDataModel responseObj = new PatientDataModel();
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

                response = await client.DeleteAsync("api/PatientData/" + patientId).ConfigureAwait(false);

                // Verification  
                if (response.IsSuccessStatusCode)
                {
                    // Reading Response.  
                    string result = response.Content.ReadAsStringAsync().Result;
                    responseObj = JsonConvert.DeserializeObject<PatientDataModel>(result);

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

        public static async Task<VitalsDataModel> DeleteVitalsData(string patientId)
        {
            VitalsDataModel responseObj = new VitalsDataModel();
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

                response = await client.DeleteAsync("api/VitalData/" + patientId).ConfigureAwait(false);

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

        public static async Task<BedDataModel> PutBedData(BedDataModel requestObj)
        {
            // Initialization.  
            BedDataModel responseObj = new BedDataModel();

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
 
                response = await client.PutAsJsonAsync("api/BedData/" + requestObj.BedId, requestObj).ConfigureAwait(false);

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
                    throw new ArgumentException(result);
                }
            }

            return responseObj;
        }

        public static async Task<string> CheckVitalAndAlert(string patientId)
        {
            string responseObj = "";
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

                response = await client.GetAsync("api/VitalData/CheckVitalAndAlert/" + patientId).ConfigureAwait(false);

                // Verification  
                if (response.IsSuccessStatusCode)
                {
                    // Reading Response.  
                    string result = response.Content.ReadAsStringAsync().Result;
                    responseObj = JsonConvert.DeserializeObject<string>(result);

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

        void CheckPatientVitals()
        {
            try
            {
                AlertMessage = CheckVitalAndAlert(bedDataModel.PatientId).Result;
            }
            catch(ArgumentException exception)
            {
                AlertMessage = exception.Message;
            }
        }
        void DischargePatient()
        {
            try
            {
                _ = DeletePatientData(bedDataModel.PatientId).Result;
                _ = DeleteVitalsData(bedDataModel.PatientId).Result;

                BedDataModel updatedBedData = new BedDataModel(bedDataModel.BedId, false, "");
                _ = PutBedData(updatedBedData).Result;

                PatientId = "";
                BedStatus = false;
            }
            catch (ArgumentException exception)
            {
                Message = exception.Message;
            }
        }

        void StopAlert()
        {
            AlertMessageHistory = AlertMessage;
            AlertMessage = "";
        }

        void UndoAlert()
        {
            AlertMessage = AlertMessageHistory;
            AlertMessageHistory = "";
        }

        #endregion

        #region Commands
        public ICommand DischargePatientCommand
        {
            get;
            set;
        }
        public ICommand CheckPatientVitalsCommand
        {
            get;
            set;
        }
        public ICommand StopAlertCommand
        {
            get;
            set;
        }
        public ICommand UndoAlertCommand
        {
            get;
            set;
        }
        #endregion

        #region Command helper Methods

        void DischargePatientWrapper(object parameter)
        {
            this.DischargePatient();
        }
        void CheckPatientVitalsWrapper(object parameter)
        {
            this.CheckPatientVitals();
        }
        void StopAlertWrapper(object parameter)
        {
            this.StopAlert();
        }
        void UndoAlertWrapper(object parameter)
        {
            this.UndoAlert();
        }
        bool CanExecuteWrapper(object parameter)
        {
            return true;
        }
        #endregion
    }
}
