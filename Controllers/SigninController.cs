using OnlineShopping.Models;
using OnlineShopping.Respository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace OnlineShopping.Controllers
{
    public class SigninController : Controller
    {
        // GET: Signin
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// GET: Login
        /// </summary>
        /// <returns></returns>
        public ActionResult Signin()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Signin(Signin signin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SigninRespository signinRepository = new SigninRespository();
                    string role = signinRepository.GetUserRole(signin.username, signin.password);

                    if (role == "customer")
                    {

                        
                        return RedirectToAction("GetProductsForUser", "Product");

                    }
                    else if (role == "seller")
                    {
                        return RedirectToAction("");
                    }
                    else if (role == "Admin")
                    {
                        return RedirectToAction("GetProducts", "Product");
                    }
                    else
                    {
                        ViewBag.Message = "Invalid username or password";
                    }
                }

                return View();


            }
            catch
            {
                return View();
            }

        }
        private string Decrypt(string cipherText)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return cipherText;

        }

    }
}
    
