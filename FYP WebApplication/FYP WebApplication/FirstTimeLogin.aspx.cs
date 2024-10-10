﻿using CERTENROLLLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP_WebApplication
{
    public partial class FirstTimeLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["username"] != null)
            {
                lblUsername.Text = "Username: " + Request.QueryString["username"];
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;


            string signatureData = hdnSignatureData.Value;
            byte[] graphSign = null;
            graphSign = Convert.FromBase64String(signatureData.Split(',')[1]);
            byte[] digiCert = CreateSelfSignedCertificate(Request.QueryString["username"]);

            string updateQuery = "UPDATE [dbo].[User] SET password = @password, graphicPic = @graphicPic, signaturePic = @signaturePic, status = @status WHERE username = @username";

            //@name, @contactNum, @position, @email, @managedBy, @comId
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@password", txtConfirmPassword.Text);
                    command.Parameters.AddWithValue("@graphicPic", graphSign);
                    command.Parameters.AddWithValue("@signaturePic", digiCert);
                    command.Parameters.AddWithValue("@status", "active");
                    command.Parameters.AddWithValue("@username", Request.QueryString["username"]);

                    int id = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            Response.Redirect("FirstTimeLogin2.aspx?username=" + Request.QueryString["username"] + "&googleAuthKey=" + GetUserGoogleAuthKey(Request.QueryString["username"]));
        }

        protected void verifyPassword()
        {

        }

        private string GetUserGoogleAuthKey(string username)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            // Replace the connection string with your actual connection string

            object result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Assuming the column storing the Google Authenticator key is named 'googleAuthKey'
                string sql = "SELECT googleAuthKey FROM [dbo].[User] WHERE username = @username";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@username", username);

                    result = command.ExecuteScalar();
                }
            }

            return result.ToString();
        }

        protected byte[] CreateSelfSignedCertificate(string subjectName)
        {
            // create DN for subject and issuer
            var dn = new CX500DistinguishedName();
            dn.Encode("CN=" + subjectName, X500NameFlags.XCN_CERT_NAME_STR_NONE);

            // create a new private key for the certificate
            CX509PrivateKey privateKey = new CX509PrivateKey();
            privateKey.ProviderName = "Microsoft Base Cryptographic Provider v1.0";
            privateKey.MachineContext = true;
            privateKey.Length = 2048;
            privateKey.KeySpec = X509KeySpec.XCN_AT_SIGNATURE; // use is not limited
            privateKey.ExportPolicy = X509PrivateKeyExportFlags.XCN_NCRYPT_ALLOW_PLAINTEXT_EXPORT_FLAG;
            privateKey.Create();

            // Use the stronger SHA512 hashing algorithm
            var hashobj = new CObjectId();
            hashobj.InitializeFromAlgorithmName(ObjectIdGroupId.XCN_CRYPT_HASH_ALG_OID_GROUP_ID,
                ObjectIdPublicKeyFlags.XCN_CRYPT_OID_INFO_PUBKEY_ANY,
                AlgorithmFlags.AlgorithmFlagsNone, "SHA256");

            // add extended key usage if you want - look at MSDN for a list of possible OIDs
            var oid = new CObjectId();
            oid.InitializeFromValue("1.3.6.1.5.5.7.3.1"); // SSL server
            var oidlist = new CObjectIds();
            oidlist.Add(oid);
            var eku = new CX509ExtensionEnhancedKeyUsage();
            eku.InitializeEncode(oidlist);

            // Create the self signing request
            var cert = new CX509CertificateRequestCertificate();
            cert.InitializeFromPrivateKey(X509CertificateEnrollmentContext.ContextMachine, privateKey, "");
            cert.Subject = dn;
            cert.Issuer = dn; // the issuer and the subject are the same
            cert.NotBefore = DateTime.Now;
            // this cert expires immediately. Change to whatever makes sense for you
            cert.NotAfter = DateTime.Now;
            cert.X509Extensions.Add((CX509Extension)eku); // add the EKU
            cert.HashAlgorithm = hashobj; // Specify the hashing algorithm
            cert.Encode(); // encode the certificate

            // Do the final enrollment process
            var enroll = new CX509Enrollment();
            enroll.InitializeFromRequest(cert); // load the certificate
            enroll.CertificateFriendlyName = subjectName; // Optional: add a friendly name

            string csr = enroll.CreateRequest(); // Output the request in base64
                                                 // and install it back as the response
            enroll.InstallResponse(InstallResponseRestrictionFlags.AllowUntrustedCertificate,
                csr, EncodingType.XCN_CRYPT_STRING_BASE64, "1234"); // no password
                                                                    // output a base64 encoded PKCS#12 so we can import it back to the .Net security classes
            var base64encoded = enroll.CreatePFX("1234", // no password, this is for internal consumption
                PFXExportOptions.PFXExportChainWithRoot);

            byte[] pfxData = Convert.FromBase64String(base64encoded);

            return pfxData;
            //// instantiate the target class with the PKCS#12 data (and the empty password)
            //return new System.Security.Cryptography.X509Certificates.X509Certificate2(
            //    System.Convert.FromBase64String(base64encoded), "",
            //    // mark the private key as exportable (this is usually what you want to do)
            //    System.Security.Cryptography.X509Certificates.X509KeyStorageFlags.Exportable
            //);
        }
    }
}