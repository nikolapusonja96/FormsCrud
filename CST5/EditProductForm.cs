using CST5.BusinessLayer;
using CST5.BusinessLayer.Operations;
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
    public partial class EditProductForm : Form
    {
        private CreateProductDto _dto;
        private int id;
        public EditProductForm(CreateProductDto dto, int id)
        {
            InitializeComponent();
            this._dto = dto;
            this.id = id;

            this.tbName.Text = dto.ProductName;
            this.price.Value = dto.Price;
            this.quantity.Value = dto.Quantity;

            //combo
            var getCategories = new OpGetCategories();
            var categories = getCategories.Execute().Data as IEnumerable<CategoryDto>;

            var getSuppliers = new OpGetSuppliers();
            var suppliers = getSuppliers.Execute().Data as IEnumerable<SupplierDto>;

            this.ddlSupplier.DisplayMember = "Name";
            this.ddlSupplier.ValueMember = "Id";
            this.ddlSupplier.DataSource = suppliers;

            this.ddlCategory.DisplayMember = "Name";
            this.ddlCategory.ValueMember = "Id";
            this.ddlCategory.DataSource = categories;

            //za combo selektovani da se prikaze

            var selectedSupplier = suppliers.SingleOrDefault(s => s.Id == this._dto.SupplierId);
            this.ddlSupplier.SelectedItem = selectedSupplier;

            var selectedCategory = categories.SingleOrDefault(c => c.Id == this._dto.CategoryId);
            this.ddlCategory.SelectedItem = selectedCategory;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void EditProductDto_Load(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var newName = this.tbName.Text;
            var newPrice = this.price.Value;
            var newQuantity = (int)this.quantity.Value;
            var newCategoryId = (this.ddlCategory.SelectedItem as CategoryDto).Id;
            var newSupplierId = (this.ddlSupplier.SelectedItem as SupplierDto).Id;

            var editProduct = new OpEditProduct(new CreateProductDto
            {
                CategoryId = newCategoryId,
                ProductName=newName,
                Price=newPrice,
                Quantity=newQuantity,
                SupplierId=newSupplierId
            }, this.id); //this.id zato sto smo ga prosledili kroz konstruktor pri kreiranju forme

            try
            {
                editProduct.Execute();
                this.Dispose();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
