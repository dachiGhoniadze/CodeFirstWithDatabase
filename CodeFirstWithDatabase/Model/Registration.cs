using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstWithDatabase.Model
{
    public class Registration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegistrationID { get; set; }

        public int PatientID { get; set; }
        [ForeignKey("PatientID")]

        public int LaboratoryID { get; set; }
        [ForeignKey("LaboratoryID")]

        public int ServiceID { get; set; }
        [ForeignKey("ServiceID")]
        public int RegistrarID { get; set; }
        [ForeignKey("RegistrarID")]
        public DateTime RegDate { get; set; }
    }
}

//[Key]
//[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//public int RegistrationID { get; set; }

//public int PatientID { get; set; }
//[ForeignKey("PatientID")]

//public DateTime RegDate { get; set; }

//public int LaboratoryID { get; set; }
//[ForeignKey("LaboratoryID")]

//public int ServiceID { get; set; }
//[ForeignKey("ServiceID")]

//public int RegistrarID { get; set; }
//[ForeignKey("RegistrarID")]
