using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SalesApplication
{
    /// <summary>
    /// Represents a form that enables the user to view customers.
    /// </summary>
    public partial class ViewCustomersForm : Form
    {
        // The list of customers displayed by this form.
        // TODO [Ex1]: Modify the SortedList to use CustomerKey as the key type.
        private SortedList<CustomerKey, Customer> _customers = new SortedList<CustomerKey, Customer>();


        /// <summary>
        /// Initializes the form object.
        /// </summary>
        public ViewCustomersForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes the appearance of the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewCustomersForm_Load(object sender, EventArgs e)
        {
            DisplayCustomersList();
        }

        /// <summary>
        /// Iterates through the list of customers,
        /// and adds each customer to the ListBox.
        /// </summary>
        private void DisplayCustomersList()
        {
            customersListBox.Items.Clear();
            foreach (Customer customer in _customers.Values)
            {
                customersListBox.Items.Add(customer);
            }
        }

        /// <summary>
        /// Handles selection changes in the ListBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Customer selectedCustomer = (Customer)customersListBox.SelectedItem;
            DisplayCustomerDetails(selectedCustomer);
        }

        /// <summary>
        /// Displays details for a particular customer.
        /// </summary>
        /// <param name="customer">The customer to display.</param>
        private void DisplayCustomerDetails(Customer customer)
        {
            if (customer == null)
            {
                customerDetailsLabel.Text = "No customer selected";
                valueOfPurchaseTextBox.Enabled = false;
                makePurchaseButton.Enabled = false;
            }
            else
            {
                // TODO [Ex2]: Display the customer details.
                customerDetailsLabel.Text = customer.Details;
                valueOfPurchaseTextBox.Enabled = true;
                makePurchaseButton.Enabled = true;
            }
        }

        /// <summary>
        /// Opens the AddCustomerForm form, to enable the user 
        /// to create a customer and add it to the customer list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customer_HighPurchase(object sender, CustomerPurchaseEventArgs e)
        {
            Customer customer = (Customer)sender;
            string text = string.Format("Value of Purchase: {0:c2}", e.ValueOfPurchase);
            string caption = string.Format("High Purchase By {0}", customer.Name);
            MessageBox.Show(text, caption);
        }

        private void addCustomerButton_Click(object sender, EventArgs e)
        {
            AddCustomerForm form = new AddCustomerForm();
            form.ShowDialog(this);

            Customer customer = form.NewCustomer;
            if (customer != null)
            {
                // TODO [Ex1]: Use a CustomerKey for the key.
                CustomerKey key = new CustomerKey(customer.Region, customer.CustomerNumber);
                _customers.Add(key, customer);

                // TODO [Ex2]: Handle the HighPurchase event.
                customer.HighPurchase += customer_HighPurchase;

                DisplayCustomersList();
                customersListBox.SelectedItem = customer;
            }
        }

        /// <summary>
        /// Adds a purchase for the currently selected customer,
        /// and then redisplays the details for that customer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void makePurchaseButton_Click(object sender, EventArgs e)
        {
            Customer selectedCustomer = (Customer)customersListBox.SelectedItem;
            if (selectedCustomer != null)
            {
                try
                {
                    decimal valueOfPurchase = decimal.Parse(valueOfPurchaseTextBox.Text);

                    // TODO [Ex2]: Invoke the MakePurchase method on selectedCustomer.

                    selectedCustomer.MakePurchase(valueOfPurchase);

                    DisplayCustomerDetails(selectedCustomer);
                }
                catch (FormatException)
                {
                    MessageBox.Show(
                        "Please enter a numeric value.",
                        "Invalid Amount",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                valueOfPurchaseTextBox.Clear();
                valueOfPurchaseTextBox.Focus();
            }
        }
    }
}