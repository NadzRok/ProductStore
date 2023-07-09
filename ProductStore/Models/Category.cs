namespace ProductStore.Models {
    public class Category {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string CategoryCode { get; set; } = string.Empty;
        [Required]
        public bool IsActive { get; set; }
    }
}
