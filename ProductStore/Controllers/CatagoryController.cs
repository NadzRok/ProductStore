namespace ProductStore.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CatagoryController : ControllerBase {
        private readonly ProductDbContext _productDbContext;
        private readonly ICatagoryService _catagoryService;

        public CatagoryController(ProductDbContext productDbContext, ICatagoryService catagoryService) {
            _productDbContext = productDbContext;
            _catagoryService = catagoryService;
        }

        [HttpGet("allcatagorys")]
        public async Task<ActionResult<List<Category>>> GetCategory() {
            return Ok(await _productDbContext.Categorys.ToListAsync());
        }

        [HttpGet("acatagory")]
        public async Task<ActionResult<Category>> Getcatagory(int CatagoryId) {
            if(CatagoryId <= 0) {
                return NotFound(new Category() { ErrorMessage = new ErrorMessage() { IsError = true, Message = $"No data." } });
            }
            var catagoryReturn = await _productDbContext.Categorys.FirstOrDefaultAsync(cr => cr.CategoryId == CatagoryId);
            if(catagoryReturn == null) {
                return NotFound(NotFound(new Category() { ErrorMessage = new ErrorMessage() { IsError = true, Message = $"No catagory found." } }));
            }
            return Ok(catagoryReturn);
        }

        [HttpPost("addcatagory")]
        public async Task<ActionResult<Category>> PostCatagory([FromBody] Category CatagoryToAdd) {
            if(CatagoryToAdd == null) {
                return BadRequest(new Category() { ErrorMessage = new ErrorMessage() { IsError = true, Message = $"No catagory to add." } });
            }
            if(_productDbContext.Categorys.Any(x => x.CategoryCode == CatagoryToAdd.CategoryCode)) {
                return BadRequest(new Category() { ErrorMessage = new ErrorMessage() { IsError = true, Message = $"Catagory ID already exists." } });
            }
            var newCatagory = new Category() {
                Name = CatagoryToAdd.Name,
                IsActive = true
            };
            var codeCheck = _catagoryService.CheckCatagoryCode(CatagoryToAdd.CategoryCode);
            if(codeCheck) {
                newCatagory.CategoryCode = CatagoryToAdd.CategoryCode;
            } else {
                return BadRequest(new Category() { ErrorMessage = new ErrorMessage() { IsError = true, Message = $"catagory code invalid." } });
            }
            await _productDbContext.AddAsync(newCatagory);
            await _productDbContext.SaveChangesAsync();
            return Ok(newCatagory);
        }

        [HttpPut("updatecatagory")]
        public async Task<ActionResult<Category>> PutCatagory(Category CatagoryToUpdate) {
            if(CatagoryToUpdate == null) {
                return BadRequest(new Category() { ErrorMessage = new ErrorMessage() { IsError = true, Message = $"No catagory to update." } });
            }
            var oldCatagory = await _productDbContext.Categorys.FirstOrDefaultAsync(op => op.CategoryId == CatagoryToUpdate.CategoryId);
            if(oldCatagory == null) {
                return BadRequest(new Category() { ErrorMessage = new ErrorMessage() { IsError = true, Message = $"No catagory found to update." } });
            }
            oldCatagory.Name = CatagoryToUpdate.Name;
            var codeCheck = _catagoryService.CheckCatagoryCode(CatagoryToUpdate.CategoryCode);
            if(codeCheck) {
                oldCatagory.CategoryCode = CatagoryToUpdate.CategoryCode;
            } else {
                return BadRequest(new Category() { ErrorMessage = new ErrorMessage() { IsError = true, Message = $"catagory code invalid." } });
            }
            oldCatagory.IsActive = CatagoryToUpdate.IsActive;
            await _productDbContext.SaveChangesAsync();
            return Ok(oldCatagory);
        }

        [HttpDelete("deletecatagory")]
        public async Task<ActionResult<Category>> DeleteCatagory(int CatagoryId) {
            if(CatagoryId == 0) {
                return BadRequest(new Category() { ErrorMessage = new ErrorMessage() { IsError = true, Message = $"No catagory." } });
            }
            var catagoryToDelete = await _productDbContext.Categorys.FirstOrDefaultAsync(ptd => ptd.CategoryId == CatagoryId);
            if(catagoryToDelete == null) {
                return BadRequest(new Category() { ErrorMessage = new ErrorMessage() { IsError = true, Message = $"No catagory found to delete." } });
            }
            _productDbContext.Categorys.Remove(catagoryToDelete);
            await _productDbContext.SaveChangesAsync();
            return Ok(catagoryToDelete);
        }
    }
}
