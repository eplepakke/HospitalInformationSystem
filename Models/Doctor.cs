namespace HospitalInformationSystem.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string SurnameInitials { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public int OfficeNumber { get; set; }
        public string Schedule { get; set; } = string.Empty;
        public override string ToString()
        {
            return Id.ToString() + " - " + SurnameInitials + " - " + Position + " - " + OfficeNumber.ToString() + " - " + Schedule;
        }
    }
}
