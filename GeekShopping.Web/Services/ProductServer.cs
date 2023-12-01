using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services
{
    public class ProductServer : IProductService
    {
        private readonly HttpClient _client;
        public const string BasePath = $@"api/v1/product";

        public ProductServer(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<ProductModel>> FindAllProduct()
        {
            var res = await _client.GetAsync(BasePath);
            return await res.ReadContentAs<List<ProductModel>>();
        }

        public async Task<ProductModel> FindProductById(long id)
        {
            var res = await _client.GetAsync($"{BasePath}/{id}");
            return await res.ReadContentAs<ProductModel>();
        }

        public async Task<ProductModel> CreateProduct(ProductModel model)
        {
            var res = await _client.PostAsJSon(BasePath, model);
            if (res.IsSuccessStatusCode)
                return await res.ReadContentAs<ProductModel>();
            else
                throw new Exception("Something went wrong when calling API");
        }

        public async Task<ProductModel> UpdateProduct(ProductModel model)
        {
            var res = await _client.PutAsJSon(BasePath, model);
            if (res.IsSuccessStatusCode)
                return await res.ReadContentAs<ProductModel>();
            else
                throw new Exception("Something went wrong when calling API");

        }

        public async Task<bool> DeleteProduct(long id)
        {
            var res = await _client.DeleteAsync($"{BasePath}/{id}");
            if (res.IsSuccessStatusCode)
                return await res.ReadContentAs<bool>();
            else
                throw new Exception("Something went wrong when calling API");
        }
    }
}