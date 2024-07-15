using System.ComponentModel.DataAnnotations;

namespace CodeFirstWithDatabase.Model
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        public bool Insurance { get; set; }
        public string PersonalID { get; set; }

    }
}
