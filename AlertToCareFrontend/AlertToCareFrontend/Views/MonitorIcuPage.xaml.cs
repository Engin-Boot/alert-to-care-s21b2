using AlertToCareFrontend.Models;
using AlertToCareFrontend.ViewModel;
using AlertToCareFrontend.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AlertToCareFrontend
{
    /// <summary>
    /// Interaction logic for MonitorIcuPage.xaml
    /// </summary>
    public partial class MonitorIcuPage : Page
    {
        public MonitorIcuPage()
        {
            InitializeComponent();

            MonitorIcuViewModel viewModel = new MonitorIcuViewModel();
            DataContext = viewModel;

            int totalNoOfBeds = viewModel.BedDataModelList.Count;
            int bedNum = 1;
            string layout = viewModel.IcuLayout;

            foreach (BedDataModel bedDataModel in viewModel.BedDataModelList)
            {
                PositionBedAsPerIcuLayout(bedDataModel, bedNum, totalNoOfBeds, layout);
                
                bedNum++;
            }

            // Register the Bubble Event Handler 
            AddHandler(BedView.AdmitButtonClickedEvent,
                new RoutedEventHandler(AdmitButtonClickedEvent_Handler));

            AddHandler(BedView.UpdateVitalsButtonClickedEvent,
                new RoutedEventHandler(UpdateVitalsButtonClickedEvent_Handler));
        }

        private void AdmitButtonClickedEvent_Handler(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            BedView bed = e.Source as BedView;
            BedViewModel bedViewModel = bed.DataContext as BedViewModel;
            string bedId = bedViewModel.BedId;
            NavigationService.Navigate(new AdmitPatientPage(bedId));
        }
        private void UpdateVitalsButtonClickedEvent_Handler(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            BedView bed = e.Source as BedView;
            BedViewModel bedViewModel = bed.DataContext as BedViewModel;
            string bedId = bedViewModel.BedId;
            string patientId = bedViewModel.PatientId;
            NavigationService.Navigate(new UpdateVitalsPage(patientId, bedId));
        }

        private void PositionBedAsPerIcuLayout(BedDataModel bedDataModel, int bedNum, int totalNumOfBeds, string layout)
        {
            BedView bed = new BedView(bedDataModel);

            if (bedNum <= totalNumOfBeds / 2)
            {
                leftStackPanel.Children.Add(bed);
            }
            else
            {
                if (layout.Equals("Parallel"))
                {
                    rightStackPanel.Children.Add(bed);
                }
                else
                {
                    bottomStackPanel.Children.Add(bed);
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if(this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
            else
            {
                this.NavigationService.Navigate(new HomePage());
            }
        }
    }
}
