namespace Smartwyre.Domain.Services.Utilities
{
    using EncryptConnectionString;
    using Smartwyre.Domain.Entities.Config; 
    using Smartwyre.Domain.Entities.ErrorHandler;
    using Smartwyre.Domain.Entities.Response;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text.RegularExpressions;
    using System.Runtime.Serialization;

    public static class Util
    {
        public static GeneralResponse ManageResponse(object information = null, bool successful = true, string message = null)
        {
            return new GeneralResponse
            {
                isSuccess = successful,
                result = information,
                message = message
            };
        }

        public static GeneralResponse ManageFailureResponse(string message = null)
        {
            return new GeneralResponse
            {
                isSuccess = false,
                result = new ErrorDetails
                {
                    ResultCode = "API_INTERNAL_ERROR",
                    ResultMsg = !string.IsNullOrEmpty(message) ? message : "Ha ocurrido un error inesperado"
                }
            };
        }

        public static GeneralResponse ManageException(Exception ex,  string source, long startDate, object request, AppSettings dataSettings)
        {
            return ManageFailureResponse(ex.Message);
        }

      
        public static GeneralResponse ManageExceptionHandler( string source, object response, AppSettings dataSettings, string errorMessage)
        {
            return ManageFailureResponse(errorMessage);
        }

        public static long GetRequestStartDate()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        public static string GetDecryptConectionString(string connectionString)
        {
            CryptLib _crypt = new CryptLib();
            string iv = "75npchtk5DpbTGbB";
            string key = CryptLib.getHashSha256("bGZkYjIwMTgq", 32);
            string connString = connectionString ?? string.Empty;
            return _crypt.decrypt(connString, key, iv);
        }

        /// <summary>
        /// The ConvertDecimal
        /// </summary>
        /// <param name="valor">The valor<see cref="object"/></param>
        /// <param name="posicionesDecimales">The posicionesDecimales<see cref="int"/></param>
        /// <returns>The <see cref="decimal?"/></returns>
        public static decimal? ConvertDecimal(this object val, int posDecimals = 0)
        {
            if (string.IsNullOrWhiteSpace(val.ToString()))
            {
                return null;
            }
            return posDecimals != 0 ? Math.Round(Convert.ToDecimal(val, CultureInfo.InvariantCulture), posDecimals) :
                Convert.ToDecimal(val, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// The BetweenDate
        /// </summary>
        /// <param name="value">The value<see cref="DateTime?"/></param>
        /// <param name="minimum">The minimum<see cref="DateTime"/></param>
        /// <param name="maximum">The maximum<see cref="DateTime"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool BetweenDate(this DateTime? value, DateTime minimum, DateTime maximum)
        {
            if (!value.HasValue)
            {
                return false;
            }
            return value >= minimum && value <= maximum;
        }

        /// <summary>
        /// The BetweenDate
        /// </summary>
        /// <param name="value">The value<see cref="DateTime"/></param>
        /// <param name="minimum">The minimum<see cref="DateTime"/></param>
        /// <param name="maximum">The maximum<see cref="DateTime"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool BetweenDate(this DateTime value, DateTime minimum, DateTime maximum)
        {
            return value >= minimum && value <= maximum;
        }

        /// <summary>
        /// The ConvertDate
        /// </summary>
        /// <param name="valor">The valor<see cref="object"/></param>
        /// <returns>The <see cref="DateTime?"/></returns>
        public static DateTime? ConvertDate(this object valor)
        {
            if (string.IsNullOrWhiteSpace(valor.ToString()) || valor.ToString() == "0")
            {
                return null;
            }
            return DateTime.FromOADate(Convert.ToDouble(valor));
        }

        /// <summary>
        /// The ConvertDateFormatt
        /// </summary>
        /// <param name="valor">The val<see cref="string"/></param>
        /// <param name="format">The format<see cref="string"/></param>
        /// <returns>The <see cref="DateTime?"/></returns>
        public static DateTime? ConvertDateFormatt(this string val, string format)
        {
            DateTime fechaConversion;
            if (DateTime.TryParseExact(val, format, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out fechaConversion))
            {
                return fechaConversion;
            }
            return null;
        }

        /// <summary>
        /// The AddIfNotNull
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lista">The lista<see cref="List{T}"/></param>
        /// <param name="newItem">The newItem<see cref="T"/></param>
        public static void AddIfNotNull<T>(this List<T> lista, T newItem)
        {
            if (newItem != null)
            {
                lista.Add(newItem);
            }
        }

        /// <summary>
        /// The CloneObjectSerializable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj<see cref="T"/></param>
        /// <returns>The <see cref="T"/></returns>
        public static T CloneObjectSerializable<T>(this T obj) where T : class
        {
            using (var ms = new MemoryStream())
            {
                var serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(ms, obj);
                ms.Position = 0;
                return (T)serializer.ReadObject(ms);
            }
        }

        /// <summary>
        /// The ConvertirPorcentaje
        /// </summary>
        /// <param name="val">The val<see cref="object"/></param>
        /// <param name="posDecimals">The posDecimals<see cref="int"/></param>
        /// <returns>The <see cref="decimal?"/></returns>
        public static decimal? ConvertPercent(this object val, int posDecimals = 0)
        {
            if (string.IsNullOrWhiteSpace(val.ToString()))
            {
                return null;
            }
            val = val.ToString().Replace("%", string.Empty).ToString(CultureInfo.InvariantCulture).ToString(new NumberFormatInfo());
            return posDecimals != 0 ? Math.Round(Convert.ToDecimal(val, CultureInfo.InvariantCulture), posDecimals) :
                Convert.ToDecimal(val, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// The ValidateEmail
        /// </summary>
        /// <param name="email">The email<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool ValidateEmail(this string email)
        {
            string formatt;
            formatt = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, formatt))
            {
                if (Regex.Replace(email, formatt, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static bool IsNumeric(this string val)
        {
            var numReference = 0;
            return int.TryParse(val, out numReference);
        }

        public static bool ComparePassword(string pass1, string pass2)
        {
            const int hashByte = 16;
            const int loopFor = 20;
            string savedPasswordHash = pass1;
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, hashByte);
            var pbkdf2 = new Rfc2898DeriveBytes(pass2, salt, 1000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < loopFor; i++)
            {
                if (hashBytes[i + hashByte] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}