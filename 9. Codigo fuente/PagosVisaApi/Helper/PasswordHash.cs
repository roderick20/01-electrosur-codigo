using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Electrosur.Helper
{
    /*******************************************************************************************
* PagosVisaWeb
* Este clase es para encriptar la contraseña
* Programador: Rodercik Cusirramos Montesinos
* Fecha de creacion: 22/06/2020
* Fecha de modificacion: 03/08/2020      
* *****************************************************************************************/

    static public class PasswordHash
    {
        static public string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }
    }
}
