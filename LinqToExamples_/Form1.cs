using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqToExamples_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        NorthwindEntities db = new NorthwindEntities();


        private void btnSorgu1_Click(object sender, EventArgs e)
        {
            // Stok miktarı 20 ile 45 arasında olan ürünleri çoktan aza sıralayın..?

            dataGridView1.DataSource = db.Products.Where(x => x.UnitsInStock >= 20 && x.UnitsInStock <= 45).OrderByDescending(x => x.UnitsInStock).Select(x => new
            {
                x.ProductID,
                x.ProductName,
                x.UnitPrice,
                x.UnitsInStock
            }).ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Stock miktarı 20 ile 40 arasında olan ürünlerin fiyatlarını çoktan aza sıralayınız ve en yüksek fiyata sahip 10 ürünü getiriniz..?

            dataGridView1.DataSource = db.Products.Where(x => x.UnitsInStock > 20 && x.UnitsInStock < 40).OrderByDescending(x => x.UnitPrice).Take(10).Select(x => new {
                x.Category.CategoryName,
                x.ProductName,
                x.UnitPrice,
                x.UnitsInStock
            }).ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // İlk harfi A veya L olan çalışanşarın listesi..?

            dataGridView1.DataSource = db.Employees.Where(x => x.FirstName.StartsWith("A") && x.FirstName.StartsWith("L")).ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Müşterilerin müştreri adı, yetkilisi, telefonu ve adresini farklı kolon isimleriyle getirin..?

            dataGridView1.DataSource = db.Customers.Select(x => new
            {
                Musteri_Sirketi = x.CompanyName,
                Yetkili = x.ContactName,
                Telefon = x.Phone,
                Adres = x.Address

            }).ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Çalışanların yaşı 60tan büyük olanların Adı, Soyadını,Ünvanını, DoğumTarihini getirin..

            dataGridView1.DataSource = db.Employees.Where(x => SqlFunctions.DateDiff("Year", x.BirthDate, DateTime.Now) > 60).Select(x => new
            {
                x.FirstName,
                x.LastName,
                x.Title,
                x.BirthDate
            }).ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Çalışanların ID'si 1 ile 10 arasında olan çalışanları A-Z'ye olacak çekilde isimlerine göre sıralayınız..?

            dataGridView1.DataSource = db.Employees.Where(x => x.EmployeeID >= 1 && x.EmployeeID <= 10).OrderBy(x => x.FirstName).ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Ünvanı mr veya dr olanları listeleyin..?

            dataGridView1.DataSource = db.Employees.Where(x => x.TitleOfCourtesy == "Mr." || x.TitleOfCourtesy == "Dr.").ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //Doğum TArihi 1930 ile 1960 arasında lup USA'de çalışanları listeleyin..?

            dataGridView1.DataSource = db.Employees.Where(x => SqlFunctions.DatePart("Year", x.BirthDate) >= 1930 && SqlFunctions.DatePart("Year", x.BirthDate) <= 1960 && x.Country == "USA").ToList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Çalışanalrın firstname,lastname,titleofcourtesy ve age ekrana getirilsin, yaşa göre azalan şekilde sırlayın..?
            dataGridView1.DataSource = db.Employees.OrderByDescending(x => SqlFunctions.DateDiff("Year", x.BirthDate, DateTime.Now)).Select(x => new {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Title = x.Title,
                Age = SqlFunctions.DateDiff("Year", x.BirthDate, DateTime.Now)
            }).ToList();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Çalışanların yaşlarını hesaplayın..?

            dataGridView1.DataSource = db.Employees.Select(x => new
            {
                Adi = x.FirstName,
                Soyadi = x.LastName,
                DogumTarihi = x.BirthDate,
                Yasi = SqlFunctions.DateDiff("Year", x.BirthDate, DateTime.Now)
            }).ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //Hangi kategoride kaç adet ürünüm var...?

            dataGridView1.DataSource = db.Products.GroupBy(x => x.Category.CategoryName).Select(y => new
            {
                KategoriAdi = y.Key,
                ToplamStok = y.Sum(x => x.UnitsInStock)
            }).ToList();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //Kategoriler tablosunda Becearges isimli bir kategori var mı yok mu..?

            bool sonuc = db.Categories.Any(x => x.CategoryName == "Becerages");

            MessageBox.Show(sonuc.ToString());
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //En pahalı ürün siyatı nedir?

            decimal? YuksekFiyat = db.Products.Max(x => x.UnitPrice);
            MessageBox.Show(YuksekFiyat.ToString());

        }

        private void button13_Click(object sender, EventArgs e)
        {

            //En ucuz ürün fiyatı?

            decimal? DusukFiyat = db.Products.Min(x => x.UnitPrice);
            MessageBox.Show(DusukFiyat.ToString());
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //CampanyName içeriisnde restaurant  kelimesi geçen müşterilerimi listele..?


            dataGridView1.DataSource = db.Customers.Where(x => x.CompanyName.Contains("restaurant")).ToList();
        }
    }
}
