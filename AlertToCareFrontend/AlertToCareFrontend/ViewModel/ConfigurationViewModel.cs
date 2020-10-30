using AlertToCareFrontend.Models;
using AlertToCareFrontend.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AlertToCareFrontend.ViewModel
{
    class ConfigurationViewModel:BaseViewModel
    {
        #region Fields
        readonly IcuDataModel _icuDataModel;
        ObservableCollection<string> _layouts;
        string _message;
        #endregion

        #region Initializers
        public ConfigurationViewModel()
        {
            Layouts = new ObservableCollection<string>()
            {
                "L Shape",
                "Parallel"
            };

            _icuDataModel = new IcuDataModel("", 1, Layouts[0]);

            AddIcuCommand = new Command.DelegateCommandClass(AddIcuWrapper, CanExecuteWrapper);

        }
        #endregion
        #region Properties
        public string IcuId
        {
            get => _icuDataModel.IcuId;
            set
            {
                if (value != _icuDataModel.IcuId)
                {
                    _icuDataModel.IcuId = value;
                    OnPropertyChanged();
                }
            }
        }
        public int TotalNoOfBeds
        {
            get => _icuDataModel.TotalNoOfBeds;
            set
            {
                if (value != _icuDataModel.TotalNoOfBeds)
                {
                    _icuDataModel.TotalNoOfBeds = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Layout
        {
            get => _icuDataModel.Layout;
            set
            {
                if (value != _icuDataModel.Layout)
                {
                    _icuDataModel.Layout = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<string> Layouts
        {
            get => _layouts;
            set
            {
                if (value != _layouts)
                {
                    _layouts = value;
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
        private void AddBeds(IcuDataModel icu)
        {
            for (int bedIndex = 1; bedIndex <= icu.TotalNoOfBeds; bedIndex++)
            {
                // Add new bed
                string bedId = icu.IcuId + "Bed" + bedIndex;
                BedDataModel bed = new BedDataModel(bedId);
                string message = HttpClientUtility.PostBedData(bed).Result;
                if (!String.IsNullOrEmpty(message))
                {
                    Message = message;
                    return;
                }
            }
            Message = "ICU Configuration successful!";
        }
        
        private void AddIcu()
        {
            // Logic to call backend api to add icu details
            IcuDataModel newIcu = new IcuDataModel(IcuId, TotalNoOfBeds, Layout);

            string message = HttpClientUtility.PostIcuData(newIcu).Result;

            if(!String.IsNullOrEmpty(message))
            {
                Message = message;
                return;
            }

            // Update settings
            Properties.Settings.Default.currentIcuId = newIcu.IcuId;
            Properties.Settings.Default.Save();

            // Add beds for icu
            AddBeds(newIcu);          
        }

        #endregion
        #region Commands
        public ICommand AddIcuCommand
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
        bool CanExecuteWrapper(object parameter)
        {
            return true;
        }
        #endregion
    }
}
