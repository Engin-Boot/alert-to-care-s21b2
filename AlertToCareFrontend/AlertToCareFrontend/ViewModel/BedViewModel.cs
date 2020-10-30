using AlertToCareFrontend.Models;
using AlertToCareFrontend.Utilities;
using System.Windows.Input;

namespace AlertToCareFrontend.ViewModel
{
    class BedViewModel:BaseViewModel
    {
        #region Fields
        readonly BedDataModel _bedDataModel;
        string _message;
        string _alertMessage;
        string _alertMessageHistory;
        #endregion

        #region Initializers

        public BedViewModel(BedDataModel bed)
        {
            _bedDataModel = bed;

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
            get => _bedDataModel.BedId;
            set
            {
                if (value != _bedDataModel.BedId)
                {
                    _bedDataModel.BedId = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool BedStatus
        {
            get => _bedDataModel.BedStatus;
            set
            {
                if (value != _bedDataModel.BedStatus)
                {
                    _bedDataModel.BedStatus = value;
                    OnPropertyChanged();
                }
            }
        }
        public string PatientId
        {
            get => _bedDataModel.PatientId;
            set
            {
                if (value != _bedDataModel.PatientId)
                {
                    _bedDataModel.PatientId = value;
                    OnPropertyChanged();
                }
            }
        }

        public string IcuId
        {
            get => _bedDataModel.IcuId;
            set
            {
                if (value != _bedDataModel.IcuId)
                {
                    _bedDataModel.IcuId = value;
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
        public string AlertMessage
        {
            get => _alertMessage;
            set
            {
                if (value != _alertMessage)
                {
                    _alertMessage = value;
                    OnPropertyChanged();
                }
            }
        }
        public string AlertMessageHistory
        {
            get => _alertMessageHistory;
            set
            {
                if (value != _alertMessageHistory)
                {
                    _alertMessageHistory = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Logic 
        void CheckPatientVitals()
        {
            AlertMessage = HttpClientUtility.GetData("api/VitalData/CheckVitalAndAlert/" + _bedDataModel.PatientId).Result;
        }
        void DischargePatient()
        {
            Message = HttpClientUtility.DeleteData("api/PatientData/" + _bedDataModel.PatientId).Result;
            Message = HttpClientUtility.DeleteData("api/VitalData/" + _bedDataModel.PatientId).Result;

            BedDataModel updatedBedData = new BedDataModel(_bedDataModel.BedId, false, "");
            _ = HttpClientUtility.PutBedData(updatedBedData).Result;

            PatientId = "";
            BedStatus = false;

            AlertMessage = "";
            AlertMessageHistory = "";
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
