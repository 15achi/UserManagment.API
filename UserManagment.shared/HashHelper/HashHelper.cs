using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UserManagment.shared.HashHelper
{
    public class HashHelper
    {
        public static string Sha512Hex(string value)
        {
            using (var sha = SHA512.Create())
            {
                var sBuilder = new StringBuilder();
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(value));
                foreach (var b in bytes)
                {
                    sBuilder.Append(b.ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }
    }
}
