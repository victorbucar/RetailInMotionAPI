using RetailInMotion.Core;
using RetailInMotion.Core.Entities;
using RetailInMotion.Core.Interfaces;
using RetailInMotion.Data.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailInMotion.Data.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly RetailContext _retailContext;

        public ProductRepository(RetailContext retailContext) : base(retailContext)
        {
            _retailContext = retailContext;
        }

        public List<Product> ListPaginated(int page, int pSize)
        {
            var products = _retailContext.Products;
            IList<Product> Products = new List<Product>();

            foreach (var product in products)
            {
                Products.Add(product);
            }
           var pagedList =  GetPaginatedResult(page, Products, pSize).ToList();
  
            return pagedList;

        }
        public IList<Product> GetPaginatedResult(int currentPage, IList<Product> products, int pSize = 5)
        {
            return products.OrderBy(d => d.Id).Skip((currentPage - 1) * pSize).Take(pSize).ToList();
        }

    }
}
