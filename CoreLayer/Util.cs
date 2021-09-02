using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CoreLayer
{
    public class Util
    {


        public static bool SendMail(string mail, string name, string lastname)
        {
            bool send = false;
            try
            {

                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(mail);//alıcı
                mailMessage.From = new MailAddress("bertictest@gmail.com", "Kayıt Oluşturuldu");//gönderen
                mailMessage.Subject = "Yeni Kayıt Tarihi : " + DateTime.Now.ToString();
                mailMessage.Body = "<br> Ad Soyad : " + name + " " + lastname;
                mailMessage.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                NetworkCredential Credentials = new NetworkCredential("bertictest@gmail.com", "****");//gönderen mail , şifre
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = Credentials;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.Send(mailMessage);
                send = true;

            }
            catch (Exception e)
            {

                throw (new Exception("Mail Gönderilemedi: " + e.ToString()));
            }

            return send;

        }
        public static void DatabaseUpdate()
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(@"data source= DESKTOP-5KB93N3\SQLEXPRESS; Initial Catalog = SendMailDb; Integrated Security = True");

                sqlConnection.Open();

                string sorgu = "Select * from Sends WHERE   Information=0 and Status=1";
                var commandControl = new SqlCommand(sorgu, sqlConnection);
                SqlDataReader dta = commandControl.ExecuteReader();
                if (dta.HasRows)
                {
                    dta.Read();
                    int id = Convert.ToInt32(dta["Id"]);
                    var name = dta["FirstName"].ToString();
                    var lastname = dta["LastName"].ToString();
                    var mail = dta["Mail"].ToString();
                    SendMail(mail, name, lastname);

                    dta.Close();

                    string query2 = "UPDATE Sends SET Information=@Information , Status=@Status WHERE Id=" + id + "";
                    SqlCommand command2 = new SqlCommand(query2, sqlConnection);
                    command2.Parameters.AddWithValue("@Information", true);
                    command2.Parameters.AddWithValue("@Status", false);

                    command2.ExecuteNonQuery();


                }
                sqlConnection.Close();

            }


            catch (Exception)
            {
                throw;


            }


        }


    }


}
