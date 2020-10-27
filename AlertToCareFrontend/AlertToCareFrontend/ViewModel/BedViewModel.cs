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
        string message;
        string alertMessage;
        #endregion

        #region Initializers

        public BedViewModel(BedDataModel bed)
        {
            //bedDataModel = new BedDataModel(ref bed);
            bedDataModel = bed;

            AlertMessage = "";

            AddBedCommand = new Command.DelegateCommandClass(AddBedWrapper, CanExecuteWrapper);

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
        #endregion

        #region Logic

        void AddBed()
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
