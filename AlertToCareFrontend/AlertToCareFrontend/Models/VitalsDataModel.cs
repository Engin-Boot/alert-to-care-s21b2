namespace AlertToCareFrontend.Models
{
    class VitalsDataModel
    {
        public string PatientId { get; set; }
        public string PatientBedId { get; set; }
        public float Bpm { get; set; }
        public float Spo2 { get; set; }
        public float RespRate { get; set; }

        public VitalsDataModel()
        {
            PatientId = "";
            PatientBedId = "";
            Bpm = 120;
            Spo2 = 94;
            RespRate = 60;
        }
        public VitalsDataModel(string patientId, string bedId)
        {
            PatientId = patientId;
            PatientBedId = bedId;
            Bpm = 120;
            Spo2 = 94;
            RespRate = 60;
        }
    }
}
