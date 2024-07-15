using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CodeFirstWithDatabase.Model
{
    public class Prescription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PrescriptionID { get; set; }

        public int PatientID { get; set; }
        [ForeignKey("PatientID")]

        public int ServiceID { get; set; }
        [ForeignKey("ServiceID")]

        public int PharmacyID { get; set; }
        [ForeignKey("PharmacyID")]

        public string Description { get; set; }
    }

}
