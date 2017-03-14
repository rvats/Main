using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows.Forms;

namespace Server
{
    public partial class FormServer : Form
    {
        bool blnStartStop;
        ServiceHost host;
        ChatService cs = new ChatService();
        

        public FormServer()
        {
            InitializeComponent();
            blnStartStop = true;
        }

        void cs_ChatListOfNames(List<string> names, object sender)
        {
            lstUser.Items.Clear();
            foreach (string s in names)
            {
                lstUser.Items.Add(s);
            }
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            
            if (blnStartStop)
            {
                host = new ServiceHost(typeof(Server.ChatService));
                host.Open();
                btnStartStop.Text = "Stop Server";
                ChatService.ChatListOfNames += new ListOfNames(cs_ChatListOfNames);
            }
            else
            {
                cs.Close();
                host.Close();
                btnStartStop.Text = "Start Server";
            }

            blnStartStop = !blnStartStop;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
