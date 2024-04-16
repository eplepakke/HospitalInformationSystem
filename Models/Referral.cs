using System;

namespace HospitalInformationSystem.Models
{
    public class Referral
    {
        public int Id { get; set; }
        public string PatientRegistrationNumber { get; set; } = string.Empty;
        public string DoctorSurnameInitials { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
    }
}
