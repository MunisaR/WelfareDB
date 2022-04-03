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
    /*//////////////////////////////////////////////*/
    //PaymentEditForm
    public partial class PaymentEditForm : Form
    {
        public PaymentEditForm()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void PaymentEditForm_Load(object sender, EventArgs e)
        {

        }
        public Payment Payment { get; set; }


        public void LoadData()
        {
            dgv.DataMember = "";
            dgv.DataSource = null;
            dgv.DataSource = new WorkerList().Search(nudRegion.Value.ToString(), ByAttribute.Region);
        }

        public FormMode Mode { get; set; }

        public void CreateNewPayment()
        {
            Mode = FormMode.CreateNew;
            Payment = new Payment();
          //  InitializeControls();
            MdiParent = MyForms.GetForm<ParentForm>();
            Show();
        }
        public void UpdatePayment(Payment payment)
        {
            Mode = FormMode.Update;
            Payment = payment;
         //  InitializeControls();
            ShowPaymentInControls();
            MdiParent = MyForms.GetForm<ParentForm>();
            Show();
        }
       // private void InitializeControls()
       // {
       // }


       //including controls in PaymentForm
        private void ShowPaymentInControls()
        {
            dtpDate.Value = Payment.Date;
            nudRegion.Value = Payment.Region;
            nudLimit.Value = Convert.ToDecimal(Payment.Limit);
        }
        private void GrabUserInput()
        {
            Payment.Date = dtpDate.Value;
            Payment.Region = Convert.ToInt32(nudRegion.Value);
            Payment.Limit = (float)nudLimit.Value;
        }
        //function is used when the button Load Data is clicked  
        private void btnLoad_Click(object sender, EventArgs e)
        {
            var workers = new WorkerManager().GetAll().Where(t => t.Region == nudRegion.Value).ToList();
            dgv.DataSource = workers;
            decimal All_Payments = 0;

            


            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                All_Payments += Convert.ToDecimal(dgv.Rows[i].Cells[2].Value);
            }
            //MessageBox.Show(Convert.ToString(All_Payments));
            decimal HoursPaid = 0;

            bool workerExist = false;
            if (dgv.Rows.Count > 0)
            {
                workerExist = true;
            }
            else
            {
                workerExist = false;
            }

            if (workerExist)
            {
                var ratio = nudLimit.Value / Convert.ToDecimal(All_Payments);

                for (int i = 0; i < dgv.Rows.Count; i++)
                {

                    HoursPaid = Convert.ToDecimal(ratio * Convert.ToDecimal(dgv.Rows[i].Cells[2].Value));
                    if (HoursPaid > Convert.ToDecimal(dgv.Rows[i].Cells[2].Value))
                    {
                        dgv.Rows[i].Cells[3].Value = Convert.ToDecimal(dgv.Rows[i].Cells[2].Value);
                    }
                    else
                    {
                        dgv.Rows[i].Cells[3].Value = Math.Round(Convert.ToDecimal(dgv.Rows[i].Cells[2].Value), 2) * Convert.ToDecimal(dgv.Rows[i].Cells[5].Value);
                    }
                }
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    decimal ExtraHours = Convert.ToDecimal(Convert.ToDecimal(dgv.Rows[i].Cells[2].Value) - Convert.ToDecimal(HoursPaid));
                    if (ExtraHours < 0)
                    {
                        dgv.Rows[i].Cells[4].Value = 0;
                    }
                    else
                    {
                        dgv.Rows[i].Cells[4].Value = Math.Round(ExtraHours, 2);
                    }

                }
            }
            else
            {
                MessageBox.Show("Data does not exist");
            }
            



            lblLimit.Text = Convert.ToString(All_Payments);

        }

        //Cancel Button
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblLimit_Click(object sender, EventArgs e)
        {
           
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        // function that works when the Save Button is clicked
        
       // public void InsertDataTo_Payment()
        //{
        //}

        private void nudRegion_ValueChanged(object sender, EventArgs e)
        {
            
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            

        }



    }
}

