using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Security;

namespace BasicRestClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            textBoxResponse.Text = "Loading...";
            string content = "Not found";

            try
            {
                WebRequestHandler handler = new WebRequestHandler();
                X509Certificate2 cert = new X509Certificate2(".\\client.crt", "password"); //findCertificate();
                if (cert != null)
                {
                    textBoxResponse.Text += Environment.NewLine;
                    textBoxResponse.Text += "certificate found: " + cert.Issuer;
                }
                else
                {
                    textBoxResponse.Text += Environment.NewLine;
                    textBoxResponse.Text += "certificate is null";
                }
                handler.ClientCertificates.Add(cert);

                HttpClient client = new HttpClient(handler);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                var requestMessage = new HttpRequestMessage(HttpMethod.Get, textBoxRequest.Text);
                requestMessage.Headers.Clear();
                requestMessage.Headers.Add("Version", "2");
                requestMessage.Headers.Add("Med-Inst-Id", "870");
                HttpResponseMessage response = await client.SendAsync(requestMessage);
                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadAsStringAsync();
                }

                //client.BaseAddress = new Uri(textBoxRequest.Text);
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
                //client.DefaultRequestHeaders.Add("Version", "2");
                //// WARNING - the header name copied directly from documention contains a non-ASCII character and 
                //// causes an exception of an invalid header name. Re-typed from scratch to ensure all ASCII charatcers
                //client.DefaultRequestHeaders.Add("Med-Inst-Id", "870");
                //String query = String.Format("applications");
                //textBoxResponse.Text += Environment.NewLine;
                //textBoxResponse.Text += "HTTPClient formatted";

                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                //                               | SecurityProtocolType.Tls11
                //                               | SecurityProtocolType.Tls12
                //                               | SecurityProtocolType.Ssl3;
                //ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                //{
                //    return true;
                //};
                //HttpResponseMessage response = await client.GetAsync(query);
                //if (response.IsSuccessStatusCode)
                //{
                //    content = await response.Content.ReadAsStringAsync();
                //}

                textBoxResponse.Text += Environment.NewLine;
                textBoxResponse.Text += "Response received";
                textBoxResponse.Text = content;
            }
            catch (Exception ex)
            {
                textBoxResponse.Text += Environment.NewLine;
                textBoxResponse.Text += "Get Async threw an exception";
                textBoxResponse.Text += Environment.NewLine;
                textBoxResponse.Text += getExceptionMessage(ex);
            }

        }

        private string getExceptionMessage(Exception e)
        {
            if (e.InnerException != null)
            {
                return getExceptionMessage(e.InnerException);
            }
            else
            {
                return e.ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxRequest.Text = "https://ws-amcas.staging.aamc.org/amcas-data-service/applications"; // 143.220.15.63

        }

        private X509Certificate findCertificate()
        {
            X509Certificate certificate = null;

            try
            {
                var store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly);

                X509Certificate2Collection certificates = store.Certificates.Find(X509FindType.FindByIssuerName, "Mira Goutseva", true);
                if (certificates != null && certificates.Count >= 1)
                {
                    certificate = certificates[0];
                }

                store.Close();
            }
            catch(Exception e)
            {
                textBoxResponse.Text += Environment.NewLine;
                textBoxResponse.Text += getExceptionMessage(e);
            }

            return certificate;
        }
    }
}
