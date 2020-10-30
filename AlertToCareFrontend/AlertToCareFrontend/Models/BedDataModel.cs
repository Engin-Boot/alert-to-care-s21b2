namespace AlertToCareFrontend.Models
{
    public class BedDataModel
    {
        public string BedId { get; set; }
        public bool BedStatus { get; set; }
        public string PatientId { get; set; }
        public string IcuId { get; set; }

        public BedDataModel()
        {
            BedId = "";
            BedStatus = false; // equivalent to IsBedOccupied
            PatientId = "";
            IcuId = Properties.Settings.Default.currentIcuId;
        }
        public BedDataModel(string bedId)
        {
            BedId = bedId;
            BedStatus = false; // equivalent to IsBedOccupied
            PatientId = "";
            IcuId = Properties.Settings.Default.currentIcuId;
        }
        public BedDataModel(string bedId, bool status, string patientId)
        {
            BedId = bedId;
            BedStatus = status;
            PatientId = patientId;
            IcuId = Properties.Settings.Default.currentIcuId;
        }
    }
}
