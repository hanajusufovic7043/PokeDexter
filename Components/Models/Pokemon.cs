namespace PokeDexter.Components.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public string ImageUrl { get; set; }
        public List<TypeInfo> Types { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int BaseExperience { get; set; }
    }

    public class TypeInfo
    {
        public TypeDetail Type { get; set; }
    }

    public class TypeDetail
    {
        public string Name { get; set; }
    }
  
}
