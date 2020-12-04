using Base32;
using OtpSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SirCoPOS.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QrCode(string id)
        {
            ViewBag.id = id;
            ViewBag.type = "qrcode";
            return View("Image");
        }
        public ActionResult BarCode(string id)
        {
            ViewBag.id = id;
            ViewBag.type = "barcode";
            return View("Image");
        }
        public ActionResult AuthKey(int id)
        {
            var ctx = new DataAccess.SirCoNominaDataContext();
            var emp = ctx.Empleados.Where(i => i.idempleado == id).Single();

            byte[] secretKey = KeyGeneration.GenerateRandomKey(20);
            var issuer = "ZapateriaTorreon";
            string barcodeUrl = KeyUrl.GetTotpUrl(secretKey, $"{emp.idempleado}") + $"&issuer={issuer}";
            //barcodeUrl = HttpUtility.UrlDecode(barcodeUrl);

            ViewBag.id = id;
            ViewBag.SecretKey = Base32Encoder.Encode(secretKey);
            ViewBag.BarcodeUrl = barcodeUrl;
            //ViewBag.issuer = issuer;
            return View();
        }
        [HttpPost]
        public ActionResult AuthKey(int id, string key, string code)
        {
            var ctx = new DataAccess.SirCoNominaDataContext();
            var emp = ctx.Empleados.Where(i => i.idempleado == id).Single();

            byte[] secretKey = Base32Encoder.Decode(key);

            long timeStepMatched = 0;
            var otp = new Totp(secretKey);
            if (otp.VerifyTotp(code, out timeStepMatched, new VerificationWindow(2, 2)))
            {
                emp.authkey = key;
                ctx.SaveChanges();

                return Content("SUCCESS");
            }

            return Content("FAILED");
        }
    }
}