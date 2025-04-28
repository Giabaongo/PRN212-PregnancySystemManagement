namespace PregnancySystemManagement.Models
{
    public class FetalGrowthStandard
    {
        public int Id { get; set; }
        public double GestationalAge { get; set; }  // in weeks
        public double HC_P3 { get; set; }  // Head Circumference 3rd percentile
        public double HC_P50 { get; set; } // Head Circumference 50th percentile
        public double HC_P97 { get; set; } // Head Circumference 97th percentile
        public double AC_P3 { get; set; }  // Abdominal Circumference 3rd percentile
        public double AC_P50 { get; set; } // Abdominal Circumference 50th percentile
        public double AC_P97 { get; set; } // Abdominal Circumference 97th percentile
        public double FL_P3 { get; set; }  // Femur Length 3rd percentile
        public double FL_P50 { get; set; } // Femur Length 50th percentile
        public double FL_P97 { get; set; } // Femur Length 97th percentile
        public double EFW_P3 { get; set; }  // Estimated Fetal Weight 3rd percentile
        public double EFW_P50 { get; set; } // Estimated Fetal Weight 50th percentile
        public double EFW_P97 { get; set; } // Estimated Fetal Weight 97th percentile
    }
}