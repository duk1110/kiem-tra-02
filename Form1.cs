using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinFormsApp4
{
    public partial class Form1 : Form
    {
        private List<Product> productList;
        private ShoppingCart shoppingCart;
        public Form1()
        {
            InitializeComponent();
            InitializeProductList();
            shoppingCart = new ShoppingCart();
            this.Load += new System.EventHandler(this.Form1_Load);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("Ảnh sản phẩm", "Ảnh sản phẩm");
            dataGridView1.Columns.Add("Tên sản phẩm", "Tên sản phẩm");
            dataGridView1.Columns.Add("Giá", "Giá");
            dataGridView1.Columns.Add("Số lượng", "Số lượng");

            dataGridView2.Columns.Add("Tên sản phẩm", "Tên sản phẩm");
            dataGridView2.Columns.Add("Giá", "Giá");
            dataGridView2.Columns.Add("Số lượng", "Số lượng");

            foreach (var product in productList)
            {
                dataGridView1.Rows.Add(product.Image, product.Name, product.Price, product.Quantity);
            }
        }

        private void InitializeProductList()
        {
            productList = new List<Product>
            {
                new Product("image1.jpg", "Máy tính", 100, 10),
                new Product("image2.jpg", "Tủ lạnh", 200, 5),
                new Product("image3.jpg", "Điều hòa", 150, 8)
            };
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    string productName = row.Cells["Tên sản phẩm"].Value.ToString();
                    double productPrice = Convert.ToDouble(row.Cells["Giá"].Value);
                    int productQuantity = Convert.ToInt32(row.Cells["Số lượng"].Value);
                    Product product = new Product("", productName, productPrice, productQuantity);
                    shoppingCart.AddProduct(product);
                    dataGridView2.Rows.Add(productName, productPrice, productQuantity);
                }
                UpdateCartSummary();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                {
                    string productName = row.Cells["Tên sản phẩm"].Value.ToString();
                    Product productToRemove = shoppingCart.Products.Find(p => p.Name == productName);
                    shoppingCart.RemoveProduct(productToRemove);
                    dataGridView2.Rows.Remove(row);
                }
                UpdateCartSummary();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double totalAmount = shoppingCart.GetTotalAmount();
            MessageBox.Show("Thành tiền: " + totalAmount);
            shoppingCart.ClearCart();
            dataGridView2.Rows.Clear();
            UpdateCartSummary();
        }

        private void UpdateCartSummary()
        {
            int totalQuantity = shoppingCart.GetTotalQuantity();
            double totalAmount = shoppingCart.GetTotalAmount();
        }

        public class Product
        {
            public string Image { get; set; }
            public string Name { get; set; }
            public double Price { get; set; }
            public int Quantity { get; set; }

            public Product(string image, string name, double price, int quantity)
            {
                Image = image;
                Name = name;
                Price = price;
                Quantity = quantity;
            }
        }

        public class ShoppingCart
        {
            public List<Product> Products { get; private set; }

            public ShoppingCart()
            {
                Products = new List<Product>();
            }

            public void AddProduct(Product product)
            {
                Products.Add(product);
            }

            public void RemoveProduct(Product product)
            {
                Products.Remove(product);
            }

            public double GetTotalAmount()
            {
                double total = 0;
                foreach (var product in Products)
                {
                    total += product.Price * product.Quantity;
                }
                return total;
            }

            public int GetTotalQuantity()
            {
                int totalQuantity = 0;
                foreach (var product in Products)
                {
                    totalQuantity += product.Quantity;
                }
                return totalQuantity;
            }

            public void ClearCart()
            {
                Products.Clear();
            }
        }
    }
}
