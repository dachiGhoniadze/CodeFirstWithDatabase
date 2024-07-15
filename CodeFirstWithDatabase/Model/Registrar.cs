using System.ComponentModel.DataAnnotations;

namespace CodeFirstWithDatabase.Model
{
    public class Registrar
    {
        public int RegistrarId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Start_Date { get; set; }
    }
}
