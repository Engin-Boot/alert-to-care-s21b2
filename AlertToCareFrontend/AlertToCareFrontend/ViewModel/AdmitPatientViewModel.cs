using AlertToCareFrontend.Models;
using System;
using System.Windows.Input;

namespace AlertToCareFrontend.ViewModel
{
    class AdmitPatientViewModel:BaseViewModel
    {
        #region Fields
        PatientDataModel patientDataModel;
        #endregion

        #region Intializer
        public AdmitPatientViewModel(string bedId)
        {
            patientDataModel = new PatientDataModel();
            patientDataModel.BedId = bedId;

            AdmitNewPatientCommand = new Command.DelegateCommandClass(AdmitNewPatientWrapper, CanExecuteWrapper);
            AdmitExistingPatientCommand = new Command.DelegateCommandClass(AdmitExistingPatientWrapper, CanExecuteWrapper);
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

        #endregion

        #region Logic
        void AdmitNewPatient()
        {
            Console.WriteLine("Admit new patient");
        }
        void AdmitExistingPatient()
        {
            Console.WriteLine("Admit existing patient");
        }

        #endregion

        #region Commands
        public ICommand AdmitNewPatientCommand
        {
            get;
            set;
        }
        public ICommand AdmitExistingPatientCommand
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
        void AdmitExistingPatientWrapper(object parameter)
        {
            AdmitExistingPatient();
        }

        bool CanExecuteWrapper(object parameter)
        {
            return true;
        }
        #endregion
    }
}
