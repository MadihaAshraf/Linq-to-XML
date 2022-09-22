using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Linq_to_XML
{
    public partial class Product : System.Web.UI.Page
    {
        XDocument xDocument;
        protected void Page_Load(object sender, EventArgs e)
        {
            xDocument = XDocument.Load(Server.MapPath("product.xml"));
            LoadGridView();
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {

            XElement Pr = xDocument.Descendants("Product").FirstOrDefault(x => x.Attribute("Id").Value.Equals(txtId.Text));
            if (Pr == null)
            {
                xDocument.Element("Products").Add(
                    new XElement("Product",
                        new XAttribute("Id", txtId.Text),
                        new XElement("name", txtName.Text),
                        new XElement("price", txtPrice.Text),
                        new XElement("description", txtdesc.Text),
                        new XElement("stock", txtStock.Text)
                        
                        )
                    );
                xDocument.Save(Server.MapPath("product.xml"));
                lblInfo.Text = "Employee data added!";
                LoadGridView();
            }
            else
            {
                lblInfo.Text = "Employee Id Already exists!";
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            XElement pr = xDocument.Descendants("Product").FirstOrDefault(x => x.Attribute("Id").Value.Equals(txtId.Text));
            pr.Element("name").Value = txtName.Text;
            pr.Element("price").Value = txtPrice.Text;
            pr.Element("description").Value = txtdesc.Text;
            pr.Element("stock").Value = txtStock.Text;
           
            xDocument.Save(Server.MapPath("product.xml"));
            LoadGridView();
            lblInfo.Text = " Data Updated!";
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

            XElement pr = xDocument.Descendants("Product").FirstOrDefault(x => x.Attribute("Id").Value.Equals(txtId.Text));
            if (pr != null)
            {
                pr.Remove();
                xDocument.Save(Server.MapPath("product.xml"));
                LoadGridView();
                lblInfo.Text = "Employee Data Deleted!";
            }
            else
            {
                lblInfo.Text = "Employee Not Found!";
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            XElement pr = xDocument.Descendants("Product").FirstOrDefault(x => x.Attribute("Id").Value.Equals(txtId.Text));
            if (pr != null)
            {
                txtName.Text = pr.Element("name").Value;
                txtPrice.Text = pr.Element("price").Value;
                txtStock.Text = pr.Element("stock").Value;
                txtdesc.Text = pr.Element("description").Value;
                
                lblInfo.Text = "Employee Record Found!";
            }
            else
            {
                lblInfo.Text = "Employee Record Not Found!";
            }
        }

        protected void dataGridView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void LoadGridView()
        {
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(Server.MapPath("product.xml"));
            if (dataSet.Tables.Count > 0)
            {
                dataGridView.DataSource = dataSet;
                dataGridView.DataBind();
            }
        }
    }
    


}