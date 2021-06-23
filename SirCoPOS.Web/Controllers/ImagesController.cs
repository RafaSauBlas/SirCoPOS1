using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZXing;
using ZXing.Common;
using ZXing.QrCode.Internal;
using ZXing.Rendering;

namespace SirCoPOS.Web.Controllers
{
    public class ImagesController : Controller
    {
        public ActionResult Producto(int? id, string marca, string modelo, int w = 100, int h = 100)
        {
            var ctx = new DataAccess.SirCoDataContext();
            var ctxi = new DataAccess.SirCoImgDataContext();

            if (id.HasValue)
            {
                var item = ctx.Articulos.Where(i => i.Id == id).SingleOrDefault();
                if (item == null)
                    return HttpNotFound();
                marca = item.Marca;
                modelo = item.Modelo;
            }
            else
            {
                if (modelo.Length < 7)
                    modelo = modelo.PadLeft(7);
            }
            //=============================================================================================================================================
            var img = ctxi.Imagenes.Where(i => i.Marca == marca && i.Estilon == modelo).SingleOrDefault();
            if (img == null || img.Foto == null)
                return HttpNotFound();

            var ms = new System.IO.MemoryStream();
            ImageResizer.ImageBuilder.Current.Build(img.Foto, ms,
                new ImageResizer.ResizeSettings()
                {
                    Mode = ImageResizer.FitMode.None,
                    Width = w,
                    Height = h,
                    Format = "png"
                });
            ms.Position = 0;

            return File(ms, "image/png", $"{marca}_{modelo.Replace(' ', '_')}.png");
        }
        public ActionResult Firma(int id, short num, int w = 100, int h = 100)
        {
            var ctx = new DataAccess.SirCoCreditoDataContext();

            var item = ctx.Distribuidores.Where(i => i.iddistrib == id).SingleOrDefault();
            if (item == null)
                return HttpNotFound();
            var img = ctx.DistribuidorFirmas.Where(i => i.distrib == item.distrib && i.numfirma == num).SingleOrDefault();
            if (img == null || img.firma == null)
                return HttpNotFound();

            var ms = new System.IO.MemoryStream();
            ImageResizer.ImageBuilder.Current.Build(img.firma, ms,
                new ImageResizer.ResizeSettings()
                {
                    Mode = ImageResizer.FitMode.None,
                    Width = w,
                    Height = h,
                    Format = "png"
                });
            ms.Position = 0;

            return File(ms, "image/png", $"{item.iddistrib}_{img.numfirma}.png");
        }

        public ActionResult Empleado(int id, int w = 100, int h = 100)
        {
            var ctx = new DataAccess.SirCoNominaDataContext();
            var img = ctx.EmpleadosImgs.Where(i => i.idempleado == id).SingleOrDefault();
            if (img == null || img.foto == null)
                return HttpNotFound();

            var ms = new System.IO.MemoryStream();
            ImageResizer.ImageBuilder.Current.Build(img.foto, ms,
                new ImageResizer.ResizeSettings()
                {
                    Mode = ImageResizer.FitMode.None,
                    Width = w,
                    Height = h,
                    Format = "png"
                });
            ms.Position = 0;

            return File(ms, "image/png", $"{id}.png");
        }
        public ActionResult Promocion(int id, int w = 200, int h = 200)
        {
            var ctx = new DataAccess.SirCoPVDataContext();
            var img = ctx.Promociones.Where(i => i.idpromocion == id).SingleOrDefault();
            if (img == null || img.imagen == null)
                return HttpNotFound();

            var ms = new System.IO.MemoryStream();
            ImageResizer.ImageBuilder.Current.Build(img.imagen, ms,
                new ImageResizer.ResizeSettings()
                {
                    Mode = ImageResizer.FitMode.None,
                    Width = w,
                    Height = h,
                    Format = "png"
                });
            ms.Position = 0;

            return File(ms, "image/png", $"{id}.png");
        }
        public ActionResult Cliente(int id, int w = 200, int h = 200)
        {
            var ctx = new DataAccess.SirCoCreditoDataContext();
            var img = ctx.Clientes.Where(i => i.idcliente == id).SingleOrDefault();
            if (img == null || img.foto == null)
                return HttpNotFound();

            var ms = new System.IO.MemoryStream();
            ImageResizer.ImageBuilder.Current.Build(img.foto, ms,
                new ImageResizer.ResizeSettings()
                {
                    Mode = ImageResizer.FitMode.None,
                    Width = w,
                    Height = h,
                    Format = "png"
                });
            ms.Position = 0;

            return File(ms, "image/png", $"{id}.png");
        }

        public ActionResult QrCode(string id, int w = 100, int h = 100)
        {
            var barcodeWriter = new ZXing.BarcodeWriter();
            var encodingOptions = new EncodingOptions { Width = w, Height = h, Margin = 0, PureBarcode = false };
            encodingOptions.Hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
            barcodeWriter.Renderer = new BitmapRenderer();
            barcodeWriter.Options = encodingOptions;
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            var bitmap = barcodeWriter.Write(id);

            var ms = new System.IO.MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);
            ms.Position = 0;
            return File(ms, "image/png", $"{id}.png");
        }
        public ActionResult Code(string code)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            QRCoder.QRCode qrCode = new QRCoder.QRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(4);
            var ms = new System.IO.MemoryStream();
            qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            ms.Position = 0;
            return File(ms, "image/png");
        }
        public ActionResult BarCode(string id, int w = 100, int h = 100)
        {
            var barcodeWriter = new ZXing.BarcodeWriter();
            var encodingOptions = new EncodingOptions { Width = w, Height = h, Margin = 0, PureBarcode = false };
            encodingOptions.Hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
            barcodeWriter.Renderer = new BitmapRenderer();
            barcodeWriter.Options = encodingOptions;
            barcodeWriter.Format = BarcodeFormat.CODE_128;
            var bitmap = barcodeWriter.Write(id);

            var ms = new System.IO.MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);
            ms.Position = 0;
            return File(ms, "image/png", $"{id}.png");
        }
    }
}