using AlertToCareFrontend.Models;
using AlertToCareFrontend.Utilities;
using System.Windows.Input;

namespace AlertToCareFrontend.ViewModel
{
    class UpdateVitalsViewModel:BaseViewModel
    {
        #region Fields
        readonly VitalsDataModel _vitalsDataModel;
        string _message;
        #endregion

        #region Intializer
        public UpdateVitalsViewModel(string patientId, string bedId)
        {
            _vitalsDataModel = new VitalsDataModel(patientId, bedId);

            VitalsDataModel responseObj = HttpClientUtility.GetVitalData(patientId).Result;
            Bpm = responseObj.Bpm;
            Spo2 = responseObj.Spo2;
            RespRate = responseObj.RespRate;

            Message = "";

            UpdateVitalsCommand = new Command.DelegateCommandClass(UpdateVitalsWrapper, CanExecuteWrapper);
        }
        #endregion

        #region Properties

        public string PatientId
        {
            get => _vitalsDataModel.PatientId;
            set
            {
                if (value != _vitalsDataModel.PatientId)
                {
                    _vitalsDataModel.PatientId = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PatientBedId
        {
            get => _vitalsDataModel.PatientBedId;
            set
            {
                if (value != _vitalsDataModel.PatientBedId)
                {
                    _vitalsDataModel.PatientBedId = value;
                    OnPropertyChanged();
                }
            }
        }

        public float Bpm
        {
            get => _vitalsDataModel.Bpm;
            set
            {
                if (!value.Equals(_vitalsDataModel.Bpm))
                {
                    _vitalsDataModel.Bpm = value;
                    OnPropertyChanged();
                }
            }
        }

        public float Spo2
        {
            get => _vitalsDataModel.Spo2;
            set
            {
                if (!value.Equals(_vitalsDataModel.Spo2))
                {
                    _vitalsDataModel.Spo2 = value;
                    OnPropertyChanged();
                }
            }
        }
        public float RespRate
        {
            get => _vitalsDataModel.RespRate;
            set
            {
                if (!value.Equals(_vitalsDataModel.RespRate))
                {
                    _vitalsDataModel.RespRate = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Message
        {
            get => _message;
            set
            {
                if (value != _message)
                {
                    _message = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Logic
        void UpdateVitals()
        {
            Message = HttpClientUtility.PutVitalData(_vitalsDataModel).Result;
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
