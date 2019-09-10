using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnrolleeForms
{
    public partial class MinutelyTrainingDirection : Form
    {
         TrainingDirection trainingDirection;

        internal MinutelyTrainingDirection(TrainingDirection trainingDirection)
        {
            InitializeComponent();
            this.trainingDirection = trainingDirection;

            DisplayInformation();


        }

        private void DisplayInformation()
        {
            if (trainingDirection != null)
            {

                label2.Text = trainingDirection.Specialty_.FullName;
                label5.Text = trainingDirection.Specialty_.ShortName;

                label6.Text = trainingDirection.Specialty_.CodeSpecialty;
                label9.Text = trainingDirection.Specialty_.CodeSpecialization;

                label10.Text = trainingDirection.FormStudy_.Name;
                label13.Text = trainingDirection.TrainingPeriod_.Name;


                label14.Text = trainingDirection.Specialty_.Faculty.FullName;
                label21.Text = trainingDirection.Specialty_.Department.FullName;

                label22.Text = trainingDirection.BudgetOrCharge_.Name;

                label23.Text = trainingDirection.FirstEntranceTest.Name;
                label24.Text = trainingDirection.SecondEntranceTest.Name;
                label25.Text = trainingDirection.AdmissionPlan.ToString();


                if (trainingDirection.ThirdEntranceTest != null)
                    label26.Text = trainingDirection.ThirdEntranceTest.Name;
                else
                    label26.Text = "Отсутствует";
            }

        }


        private void AddEnrollee_Load(object sender, EventArgs e)
        {
          


        
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(trainingDirection.Specialty_.Faculty.Info());
        }

        private void label28_Click(object sender, EventArgs e)
        {
             MessageBox.Show(trainingDirection.Specialty_.Department.Info());
        }
    }
}
