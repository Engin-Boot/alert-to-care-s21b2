using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AlertToCareFrontend.ViewModel
{
    class MainWindowViewModel:BaseViewModel
    {
        private IPageViewModel currentPageViewModel;
        private List<IPageViewModel> pageViewModels;

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (pageViewModels == null)
                    pageViewModels = new List<IPageViewModel>();

                return pageViewModels;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return currentPageViewModel;
            }
            set
            {
                currentPageViewModel = value;
                OnPropertyChanged("CurrentPageViewModel");
            }
        }

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        public MainWindowViewModel()
        {
            PageViewModels.Add(new HomeViewModel());
            PageViewModels.Add(new ConfigurationViewModel());
            PageViewModels.Add(new MonitorIcuViewModel());

            CurrentPageViewModel = PageViewModels[0];

            this.GoToConfigurationCommand = new Commands.GoToConfigurationCommandClass(this);
            this.GoToHomeCommand = new Commands.GoToHomeCommandClass(this);
            this.GoToMonitorIcuCommand = new Commands.GoToMonitorIcuCommandClass(this);
        }

        public ICommand GoToConfigurationCommand
        {
            get;
            set;
        }
        public ICommand GoToHomeCommand
        {
            get;
            set;
        }
        public ICommand GoToMonitorIcuCommand
        {
            get;
            set;
        }
        public void GoToHomeView()
        {
            ChangeViewModel(pageViewModels[0]);
        }
        public void GoToConfigurationView()
        {
            ChangeViewModel(pageViewModels[1]);
        }

        public void GoToMonitorIcuView()
        {
            ChangeViewModel(pageViewModels[2]);
        }
        
       
    }
}
