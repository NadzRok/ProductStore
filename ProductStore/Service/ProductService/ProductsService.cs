namespace ProductStore.Service.ProductService {
    public class ProductsService : IProductsService {

        public string GetProductCode(int InvoiceCount) {
            if(InvoiceCount >= 1000) {
                while(InvoiceCount > 1000) {
                    InvoiceCount -= 999;
                }
            }
            var codeEnd = string.Empty;
            if(InvoiceCount < 10) {
                codeEnd = "00" + InvoiceCount;
            } else if(InvoiceCount < 100) {
                codeEnd = "0" + InvoiceCount;
            } else {
                codeEnd = InvoiceCount.ToString();
            }
            return DateTime.Now.Year.ToString() + DateTime.Now.Day.ToString() + "-" + codeEnd;
        }
    }
}
