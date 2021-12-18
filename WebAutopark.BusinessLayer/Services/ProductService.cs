using AutoMapper;
using WebAutopark.BusinessLayer.Interfaces;
using WebAutopark.BusinessLayer.Models;
using WebAutopark.BusinessLayer.Services.Base;
using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories.Base;

namespace WebAutopark.BusinessLayer.Services
{
    public class ProductService : Service<ProductModel, Product, IRepository<Product>>, IProductService
    {
        public ProductService(IRepository<Product> repository, IMapper mapper) 
            : base(repository, mapper)
        {
        }
    }
}