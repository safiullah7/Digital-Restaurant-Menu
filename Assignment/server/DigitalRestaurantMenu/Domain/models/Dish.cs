namespace Domain.models
{
    public class Dish: BaseModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; } //might have separate model
        public string Availability { get; set; } //might have separate model
        public bool Active { get; set; }
        public int PreparationTime { get; set; }
    }
}