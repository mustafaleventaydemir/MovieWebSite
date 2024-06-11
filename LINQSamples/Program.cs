using LINQSamples.Data;
using Microsoft.EntityFrameworkCore;

namespace LINQSamples
{
    internal class Program
    {
        class ProductModel
        {
            public string? Name { get; set; }
            public decimal? Price { get; set; }
        }
        static void Main(string[] args)
        {
            using (var db = new NorthwindContext())
            {
                //var products = db.Products.Where(p => p.CategoryId == 1).ToList(); //Product tablosundaki categoryıd'si 1 olanlar gelicekti.

                //var products = db.Products.Include(p => p.Category).Where(p => p.Category.CategoryName == "Beverages").ToList(); //Include ile farklı bir tabloyuda bağlayabilirim
                //foreach (var item in products)
                //{
                //    Console.WriteLine(item.ProductName + " " + item.CategoryId);
                //}

                //var products = db.Products
                //    .Where(p => p.Category.CategoryName == "Beverages")
                //    .Select(p => new
                //    {
                //        name = p.ProductName,
                //        id = p.CategoryId,
                //        categoryname = p.Category.CategoryName
                //    }).ToList();

                //foreach (var product in products)
                //{
                //    Console.WriteLine(product.name + " " + product.id + " " + product.categoryname);
                //}


                //var categories = db.Categories.Where(p => p.Products.Count() == 0).ToList(); //category tablosunda products'u 0 olan varmı, ikinci bir yolu altta
                //var categories = db.Categories.Where(p => p.Products.Any()).ToList(); //Any ile kontrolü yaptı ve true/false döndürdü.
                //foreach (var category in categories)
                //{
                //    Console.WriteLine(category.CategoryName);
                //}

                //var products = db.Products
                //    .Select(p => new
                //    {
                //        companyName = p.Supplier.CompanyName,
                //        contactName = p.Supplier.ContactName, //left join oldu. 
                //        p.ProductName
                //    })
                //    .ToList();
                //foreach (var product in products)
                //{
                //    Console.WriteLine(product.ProductName + " " + product.companyName + " " + product.contactName);
                //}

                var products = (from p in db.Products
                                join s in db.Suppliers on p.SupplierId equals s.SupplierId
                                select new
                                {
                                    p.ProductName,
                                    contactName = s.ContactName,            //inner join oldu.
                                    companyName = s.CompanyName,
                                }).ToList();
                foreach (var product in products)
                {
                    Console.WriteLine(product.ProductName + " " + product.companyName + " " + product.contactName);
                }
            }
            Console.ReadLine();
        }

        private static void Ders9(NorthwindContext db)
        {
            //var p = db.Products.Find(89); //select sorgusu attırdık. select sorgusuzda yapabiliriz. altta örneği var
            //if (p != null)
            //{
            //    db.Products.Remove(p);
            //    db.SaveChanges();
            //}

            //var p = new Product() { ProductId = 87 };
            //// db.Entry(p).State = EntityState.Deleted; //
            //db.Products.Remove(p); //ikiside olur Remove metoduda arkada trackingile çalışıyor.
            //db.SaveChanges();

            //birden fazla kayıt silmek istersek bir Product List'i içine atıp RemoveRange ile siliyoruz.
            var p1 = new Product() { ProductId = 86 };
            var p2 = new Product() { ProductId = 85 };

            var products = new List<Product>() { p1, p2 };
            db.Products.RemoveRange(products);
            db.SaveChanges();
        }

        private static void Ders8(NorthwindContext db)
        {
            var product = db.Products.Find(1);
            if (product != null)
            {
                product.UnitPrice = 28;
                db.Update(product);
                db.SaveChanges();
            }
        }

        private static void Ders7(NorthwindContext db)
        {
            //obje oluşturuyoruz ve change tracking'i aktif et demek için Attach() metodunu kullanabiliriz.
            //ya da ders8'deki gibi db.update metoduyla yaptık. Ama bütün veritabanı propertilerinde sorgu atıyor.
            var p = new Product() { ProductId = 1 };
            db.Products.Attach(p);
            p.UnitsInStock = 50;
            db.SaveChanges();
        }

        private static void Ders6(NorthwindContext db)
        {
            //change tracking
            //tracking özelliğini aktif ettiğimizde bir select sorgusu oluşuyor.
            //select sorgusu göndermek istemiyorsak ders7 ve ders8'deki gibi
            var product = db.Products
                //.AsNoTracking()
                .FirstOrDefault(p => p.ProductId == 1);

            if (product != null)
            {
                product.UnitsInStock += 10;
                db.SaveChanges();
                Console.WriteLine("Veri Güncellendi");
            }
        }

