using RetailInMotion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RetailInMotion.Core.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        List<Product> ListPaginated(int page, int pSize);
    }
}
