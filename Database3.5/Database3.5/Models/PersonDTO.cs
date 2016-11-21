namespace Database3._5.Models
{
    public class PersonDTO
    {
        public string Name { get; set; }
        public long PersonNummer { get; set; }
        public virtual Adresse Adresse { get; set; }

    }
}