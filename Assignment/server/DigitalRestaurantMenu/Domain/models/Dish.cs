namespace Domain.models
{
    public class Dish: BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Availability { get; set; }
        public bool Active { get; set; }
        public int PreparationTime { get; set; }
    }
}