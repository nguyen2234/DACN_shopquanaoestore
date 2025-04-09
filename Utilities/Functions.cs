using System.Security.Cryptography;
using System.Text;

namespace estore.Utilities
{
    public class Functions
    {
        public static string TitleSlugGeneration(string type, string? title, long id)
        {
            return type + "_" + SlugGenerator.SlugGenerator.GenerateSlug(title) + "-" + id.ToString() + ".html";
        }
        public static int _UserID = 0;
        public static string _UserName = string.Empty;
        public static string _Email = string.Empty;
        public static string _Message = string.Empty;
        public static string _MessageEmail = string.Empty;

        public static int _userid = 0;
        public static string _username = string.Empty;
        public static string _email = string.Empty;
        public static string _message = string.Empty;
        public static string _messageemail = string.Empty;

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                stringBuilder.Append(result[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }
        public static string MD5Password(string? text)
        {
            string str = MD5Hash(text);
            for (int i = 0; i <= 5; i++)
                str = MD5Hash(str + "_" + str);
            return str;
        }
        public static bool IsLogin()
        {
            if (string.IsNullOrEmpty(Functions._UserName) || string.IsNullOrEmpty(Functions._Email) || (Functions._UserID <= 0))
                return false;
            return true;
        }
        public static bool islogin()
        {
            if (string.IsNullOrEmpty(Functions._username) || string.IsNullOrEmpty(Functions._email) || (Functions._userid <= 0))
                return false;
            return true;
        }
        public static string getCurrentDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}