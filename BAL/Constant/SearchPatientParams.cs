namespace BAL.Constant
{
    public class SearchPatientParams
    {
        public string keyword { get; set; }
        public int pagenumber { get; set; }
        public int pagesize { get; set; }
        public string patient_name { get; set; }
        public string date_of_history_vaccine { get; set; }
        public string patient_status { get; set; }
        public string address { get; set; }
        public string person { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string zip_code { get; set; }
        public string sortBy { get; set; } = "patientname";
        public string sortDirection { get; set; } = "desc";


    }
}