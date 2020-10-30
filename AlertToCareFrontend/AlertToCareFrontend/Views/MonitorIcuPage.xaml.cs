using AlertToCareFrontend.Models;
using AlertToCareFrontend.ViewModel;
using System.Windows;

namespace AlertToCareFrontend.Views
{
    /// <summary>
    /// Interaction logic for MonitorIcuPage.xaml
    /// </summary>
    public partial class MonitorIcuPage
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
            BedViewModel bedViewModel = GetBedViewModel(e.Source);
            if (bedViewModel == null)
                return;
            //BedView bed = e.Source as BedView;
            //BedViewModel bedViewModel = bed.DataContext as BedViewModel;
            string bedId = bedViewModel.BedId;
            if (NavigationService != null)
            {
                NavigationService.Navigate(new AdmitPatientPage(bedId));
            }
        }

        private BedViewModel GetBedViewModel(object source)
        {
            if (source != null)
            {
                if (source is BedView bed)
                {
                    return bed.DataContext as BedViewModel;
                }
            }
            return null;
        }
        private void UpdateVitalsButtonClickedEvent_Handler(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            //BedView bed = e.Source as BedView;
            //BedViewModel bedViewModel = bed.DataContext as BedViewModel;
            BedViewModel patientBedViewModel = GetBedViewModel(e.Source);
            if (patientBedViewModel == null)
                return;

            string bedId = patientBedViewModel.BedId;
            string patientId = patientBedViewModel.PatientId;
            if (NavigationService != null)
            {
                NavigationService.Navigate(new UpdateVitalsPage(patientId, bedId));
            }
        }

        private void PositionBedAsPerIcuLayout(BedDataModel bedDataModel, int bedNum, int totalNumOfBeds, string layout)
        {
            BedView bed = new BedView(bedDataModel);

            if (bedNum <= totalNumOfBeds / 2)
            {
                LeftStackPanel.Children.Add(bed);
            }
            else
            {
                if (layout.Equals("Parallel"))
                {
                    RightStackPanel.Children.Add(bed);
                }
                else
                {
                    BottomStackPanel.Children.Add(bed);
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService != null)
            {
                NavigationService.Navigate(new HomePage());
            }
        }
    }
}
