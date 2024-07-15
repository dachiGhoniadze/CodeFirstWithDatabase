namespace CodeFirstWithDatabase.Model
{
    public class Laboratory
    {
        public int LaboratoryID { get; set; }
        public int? Tested { get; set; }
        public string Lab_Result { get; set; }
        public int? Payment { get; set; }
        public int? Result_Sended { get; set; }
    }
}
