using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService : IProductService
    {
        private List<Product> productItems;

        public ProductService()
        {
            productItems = new List<Product>();
        }

        #region Public methods
        public Product AddProduct(Product productItem)
        {
            productItems.Add(productItem);
            return productItem;
        }

        public string DeleteProduct(string id)
        {
            for (var index = productItems.Count - 1; index >= 0; index--)
            {
                if (productItems[index].ProductID == id)
                {
                    productItems.RemoveAt(index);
                }
            }
            return id;
        }

        public List<Product> GetProducts()
        {
            return productItems;
        }

        public Product UpdateProduct(string id, Product productItem)
        {
            for (var index = productItems.Count - 1; index >= 0; index--)
            {
                if (productItems[index].ProductID == id)
                {
                    productItems[index] = productItem;
                }
            }
            return productItem;
        }
        #endregion
    }
}