        private static void Ders5(NorthwindContext db)
        {
            //Sıralama ve Hesaplama Sorguları
            //var result=db.Products.Count(); //Product tablosunda kaç veri var
            //var result = db.Products.Count(i => i.UnitPrice > 15 && i.UnitPrice < 25); //Fiyatları 15-25 arası olan kaç tane
            //var result = db.Products.Count(i => !i.Discontinued); //satışta olan ürünleri kaç tane? False olanlar bize satıştaki ürünleri verir.
            //var result = db.Products.Min(p => p.UnitPrice); //minimum fiyat
            //var result = db.Products.Max(p => p.UnitPrice); //maksimum fiyat
            //var result = db.Products.Where(p => p.CategoryId == 1).Max(p => p.UnitPrice); //CategoryId'si 1 olanların Maksimum fiyatta olanını verir.
            //var result = db.Products.Average(p => p.UnitPrice); //ürünlerin ortalama fiyatlarını verir.
            //var result = db.Products.Where(p => !p.Discontinued).Average(p => p.UnitPrice); //Satışta olan ürünlerin fiyat ortalaması
            //var result = db.Products.Where(p => !p.Discontinued).Sum(p => p.UnitPrice); //Satışta olan ürünlerin toplam fiyatı
            //Console.WriteLine(result);

            //var result = db.Products.OrderBy(p => p.UnitPrice).ToList(); //Fiyatı artarak sırala
            //var result = db.Products.OrderByDescending(p => p.UnitPrice).ToList(); //Fiyatı azalarak sırala
            //foreach (var product in result)
            //{
            //    Console.WriteLine(product.ProductName + " " + product.UnitPrice);
            //}

            //var result = db.Products.OrderByDescending(p => p.UnitPrice).FirstOrDefault(); //en parahalı ürünlerin ilk olanı hangisi
            var result = db.Products.OrderByDescending(p => p.UnitPrice).LastOrDefault(); //en ucuz ürünlerin ilk olanı hangisi.
            Console.WriteLine(result.ProductName + " " + result.UnitPrice);
        }

        private static void Ders4(NorthwindContext db)
        {
            //VERİ EKLEME ÖRNEKLERİ BAKINCA ANLAŞILIYOR...

            //var p1 = new Product() { ProductName = "Yeni Ürün 3"};
            //db.Products.Add(p1);
            //db.SaveChanges();


            //var p2 = new Product() { ProductName = "Yeni Ürün 3" };
            //var p3 = new Product() { ProductName = "Yeni Ürün 4" };
            //var products = new List<Product>()
            //{
            //    p2, p3
            //};
            //db.Products.AddRange(products);
            //db.SaveChanges();
            //Console.WriteLine("Veriler Eklendi.");
            //Console.WriteLine();

            //var p2 = new Product() { ProductName = "Yeni Ürün 5", CategoryId = 1 };
            //var p3 = new Product() { ProductName = "Yeni Ürün 6", CategoryId = 1 };
            //var products = new List<Product>()
            //{
            //    p2, p3
            //};
            //db.Products.AddRange(products);
            //db.SaveChanges();

            //var category = db.Categories.Where(i => i.CategoryName == "Beverages").FirstOrDefault();
            //var p2 = new Product() { ProductName = "Yeni Ürün 7", Category = category };
            //var p3 = new Product() { ProductName = "Yeni Ürün 8", Category = category };
            //var products = new List<Product>()
            //{
            //    p2, p3
            //};
            //db.Products.AddRange(products);
            //db.SaveChanges();

            //var category = db.Categories.Where(i => i.CategoryName == "Beverages").FirstOrDefault();
            //var p2 = new Product() { ProductName = "Yeni Ürün 9", Category = new Category() { CategoryName = "Yeni Kategori 1" } };
            //var p3 = new Product() { ProductName = "Yeni Ürün 10", Category = new Category() { CategoryName = "Yeni Kategori 2" } };
            //var products = new List<Product>()
            //{
            //    p2, p3
            //};
            //db.Products.AddRange(products);
            //db.SaveChanges();

            var category = db.Categories.Where(i => i.CategoryName == "Beverages").FirstOrDefault();
            var p2 = new Product() { ProductName = "Yeni Ürün 11" };
            var p3 = new Product() { ProductName = "Yeni Ürün 12" };
            category.Products.Add(p2);
            category.Products.Add(p3);

            db.SaveChanges();
        }

