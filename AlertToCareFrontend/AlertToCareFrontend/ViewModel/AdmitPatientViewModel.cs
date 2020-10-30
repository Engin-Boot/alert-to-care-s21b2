using AlertToCareFrontend.Models;
using AlertToCareFrontend.Utilities;
using System.Windows.Input;

namespace AlertToCareFrontend.ViewModel
{
    class AdmitPatientViewModel:BaseViewModel
    {
        #region Fields
        readonly PatientDataModel _patientDataModel;
        private string _message;
        #endregion

        #region Intializer
        public AdmitPatientViewModel(string bedId)
        {
            _patientDataModel = new PatientDataModel
            {
                BedId = bedId,
                PatientAge = 50,
                ContactNo = 88888888
            };

            Message = "";

            AdmitNewPatientCommand = new Command.DelegateCommandClass(AdmitNewPatientWrapper, CanExecuteWrapper);
        }
        #endregion

        #region Properties

        public string PatientId
        {
            get => _patientDataModel.PatientId;
            set
            {
                if (value != _patientDataModel.PatientId)
                {
                    _patientDataModel.PatientId = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PatientName
        {
            get => _patientDataModel.PatientName;
            set
            {
                if (value != _patientDataModel.PatientName)
                {
                    _patientDataModel.PatientName = value;
                    OnPropertyChanged();
                }
            }
        }

        public int PatientAge
        {
            get => _patientDataModel.PatientAge;
            set
            {
                if (value != _patientDataModel.PatientAge)
                {
                    _patientDataModel.PatientAge = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Email
        {
            get => _patientDataModel.Email;
            set
            {
                if (value != _patientDataModel.Email)
                {
                    _patientDataModel.Email = value;
                    OnPropertyChanged();
                }
            }
        }

        public int ContactNo
        {
            get => _patientDataModel.ContactNo;
            set
            {
                if (value != _patientDataModel.ContactNo)
                {
                    _patientDataModel.ContactNo = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Address
        {
            get => _patientDataModel.Address;
            set
            {
                if (value != _patientDataModel.Address)
                {
                    _patientDataModel.Address = value;
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
        void AdmitNewPatient()
        {
            Message = HttpClientUtility.PostPatientData(_patientDataModel).Result;

            VitalsDataModel vitalDataModel = new VitalsDataModel(_patientDataModel.PatientId, _patientDataModel.BedId);
            Message = HttpClientUtility.PostVitalsData(vitalDataModel).Result;

            BedDataModel updateBed = new BedDataModel(_patientDataModel.BedId, true, _patientDataModel.PatientId);
            Message = HttpClientUtility.PutBedData(updateBed).Result;
        }
        
        
        #endregion

        #region Commands
        public ICommand AdmitNewPatientCommand
        {
            get;
            private set;
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
