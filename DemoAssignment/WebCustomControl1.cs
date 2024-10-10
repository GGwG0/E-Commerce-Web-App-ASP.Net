

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:WebCustomControl1 runat=server></{0}:WebCustomControl1>")]
    public class WebCustomControl1 : WebControl
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get
            {
                String s = (String)ViewState["Text"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["Text"] = value;
            }
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            output.Write(Text);
        }
        public static string EncryptQueryString(string input)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(input);

            string key = "023S#59~#$dmkFfkm12ekj3"; // Replace with your own secret key

            // Use SHA-256 hash to derive the key from the secret key
            SHA256CryptoServiceProvider hash = new SHA256CryptoServiceProvider();
            keyArray = hash.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            Array.Resize(ref keyArray, 24);

            // Create the encryption algorithm
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            // Encrypt the input value using the encryption algorithm
            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            // Convert the encrypted value to a base64-encoded string
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string DecryptQueryString(string input)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(input);

            string key = "023S#59~#$dmkFfkm12ekj3"; // Replace with your own secret key

            // Use SHA-256 hash to derive the key from the secret key
            SHA256CryptoServiceProvider hash = new SHA256CryptoServiceProvider();
            keyArray = hash.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            Array.Resize(ref keyArray, 24);

            // Create the encryption algorithm
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            // Decrypt the input value using the decryption algorithm
            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            // Convert the decrypted value to a string
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

    }
}
