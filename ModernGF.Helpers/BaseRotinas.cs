using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ModernGF.Helpers
{
    public class BaseRotinas
    {
        static readonly string PasswordHash = "P@@Sw0rd";
        static readonly string SaltKey = "S@LT&KEYS";

        public static string TratarTexto(char caracter, string valor)
        {
            return valor.Replace(caracter, '"');
        }

        static readonly string VIKey = "@1B2c3D4e5F6g7H8";
        public static long ConverterParaLong(object valor)
        {
            long v = 0;

            if (valor != null)
            {
                long.TryParse(valor.ToString(), out v);
            }

            return v;
        }

        public static float ConverterParaFloat(object valor)
        {
            float v = 0;
            if (valor != null)
            {
                float.TryParse(valor.ToString(), out v);
            }
            return v;
        }

        //public static string ObterConnectionString()
        //{
        //    var conn = ConfigurationManager.ConnectionStrings["junix_cobrancaContext"];

        //    if (conn != null)
        //        return conn.ToString();
        //    else
        //        return string.Empty;
        //}

        public static decimal ConverterParaDecimal(object valor)
        {



            decimal v = 0;
            if (valor != null)
            {

                //string valorStr = valor.ToString().Replace(".",",");

                decimal.TryParse(valor.ToString(), out v);
            }
            return v;
        }

        public static short ConverterParaShort(object p)
        {
            short v = 0;
            if (p != null)
            {
                short.TryParse(p.ToString(), out v);
            }
            return v;
        }

        public static int ConverterParaInt(object key)
        {
            if (key == null)
            {
                return 0;
            }

            int v = 0;

            Int32.TryParse(key.ToString(), out v);

            return v;
        }



        internal static object ConverterDbValue(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }
            else
            {
                if (string.IsNullOrEmpty(value.ToString()))
                    return DBNull.Value;

            }

            return value;
        }

        public static object ConverterDbDateValue(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }
            else
            {
                if (string.IsNullOrEmpty(value.ToString()))
                    return DBNull.Value;

            }

            return value;
        }

        public static DateTime? ConverterParaDatetimeOuNull(object value)
        {
            DateTime v = DateTime.MinValue;

            if (value != null)
            {
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    DateTime.TryParse(value.ToString(), out v);
                    return v;
                }
            }
            return null;
        }

        public static object Descriptografar(object numeroContrato)
        {
            throw new NotImplementedException();
        }

        public static DateTime ConverterParaDatetime(object value)
        {
            DateTime v = DateTime.MinValue;

            if (value != null)
            {
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    DateTime.TryParse(value.ToString(), out v);
                    return v;
                }
            }
            return v;
        }
        public static int DiferencaMes(DateTime lValue, DateTime rValue)
        {
            int diff = (lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year);
            return Math.Abs(diff > 0 ? 0 : diff);
        }

        public static double ConverterParaDouble(object valor)
        {
            double v = 0;
            if (valor == null)
            {
                return v;

            }
            else
            {
                double.TryParse(valor.ToString(), out v);
            }
            return v;

        }


        public static string ConverterParaString(object valor)
        {
            if (valor == null)
            {
                return string.Empty;
            }
            return valor.ToString();
        }

        public static bool ConverterParaBool(object valor)
        {
            bool v = false;
            if (valor != null)
            {
                bool.TryParse(valor.ToString(), out v);
            }
            return v;
        }
        public static string FormatarData(DateTime? data)
        {
            return string.Format("{0:dd/MM/yyyy}", data);
        }
        public static string FormatarMoeda(decimal v)
        {
            return string.Format("{0:n2}", v);
        }
        public static string FormatarMoeda(decimal? v)
        {
            return FormatarMoeda(BaseRotinas.ConverterParaDecimal(v));
        }

        public static string RemoverTracosPontos(string valor)
        {
            return valor.Replace("-", "").Replace(".", "").Replace("/", "").Replace(@"\", "");
        }
        public static string FormatarText(string valor, int dig)
        {
            if (valor.Length > dig)
            {
                throw new Exception("Valor é maior que a quantidade de digitos!");
            }
            //else
            //{
            //    int diff = valor.Length - dig;

            //    for (int i = 0; i <= diff; i++)
            //    {
            //        valor = "0";
            //    }
            //}

            return valor.Substring(0, dig);
        }
        public static string CriptografarNew(string dados)
        {
            if (!String.IsNullOrEmpty(dados))
            {
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(dados);

                byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
                var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
                var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

                byte[] cipherTextBytes;

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        cipherTextBytes = memoryStream.ToArray();
                        cryptoStream.Close();
                    }
                    memoryStream.Close();
                }
                return Convert.ToBase64String(cipherTextBytes);
            }
            else
            {
                return dados;
            }
        }

        public static string DescriptografarNew(string dados)
        {
            if (!String.IsNullOrEmpty(dados))
            {
                byte[] cipherTextBytes = Convert.FromBase64String(dados);
                byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
                var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

                var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
                var memoryStream = new MemoryStream(cipherTextBytes);
                var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];

                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
            }
            else
            {
                return dados;
            }
        }

        public static string RemoverCaracterEspeciais(string valor)
        {
            if (!String.IsNullOrEmpty(valor))
            {
                Regex pattern = new Regex("[-;,\t\r]");
                valor = pattern.Replace(valor, "");
            }
            return valor;
        }

        public static string formatarCpfCnpj(string valor)
        {
            if (!String.IsNullOrEmpty(valor))
            {
                Regex pattern = new Regex("[-;,\t\r]");
                valor = pattern.Replace(valor, "");
                if (valor.Length <= 11)
                {
                    valor = Convert.ToUInt64(valor).ToString(@"000\.000\.000\-00");
                }
                else if (valor.Length <= 14)
                {
                    valor = Convert.ToUInt64(valor).ToString(@"00\.000\.000\/0000\-00");
                }

            }
            return valor;
        }
        public static string formatarCEP(string valor)
        {
            if (!String.IsNullOrEmpty(valor))
            {
                Regex pattern = new Regex("[-;,\t\r]");
                valor = pattern.Replace(valor, "");
                var numeros = BaseRotinas.ConverterParaInt(valor);
                if (numeros > 0)
                {
                    valor = Convert.ToUInt64(valor).ToString(@"00000\-000");
                }
            }
            return valor;
        }
        public static string Descriptografar(string valor)
        {
            //return valor;
            if (!String.IsNullOrEmpty(valor))
            {
                string Chave = "XINUJ-SSITORSFD";
                string NovaSenha;

                for (int x = 0; x < Chave.Length; x++)
                {
                    NovaSenha = "";
                    for (int y = 0; y < valor.Length; y++)
                    {
                        NovaSenha += Convert.ToChar(((Chave[x]) ^ (valor[y])));
                    }
                    valor = NovaSenha;
                }
            }
            return valor;

        }
        public static string StringJoinSemErro(List<int> lista)
        {
            var valor = "";
            if (lista != null)
            {
                valor = String.Join(",", lista);
            }
            return valor;
        }

        public static string Criptografar(string valor)
        {
            if (!String.IsNullOrEmpty(valor))
            {
                string Chave = "XINUJ-SSITORSFD";
                string NovaSenha;

                for (int x = 0; x < Chave.Length; x++)
                {
                    NovaSenha = "";
                    for (int y = 0; y < valor.Length; y++)
                    {
                        NovaSenha += Convert.ToChar(((Chave[x]) ^ (valor[y])));
                    }
                    valor = NovaSenha;
                }
            }
            return valor;
        }


        public class ListtoDataTableConverter
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties  
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                // Loop through all the properties  
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names  
                    dataTable.Columns.Add(prop.Name);
                }

                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows  
                        values[i] = Props[i].GetValue(item, null);
                    }
                    // Finally add value to datatable  
                    dataTable.Rows.Add(values);

                }
                //put a breakpoint here and check datatable of return values  
                return dataTable;
            }

        }

        /// <summary>
        /// Função para completar um string com zeros ou espacos em branco. Pode servir para criar a remessa.
        /// </summary>
        /// <param name="text">O valor recebe os zeros ou espaços em branco</param>
        /// <param name="with">caractere a ser inserido</param>
        /// <param name="size">Tamanho do campo</param>
        /// <param name="left">Indica se caracteres serão inseridos à esquerda ou à direita, o valor default é inicializar pela esquerda (left)</param>
        /// <returns></returns>
        public static string FormatCode(string text, string with, int length, bool left)
        {
            if (String.IsNullOrEmpty(text))
            {
                text = " ";
            }
            //Esse método já existe, é PadLeft e PadRight da string
            length -= text.Length;
            if (left)
            {
                for (int i = 0; i < length; ++i)
                {
                    text = with + text;
                }
            }
            else
            {
                for (int i = 0; i < length; ++i)
                {
                    text += with;
                }
            }
            return text;
        }

        public static string FormatCode(string text, string with, int length)
        {
            return FormatCode(text, with, length, false);
        }

        public static string FormatCode(string text, int length)
        {
            return text.PadLeft(length, '0');
        }

    }
}
