using AlertToCareFrontend.ViewModel;
using System.Windows;

namespace AlertToCareFrontend.Views
{
    /// <summary>
    /// Interaction logic for AdmitPatientPage.xaml
    /// </summary>
    public partial class AdmitPatientPage
    {
        public AdmitPatientPage(string bedId)
        {
            InitializeComponent();
            DataContext = new AdmitPatientViewModel(bedId);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService == null)
                return;
            NavigationService.Navigate(new MonitorIcuPage());
        }

        private void AdmitPatientButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is AdmitPatientViewModel viewModel)
            {
                ExecuteAdmitPatientCommand(viewModel);
                ReturnToMonitorIcuPageOnSuccess();
            }
        }

        private void ExecuteAdmitPatientCommand(AdmitPatientViewModel viewModel)
        {
            if (viewModel.AdmitNewPatientCommand != null && viewModel.AdmitNewPatientCommand.CanExecute(null))
            {
                viewModel.AdmitNewPatientCommand.Execute(null);
            }
        }

        private void ReturnToMonitorIcuPageOnSuccess()
        {
            if (NavigationService == null)
            {
                return;
            }

            NavigationService.Navigate(new MonitorIcuPage());
        }
    }
}
