namespace WebApplication2.Controllers
{
    using System.Security.Cryptography;
    using System.Text;

    public class HashingExample
    {
        public static string HashString(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

               
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public static bool CompareHash(string plainText, string hashedText)
        {
   
            string hashedInput = HashingExample.HashString(plainText);

            
            return hashedInput.Equals(hashedText, StringComparison.OrdinalIgnoreCase);
        }
    }

}
