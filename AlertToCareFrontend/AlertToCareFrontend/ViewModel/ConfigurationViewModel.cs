using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public void AddIcu()
        {
            // Logic to call backend api to add icu details
            // Set success/failure message
            Message = "Add request received: " + IcuId + ", " + TotalNoOfBeds.ToString() + ", " + Layout;
        }
        public void UpdateIcu()
        {
            // Logic to call backend api to update icu details
            // Set success/failure message
            Message = "Update request received: " + IcuId + ", " + TotalNoOfBeds.ToString() + ", " + Layout;
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
