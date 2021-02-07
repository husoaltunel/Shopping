using Shopping.MVCWebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopping.MVCWebUI.Models
{
    public class Cart
    {
        private List<CartLine> _CartLines = new List<CartLine>();
        public List<CartLine> CartLines { get { return _CartLines; } }
        public void AddProduct(Product product, int quantity)
        {
            var cartLine = _CartLines.FirstOrDefault(cLine => cLine.Product.Id == product.Id);
            if (cartLine == null)
            {
                _CartLines.Add(new CartLine() { Product = product, Quantity = quantity });
            }
            else
            {
                if (product.CampaignStatus == true && product.DiscountedPrice != 0)
                {
                    cartLine.Product.Price = product.DiscountedPrice;
                }
                else
                {
                    cartLine.Product.Price = product.Price;
                }
                cartLine.Quantity += quantity;
            }
        }
        public void DeleteProduct(Product product)
        {
            _CartLines.RemoveAll(cLine => cLine.Product.Id == product.Id);
        }
        public double TotalPrice()
        {
            
            return _CartLines.Sum(cLine => (cLine.Product.CampaignStatus == true && cLine.Product.DiscountedPrice != 0 ? cLine.Product.DiscountedPrice : cLine.Product.Price) * cLine.Quantity);
        }
        public void Clear()
        {
            _CartLines.Clear();
        }
    }
    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

}