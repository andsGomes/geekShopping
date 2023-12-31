using AutoMapper;
using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySqlContext _mySqlContext;
        private readonly IMapper _mapper;

        public ProductRepository(MySqlContext mySqlContext, IMapper mapper)
        {
            _mySqlContext = mySqlContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            List<Product> products = await _mySqlContext.Products.ToListAsync();
            return _mapper.Map<List<ProductVO>>(products);
        }

        public async Task<ProductVO> FindById(long id)
        {
            Product product =
                await _mySqlContext.Products.Where(p => p.Id == id)
                .FirstOrDefaultAsync();
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<ProductVO> Create(ProductVO vo)
        {
            Product product = _mapper.Map<Product>(vo);
            _mySqlContext.Products.Add(product);
            await _mySqlContext.SaveChangesAsync();
            return _mapper.Map<ProductVO>(product);
        }
        public async Task<ProductVO> Update(ProductVO vo)
        {
            Product product = _mapper.Map<Product>(vo);
            _mySqlContext.Products.Update(product);
            await _mySqlContext.SaveChangesAsync();
            return _mapper.Map<ProductVO>(product);

        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                Product product =
                   await _mySqlContext.Products.Where(p => p.Id == id)
                   .FirstOrDefaultAsync();

                if (product == null) return false;

                _mySqlContext.Products.Remove(product);
                await _mySqlContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }

    }
}