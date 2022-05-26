using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactBook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView.ReadOnly = true;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    contactBookBindingSource.RemoveCurrent();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            panel1.Enabled = true;
            txtTelephoneNumber.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            contactBookBindingSource.ResetBindings(false);
            panel1.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                contactBookBindingSource.EndEdit();
                App.ContactBook.AcceptChanges();
                App.ContactBook.WriteXml(string.Format("{0}//data.dat", Application.StartupPath));
                panel1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                App.ContactBook.RejectChanges();
            }
        }

        static AppData db;
        protected static AppData App
        {
            get
            {
                if (db == null)
                    db = new AppData();
                return db;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string fileName = string.Format("{0}//data.dat", Application.StartupPath);
            if (File.Exists(fileName))
                App.ContactBook.ReadXml(fileName);
            contactBookBindingSource.DataSource = App.ContactBook;
            panel1.Enabled = false;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                panel1.Enabled = true;
                App.ContactBook.AddContactBookRow(App.ContactBook.NewContactBookRow());
                contactBookBindingSource.MoveLast();
                txtTelephoneNumber.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                App.ContactBook.RejectChanges();
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (!string.IsNullOrEmpty(textBox6.Text))
                {
                    var query = from o in App.ContactBook
                                where o.TelephoneNumber == textBox6.Text || o.Name.ToLowerInvariant().Contains(textBox6.Text.ToLowerInvariant()) || o.Email.ToLowerInvariant() == textBox6.Text.ToLowerInvariant()
                                select o;
                    dataGridView.DataSource = query.ToList();
                }
                else
                    dataGridView.DataSource = contactBookBindingSource;
            }
        }
    }
}



// Name = Selim Can
// Surname = YILDIZ
// ID =  37044


// Thank you for the semester, Sir. This is my project for .NET 





