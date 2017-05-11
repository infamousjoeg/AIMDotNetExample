using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// Use the following NetPasswordSDK namespaces
using CyberArk.AIM.NetPasswordSDK;
using CyberArk.AIM.NetPasswordSDK.Exceptions;

namespace AIMDotNetExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                PSDKPasswordRequest passRequest = new PSDKPasswordRequest();
                PSDKPassword password;

                // Set passRequest hash table variables
                passRequest.AppID = "RESTExamples";
                passRequest.ConnectionPort = 18923;
                passRequest.ConnectionTimeout = 30;
                passRequest.Safe = "T-APP-CYBR-RESTAPI";
                passRequest.Folder = "Root";
                passRequest.Object = "Database-MicrosoftSQLServer-JG-sql01.joe-garcia.local-Svc_BambooHR";
                passRequest.Reason = "Testing Application - Connect to SQL";

                // Set RequiredProperties to be returned in addition to the Password
                passRequest.RequiredProperties.Add("PolicyId");
                passRequest.RequiredProperties.Add("UserName");
                passRequest.RequiredProperties.Add("Address");
                passRequest.RequiredProperties.Add("Database");
                passRequest.RequiredProperties.Add("Port");

                // Sending the request to get the password
                password = PasswordSDK.GetPassword(passRequest);

                // Initializes the variables to pass to the MessageBox.Show method
                string message = "The Object's Name: " + passRequest.Object + "\n\n" + "The Object's Address: " + password.Address + "\n" + "The Object's Database: " + password.Database + "\n" + "The Object's Port: " + password.GetAttribute("PassProps.Port") + "\n\n" + "The Object's Username: " + password.UserName + "\n" + "The Object's Password: " + password.Content + "\n" + "The Object's PlatformID: " + password.PolicyID;
                string caption = "AIM .NET Example Results";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                // Displays the MessageBox.
                result = MessageBox.Show(message, caption, buttons);

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    // Closes the parent form.
                    this.Close();
                }
            }
            catch (PSDKException ex)
            {
                // Displays MessageBox showing error returned
                string message = ex.Reason;
                string caption = "Error";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    // Closes the parent form.
                    this.Close();
                }
            }
        }
    }
}
