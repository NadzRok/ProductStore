namespace ProductStore.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase {
        private readonly ProductDbContext _productDbContext;
        private readonly IProductsService _productService;

        public ProductController(ProductDbContext productDbContext, IProductsService productsService) {
            _productDbContext = productDbContext;
            _productService = productsService;
        }

        [HttpGet("allproducts")]
        public async Task<ActionResult<List<Product>>> GetProducts() {
            return Ok(await _productDbContext.Products.ToListAsync());
        }

        [HttpGet("aproduct")]
        public async Task<ActionResult<Product>> GetProduct(int ProductId) {
            if(ProductId <= 0) {
                return NotFound(new Product() { ErrorMessage = new ErrorMessage() { IsError = true, Message = $"No data." } });
            }
            var productReturn = await _productDbContext.Products.FirstOrDefaultAsync(pr => pr.ProductId == ProductId);
            if(productReturn == null) {
                return NotFound(NotFound(new Product() { ErrorMessage = new ErrorMessage() { IsError = true, Message = $"No product found." } }));
            }
            return Ok(productReturn);
        }

        [HttpPost("addproduct")]
        public async Task<ActionResult<Product>> PostProduct([FromBody]Product ProductToAdd) {
            if(ProductToAdd == null) {
                return BadRequest(new Product() { ErrorMessage = new ErrorMessage() { IsError = true, Message = $"No product to add." } });
            }
            var newProduct = new Product() {
                Name = ProductToAdd.Name,
                Price = ProductToAdd.Price,
                ImageUrl = ProductToAdd.ImageUrl,
                CatagorieId = ProductToAdd.CatagorieId
            };
            newProduct.ProductCode = _productService.GetProductCode();
            await _productDbContext.AddAsync(newProduct);
            await _productDbContext.SaveChangesAsync();
            return Ok(newProduct);
        }

        [HttpPut("updateproduct")]
        public async Task<ActionResult<Product>> PutProduct(Product ProductToUpdate) {
            if(ProductToUpdate == null) {
                return BadRequest(new Product() { ErrorMessage = new ErrorMessage() { IsError = true, Message = $"No product to update." } });
            }
            var oldProduct = await _productDbContext.Products.FirstOrDefaultAsync(op => op.ProductId == ProductToUpdate.ProductId);
            if(oldProduct == null) {
                return BadRequest(new Product() { ErrorMessage = new ErrorMessage() { IsError = true, Message = $"No product found to update." } });
            }
            oldProduct.Name = ProductToUpdate.Name;
            oldProduct.Price = ProductToUpdate.Price;
            oldProduct.ImageUrl = ProductToUpdate.ImageUrl;
            oldProduct.Description = ProductToUpdate.Description;
            oldProduct.CatagorieId = ProductToUpdate.CatagorieId;
            await _productDbContext.SaveChangesAsync();
            return Ok(oldProduct);
        }

        [HttpDelete("deleteproduct")]
        public async Task<ActionResult<bool>> DeleteProduct(int ProductId) {
            if(ProductId == 0) {
                return BadRequest(false);
            }
            var productToDelete = await _productDbContext.Products.FirstOrDefaultAsync(ptd => ptd.ProductId == ProductId);
            if(productToDelete == null) {
                return BadRequest(false);
            }
            _productDbContext.Products.Remove(productToDelete);
            await _productDbContext.SaveChangesAsync();
            return Ok(true);
        }
    }
}
