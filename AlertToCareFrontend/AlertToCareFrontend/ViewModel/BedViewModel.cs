using AlertToCareFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AlertToCareFrontend.ViewModel
{
    class BedViewModel:BaseViewModel
    {
        #region Fields
        BedDataModel bedDataModel;
        PatientDataModel patientDataModel;
        string message;
        string alertMessage;
        #endregion

        #region Initializers

        public BedViewModel(BedDataModel bed)
        {
            //bedDataModel = new BedDataModel(ref bed);
            bedDataModel = bed;

            patientDataModel = new PatientDataModel();

            AlertMessage = "";

            AddBedCommand = new Command.DelegateCommandClass(AddBedWrapper, CanExecuteWrapper);

            AdmitPatientCommand = new Command.DelegateCommandClass(AdmitPatientWrapper, CanExecuteWrapper);

            CheckPatientVitalsCommand = new Command.DelegateCommandClass(CheckPatientVitalsWrapper, CanExecuteWrapper);

            DischargePatientCommand = new Command.DelegateCommandClass(DischargePatientWrapper, CanExecuteWrapper);

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
        #endregion

        #region Logic

        void AddBed()
        {

        }
        void AdmitPatient()
        {

        }
        void CheckPatientVitals()
        {

        }
        void DischargePatient()
        {

        }


        #endregion

        #region Commands
        public ICommand AddBedCommand
        {
            get;
            set;
        }
        public ICommand AdmitPatientCommand
        {
            get;
            set;
        }
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

        #endregion

        #region Command helper Methods
        void AddBedWrapper(object parameter)
        {
            this.AddBed();
        }
        void AdmitPatientWrapper(object parameter)
        {
            this.AdmitPatient();
        }
        void DischargePatientWrapper(object parameter)
        {
            this.DischargePatient();
        }
        void CheckPatientVitalsWrapper(object parameter)
        {
            this.CheckPatientVitals();
        }
        bool CanExecuteWrapper(object parameter)
        {
            return true;
        }
        #endregion
    }
}
