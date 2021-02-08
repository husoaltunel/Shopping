using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Shopping.MVCWebUI.Entity
{
    public class DataInitializer : CreateDatabaseIfNotExists<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var categories = new List<Category>()
            {
                new Category(){Name = "Giyim & Ayakkabı", Description = "Bayan ve Erkek genç yaştan itibaren herkese hitap eden giyim ve ayakkabılar"},
                new Category(){Name = "Elektronik", Description = "Her türlü ev ve iş yerlerine uygun elektronik aletler"},
                new Category(){Name = "Anne & Bebek", Description = "Anne ve Bebek Giyimleri"}
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();

            var subCategories = new List<SubCategory>()
            {
                new SubCategory(){Name = "Kadın Giyim & Aksesuar",Description = "Bayanlara özel tarzda şık giyim ve özel aksesuarlar",CategoryId = 1},
                new SubCategory(){Name = "Erkek Giyim & Aksesuar",Description = "Erkeklere özel tarzda şık giyim ve özel aksesuarlar",CategoryId = 1},
                new SubCategory(){Name = "Telefon & Aksesuarları",Description = "Her bütçeye uygun telefon modelleri ve onlara uygun aksesuarlar",CategoryId = 2},
                new SubCategory(){Name = "Bilgisayar",Description = "Laptop ve Masaüstü Pc'ler",CategoryId = 2},
                new SubCategory(){Name = "Televizyon ve Ses Sistemleri",Description = "Eski ve yeni model Televizyon ve ses sistemleri",CategoryId = 2},
                new SubCategory(){Name = "Bebek Giyim",Description = "Bebeklere özel kıyafetler",CategoryId = 3},
                new SubCategory(){Name = "Hamile Giyim",Description = "Hamilelere özel ölçüde günlük kıyafetler",CategoryId = 3}
            };
            context.SubCategories.AddRange(subCategories);
            context.SaveChanges();

            var products = new List<Product>()
            {
                new Product(){Name = "Kışlık Uzun Kollu Bayan Peluş Polar Pijama Takımı",Description = "Slimfit Gözbandı Hediyeli Kışlık Kadın Welsoft Peluş Pijama Takım",Price = 109.00, Stock = 10,QuantitySold = 10,Images = "1.jpg,", DefaultImage = "1.jpg", IsHome = true,IsActive = EnumIsActiveState.Active, SubCategoryId = 1},
                                            
            };

            context.Products.AddRange(products);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}