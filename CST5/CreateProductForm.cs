using CST5.BusinessLayer;
using CST5.BusinessLayer.Operations;
using CST5.DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CST5
{
    public partial class CreateProductForm : Form
    {
        public CreateProductForm()
        {
            InitializeComponent();
            
            //combo
            var getCategories = new OpGetCategories();
            var categories = getCategories.Execute().Data as IEnumerable<CategoryDto>;

            var getSuppliers = new OpGetSuppliers();
            var suppliers = getSuppliers.Execute().Data as IEnumerable<SupplierDto>;

            this.ddlCategories.DisplayMember = "Name";
            this.ddlCategories.ValueMember = "Id";
            this.ddlCategories.DataSource = categories;

            this.ddlSuppliers.DisplayMember = "Name";
            this.ddlSuppliers.ValueMember = "Id";
            this.ddlSuppliers.DataSource = suppliers;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //zatvara formu
            this.Dispose();
        }

        private void CreateProductForm_Load(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var productName = this.tbProductName.Text;
            var price = this.numPrice.Value;
            var quantity = this.numQuantity.Value;

            var categoryId = (this.ddlCategories.SelectedItem as CategoryDto).Id;
            var supplierId = (this.ddlSuppliers.SelectedItem as SupplierDto).Id;

            if (string.IsNullOrEmpty(productName))
            {
                MessageBox.Show("Ime je obavezno uneti");
                return;
            }
            if (quantity < 0)
            {
                MessageBox.Show("Kolicina mora biti veca ili jednaka 0");
            }
            if(price < 1)
            {
                MessageBox.Show("Cena ne sme biti manja od 1");
            }

            var createProductDto = new CreateProductDto
            {
                ProductName = productName,
                Price = price,
                Quantity = (int)quantity,
                CategoryId = categoryId,
                SupplierId = supplierId
            };

            var createProduct = new OpCreateProduct(createProductDto);

            var result = createProduct.Execute();
            if (!result.IsSuccess)
            {
                MessageBox.Show(result.ErrorMessages.First());
            }
            else
            {
                MessageBox.Show("Uspesno dodato");
                this.Dispose();

                
            }
        }
    }
}
