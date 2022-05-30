using Rotativa.AspNetCore;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImpresionLotes.Models;
using Rotativa;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ImpresionLotes.Controllers
{
    public class CrearPDFController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        [Obsolete]
        public ActionResult Index(Impresiones parametros)
        {
            Impresiones loteImpresion = new Impresiones();
            loteImpresion.correlativo = 1;
            loteImpresion.loteTotal = parametros.loteTotal;

            string path = @"C:\Users\GarciaCornejo\Desktop\ImpresionLotes\ImpresionLotes\PDF\";
            for (int i = 1; i <= loteImpresion.loteTotal; i++)
            {
                DateTime hoy = DateTime.Now;
                string nombredoc = hoy.Day + hoy.Month + hoy.Year + "-" + hoy.Hour + hoy.Minute + hoy.Second + hoy.Millisecond + ".pdf";
                var pdf = new Rotativa.ViewAsPdf("Index", loteImpresion);
                System.Web.Mvc.ControllerContext controllerContext = new System.Web.Mvc.ControllerContext();
                var byteArray = pdf.BuildPdf(controllerContext);
                var fileStream = new FileStream(path+nombredoc, FileMode.Create, FileAccess.Write);
                fileStream.Write(byteArray, 0, byteArray.Length);
                fileStream.Close();
            }

            return View(loteImpresion);
        }

        public ActionResult GenerarPDF(Impresiones parametros)
        {
            return new Rotativa.AspNetCore.ViewAsPdf("MetodoAcion", parametros)
            {

                PageSize = (Rotativa.AspNetCore.Options.Size?)Rotativa.Options.Size.Letter
            };
            //return new ViewAsPdf("Index", impresion)
            //{
            //    //carta es 28 
            //    PageSize = Rotativa.AspNetCore.Options.Size.Letter,
            //    FileName = DateTime.Now.ToString()
            //};
        }
    }
}
