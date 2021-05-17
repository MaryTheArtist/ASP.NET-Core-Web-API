using Data.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.DataManager
{
    public class ProductManager : IDataRepository<Product>
    {
        readonly ProductContext _productContext;

        public ProductManager(ProductContext context)
        {
            _productContext = context;
        }

        public void Add(Product entity)
        {
            _productContext.Products.Add(entity);
            _productContext.SaveChanges();
        }

        public void Delete(Product entity)
        {
            _productContext.Products.Remove(entity);
            _productContext.SaveChanges();
        }

        public Product Get(string id)
        {
            return _productContext.Products.FirstOrDefault(p => p.ProductID == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productContext.Products.ToList();
        }

        public void Update(Product dbEntity, Product entity)
        {
            dbEntity.Name = entity.Name;
            dbEntity.Brand = entity.Brand;
            dbEntity.Price = entity.Price;
            dbEntity.Quantity = entity.Quantity;
            dbEntity.Weight = entity.Weight;

            _productContext.SaveChanges();
        }
    }
}
