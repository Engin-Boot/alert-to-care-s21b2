using AlertToCareFrontend.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AlertToCareFrontend.ViewModel
{
    class AdmitPatientViewModel:BaseViewModel
    {
        #region Fields
        PatientDataModel patientDataModel;
        string message;
        #endregion

        #region Intializer
        public AdmitPatientViewModel(string bedId)
        {
            patientDataModel = new PatientDataModel();
            patientDataModel.BedId = bedId;

            Message = "";

            AdmitNewPatientCommand = new Command.DelegateCommandClass(AdmitNewPatientWrapper, CanExecuteWrapper);
        }
        #endregion

        #region Properties

        public string PatientId
        {
            get { return patientDataModel.PatientId; }
            set
            {
                if (value != patientDataModel.PatientId)
                {
                    patientDataModel.PatientId = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PatientName
        {
            get { return patientDataModel.PatientName; }
            set
            {
                if (value != patientDataModel.PatientName)
                {
                    patientDataModel.PatientName = value;
                    OnPropertyChanged();
                }
            }
        }

        public int PatientAge
        {
            get { return patientDataModel.PatientAge; }
            set
            {
                if (value != patientDataModel.PatientAge)
                {
                    patientDataModel.PatientAge = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Email
        {
            get { return patientDataModel.Email; }
            set
            {
                if (value != patientDataModel.Email)
                {
                    patientDataModel.Email = value;
                    OnPropertyChanged();
                }
            }
        }

        public int ContactNo
        {
            get { return patientDataModel.ContactNo; }
            set
            {
                if (value != patientDataModel.ContactNo)
                {
                    patientDataModel.ContactNo = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Address
        {
            get { return patientDataModel.Address; }
            set
            {
                if (value != patientDataModel.Address)
                {
                    patientDataModel.Address = value;
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
        void AdmitNewPatient()
        {
            try
            {
                _ = PostPatientData(patientDataModel).Result;

                VitalsDataModel vitalDataModel = new VitalsDataModel(patientDataModel.PatientId, patientDataModel.BedId);
                _ = PostVitalsData(vitalDataModel).Result;

                BedDataModel updateBed = new BedDataModel(patientDataModel.BedId, true, patientDataModel.PatientId);
                _ = PutBedData(updateBed).Result;
                
            }
            catch(ArgumentException exception)
            {
                Message = exception.Message;
            }
        }
        public static async Task<PatientDataModel> PostPatientData(PatientDataModel requestObj)
        {
            // Initialization.  
            PatientDataModel responseObj = new PatientDataModel();

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
                response = await client.PostAsJsonAsync("api/PatientData/", requestObj).ConfigureAwait(false);

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

        public static async Task<VitalsDataModel> PostVitalsData(VitalsDataModel requestObj)
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

                // HTTP POST  
                response = await client.PostAsJsonAsync("api/VitalData/", requestObj).ConfigureAwait(false);

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

                response = await client.PutAsJsonAsync("api/BedData/"+requestObj.BedId, requestObj).ConfigureAwait(false);

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

        #endregion

        #region Commands
        public ICommand AdmitNewPatientCommand
        {
            get;
            set;
        }
        
        #endregion

        #region CommandHelpers
        void AdmitNewPatientWrapper(object parameter)
        {
            AdmitNewPatient();
        }

        bool CanExecuteWrapper(object parameter)
        {
            return true;
        }
        #endregion
    }
}
