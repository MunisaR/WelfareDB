using _00012072.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _00012072
{
    public partial class WorkerListForm : Form
    {
        public WorkerListForm()
        {
            InitializeComponent();
        }

        private void WorkerListForm_Load(object sender, EventArgs e)
        {
            MdiParent = MyForms.GetForm<ParentForm>();
            LoadData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()

        {

            dgv.DataMember = "";

            dgv.DataSource = null;

            dgv.DataSource = new WorkerList().GetAllWorkers();

        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cmbSearch.SelectedIndex < 0)

                MessageBox.Show("Select an attribute to search by");

            else if (string.IsNullOrWhiteSpace(tbxSearch.Text))

                MessageBox.Show("Provide the search term");

            else

            {

                var selectedAttribute = cmbSearch.SelectedIndex == 0 ? ByAttribute.Name : ByAttribute.Name;

                dgv.DataMember = "";

                dgv.DataSource = null;

                dgv.DataSource = new WorkerList().Search(tbxSearch.Text, selectedAttribute);

            }

        }

        private void btnSort_Click(object sender, EventArgs e, ByAttribute selectedAttribute)
        {
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            new WorkerEditForm().CreateNewWorker();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0)
                MessageBox.Show("Please select a worker");
            else
            {
                var c = (Worker)dgv.SelectedRows[0].DataBoundItem;
                new WorkerEditForm().UpdateWorker(c);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0)
                MessageBox.Show("Please select a course");
            else
            {
                if (MessageBox.Show("Do you want to delete your data?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var c = (Worker)dgv.SelectedRows[0].DataBoundItem;
                    new WorkerManager().Delete(c.Id);
                    LoadData();
                }
            }
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            if (cmbSort.SelectedIndex < 0)
                MessageBox.Show("Select an attribute to sort by");
            else
            {
                dgv.DataMember = "";
                dgv.DataSource = null;
                dgv.DataSource = new WorkerList().Sort(ByAttribute.Hours);
            }
        }
    }
}
            
        
    

