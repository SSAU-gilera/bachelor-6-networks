using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace LR04
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBoxLogin.Text = "yakotik_tikotik@mail.ru";
            textBoxHost.Text = "smtp.mail.ru";
            textBoxPort.Text = "587";
            textBoxRecipient.Text = "leraolera@mail.ru";
            textBoxSubject.Text = "Топ 10 песен из Тиктока";
        }
        List<string> attachments = new List<string>();
        private void buttonMail_Click(object sender, EventArgs e)
        {
            try
            {
                MailAddress to = new MailAddress(textBoxRecipient.Text);
                MailAddress from = new MailAddress(textBoxLogin.Text);
                MailMessage message = new MailMessage(from, to);
                message.Subject = textBoxSubject.Text;
                message.Body = richTextBoxLetter.Text;
                for (int i = 0; i < attachments.Count; i++)
                    message.Attachments.Add(new Attachment(attachments[i]));
                SmtpClient smtpClient = new SmtpClient(textBoxHost.Text, Convert.ToInt32(textBoxPort.Text));
                smtpClient.Credentials = new NetworkCredential(textBoxLogin.Text, "aTU-Xs7-EAD-u2w");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;
                smtpClient.Send(message);
                MessageBox.Show("Письмо отправлено");
                richTextBoxLetter.Clear();
                listBoxAttechments.Items.Clear();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK && fileDialog.FileName != "")
            {
                attachments.Add(fileDialog.FileName);
                listBoxAttechments.Items.Add(fileDialog.FileName);
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