        private static void Ders3(NorthwindContext db)
        {
            //*Tüm Müşteri kayıtlarını getiriniz. (Customers)
            //var customers = db.Customers.ToList();
            //foreach (var customer in customers)
            //{
            //    Console.WriteLine(customer.ContactName);
            //}

            //*Tüm müşterilerin sadece CustomerId ve ContactName kolonlarını getiriniz.
            //var customers = db.Customers.Select(c => new { c.CustomerId, c.ContactName }).ToList();
            //foreach (var customer in customers)
            //{
            //    Console.WriteLine(customer.CustomerId + ' ' + customer.ContactName);
            //}

            //*Almanyada yaşayan müşterilerin adlarını getiriniz
            //var customers = db.Customers.Where(c => c.Country == "Germany").Select(c => new { c.ContactName, c.Country }).ToList();
            //foreach (var customer in customers)
            //{
            //    Console.WriteLine(customer.Country + ' ' + customer.ContactName);
            //}

            //*"Diego Roel" isimli kişi nerede çalışmaktadır?
            //var customer = db.Customers.Where(c => c.ContactName == "Diego Roel").FirstOrDefault();
            //Console.WriteLine(customer.ContactName + " " + customer.CompanyName);

            //*Stokta olmayan ürünler hangileridir?
            //var products = db.Products.Select(p => new { p.ProductName, p.UnitsInStock }).Where(p => p.UnitsInStock == 0).ToList();
            //foreach (var product in products)
            //{
            //    Console.WriteLine(product.ProductName + " " + product.UnitsInStock);
            //}

            //*Tüm çalışanların ad ve doyadını tek kolon şeklinde getiriniz. (Employees)
            //var employees = db.Employees.Select(e => new
            //{
            //    FullName = e.FirstName + " " + e.LastName
            //}).ToList();
            //foreach (var employee in employees)
            //{
            //    Console.WriteLine(employee.FullName);
            //}

            //*Ürünler tablosundaki ilk 5 kaydı alınız. (Products) (Take)
            //var products = db.Products.Take(5).ToList();
            //foreach (var product in products)
            //{
            //    Console.WriteLine(product.ProductId + " " + product.ProductName);
            //}

            //*Ürünler tablosundaki ikinci 5 kaydı alınız.(Skip)
            var products = db.Products.Skip(5).Take(5).ToList();
            foreach (var product in products)
            {
                Console.WriteLine(product.ProductId + " " + product.ProductName);
            }
        }

        private static void Ders2(NorthwindContext db)
        {
            //var products = db.Products.Where(p => p.UnitPrice > 18).ToList();
            //var products = db.Products.Select(p => new { p.ProductName, p.UnitPrice }).Where(p => p.UnitPrice > 18).ToList();
            //var products = db.Products.Where(p => p.UnitPrice > 18 && p.UnitPrice < 30).ToList();
            //var products = db.Products.Where(p => p.CategoryId >= 1 && p.CategoryId <= 5).ToList();
            //var products = db.Products.Where(p => p.CategoryId == 1 || p.CategoryId == 5);
            //var products = db.Products.Where(p => p.CategoryId == 1).Select(p => new { p.ProductName, p.UnitPrice }).ToList();
            //var products = db.Products.Where(i => i.ProductName == "Chai").ToList();
            var products = db.Products.Where(i => i.ProductName.Contains("Ch")).ToList();

            foreach (var product in products)
            {
                Console.WriteLine(product.ProductName + ' ' + product.UnitPrice);
            }
        }

        private static void Ders1(NorthwindContext db)
        {
            // var products = db.Products.ToList(); // 1-tüm Product tablosuna sorgu attım
            // var products = db.Products.Select(p => p.ProductName).ToList(); // 2-seçerek sorgu attım.
            //var products = db.Products.Select(p => new { p.ProductName, p.UnitPrice }).ToList(); // 3-iki ayrı bilgi sorgusu
            //var products = db.Products.Select(p =>
            //new ProductModel                              //  4
            //{                                           //web uygulamalarında kullanmak için geriye bir değer döndürmemiz gerekiyor.
            //    Name = p.ProductName,                   //yukarıya ProductModel isminde class oluşturuyoruz.    
            //    Price = p.UnitPrice                     //console'da sütuna takma isim verdiğimizde görünüyor AS [Name] gibi
            //}).ToList();
            var products = db.Products.First(); // 5-tablodan gelen ilk veriyi bu şekilde alabiliriz.
            Console.WriteLine(products.ProductName + ' ' + products.UnitPrice);// 5

            //foreach (var item in products)
            //{
            //    //Console.WriteLine(item.ProductName); //1
            //    //Console.WriteLine(item); //2
            //    //Console.WriteLine(item.ProductName + ' ' + item.UnitPrice); //3

            //    //Console.WriteLine(item.Name + ' ' + item.Price); //4
            //}
        }
    }
}
