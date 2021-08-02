using System;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace CustomerObserver
{
    public class NotificationData : IData
    {
        public NotificationData()
        {

        }

        public NotificationData(string productName, string customerEmail, string aboutProduct)
        {
            ProductName = productName;
            CustomerEmail = customerEmail;
            AboutProduct = aboutProduct;
        }

        public string ProductName { get; set; }

        public string CustomerEmail { get; set; }
        public string AboutProduct { get; set; }

        public string FullData => $" {ProductName} {CustomerEmail} {AboutProduct} ";
        public void GetMessage()
        {
            string[] discount = new string[] { "5%", "10%", "15%", "25%" };

            Random r1 = new Random();
            int n = discount.Length;


            while (n > 1)
            {
                n--;
                int k = r1.Next(n + 1);
                string value = discount[k];
                discount[k] = discount[n];
                discount[n] = value;
            }



            // Console.WriteLine($"Name of product: {ProductName} \n Customer's email: {CustomerEmail} \n\n About of Product: {AboutProduct} \n\n");

            if (CustomerEmail.Contains("@"))
            {
                string mailBodyhtml =
           $"<h1 style=\"color: #EB0008;\"> Discount: {discount[0]}</h1> <h1 style=\"color: #42A1D7;\"> Product Name: {ProductName}</h1> <h2 style=\"color: #42A1D7;\"> About Product: {AboutProduct}</h2>";
                var msg = new MailMessage("testtext24@yandex.com", CustomerEmail, "For victims of commodity fetishism", mailBodyhtml);
                msg.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient($"smtp.yandex.com", 587);
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new NetworkCredential("testtext24@yandex.com", "210193M");
                smtpClient.EnableSsl = true;

                try
                {
                    smtpClient.Send(msg);

                }
                catch (Exception)
                {
                    MessageBox.Show($"<h1 style=\"color: #42A1D7;\">  An error occurred while sending your message. ");


                }


            }
        }

   
    }




}
