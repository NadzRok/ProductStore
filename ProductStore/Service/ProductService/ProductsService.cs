namespace ProductStore.Service.ProductService {
    public class ProductsService : IProductsService {

        public string GetProductCode() {
            return DateTime.Now.Year.ToString() + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        }
    }
}
