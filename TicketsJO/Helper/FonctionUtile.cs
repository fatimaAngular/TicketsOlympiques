using BarcodeStandard;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using TicketsJO.Models;
using ZXing;
using ZXing.Windows.Compatibility;


namespace TicketsJO.Helper
{
    public class FonctionUtile
    {
        private static readonly Random Random = new Random();

        public static byte[] GenerateQRCode(string TokenGenerate, User user, Offre offre)
        {
            byte[] MonFichier = null;
            // Crée un objet BarcodeWriter pour générer le QRCode

            var barcodeWriter = new BarcodeWriter
            {
                //Format = BarcodeFormat.QR_CODE,
                //Options = new ZXing.Common.EncodingOptions
                //{
                //    Width = 300,
                //    Height = 300 // Ajuste la taille du QRCode selon tes besoins
                //}
            };


            var contactInfo = new StringBuilder();
            contactInfo.Append("Nom : " + user.Name);
            contactInfo.Append("Prénom : " + user.Prenom);
            contactInfo.Append("Mail : " + user.Email);
            contactInfo.Append("Offre : " + offre.TypeOffre);
            contactInfo.Append("Token : " + GenerateHash(TokenGenerate));

            // Encode le texte dans un QRCode bitmap
            //using (var bitmap = barcodeWriter(contactInfo.ToString()))
            //{
            //    // Convertit le bitmap en tableau de bytes
            //    using (var stream = new MemoryStream())
            //    {
            //        bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            //        // Retourne l'image du QRCode
            //        MonFichier = stream.ToArray();
            //    }
            //}
            return MonFichier;
        }

        public static string GenerateToken(string characters, int length)
        {
            return new string(Enumerable
               .Range(0, length)
               .Select(num => characters[Random.Next() % characters.Length])
               .ToArray());
        }


        public static string GenerateHash(string text)
        {
            using (var hash = SHA256.Create())
            {
                return Convert.ToBase64String(hash.ComputeHash(Encoding.UTF8.GetBytes(text)));
            }
        }

        /// <summary>
        /// Per
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool CompareHash(string hash, string text)
        {
            return GenerateHash(text) == hash;
        }
    }

}
