using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace Haag_CourseProject_Part2
{
    public partial class MainForm : Form
    {
        // class level reference
        private const string FILENAME = "Employee.dat";
        public MainForm()
        {
            InitializeComponent();
            ReadEmpsFromFile();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            // add item to the employee listbox
            InputForm frmInput = new InputForm();

            using (frmInput)
            {
                DialogResult result = frmInput.ShowDialog();

                // see if input form was cancelled
                if (result == DialogResult.Cancel)
                    return;    //  end the method since the user cancelled the input

                // get user's input and create an Employee object
                string fName = frmInput.FirstNameTextBox.Text;
                string lName = frmInput.LastNameTextBox.Text;
                string ssn = frmInput.SSNTextBox.Text;
                string date = frmInput.HireDateTextBox.Text;
                DateTime hireDate = DateTime.Parse(date);
                string healthIns = frmInput.HealthInsTextBox.Text;
                int lifeIns = int.Parse(frmInput.LifeInsTextBox.Text);
                int vacation = Int32.Parse(frmInput.VacationTextBox.Text);

                Benefits benefits = new Benefits(healthIns, lifeIns, vacation);
                Employee emp = null;  // empty reference

                if (frmInput.SalaryRadioButton.Checked)
                {
                    double salary = double.Parse(frmInput.SalaryTextBox.Text);
                    emp = new Salary(fName, lName, ssn, hireDate, benefits, salary);
                }
                else if (frmInput.HourlyRadioButton.Checked)
                {
                    double hourlyRate = double.Parse(frmInput.HourlyRateTextBox.Text);
                    double hoursWorked = double.Parse(frmInput.HoursWorkedTextBox.Text);
                    emp = new Hourly(fName, lName, ssn, hireDate, benefits, hourlyRate, hoursWorked);
                }
                else
                {
                    MessageBox.Show("Error. Please select an Employee Type.");
                    return;
                }

                // add the Employee object to the employees listbox
                EmployeesListBox.Items.Add(emp);
                WriteEmpsToFile();
            }
        }

        private void WriteEmpsToFile()
        {
            
            // convert the ListBox items to a generic list 
            List<Employee> empList = new List<Employee>();

            foreach (Employee emp in EmployeesListBox.Items)
            {
                empList.Add(emp);
            }

            // open a pipe to the file and create a translator
            FileStream fs = new FileStream(FILENAME, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();

            // write the generic list to the file
            formatter.Serialize(fs, empList);

            // close the pipe
            fs.Close();


        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            // remove the selected item from the employee listbox
            int itemNumber = EmployeesListBox.SelectedIndex;

            if (itemNumber > -1)
            {
                EmployeesListBox.Items.RemoveAt(itemNumber);
                WriteEmpsToFile();

            }
            else
            {
                MessageBox.Show("Please select employee to remove.");
            }


        }

        private void DisplayButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Displaying all employees...");
            ReadEmpsFromFile();
        }

        private void ReadEmpsFromFile()
        {
            EmployeesListBox.Items.Clear();

            // check to see if file exists
            if (File.Exists(FILENAME) && new FileInfo(FILENAME).Length > 0)
            {
                // create a pipe from the file and create the "translator"
                FileStream fs = new FileStream(FILENAME, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();

                // read all Employee objects from the file
                List<Employee> list = (List<Employee>)formatter.Deserialize(fs);

                // close the pipe
                fs.Close();

                // clear ListBox items and copy the file’s Employee objects into our listbox
                EmployeesListBox.Items.Clear();
                foreach (Employee emp in list)
                    EmployeesListBox.Items.Add(emp);
            }

        }

        private void PrintPaychecksButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Printing paychecks for all employees...");
            foreach (Employee emp in EmployeesListBox.Items)
            {
                string line1 = "Pay To:" + emp.FirstName + " " + emp.LastName;
                string line2 = "Amount Of: " + emp.CalculatePay().ToString("C2");

                string output = "Paycheck:\n\n" + line1 + "\n" + line2;

                MessageBox.Show(output);
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void EmployeesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void EmployeesListBox_DoubleClick(object sender, EventArgs e)
        {
            Employee emp = (Employee)EmployeesListBox.SelectedItem;    
            if (emp != null) 
            {
                InputForm updateForm = new InputForm();
                
                updateForm.Text = "Update Employee Information";
                updateForm.SubmitButton.Text = "Update";
                updateForm.StartPosition = FormStartPosition.CenterParent;
                updateForm.FirstNameTextBox.Text = emp.FirstName;
                updateForm.LastNameTextBox.Text = emp.LastName;
                updateForm.SSNTextBox.Text = emp.SSN;
                updateForm.HireDateTextBox.Text = emp.HireDate.ToShortDateString();
                updateForm.HealthInsTextBox.Text=emp.BenefitsEmp.HealthInsurance;
                updateForm.LifeInsTextBox.Text = emp.BenefitsEmp.LifeInsurance.ToString();
                updateForm.VacationTextBox.Text = emp.BenefitsEmp.Vacation.ToString();

                // check if hourly or salary
                if (emp is Salary)
                {
                    updateForm.HourlyRateLabel.Visible = false;
                    updateForm.HourlyRateTextBox.Visible = false;
                    updateForm.HoursWorkedLabel.Visible = false;
                    updateForm.HoursWorkedTextBox.Visible = false;
                    updateForm.SalaryLabel.Visible = true;
                    updateForm.SalaryTextBox.Visible = true;

                    updateForm.SalaryRadioButton.Checked = true;

                    // convert employee to salary object
                    Salary sal= (Salary)emp;

                    // show salary information
                    updateForm.SalaryTextBox.Text = sal.AnnualSalary.ToString("F2");
                  
                }
                else if (emp is Hourly)
                {
                    updateForm.HourlyRateLabel.Visible = true;
                    updateForm.HourlyRateTextBox.Visible = true;
                    updateForm.HoursWorkedLabel.Visible = true;
                    updateForm.HoursWorkedTextBox.Visible = true;
                    updateForm.SalaryLabel.Visible = false;
                    updateForm.SalaryTextBox.Visible = false;

                    updateForm.HourlyRadioButton.Checked = true;

                    // convert employee to hourly object
                    Hourly hrly = (Hourly)emp;

                    // show salary information
                    updateForm.HourlyRateTextBox.Text = hrly.HourlyRate.ToString("F2");
                    updateForm.HoursWorkedTextBox.Text = hrly.HoursWorked.ToString("F2");
                }
                else
                {
                    MessageBox.Show("Error.  Invalid employee type found.");
                    return;
                }

                DialogResult result = updateForm.ShowDialog();

                // stop if canceled
                if (result == DialogResult.Cancel)
                    return;

                // delete object selected
                int position = EmployeesListBox.SelectedIndex;
                EmployeesListBox.Items.RemoveAt(position);

                // create the new employee using update
                Employee newEmp = null;

                string fName = updateForm.FirstNameTextBox.Text;
                string lName = updateForm.LastNameTextBox.Text;
                string ssn = updateForm.SSNTextBox.Text;
                DateTime hireDate = DateTime.Parse(updateForm.HireDateTextBox.Text);
                string healthInsurance = updateForm.HealthInsTextBox.Text;
                int lifeInsurance = int.Parse(updateForm.LifeInsTextBox.Text);
                int vacation = int.Parse(updateForm.VacationTextBox.Text);
                Benefits benefits = new Benefits(healthInsurance, lifeInsurance, vacation);
                if (updateForm.SalaryRadioButton.Checked)
                {
                    double salary = double.Parse(updateForm.SalaryTextBox.Text);
                    newEmp = new Salary(fName, lName, ssn, hireDate, benefits, salary);

                }
                else if (updateForm.HourlyRadioButton.Checked)
                {
                    double hourlyRate = double.Parse(updateForm.HourlyRateTextBox.Text);
                    double hoursWorked = double.Parse(updateForm.HoursWorkedTextBox.Text);
                    newEmp = new Hourly(fName, lName, ssn, hireDate, benefits, hourlyRate, hoursWorked);
                }
                else
                {
                    MessageBox.Show("Error.  Invalid employee type.");
                    return;

                }

                // add new employee to listbox
                EmployeesListBox.Items.Add(newEmp);

            }
        }
    }
}
