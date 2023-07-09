namespace ProductStore.Models {
    public class Product {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductCode { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        [Required, ForeignKey("CatagoryId")]
        public Category Catagories { get; set; }
    }
}
