namespace ProductStore.Models {
    public class Product {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductCode { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public int CatagorieId { get; set; }
        public Category? Catagorie { get; set; }

        [NotMapped]
        public ErrorMessage ErrorMessage { get; set; } = new ErrorMessage();
    }
}
