using System;

namespace Application.Dishes.Dtos
{
    public class DishDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Availability { get; set; }
        public bool Active { get; set; }
        public int PreparationTime { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}