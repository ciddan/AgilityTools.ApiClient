using System.Security.Cryptography;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public static class PasswordEncoder
    {
        public static string EncodePassword(string password) {
            using (SHA1 encoder = new SHA1Managed()) {
                var utf8Encoder = new System.Text.UTF8Encoding();
                byte[] hash = encoder.ComputeHash(utf8Encoder.GetBytes(password));
                string encodedString = System.Convert.ToBase64String(hash);

                return encodedString;
            }
        }
    }
}