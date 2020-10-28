using AlertToCareFrontend.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AlertToCareFrontend.Views
{
    /// <summary>
    /// Interaction logic for AdmitPatientPage.xaml
    /// </summary>
    public partial class AdmitPatientPage : Page
    {
        public AdmitPatientPage(string bedId)
        {
            InitializeComponent();
            DataContext = new AdmitPatientViewModel(bedId);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
            else
            {
                this.NavigationService.Navigate(new MonitorIcuPage());
            }
        }

        private void AdmitPatientButton_Click(object sender, RoutedEventArgs e)
        {
            AdmitPatientViewModel viewModel = (DataContext as AdmitPatientViewModel);
            if (viewModel.AdmitNewPatientCommand.CanExecute(null))
            {
                viewModel.AdmitNewPatientCommand.Execute(null);
                
                NavigationService.Navigate(new MonitorIcuPage());
            }
        }
    }
}
