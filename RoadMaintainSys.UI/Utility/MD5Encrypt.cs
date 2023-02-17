using System.Security.Cryptography;
using System.Text;

namespace RoadMaintainSys.UI.Utility
{
    public class MD5Encrypt//MD5加密密码
    {
        public static string MD5Encrypt32(string password)
        {
            StringBuilder pwd=new StringBuilder("");

            MD5 md5 = MD5.Create();

            byte[] s=md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            for(int i=0; i<s.Length; i++)
            {
                pwd.Append(s[i].ToString("X"));
            }
            return pwd.ToString();
        }
    }
}
