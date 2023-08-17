using Eshop.Core.Entities;
using Eshop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Infrastructure.Repositories
{
    internal sealed class ProductsRepository : Core.Interfaces.IProducts
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Product> List()
        {
            return _dbContext.Products.ToList();
        }
    }
}
