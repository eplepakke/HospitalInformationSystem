using System;

namespace HospitalInformationSystem.Models
{
    public class Referral
    {
        public int Id { get; set; }
        public string PatientRegistrationNumber { get; set; } = string.Empty;
        public string DoctorSurnameInitials { get; set; } = string.Empty;
        public DateTime Datetime { get; set; }
        public override string ToString()
        {
            return Id.ToString() + " - " + PatientRegistrationNumber + " - " + DoctorSurnameInitials + " - " + Datetime.ToString();
        }
    }
}
