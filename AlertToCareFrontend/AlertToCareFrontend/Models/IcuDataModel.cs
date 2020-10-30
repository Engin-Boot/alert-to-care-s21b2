namespace AlertToCareFrontend.Models
{
    class IcuDataModel
    {
        public string IcuId { get; set; }
        public int TotalNoOfBeds { get; set; }
        public string Layout { get; set; }

        public IcuDataModel(string id, int numBeds, string layout)
        {
            IcuId = id;
            TotalNoOfBeds = numBeds;
            Layout = layout;
        }
    }
}
