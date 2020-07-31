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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.productsListView.Columns.Add("ID");
            this.productsListView.Columns.Add("Name");
            this.productsListView.Columns.Add("Category Name");
            this.productsListView.Columns.Add("Been in order");
            this.productsListView.Columns.Add("Total Money Made");
            this.productsListView.Columns.Add("Unit Price");


           PopulateListView();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
        private void PopulateListView(string keyword = null)
        {
            var products = GetProducts(keyword); //niz products sad tu imamo

            productsListView.Items.Clear();
            productsListView.BeginUpdate();
            foreach (var product in products)
            {
                var listViewItem = new ListViewItem();

                listViewItem.Tag = product.Id; //za svaki red cuva Id

                listViewItem.Text = product.Id.ToString(); //1.u itemu ide .Text
                listViewItem.SubItems.Add(product.Name);
                listViewItem.SubItems.Add(product.CategoryName);
                listViewItem.SubItems.Add(product.TimesBeenInOrder.ToString());
                listViewItem.SubItems.Add(product.TotalMoneyMade.ToString());
                listViewItem.SubItems.Add(product.UnitPrice.ToString());
                productsListView.FullRowSelect = true;

                this.productsListView.Items.Add(listViewItem);            
            }
            productsListView.EndUpdate();
        }
        private IEnumerable<ProductDto> GetProducts(string keyword = null)
        {
            var operation = new OpGetProducts(new OpProductSearchCriteria
            {
                Name = keyword
            }); //opciono zbog filtera

            var result = operation.Execute();

            return result.Data as IEnumerable<ProductDto>;
        }

        private void productsListView_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            var createProductForm = new CreateProductForm();
            createProductForm.Show();

            //Delegat
            createProductForm.Disposed += new EventHandler(AddProduct_Disposed);
        }

        //za refresh
        private void AddProduct_Disposed(object sender, EventArgs e)
        {
            PopulateListView();
        }

        private void tbInput_KeyUp(object sender, KeyEventArgs e)
        {
            var input = this.tbInput.Text.Trim().ToLower();
            PopulateListView(input);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var items = this.productsListView.SelectedItems;

            if (items.Count == 0)
            {
                MessageBox.Show("Niste izabrali stavku za brisanje");
                return;
            }

            var idsToDelete = new List<int>();
            foreach (var item in items)
            {
                //ispituje i castuje
                if (item is ListViewItem itemTemp)
                {
                    var id = (int)itemTemp.Tag;
                    idsToDelete.Add(id);
                }
            }

            try
            {
                var operation = new OpDeleteProductBatch(idsToDelete);
                var result = operation.Execute();
                if (result.IsSuccess)
                {
                    MessageBox.Show("Uspesno ste obrisali proizvode");
                    PopulateListView();
                }
                else
                {
                    MessageBox.Show("Neuspelo brisanje");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Doslo je do greske");
            }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(this.productsListView.SelectedItems.Count != 1)
            {
                MessageBox.Show("Morate izabrati samo 1 proizvod za izmenu");
                return;
            }


            //update odavde
            var item = this.productsListView.SelectedItems[0];

            var id = (int)item.Tag;

            var getProduct = new OpGetProduct(id);
            //vraca ProductDto i onda cast mora
            var product = getProduct.Execute().Data.First() as CreateProductDto;

            var editForm = new EditProductForm(product, id);
            editForm.Show();

            editForm.Disposed += new EventHandler(AddProduct_Disposed);
        
        }
    }
}

