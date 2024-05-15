namespace HospitalInformationSystem.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public int YearOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Workplace { get; set; } = string.Empty;
        public override string ToString() 
        {
            return Id.ToString() + " - " + RegistrationNumber + " - " + FullName + " - " + YearOfBirth.ToString() + " - " + Address + " - " + Workplace;
        }
    }
}
