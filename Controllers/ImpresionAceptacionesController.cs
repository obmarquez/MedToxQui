using MedToxQui.Data;
using MedToxQui.Models.Consultas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace MedToxQui.Controllers
{
    [Authorize]

    [Authorize(Roles = "Administrador, Quimica")]

    public class ImpresionAceptacionesController : Controller
    {
        private DBOperaciones repo;

        public ImpresionAceptacionesController()
        {
            repo = new DBOperaciones();
        }

        public IActionResult IndexAceptaciones()
        {
            return View();
        }

		public IActionResult aceptacionMedico(string fecha)
		{
			var datosC3 = repo.Get<ConsultasModel>("sp_general_obtener_certificacion_acreditacion").FirstOrDefault();
			var aceMed = repo.Getdosparam1<ConsultasModel>("sp_medicos_rep_cabeceras_fecha", new { @fecha = fecha }).ToList();

			var _totalAceptacion = aceMed.Count();

			var fonEiqueta = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK);
			var fontDato = FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK);
			var fontDatosmall = FontFactory.GetFont("Arial", 9, Font.NORMAL, BaseColor.BLACK);

			MemoryStream msAceMed = new MemoryStream();
			Document docAceMed = new Document(PageSize.LETTER, 30f, 20f, 20f, 40f);
			PdfWriter pwAceMed = PdfWriter.GetInstance(docAceMed, msAceMed);
			docAceMed.Open();

			for (int id = 0; id < _totalAceptacion; id++)
			{
				#region encabezado
				//-------------------------------------------------------------------------------------------------------- 1a linea
				string imageizq = @"C:/inetpub/wwwroot/fotoUser/gobedohor.png";
				iTextSharp.text.Image jpgSupIzq = iTextSharp.text.Image.GetInstance(imageizq);
				jpgSupIzq.ScaleToFit(80f, 80f);

				PdfPCell clLogoSupIzq = new PdfPCell();
				clLogoSupIzq.BorderWidth = 0;
				clLogoSupIzq.VerticalAlignment = Element.ALIGN_BOTTOM;
				clLogoSupIzq.AddElement(jpgSupIzq);

				string imageder = @"C:/inetpub/wwwroot/fotoUser/nuevoCeccc.png";
				iTextSharp.text.Image jpgSupDer = iTextSharp.text.Image.GetInstance(imageder);
				jpgSupDer.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
				jpgSupDer.ScaleToFit(100f, 100f);

				PdfPCell clLogoSupDer = new PdfPCell();
				clLogoSupDer.BorderWidth = 0;
				clLogoSupDer.VerticalAlignment = Element.ALIGN_BOTTOM;
				clLogoSupDer.AddElement(jpgSupDer);

				Chunk chkTit = new Chunk("Dirección Médica y Toxicológica", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
				Paragraph paragraph = new Paragraph();
				paragraph.Alignment = Element.ALIGN_CENTER;
				paragraph.Add(chkTit);

				Chunk chkSub = new Chunk("Aceptación de exámen médico", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
				Paragraph paragraph1 = new Paragraph();
				paragraph1.Alignment = Element.ALIGN_CENTER;
				paragraph1.Add(chkSub);

				PdfPCell clTitulo = new PdfPCell();
				clTitulo.BorderWidth = 0;
				clTitulo.AddElement(paragraph);

				PdfPCell clSubTit = new PdfPCell();
				clSubTit.BorderWidth = 0;
				clSubTit.AddElement(paragraph1);

				PdfPTable tblTitulo = new PdfPTable(1);
				tblTitulo.WidthPercentage = 100;
				tblTitulo.AddCell(clTitulo);
				tblTitulo.AddCell(clSubTit);

				PdfPCell clTablaTitulo = new PdfPCell();
				clTablaTitulo.BorderWidth = 0;
				clTablaTitulo.VerticalAlignment = Element.ALIGN_MIDDLE;
				clTablaTitulo.AddElement(tblTitulo);

				PdfPTable tblEncabezado = new PdfPTable(3);
				tblEncabezado.WidthPercentage = 100;
				float[] widths = new float[] { 20f, 60f, 20f };
				tblEncabezado.SetWidths(widths);

				tblEncabezado.AddCell(clLogoSupIzq);
				tblEncabezado.AddCell(clTablaTitulo);
				tblEncabezado.AddCell(clLogoSupDer);

				docAceMed.Add(tblEncabezado);

				#endregion

				#region emision - revision - codigo
				Paragraph paragraphemision = new Paragraph(new Phrase("EMISION", fonEiqueta));
				paragraphemision.Alignment = Element.ALIGN_CENTER;

				PdfPCell clEmision = new PdfPCell();
				clEmision.BorderWidth = 0;
				clEmision.AddElement(paragraphemision);

				Paragraph paragrarevision = new Paragraph(new Phrase("REVISION", fonEiqueta));
				paragrarevision.Alignment = Element.ALIGN_CENTER;

				PdfPCell clrevision = new PdfPCell();
				clrevision.BorderWidth = 0;
				clrevision.AddElement(paragrarevision);

				Paragraph paragracodigo = new Paragraph(new Phrase("CODIGO", fonEiqueta));
				paragracodigo.Alignment = Element.ALIGN_CENTER;

				PdfPCell clcodigo = new PdfPCell();
				clcodigo.BorderWidth = 0;
				clcodigo.AddElement(paragracodigo);

				Paragraph paragraphemision_b = new Paragraph(new Phrase(DateTime.Now.Year.ToString(), fonEiqueta));
				paragraphemision_b.Alignment = Element.ALIGN_CENTER;

				PdfPCell clEmision_b = new PdfPCell();
				clEmision_b.BorderWidth = 0;
				clEmision_b.VerticalAlignment = Element.ALIGN_MIDDLE;
				clEmision_b.UseAscender = true;
				clEmision_b.AddElement(paragraphemision_b);

				Paragraph paragrarevision_b = new Paragraph(new Phrase("1.1", fonEiqueta));
				paragrarevision_b.Alignment = Element.ALIGN_CENTER;

				PdfPCell clrevision_b = new PdfPCell();
				clrevision_b.BorderWidth = 0;
				clrevision_b.VerticalAlignment = Element.ALIGN_MIDDLE;
				clrevision_b.UseAscender = true;
				clrevision_b.AddElement(paragrarevision_b);

				Paragraph paragracodigo_b = new Paragraph(new Phrase("CECCC/DMT/08", fonEiqueta));
				paragracodigo_b.Alignment = Element.ALIGN_CENTER;

				PdfPCell clcodigo_b = new PdfPCell();
				clcodigo_b.BorderWidth = 0;
				clcodigo_b.VerticalAlignment = Element.ALIGN_MIDDLE;
				clcodigo_b.UseAscender = true;
				clcodigo_b.AddElement(paragracodigo_b);

				PdfPCell clLinea = new PdfPCell(new Phrase("", fontDato)) { Colspan = 3 };
				clLinea.BorderWidthBottom = 1;
				clLinea.BorderWidthTop = 0;
				clLinea.BorderWidthLeft = 0;
				clLinea.BorderWidthRight = 0;

				PdfPTable tblemision = new PdfPTable(3);
				tblemision.WidthPercentage = 100;
				float[] widthsemision = new float[] { 20f, 60f, 20f };
				tblemision.SetWidths(widthsemision);

				tblemision.AddCell(clLinea);

				tblemision.AddCell(clEmision);
				tblemision.AddCell(clrevision);
				tblemision.AddCell(clcodigo);

				tblemision.AddCell(clEmision_b);
				tblemision.AddCell(clrevision_b);
				tblemision.AddCell(clcodigo_b);

				docAceMed.Add(tblemision);
				#endregion

				#region certificacion acreditacion
				PdfPCell celCertificacion = new PdfPCell(new Phrase("Certificación No. " + datosC3.certifica, fontDato));
				celCertificacion.HorizontalAlignment = Element.ALIGN_LEFT;
				celCertificacion.BorderWidth = 0;

				PdfPCell celAcreditacion = new PdfPCell(new Phrase("Acreditación No. " + datosC3.acredita, fontDato));
				celAcreditacion.HorizontalAlignment = Element.ALIGN_RIGHT;
				celAcreditacion.BorderWidth = 0;

				PdfPTable tblAcrCer = new PdfPTable(2)
				{
					TotalWidth = 560,
					LockedWidth = true
				};
				float[] widthAcreditacion = new float[2] { 280, 280 };
				tblAcrCer.SetWidths(widthAcreditacion);
				tblAcrCer.HorizontalAlignment = 0;
				tblAcrCer.SpacingBefore = 5f;
				tblAcrCer.SpacingAfter = 5f;
				tblAcrCer.DefaultCell.Border = 0;

				tblAcrCer.AddCell(celCertificacion);
				tblAcrCer.AddCell(celAcreditacion);

				docAceMed.Add(tblAcrCer);

				#endregion

				Paragraph laFecha = new Paragraph(new Phrase("Tuxtla Gutiérrez; Chiapas a " + DateTime.Now.ToString("dd MMMM yyyy"), fontDato));
				laFecha.Alignment = Element.ALIGN_RIGHT;
				laFecha.Add(Chunk.NEWLINE);
				docAceMed.Add(laFecha);

				Paragraph DatosPersonales = new Paragraph(new Phrase("Datos personales ", fonEiqueta));
				DatosPersonales.Alignment = Element.ALIGN_LEFT;
				DatosPersonales.Add(Chunk.NEWLINE);
				docAceMed.Add(DatosPersonales);

				#region tabla datos personales
				PdfPTable tblDatosPersonales = new PdfPTable(6)
				{
					TotalWidth = 560,
					LockedWidth = true
				};
				float[] valuesDatosPersonales = new float[6] { 120, 90, 90, 80, 90, 90 };
				tblDatosPersonales.SetWidths(valuesDatosPersonales);
				tblDatosPersonales.HorizontalAlignment = 0;
				tblDatosPersonales.SpacingBefore = 5f;
				tblDatosPersonales.SpacingAfter = 5f;
				tblDatosPersonales.DefaultCell.Border = 0;

				PdfPCell clNombre = new PdfPCell(new Phrase("Nombre:", fonEiqueta));
				clNombre.BorderWidth = 0;
				clNombre.HorizontalAlignment = Element.ALIGN_LEFT;
				clNombre.VerticalAlignment = Element.ALIGN_MIDDLE;
				clNombre.UseAscender = true;
				clNombre.FixedHeight = 20f;

				PdfPCell clNombreDato = new PdfPCell(new Phrase(aceMed[id].evaluado, fontDato)) { Colspan = 5 };
				clNombreDato.BorderWidthBottom = 1;
				clNombreDato.BorderWidthLeft = 0;
				clNombreDato.BorderWidthRight = 0;
				clNombreDato.BorderWidthTop = 0;
				clNombreDato.HorizontalAlignment = Element.ALIGN_LEFT;
				clNombreDato.VerticalAlignment = Element.ALIGN_MIDDLE;
				clNombreDato.UseAscender = true;

				PdfPCell clRfc = new PdfPCell(new Phrase("RFC:", fonEiqueta));
				clRfc.BorderWidth = 0;
				clRfc.HorizontalAlignment = Element.ALIGN_LEFT;
				clRfc.VerticalAlignment = Element.ALIGN_MIDDLE;
				clRfc.UseAscender = true;
				clRfc.FixedHeight = 20f;

				PdfPCell clRFCDatos = new PdfPCell(new Phrase(aceMed[id].rfc, fontDato));
				clRFCDatos.BorderWidthBottom = 1;
				clRFCDatos.BorderWidthLeft = 0;
				clRFCDatos.BorderWidthRight = 0;
				clRFCDatos.BorderWidthTop = 0;
				clRFCDatos.HorizontalAlignment = Element.ALIGN_CENTER;
				clRFCDatos.VerticalAlignment = Element.ALIGN_MIDDLE;
				clRFCDatos.UseAscender = true;

				PdfPCell clEdad = new PdfPCell(new Phrase("Edad:", fonEiqueta));
				clEdad.BorderWidth = 0;
				clEdad.HorizontalAlignment = Element.ALIGN_CENTER;
				clEdad.VerticalAlignment = Element.ALIGN_MIDDLE;
				clEdad.UseAscender = true;

				PdfPCell clEdadDatos = new PdfPCell(new Phrase(aceMed[id].edad.ToString(), fontDato));
				clEdadDatos.BorderWidthBottom = 1;
				clEdadDatos.BorderWidthLeft = 0;
				clEdadDatos.BorderWidthRight = 0;
				clEdadDatos.BorderWidthTop = 0;
				clEdadDatos.HorizontalAlignment = Element.ALIGN_CENTER;
				clEdadDatos.VerticalAlignment = Element.ALIGN_MIDDLE;
				clEdadDatos.UseAscender = true;

				PdfPCell clGenero = new PdfPCell(new Phrase("Género:", fonEiqueta));
				clGenero.BorderWidth = 0;
				clGenero.HorizontalAlignment = Element.ALIGN_CENTER;
				clGenero.VerticalAlignment = Element.ALIGN_MIDDLE;
				clGenero.UseAscender = true;

				PdfPCell clGeneroDatos = new PdfPCell(new Phrase(aceMed[id].sexo, fontDato));
				clGeneroDatos.BorderWidthBottom = 1;
				clGeneroDatos.BorderWidthLeft = 0;
				clGeneroDatos.BorderWidthRight = 0;
				clGeneroDatos.BorderWidthTop = 0;
				clGeneroDatos.HorizontalAlignment = Element.ALIGN_CENTER;
				clGeneroDatos.VerticalAlignment = Element.ALIGN_MIDDLE;
				clGeneroDatos.UseAscender = true;

				PdfPCell clDependencia = new PdfPCell(new Phrase("Dependencia:", fonEiqueta));
				clDependencia.BorderWidth = 0;
				clDependencia.HorizontalAlignment = Element.ALIGN_LEFT;
				clDependencia.VerticalAlignment = Element.ALIGN_MIDDLE;
				clDependencia.UseAscender = true;
				clDependencia.FixedHeight = 20f;

				PdfPCell clDependenciaDato = new PdfPCell(new Phrase(aceMed[id].dependencia, fontDato)) { Colspan = 5 };
				clDependenciaDato.BorderWidthBottom = 1;
				clDependenciaDato.BorderWidthLeft = 0;
				clDependenciaDato.BorderWidthRight = 0;
				clDependenciaDato.BorderWidthTop = 0;
				clDependenciaDato.HorizontalAlignment = Element.ALIGN_LEFT;
				clDependenciaDato.VerticalAlignment = Element.ALIGN_MIDDLE;
				clDependenciaDato.UseAscender = true;

				PdfPCell clAdscripcion = new PdfPCell(new Phrase("Adscripción:", fonEiqueta));
				clAdscripcion.BorderWidth = 0;
				clAdscripcion.HorizontalAlignment = Element.ALIGN_LEFT;
				clAdscripcion.VerticalAlignment = Element.ALIGN_MIDDLE;
				clAdscripcion.UseAscender = true;
				clAdscripcion.FixedHeight = 20f;

				PdfPCell clAdscripcionDato = new PdfPCell(new Phrase(aceMed[id].adscripcion, fontDato)) { Colspan = 5 };
				clAdscripcionDato.BorderWidthBottom = 1;
				clAdscripcionDato.BorderWidthLeft = 0;
				clAdscripcionDato.BorderWidthRight = 0;
				clAdscripcionDato.BorderWidthTop = 0;
				clAdscripcionDato.HorizontalAlignment = Element.ALIGN_LEFT;
				clAdscripcionDato.VerticalAlignment = Element.ALIGN_MIDDLE;
				clAdscripcionDato.UseAscender = true;

				PdfPCell clPuesto = new PdfPCell(new Phrase("Puesto:", fonEiqueta));
				clPuesto.BorderWidth = 0;
				clPuesto.HorizontalAlignment = Element.ALIGN_LEFT;
				clPuesto.VerticalAlignment = Element.ALIGN_MIDDLE;
				clPuesto.UseAscender = true;
				clPuesto.FixedHeight = 20f;

				PdfPCell clPuestoDato = new PdfPCell(new Phrase(aceMed[id].puesto, fontDato)) { Colspan = 5 };
				clPuestoDato.BorderWidthBottom = 1;
				clPuestoDato.BorderWidthLeft = 0;
				clPuestoDato.BorderWidthRight = 0;
				clPuestoDato.BorderWidthTop = 0;
				clPuestoDato.HorizontalAlignment = Element.ALIGN_LEFT;
				clPuestoDato.VerticalAlignment = Element.ALIGN_MIDDLE;
				clPuestoDato.UseAscender = true;

				PdfPCell clEvaluacion = new PdfPCell(new Phrase("Tipo de evaluación:", fonEiqueta));
				clEvaluacion.BorderWidth = 0;
				clEvaluacion.HorizontalAlignment = Element.ALIGN_LEFT;
				clEvaluacion.VerticalAlignment = Element.ALIGN_MIDDLE;
				clEvaluacion.UseAscender = true;
				clEvaluacion.FixedHeight = 20f;

				PdfPCell clEvaluacionDato = new PdfPCell(new Phrase(aceMed[id].evaluacion, fontDato)) { Colspan = 5 };
				clEvaluacionDato.BorderWidthBottom = 1;
				clEvaluacionDato.BorderWidthLeft = 0;
				clEvaluacionDato.BorderWidthRight = 0;
				clEvaluacionDato.BorderWidthTop = 0;
				clEvaluacionDato.HorizontalAlignment = Element.ALIGN_LEFT;
				clEvaluacionDato.VerticalAlignment = Element.ALIGN_MIDDLE;
				clEvaluacionDato.UseAscender = true;

				PdfPCell clLugar = new PdfPCell(new Phrase("Lugar de evaluación:", fonEiqueta));
				clLugar.BorderWidth = 0;
				clLugar.HorizontalAlignment = Element.ALIGN_LEFT;
				clLugar.VerticalAlignment = Element.ALIGN_MIDDLE;
				clLugar.UseAscender = true;
				clLugar.FixedHeight = 20f;

				PdfPCell clLugarDato = new PdfPCell(new Phrase("CENTRO ESTATAL DE CONTROL DE CONFIANZA CERTIFICADO", fontDato)) { Colspan = 5 };
				clLugarDato.BorderWidthBottom = 1;
				clLugarDato.BorderWidthLeft = 0;
				clLugarDato.BorderWidthRight = 0;
				clLugarDato.BorderWidthTop = 0;
				clLugarDato.HorizontalAlignment = Element.ALIGN_LEFT;
				clLugarDato.VerticalAlignment = Element.ALIGN_MIDDLE;
				clLugarDato.UseAscender = true;

				tblDatosPersonales.AddCell(clNombre);
				tblDatosPersonales.AddCell(clNombreDato);

				tblDatosPersonales.AddCell(clRfc);
				tblDatosPersonales.AddCell(clRFCDatos);
				tblDatosPersonales.AddCell(clEdad);
				tblDatosPersonales.AddCell(clEdadDatos);
				tblDatosPersonales.AddCell(clGenero);
				tblDatosPersonales.AddCell(clGeneroDatos);

				tblDatosPersonales.AddCell(clDependencia);
				tblDatosPersonales.AddCell(clDependenciaDato);

				tblDatosPersonales.AddCell(clAdscripcion);
				tblDatosPersonales.AddCell(clAdscripcionDato);

				tblDatosPersonales.AddCell(clPuesto);
				tblDatosPersonales.AddCell(clPuestoDato);

				tblDatosPersonales.AddCell(clEvaluacion);
				tblDatosPersonales.AddCell(clEvaluacionDato);

				tblDatosPersonales.AddCell(clLugar);
				tblDatosPersonales.AddCell(clLugarDato);

				docAceMed.Add(tblDatosPersonales);
				#endregion

				#region Fundamento
				Paragraph fundamento_a = new Paragraph();
				fundamento_a.Alignment = Element.ALIGN_JUSTIFIED;
				fundamento_a.Add(new Phrase("Con fundamento en el artículo 21 de la Constitución Política de los Estados Unidos Mexicanos; artículo 7 fracción VI y 40 fracción XV de la Ley General del Sistema Nacional de Seguridad Publica, artículo 7 fracción VI, y 33 fracción XIII y 56 fracción I y II de la Ley del Sistema Estatal de Seguridad Publica y artículo 3° del Reglamento Interior del Centro Estatal de Control de Confianza Certificado del Estado de Chiapas, otorgo la más amplia autorización al personal adscrito a la Dirección Médica y Toxicológico del Centro Estatal de Control de Confianza Certificado del Estado de Chiapas, para que realice mi examen médico.", fontDato));
				fundamento_a.Add(Chunk.NEWLINE); fundamento_a.Add(Chunk.NEWLINE);
				fundamento_a.Add(new Phrase("Declaro que me fue explicado la naturaleza y características del examen médico que consiste en un interrogatorio personal y familiar, así como una exploración física, por lo que estoy de acuerdo en retirar mi ropa y colocarme una bata,  y que en caso de tener tatuajes en cualquier parte de mi cuerpo se tome una fotografía donde se ubica para anexarlo al expediente.", fontDato));
				fundamento_a.Add(Chunk.NEWLINE); fundamento_a.Add(Chunk.NEWLINE);
				fundamento_a.Add(new Phrase("Bajo protesta de decir verdad, me someto a la evaluación médica de manera voluntaria y sin que medie presión alguna, toda vez que como servidor público en materia de seguridad es mi obligación; asi mismo, estoy conforme que el resultado se notifique al Titular de mi Dependencia de adscripción al ser considerado como información confidencial, por lo que no tengo inconveniente alguno en que los datos obtenidos durante el proceso de evaluación, así como los documentos se destruya en el momento que el Centro lo considere conveniente.", fontDato));
				fundamento_a.Add(Chunk.NEWLINE); fundamento_a.Add(Chunk.NEWLINE);

				docAceMed.Add(fundamento_a);
				#endregion

				#region firmas
				PdfPTable tblFirmaMedico = new PdfPTable(4)
				{
					TotalWidth = 560,
					LockedWidth = true
				};
				float[] valuesFirmaMedico = new float[4] { 350, 55, 100, 55 };
				tblFirmaMedico.SetWidths(valuesFirmaMedico);
				tblFirmaMedico.HorizontalAlignment = 0;
				tblFirmaMedico.SpacingBefore = 5f;
				tblFirmaMedico.SpacingAfter = 5f;
				tblFirmaMedico.DefaultCell.Border = 0;

				PdfPCell clFirmaNombre = new PdfPCell();
				clFirmaNombre.BorderWidthBottom = 1;
				clFirmaNombre.BorderWidthLeft = 0;
				clFirmaNombre.BorderWidthRight = 0;
				clFirmaNombre.BorderWidthTop = 0;
				clFirmaNombre.FixedHeight = 80f;

				PdfPCell clVacio_a = new PdfPCell();
				clVacio_a.BorderWidth = 0;

				PdfPCell clHuela = new PdfPCell();
				clHuela.BorderWidthBottom = 1;
				clHuela.BorderWidthLeft = 1;
				clHuela.BorderWidthRight = 1;
				clHuela.BorderWidthTop = 1;

				PdfPCell clVacio_b = new PdfPCell();
				clVacio_b.BorderWidth = 0;

				PdfPCell clFirmaNombre_b = new PdfPCell(new Phrase(aceMed[id].evaluado, fontDato));
				clFirmaNombre_b.HorizontalAlignment = Element.ALIGN_CENTER;
				clFirmaNombre_b.BorderWidth = 0;

				PdfPCell clVacio_bb = new PdfPCell();
				clVacio_bb.BorderWidth = 0;

				PdfPCell clHuela_b = new PdfPCell(new Phrase("Huella digital del evaluado", fontDatosmall));
				clHuela_b.HorizontalAlignment = Element.ALIGN_CENTER;
				clHuela_b.BorderWidth = 0;

				PdfPCell clVacio_bbb = new PdfPCell();
				clVacio_bbb.BorderWidth = 0;

				tblFirmaMedico.AddCell(clFirmaNombre);
				tblFirmaMedico.AddCell(clVacio_a);
				tblFirmaMedico.AddCell(clHuela);
				tblFirmaMedico.AddCell(clVacio_b);

				tblFirmaMedico.AddCell(clFirmaNombre_b);
				tblFirmaMedico.AddCell(clVacio_bb);
				tblFirmaMedico.AddCell(clHuela_b);
				tblFirmaMedico.AddCell(clVacio_bbb);

				docAceMed.Add(tblFirmaMedico);
				#endregion

				#region pie pagina
				Paragraph pie = new Paragraph(new Phrase("Toda información contenida en este formato está clasificada como reservada y confidencial, de conformidad con lo dispuesto por los artículos 125 fracción I, 128, 133 de la Ley de Transparencia y Acceso a la Información Pública del Estado de Chiapas.", fontDatosmall));
				pie.Alignment = Element.ALIGN_CENTER;

				docAceMed.Add(pie);
				#endregion

				#region fin
				PdfPTable fin = new PdfPTable(2)
				{
					TotalWidth = 560,
					LockedWidth = true
				};
				float[] final = new float[2] { 280, 280 };
				fin.SetWidths(final);
				fin.HorizontalAlignment = 0;
				fin.SpacingBefore = 15f;
				fin.SpacingAfter = 5f;
				fin.DefaultCell.Border = 0;

				PdfPCell clfolio = new PdfPCell(new Phrase(aceMed[id].folio, fonEiqueta));
				clfolio.BorderWidth = 0;
				clfolio.HorizontalAlignment = Element.ALIGN_LEFT;

				PdfPCell clCodigo_c = new PdfPCell(new Phrase(aceMed[id].codigoevaluado, fonEiqueta));
				clCodigo_c.BorderWidth = 0;
				clCodigo_c.HorizontalAlignment = Element.ALIGN_RIGHT;

				fin.AddCell(clfolio);
				fin.AddCell(clCodigo_c);

				docAceMed.Add(fin);
				#endregion

				docAceMed.NewPage();
			}

			docAceMed.Close();
			byte[] byteStream = msAceMed.ToArray();
			msAceMed = new MemoryStream();
			msAceMed.Write(byteStream, 0, byteStream.Length);
			msAceMed.Position = 0;

			return new FileStreamResult(msAceMed, "application/pdf");
		}

		public IActionResult aceptacionToxicologico(string fecha)
		{
			var datosC3 = repo.Get<ConsultasModel>("sp_general_obtener_certificacion_acreditacion").FirstOrDefault();
			var aceTox = repo.Getdosparam1<ConsultasModel>("sp_medicos_rep_cabeceras_fecha", new { @fecha = fecha }).ToList();

			var _totalAceptacion = aceTox.Count();

			var fonEiqueta = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK);
			var fontDato = FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK);
			var fontDatosmall = FontFactory.GetFont("Arial", 9, Font.NORMAL, BaseColor.BLACK);

			MemoryStream msTox = new MemoryStream();
			Document docAceTox = new Document(PageSize.LETTER, 30f, 20f, 20f, 40f);
			PdfWriter pwAceTox = PdfWriter.GetInstance(docAceTox, msTox);
			docAceTox.Open();

			for (int id = 0; id < _totalAceptacion; id++)
			{
				#region encabezado
				//-------------------------------------------------------------------------------------------------------- 1a linea
				string imageizq = @"C:/inetpub/wwwroot/fotoUser/gobedohor.png";
				iTextSharp.text.Image jpgSupIzq = iTextSharp.text.Image.GetInstance(imageizq);
				jpgSupIzq.ScaleToFit(80f, 80f);

				PdfPCell clLogoSupIzq = new PdfPCell();
				clLogoSupIzq.BorderWidth = 0;
				clLogoSupIzq.VerticalAlignment = Element.ALIGN_BOTTOM;
				clLogoSupIzq.AddElement(jpgSupIzq);

				string imageder = @"C:/inetpub/wwwroot/fotoUser/nuevoCeccc.png";
				iTextSharp.text.Image jpgSupDer = iTextSharp.text.Image.GetInstance(imageder);
				jpgSupDer.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
				jpgSupDer.ScaleToFit(100f, 100f);

				PdfPCell clLogoSupDer = new PdfPCell();
				clLogoSupDer.BorderWidth = 0;
				clLogoSupDer.VerticalAlignment = Element.ALIGN_BOTTOM;
				clLogoSupDer.AddElement(jpgSupDer);

				Chunk chkTit = new Chunk("Dirección Médica y Toxicológica", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
				Paragraph paragraph = new Paragraph();
				paragraph.Alignment = Element.ALIGN_CENTER;
				paragraph.Add(chkTit);

				Chunk chkSub = new Chunk("Aceptación de exámen toxicológico", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
				Paragraph paragraph1 = new Paragraph();
				paragraph1.Alignment = Element.ALIGN_CENTER;
				paragraph1.Add(chkSub);

				PdfPCell clTitulo = new PdfPCell();
				clTitulo.BorderWidth = 0;
				clTitulo.AddElement(paragraph);

				PdfPCell clSubTit = new PdfPCell();
				clSubTit.BorderWidth = 0;
				clSubTit.AddElement(paragraph1);

				PdfPTable tblTitulo = new PdfPTable(1);
				tblTitulo.WidthPercentage = 100;
				tblTitulo.AddCell(clTitulo);
				tblTitulo.AddCell(clSubTit);

				PdfPCell clTablaTitulo = new PdfPCell();
				clTablaTitulo.BorderWidth = 0;
				clTablaTitulo.VerticalAlignment = Element.ALIGN_MIDDLE;
				clTablaTitulo.AddElement(tblTitulo);

				PdfPTable tblEncabezado = new PdfPTable(3);
				tblEncabezado.WidthPercentage = 100;
				float[] widths = new float[] { 20f, 60f, 20f };
				tblEncabezado.SetWidths(widths);

				tblEncabezado.AddCell(clLogoSupIzq);
				tblEncabezado.AddCell(clTablaTitulo);
				tblEncabezado.AddCell(clLogoSupDer);

				docAceTox.Add(tblEncabezado);

				#endregion

				#region emision - revision - codigo
				Paragraph paragraphemision = new Paragraph(new Phrase("EMISION", fonEiqueta));
				paragraphemision.Alignment = Element.ALIGN_CENTER;

				PdfPCell clEmision = new PdfPCell();
				clEmision.BorderWidth = 0;
				clEmision.AddElement(paragraphemision);

				Paragraph paragrarevision = new Paragraph(new Phrase("REVISION", fonEiqueta));
				paragrarevision.Alignment = Element.ALIGN_CENTER;

				PdfPCell clrevision = new PdfPCell();
				clrevision.BorderWidth = 0;
				clrevision.AddElement(paragrarevision);

				Paragraph paragracodigo = new Paragraph(new Phrase("CODIGO", fonEiqueta));
				paragracodigo.Alignment = Element.ALIGN_CENTER;

				PdfPCell clcodigo = new PdfPCell();
				clcodigo.BorderWidth = 0;
				clcodigo.AddElement(paragracodigo);

				Paragraph paragraphemision_b = new Paragraph(new Phrase(DateTime.Now.Year.ToString(), fonEiqueta));
				paragraphemision_b.Alignment = Element.ALIGN_CENTER;

				PdfPCell clEmision_b = new PdfPCell();
				clEmision_b.BorderWidth = 0;
				clEmision_b.VerticalAlignment = Element.ALIGN_MIDDLE;
				clEmision_b.UseAscender = true;
				clEmision_b.AddElement(paragraphemision_b);

				Paragraph paragrarevision_b = new Paragraph(new Phrase("1.1", fonEiqueta));
				paragrarevision_b.Alignment = Element.ALIGN_CENTER;

				PdfPCell clrevision_b = new PdfPCell();
				clrevision_b.BorderWidth = 0;
				clrevision_b.VerticalAlignment = Element.ALIGN_MIDDLE;
				clrevision_b.UseAscender = true;
				clrevision_b.AddElement(paragrarevision_b);

				Paragraph paragracodigo_b = new Paragraph(new Phrase("CECCC/DMT/01", fonEiqueta));
				paragracodigo_b.Alignment = Element.ALIGN_CENTER;

				PdfPCell clcodigo_b = new PdfPCell();
				clcodigo_b.BorderWidth = 0;
				clcodigo_b.VerticalAlignment = Element.ALIGN_MIDDLE;
				clcodigo_b.UseAscender = true;
				clcodigo_b.AddElement(paragracodigo_b);

				PdfPCell clLinea = new PdfPCell(new Phrase("", fontDato)) { Colspan = 3 };
				clLinea.BorderWidthBottom = 1;
				clLinea.BorderWidthTop = 0;
				clLinea.BorderWidthLeft = 0;
				clLinea.BorderWidthRight = 0;

				PdfPTable tblemision = new PdfPTable(3);
				tblemision.WidthPercentage = 100;
				float[] widthsemision = new float[] { 20f, 60f, 20f };
				tblemision.SetWidths(widthsemision);

				tblemision.AddCell(clLinea);

				tblemision.AddCell(clEmision);
				tblemision.AddCell(clrevision);
				tblemision.AddCell(clcodigo);

				tblemision.AddCell(clEmision_b);
				tblemision.AddCell(clrevision_b);
				tblemision.AddCell(clcodigo_b);

				docAceTox.Add(tblemision);
				#endregion

				#region certificacion acreditacion
				PdfPCell celCertificacion = new PdfPCell(new Phrase("Certificación No. " + datosC3.certifica, fontDato));
				celCertificacion.HorizontalAlignment = Element.ALIGN_LEFT;
				celCertificacion.BorderWidth = 0;

				PdfPCell celAcreditacion = new PdfPCell(new Phrase("Acreditación No. " + datosC3.acredita, fontDato));
				celAcreditacion.HorizontalAlignment = Element.ALIGN_RIGHT;
				celAcreditacion.BorderWidth = 0;

				PdfPTable tblAcrCer = new PdfPTable(2)
				{
					TotalWidth = 560,
					LockedWidth = true
				};
				float[] widthAcreditacion = new float[2] { 280, 280 };
				tblAcrCer.SetWidths(widthAcreditacion);
				tblAcrCer.HorizontalAlignment = 0;
				tblAcrCer.SpacingBefore = 5f;
				tblAcrCer.SpacingAfter = 5f;
				tblAcrCer.DefaultCell.Border = 0;

				tblAcrCer.AddCell(celCertificacion);
				tblAcrCer.AddCell(celAcreditacion);

				docAceTox.Add(tblAcrCer);

				#endregion

				Paragraph laFecha = new Paragraph(new Phrase("Tuxtla Gutiérrez; Chiapas a " + DateTime.Now.ToString("dd MMMM yyyy"), fontDato));
				laFecha.Alignment = Element.ALIGN_RIGHT;
				laFecha.Add(Chunk.NEWLINE);
				docAceTox.Add(laFecha);

				Paragraph DatosPersonales = new Paragraph(new Phrase("Datos personales ", fonEiqueta));
				DatosPersonales.Alignment = Element.ALIGN_LEFT;
				DatosPersonales.Add(Chunk.NEWLINE);
				docAceTox.Add(DatosPersonales);

				#region tabla datos personales
				PdfPTable tblDatosPersonales = new PdfPTable(6)
				{
					TotalWidth = 560,
					LockedWidth = true
				};
				float[] valuesDatosPersonales = new float[6] { 120, 90, 90, 80, 90, 90 };
				tblDatosPersonales.SetWidths(valuesDatosPersonales);
				tblDatosPersonales.HorizontalAlignment = 0;
				tblDatosPersonales.SpacingBefore = 5f;
				tblDatosPersonales.SpacingAfter = 5f;
				tblDatosPersonales.DefaultCell.Border = 0;

				PdfPCell clNombre = new PdfPCell(new Phrase("Nombre:", fonEiqueta));
				clNombre.BorderWidth = 0;
				clNombre.HorizontalAlignment = Element.ALIGN_LEFT;
				clNombre.VerticalAlignment = Element.ALIGN_MIDDLE;
				clNombre.UseAscender = true;
				clNombre.FixedHeight = 20f;

				PdfPCell clNombreDato = new PdfPCell(new Phrase(aceTox[id].evaluado, fontDato)) { Colspan = 5 };
				clNombreDato.BorderWidthBottom = 1;
				clNombreDato.BorderWidthLeft = 0;
				clNombreDato.BorderWidthRight = 0;
				clNombreDato.BorderWidthTop = 0;
				clNombreDato.HorizontalAlignment = Element.ALIGN_LEFT;
				clNombreDato.VerticalAlignment = Element.ALIGN_MIDDLE;
				clNombreDato.UseAscender = true;

				PdfPCell clRfc = new PdfPCell(new Phrase("RFC:", fonEiqueta));
				clRfc.BorderWidth = 0;
				clRfc.HorizontalAlignment = Element.ALIGN_LEFT;
				clRfc.VerticalAlignment = Element.ALIGN_MIDDLE;
				clRfc.UseAscender = true;
				clRfc.FixedHeight = 20f;

				PdfPCell clRFCDatos = new PdfPCell(new Phrase(aceTox[id].rfc, fontDato));
				clRFCDatos.BorderWidthBottom = 1;
				clRFCDatos.BorderWidthLeft = 0;
				clRFCDatos.BorderWidthRight = 0;
				clRFCDatos.BorderWidthTop = 0;
				clRFCDatos.HorizontalAlignment = Element.ALIGN_CENTER;
				clRFCDatos.VerticalAlignment = Element.ALIGN_MIDDLE;
				clRFCDatos.UseAscender = true;

				PdfPCell clEdad = new PdfPCell(new Phrase("Edad:", fonEiqueta));
				clEdad.BorderWidth = 0;
				clEdad.HorizontalAlignment = Element.ALIGN_CENTER;
				clEdad.VerticalAlignment = Element.ALIGN_MIDDLE;
				clEdad.UseAscender = true;

				PdfPCell clEdadDatos = new PdfPCell(new Phrase(aceTox[id].edad.ToString(), fontDato));
				clEdadDatos.BorderWidthBottom = 1;
				clEdadDatos.BorderWidthLeft = 0;
				clEdadDatos.BorderWidthRight = 0;
				clEdadDatos.BorderWidthTop = 0;
				clEdadDatos.HorizontalAlignment = Element.ALIGN_CENTER;
				clEdadDatos.VerticalAlignment = Element.ALIGN_MIDDLE;
				clEdadDatos.UseAscender = true;

				PdfPCell clGenero = new PdfPCell(new Phrase("Género:", fonEiqueta));
				clGenero.BorderWidth = 0;
				clGenero.HorizontalAlignment = Element.ALIGN_CENTER;
				clGenero.VerticalAlignment = Element.ALIGN_MIDDLE;
				clGenero.UseAscender = true;

				PdfPCell clGeneroDatos = new PdfPCell(new Phrase(aceTox[id].sexo, fontDato));
				clGeneroDatos.BorderWidthBottom = 1;
				clGeneroDatos.BorderWidthLeft = 0;
				clGeneroDatos.BorderWidthRight = 0;
				clGeneroDatos.BorderWidthTop = 0;
				clGeneroDatos.HorizontalAlignment = Element.ALIGN_CENTER;
				clGeneroDatos.VerticalAlignment = Element.ALIGN_MIDDLE;
				clGeneroDatos.UseAscender = true;

				PdfPCell clDependencia = new PdfPCell(new Phrase("Dependencia:", fonEiqueta));
				clDependencia.BorderWidth = 0;
				clDependencia.HorizontalAlignment = Element.ALIGN_LEFT;
				clDependencia.VerticalAlignment = Element.ALIGN_MIDDLE;
				clDependencia.UseAscender = true;
				clDependencia.FixedHeight = 20f;

				PdfPCell clDependenciaDato = new PdfPCell(new Phrase(aceTox[id].dependencia, fontDato)) { Colspan = 5 };
				clDependenciaDato.BorderWidthBottom = 1;
				clDependenciaDato.BorderWidthLeft = 0;
				clDependenciaDato.BorderWidthRight = 0;
				clDependenciaDato.BorderWidthTop = 0;
				clDependenciaDato.HorizontalAlignment = Element.ALIGN_LEFT;
				clDependenciaDato.VerticalAlignment = Element.ALIGN_MIDDLE;
				clDependenciaDato.UseAscender = true;

				PdfPCell clAdscripcion = new PdfPCell(new Phrase("Adscripción:", fonEiqueta));
				clAdscripcion.BorderWidth = 0;
				clAdscripcion.HorizontalAlignment = Element.ALIGN_LEFT;
				clAdscripcion.VerticalAlignment = Element.ALIGN_MIDDLE;
				clAdscripcion.UseAscender = true;
				clAdscripcion.FixedHeight = 20f;

				PdfPCell clAdscripcionDato = new PdfPCell(new Phrase(aceTox[id].adscripcion, fontDato)) { Colspan = 5 };
				clAdscripcionDato.BorderWidthBottom = 1;
				clAdscripcionDato.BorderWidthLeft = 0;
				clAdscripcionDato.BorderWidthRight = 0;
				clAdscripcionDato.BorderWidthTop = 0;
				clAdscripcionDato.HorizontalAlignment = Element.ALIGN_LEFT;
				clAdscripcionDato.VerticalAlignment = Element.ALIGN_MIDDLE;
				clAdscripcionDato.UseAscender = true;

				PdfPCell clPuesto = new PdfPCell(new Phrase("Puesto:", fonEiqueta));
				clPuesto.BorderWidth = 0;
				clPuesto.HorizontalAlignment = Element.ALIGN_LEFT;
				clPuesto.VerticalAlignment = Element.ALIGN_MIDDLE;
				clPuesto.UseAscender = true;
				clPuesto.FixedHeight = 20f;

				PdfPCell clPuestoDato = new PdfPCell(new Phrase(aceTox[id].puesto, fontDato)) { Colspan = 5 };
				clPuestoDato.BorderWidthBottom = 1;
				clPuestoDato.BorderWidthLeft = 0;
				clPuestoDato.BorderWidthRight = 0;
				clPuestoDato.BorderWidthTop = 0;
				clPuestoDato.HorizontalAlignment = Element.ALIGN_LEFT;
				clPuestoDato.VerticalAlignment = Element.ALIGN_MIDDLE;
				clPuestoDato.UseAscender = true;

				PdfPCell clEvaluacion = new PdfPCell(new Phrase("Tipo de evaluación:", fonEiqueta));
				clEvaluacion.BorderWidth = 0;
				clEvaluacion.HorizontalAlignment = Element.ALIGN_LEFT;
				clEvaluacion.VerticalAlignment = Element.ALIGN_MIDDLE;
				clEvaluacion.UseAscender = true;
				clEvaluacion.FixedHeight = 20f;

				PdfPCell clEvaluacionDato = new PdfPCell(new Phrase(aceTox[id].evaluacion, fontDato)) { Colspan = 5 };
				clEvaluacionDato.BorderWidthBottom = 1;
				clEvaluacionDato.BorderWidthLeft = 0;
				clEvaluacionDato.BorderWidthRight = 0;
				clEvaluacionDato.BorderWidthTop = 0;
				clEvaluacionDato.HorizontalAlignment = Element.ALIGN_LEFT;
				clEvaluacionDato.VerticalAlignment = Element.ALIGN_MIDDLE;
				clEvaluacionDato.UseAscender = true;

				PdfPCell clLugar = new PdfPCell(new Phrase("Lugar de evaluación:", fonEiqueta));
				clLugar.BorderWidth = 0;
				clLugar.HorizontalAlignment = Element.ALIGN_LEFT;
				clLugar.VerticalAlignment = Element.ALIGN_MIDDLE;
				clLugar.UseAscender = true;
				clLugar.FixedHeight = 20f;

				PdfPCell clLugarDato = new PdfPCell(new Phrase("CENTRO ESTATAL DE CONTROL DE CONFIANZA CERTIFICADO", fontDato)) { Colspan = 5 };
				clLugarDato.BorderWidthBottom = 1;
				clLugarDato.BorderWidthLeft = 0;
				clLugarDato.BorderWidthRight = 0;
				clLugarDato.BorderWidthTop = 0;
				clLugarDato.HorizontalAlignment = Element.ALIGN_LEFT;
				clLugarDato.VerticalAlignment = Element.ALIGN_MIDDLE;
				clLugarDato.UseAscender = true;

				tblDatosPersonales.AddCell(clNombre);
				tblDatosPersonales.AddCell(clNombreDato);

				tblDatosPersonales.AddCell(clRfc);
				tblDatosPersonales.AddCell(clRFCDatos);
				tblDatosPersonales.AddCell(clEdad);
				tblDatosPersonales.AddCell(clEdadDatos);
				tblDatosPersonales.AddCell(clGenero);
				tblDatosPersonales.AddCell(clGeneroDatos);

				tblDatosPersonales.AddCell(clDependencia);
				tblDatosPersonales.AddCell(clDependenciaDato);

				tblDatosPersonales.AddCell(clAdscripcion);
				tblDatosPersonales.AddCell(clAdscripcionDato);

				tblDatosPersonales.AddCell(clPuesto);
				tblDatosPersonales.AddCell(clPuestoDato);

				tblDatosPersonales.AddCell(clEvaluacion);
				tblDatosPersonales.AddCell(clEvaluacionDato);

				tblDatosPersonales.AddCell(clLugar);
				tblDatosPersonales.AddCell(clLugarDato);

				docAceTox.Add(tblDatosPersonales);
				#endregion

				#region Fundamento
				Paragraph fundamento_a = new Paragraph();
				fundamento_a.Alignment = Element.ALIGN_JUSTIFIED;
				fundamento_a.Add(new Phrase("Con fundamento en el artículo 21 de la Constitución Política de los Estados Unidos Mexicanos; artículo 7 fracción VI y 40 fracción XV de la Ley General del Sistema Nacional de Seguridad Publica, artículo 7 fracción VI, y 33 fracción XIII y 56 fracción I y II de la Ley del Sistema Estatal de Seguridad Publica y artículo 3° del Reglamento Interior del Centro Estatal de Control de Confianza Certificado del Estado de Chiapas, otorgo la más amplia autorización al personal adscrito a la Dirección Médica y Toxicológico del Centro Estatal de Control de Confianza Certificado del Estado de Chiapas, para que realice mi examen toxicológico.", fontDato));
				fundamento_a.Add(Chunk.NEWLINE); fundamento_a.Add(Chunk.NEWLINE);
				fundamento_a.Add(new Phrase("Declaro que me fue explicado la naturaleza y características del examen toxicológico, autorizando que se me tome la muestra de orina bajo supervisión ocular.", fontDato));
				fundamento_a.Add(Chunk.NEWLINE); fundamento_a.Add(Chunk.NEWLINE);
				fundamento_a.Add(new Phrase("Bajo protesta de decir verdad, me someto a la evaluación toxicológica de manera voluntaria y sin que medie presión alguna, toda vez que como servidor público en material de seguridad es mi obligación; así mismo, estoy conforme que el resultado se notifique al Titular de mi Dependencia de adscripción al ser considerado como información confidencial, por lo que no tengo inconveniente alguno en que los datos obtenidos durante el proceso de evaluación, así como los documentos se destruya en el momento que el Centro lo considere conveniente.", fontDato));
				fundamento_a.Add(Chunk.NEWLINE); fundamento_a.Add(Chunk.NEWLINE);

				docAceTox.Add(fundamento_a);
				#endregion

				#region firmas
				PdfPTable tblFirmaMedico = new PdfPTable(4)
				{
					TotalWidth = 560,
					LockedWidth = true
				};
				float[] valuesFirmaMedico = new float[4] { 350, 55, 100, 55 };
				tblFirmaMedico.SetWidths(valuesFirmaMedico);
				tblFirmaMedico.HorizontalAlignment = 0;
				tblFirmaMedico.SpacingBefore = 1f;
				tblFirmaMedico.SpacingAfter = 1f;
				tblFirmaMedico.DefaultCell.Border = 0;

				PdfPCell clFirmaNombre = new PdfPCell();
				clFirmaNombre.BorderWidthBottom = 1;
				clFirmaNombre.BorderWidthLeft = 0;
				clFirmaNombre.BorderWidthRight = 0;
				clFirmaNombre.BorderWidthTop = 0;
				clFirmaNombre.FixedHeight = 80f;

				PdfPCell clVacio_a = new PdfPCell();
				clVacio_a.BorderWidth = 0;

				PdfPCell clHuela = new PdfPCell();
				clHuela.BorderWidthBottom = 1;
				clHuela.BorderWidthLeft = 1;
				clHuela.BorderWidthRight = 1;
				clHuela.BorderWidthTop = 1;

				PdfPCell clVacio_b = new PdfPCell();
				clVacio_b.BorderWidth = 0;

				PdfPCell clFirmaNombre_b = new PdfPCell(new Phrase(aceTox[id].evaluado, fontDato));
				clFirmaNombre_b.HorizontalAlignment = Element.ALIGN_CENTER;
				clFirmaNombre_b.BorderWidth = 0;

				PdfPCell clVacio_bb = new PdfPCell();
				clVacio_bb.BorderWidth = 0;

				PdfPCell clHuela_b = new PdfPCell(new Phrase("Huella digital del evaluado", fontDatosmall));
				clHuela_b.HorizontalAlignment = Element.ALIGN_CENTER;
				clHuela_b.BorderWidth = 0;

				PdfPCell clVacio_bbb = new PdfPCell();
				clVacio_bbb.BorderWidth = 0;

				tblFirmaMedico.AddCell(clFirmaNombre);
				tblFirmaMedico.AddCell(clVacio_a);
				tblFirmaMedico.AddCell(clHuela);
				tblFirmaMedico.AddCell(clVacio_b);

				tblFirmaMedico.AddCell(clFirmaNombre_b);
				tblFirmaMedico.AddCell(clVacio_bb);
				tblFirmaMedico.AddCell(clHuela_b);
				tblFirmaMedico.AddCell(clVacio_bbb);

				docAceTox.Add(tblFirmaMedico);
				#endregion

				#region Resguardo
				PdfPTable tblResguardo = new PdfPTable(3)
				{
					TotalWidth = 560,
					LockedWidth = true
				};
				float[] valuesRes = new float[3] { 300, 160, 100 };
				tblResguardo.SetWidths(valuesRes);
				tblResguardo.HorizontalAlignment = 0;
				tblResguardo.SpacingBefore = 20f;
				tblResguardo.SpacingAfter = 1f;
				tblResguardo.DefaultCell.Border = 0;

				PdfPCell clRes1 = new PdfPCell();
				clRes1.BorderWidth = 0;

				PdfPCell clRes2 = new PdfPCell(new Phrase("Resguardó la muestra", fontDato));
				clRes2.HorizontalAlignment = Element.ALIGN_CENTER;
				clRes2.BorderWidthTop = 1;
				clRes2.BorderWidthBottom = 0;
				clRes2.BorderWidthLeft = 0;
				clRes2.BorderWidthRight = 0;

				tblResguardo.AddCell(clRes1);
				tblResguardo.AddCell(clRes2);
				tblResguardo.AddCell(clRes1);

				docAceTox.Add(tblResguardo);
				#endregion

				#region pie pagina
				Paragraph pie = new Paragraph(new Phrase("Toda información contenida en este formato está clasificada como reservada y confidencial, de conformidad con lo dispuesto por los artículos 125 fracción I, 128, 133 de la Ley de Transparencia y Acceso a la Información Pública del Estado de Chiapas.", fontDatosmall));
				pie.Alignment = Element.ALIGN_CENTER;

				docAceTox.Add(pie);
				#endregion

				#region fin
				PdfPTable fin = new PdfPTable(2)
				{
					TotalWidth = 560,
					LockedWidth = true
				};
				float[] final = new float[2] { 280, 280 };
				fin.SetWidths(final);
				fin.HorizontalAlignment = 0;
				fin.SpacingBefore = 10f;
				fin.SpacingAfter = 5f;
				fin.DefaultCell.Border = 0;

				PdfPCell clfolio = new PdfPCell(new Phrase(aceTox[id].folio, fonEiqueta));
				clfolio.BorderWidth = 0;
				clfolio.HorizontalAlignment = Element.ALIGN_LEFT;

				PdfPCell clCodigo_c = new PdfPCell(new Phrase(aceTox[id].codigoevaluado, fonEiqueta));
				clCodigo_c.BorderWidth = 0;
				clCodigo_c.HorizontalAlignment = Element.ALIGN_RIGHT;

				fin.AddCell(clfolio);
				fin.AddCell(clCodigo_c);

				docAceTox.Add(fin);
				#endregion

				docAceTox.NewPage();
			}

			docAceTox.Close();
			byte[] byteStream = msTox.ToArray();
			msTox = new MemoryStream();
			msTox.Write(byteStream, 0, byteStream.Length);
			msTox.Position = 0;

			return new FileStreamResult(msTox, "application/pdf");
		}

		public IActionResult aceptacionAnalisis(string fecha)
		{
			var datosC3 = repo.Get<ConsultasModel>("sp_general_obtener_certificacion_acreditacion").FirstOrDefault();
			var aceAna = repo.Getdosparam1<ConsultasModel>("sp_medicos_rep_cabeceras_fecha", new { @fecha = fecha }).ToList();

			var _totalAceptacion = aceAna.Count();

			var fonEiqueta = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK);
			var fontDato = FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK);
			var fontDatosmall = FontFactory.GetFont("Arial", 9, Font.NORMAL, BaseColor.BLACK);

			MemoryStream msAna = new MemoryStream();
			Document docAceAna = new Document(PageSize.LETTER, 30f, 20f, 20f, 40f);
			PdfWriter pwAceAna = PdfWriter.GetInstance(docAceAna, msAna);

			docAceAna.Open();

			for (int idA = 0; idA < _totalAceptacion; idA++)
			{
				#region encabezado
				//-------------------------------------------------------------------------------------------------------- 1a linea
				string imageizq = @"C:/inetpub/wwwroot/fotoUser/gobedohor.png";
				iTextSharp.text.Image jpgSupIzq = iTextSharp.text.Image.GetInstance(imageizq);
				jpgSupIzq.ScaleToFit(80f, 80f);

				PdfPCell clLogoSupIzq = new PdfPCell();
				clLogoSupIzq.BorderWidth = 0;
				clLogoSupIzq.VerticalAlignment = Element.ALIGN_BOTTOM;
				clLogoSupIzq.AddElement(jpgSupIzq);

				string imageder = @"C:/inetpub/wwwroot/fotoUser/nuevoCeccc.png";
				iTextSharp.text.Image jpgSupDer = iTextSharp.text.Image.GetInstance(imageder);
				jpgSupDer.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
				jpgSupDer.ScaleToFit(100f, 100f);

				PdfPCell clLogoSupDer = new PdfPCell();
				clLogoSupDer.BorderWidth = 0;
				clLogoSupDer.VerticalAlignment = Element.ALIGN_BOTTOM;
				clLogoSupDer.AddElement(jpgSupDer);

				Chunk chkTit = new Chunk("Dirección Médica y Toxicológica", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
				Paragraph paragraph = new Paragraph();
				paragraph.Alignment = Element.ALIGN_CENTER;
				paragraph.Add(chkTit);

				Chunk chkSub = new Chunk("Aceptación de Análisis Clínicos", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
				Paragraph paragraph1 = new Paragraph();
				paragraph1.Alignment = Element.ALIGN_CENTER;
				paragraph1.Add(chkSub);

				PdfPCell clTitulo = new PdfPCell();
				clTitulo.BorderWidth = 0;
				clTitulo.AddElement(paragraph);

				PdfPCell clSubTit = new PdfPCell();
				clSubTit.BorderWidth = 0;
				clSubTit.AddElement(paragraph1);

				PdfPTable tblTitulo = new PdfPTable(1);
				tblTitulo.WidthPercentage = 100;
				tblTitulo.AddCell(clTitulo);
				tblTitulo.AddCell(clSubTit);

				PdfPCell clTablaTitulo = new PdfPCell();
				clTablaTitulo.BorderWidth = 0;
				clTablaTitulo.VerticalAlignment = Element.ALIGN_MIDDLE;
				clTablaTitulo.AddElement(tblTitulo);

				PdfPTable tblEncabezado = new PdfPTable(3);
				tblEncabezado.WidthPercentage = 100;
				float[] widths = new float[] { 20f, 60f, 20f };
				tblEncabezado.SetWidths(widths);

				tblEncabezado.AddCell(clLogoSupIzq);
				tblEncabezado.AddCell(clTablaTitulo);
				tblEncabezado.AddCell(clLogoSupDer);

				docAceAna.Add(tblEncabezado);
				#endregion

				#region emision - revision - codigo
				Paragraph paragraphemision = new Paragraph(new Phrase("EMISION", fonEiqueta));
				paragraphemision.Alignment = Element.ALIGN_CENTER;

				PdfPCell clEmision = new PdfPCell();
				clEmision.BorderWidth = 0;
				clEmision.AddElement(paragraphemision);

				Paragraph paragrarevision = new Paragraph(new Phrase("REVISION", fonEiqueta));
				paragrarevision.Alignment = Element.ALIGN_CENTER;

				PdfPCell clrevision = new PdfPCell();
				clrevision.BorderWidth = 0;
				clrevision.AddElement(paragrarevision);

				Paragraph paragracodigo = new Paragraph(new Phrase("CODIGO", fonEiqueta));
				paragracodigo.Alignment = Element.ALIGN_CENTER;

				PdfPCell clcodigo = new PdfPCell();
				clcodigo.BorderWidth = 0;
				clcodigo.AddElement(paragracodigo);

				Paragraph paragraphemision_b = new Paragraph(new Phrase(DateTime.Now.Year.ToString(), fonEiqueta));
				paragraphemision_b.Alignment = Element.ALIGN_CENTER;

				PdfPCell clEmision_b = new PdfPCell();
				clEmision_b.BorderWidth = 0;
				clEmision_b.VerticalAlignment = Element.ALIGN_MIDDLE;
				clEmision_b.UseAscender = true;
				clEmision_b.AddElement(paragraphemision_b);

				Paragraph paragrarevision_b = new Paragraph(new Phrase("1.1", fonEiqueta));
				paragrarevision_b.Alignment = Element.ALIGN_CENTER;

				PdfPCell clrevision_b = new PdfPCell();
				clrevision_b.BorderWidth = 0;
				clrevision_b.VerticalAlignment = Element.ALIGN_MIDDLE;
				clrevision_b.UseAscender = true;
				clrevision_b.AddElement(paragrarevision_b);

				Paragraph paragracodigo_b = new Paragraph(new Phrase("CECCC/DMT/30", fonEiqueta));
				paragracodigo_b.Alignment = Element.ALIGN_CENTER;

				PdfPCell clcodigo_b = new PdfPCell();
				clcodigo_b.BorderWidth = 0;
				clcodigo_b.VerticalAlignment = Element.ALIGN_MIDDLE;
				clcodigo_b.UseAscender = true;
				clcodigo_b.AddElement(paragracodigo_b);

				PdfPCell clLinea = new PdfPCell(new Phrase("", fontDato)) { Colspan = 3 };
				clLinea.BorderWidthBottom = 1;
				clLinea.BorderWidthTop = 0;
				clLinea.BorderWidthLeft = 0;
				clLinea.BorderWidthRight = 0;

				PdfPTable tblemision = new PdfPTable(3);
				tblemision.WidthPercentage = 100;
				float[] widthsemision = new float[] { 20f, 60f, 20f };
				tblemision.SetWidths(widthsemision);

				tblemision.AddCell(clLinea);

				tblemision.AddCell(clEmision);
				tblemision.AddCell(clrevision);
				tblemision.AddCell(clcodigo);

				tblemision.AddCell(clEmision_b);
				tblemision.AddCell(clrevision_b);
				tblemision.AddCell(clcodigo_b);

				docAceAna.Add(tblemision);
				#endregion

				#region certificacion acreditacion
				PdfPCell celCertificacion = new PdfPCell(new Phrase("Certificación No. " + datosC3.certifica, fontDato));
				celCertificacion.HorizontalAlignment = Element.ALIGN_LEFT;
				celCertificacion.BorderWidth = 0;

				PdfPCell celAcreditacion = new PdfPCell(new Phrase("Acreditación No. " + datosC3.acredita, fontDato));
				celAcreditacion.HorizontalAlignment = Element.ALIGN_RIGHT;
				celAcreditacion.BorderWidth = 0;

				PdfPTable tblAcrCer = new PdfPTable(2)
				{
					TotalWidth = 560,
					LockedWidth = true
				};
				float[] widthAcreditacion = new float[2] { 280, 280 };
				tblAcrCer.SetWidths(widthAcreditacion);
				tblAcrCer.HorizontalAlignment = 0;
				tblAcrCer.SpacingBefore = 5f;
				tblAcrCer.SpacingAfter = 5f;
				tblAcrCer.DefaultCell.Border = 0;

				tblAcrCer.AddCell(celCertificacion);
				tblAcrCer.AddCell(celAcreditacion);

				docAceAna.Add(tblAcrCer);

				#endregion

				Paragraph laFecha = new Paragraph(new Phrase("Tuxtla Gutiérrez; Chiapas a " + DateTime.Now.ToString("dd MMMM yyyy"), fontDato));
				laFecha.Alignment = Element.ALIGN_RIGHT;
				laFecha.Add(Chunk.NEWLINE);
				docAceAna.Add(laFecha);

				Paragraph DatosPersonales = new Paragraph(new Phrase("Datos personales ", fonEiqueta));
				DatosPersonales.Alignment = Element.ALIGN_LEFT;
				DatosPersonales.Add(Chunk.NEWLINE);
				docAceAna.Add(DatosPersonales);

				#region tabla datos personales
				PdfPTable tblDatosPersonales = new PdfPTable(6)
				{
					TotalWidth = 560,
					LockedWidth = true
				};
				float[] valuesDatosPersonales = new float[6] { 120, 90, 90, 80, 90, 90 };
				tblDatosPersonales.SetWidths(valuesDatosPersonales);
				tblDatosPersonales.HorizontalAlignment = 0;
				tblDatosPersonales.SpacingBefore = 5f;
				tblDatosPersonales.SpacingAfter = 5f;
				tblDatosPersonales.DefaultCell.Border = 0;

				PdfPCell clNombre = new PdfPCell(new Phrase("Nombre:", fonEiqueta));
				clNombre.BorderWidth = 0;
				clNombre.HorizontalAlignment = Element.ALIGN_LEFT;
				clNombre.VerticalAlignment = Element.ALIGN_MIDDLE;
				clNombre.UseAscender = true;
				clNombre.FixedHeight = 20f;

				PdfPCell clNombreDato = new PdfPCell(new Phrase(aceAna[idA].evaluado, fontDato)) { Colspan = 5 };
				clNombreDato.BorderWidthBottom = 1;
				clNombreDato.BorderWidthLeft = 0;
				clNombreDato.BorderWidthRight = 0;
				clNombreDato.BorderWidthTop = 0;
				clNombreDato.HorizontalAlignment = Element.ALIGN_LEFT;
				clNombreDato.VerticalAlignment = Element.ALIGN_MIDDLE;
				clNombreDato.UseAscender = true;

				PdfPCell clRfc = new PdfPCell(new Phrase("RFC:", fonEiqueta));
				clRfc.BorderWidth = 0;
				clRfc.HorizontalAlignment = Element.ALIGN_LEFT;
				clRfc.VerticalAlignment = Element.ALIGN_MIDDLE;
				clRfc.UseAscender = true;
				clRfc.FixedHeight = 20f;

				PdfPCell clRFCDatos = new PdfPCell(new Phrase(aceAna[idA].rfc, fontDato));
				clRFCDatos.BorderWidthBottom = 1;
				clRFCDatos.BorderWidthLeft = 0;
				clRFCDatos.BorderWidthRight = 0;
				clRFCDatos.BorderWidthTop = 0;
				clRFCDatos.HorizontalAlignment = Element.ALIGN_CENTER;
				clRFCDatos.VerticalAlignment = Element.ALIGN_MIDDLE;
				clRFCDatos.UseAscender = true;

				PdfPCell clEdad = new PdfPCell(new Phrase("Edad:", fonEiqueta));
				clEdad.BorderWidth = 0;
				clEdad.HorizontalAlignment = Element.ALIGN_CENTER;
				clEdad.VerticalAlignment = Element.ALIGN_MIDDLE;
				clEdad.UseAscender = true;

				PdfPCell clEdadDatos = new PdfPCell(new Phrase(aceAna[idA].edad.ToString(), fontDato));
				clEdadDatos.BorderWidthBottom = 1;
				clEdadDatos.BorderWidthLeft = 0;
				clEdadDatos.BorderWidthRight = 0;
				clEdadDatos.BorderWidthTop = 0;
				clEdadDatos.HorizontalAlignment = Element.ALIGN_CENTER;
				clEdadDatos.VerticalAlignment = Element.ALIGN_MIDDLE;
				clEdadDatos.UseAscender = true;

				PdfPCell clGenero = new PdfPCell(new Phrase("Género:", fonEiqueta));
				clGenero.BorderWidth = 0;
				clGenero.HorizontalAlignment = Element.ALIGN_CENTER;
				clGenero.VerticalAlignment = Element.ALIGN_MIDDLE;
				clGenero.UseAscender = true;

				PdfPCell clGeneroDatos = new PdfPCell(new Phrase(aceAna[idA].sexo, fontDato));
				clGeneroDatos.BorderWidthBottom = 1;
				clGeneroDatos.BorderWidthLeft = 0;
				clGeneroDatos.BorderWidthRight = 0;
				clGeneroDatos.BorderWidthTop = 0;
				clGeneroDatos.HorizontalAlignment = Element.ALIGN_CENTER;
				clGeneroDatos.VerticalAlignment = Element.ALIGN_MIDDLE;
				clGeneroDatos.UseAscender = true;

				PdfPCell clDependencia = new PdfPCell(new Phrase("Dependencia:", fonEiqueta));
				clDependencia.BorderWidth = 0;
				clDependencia.HorizontalAlignment = Element.ALIGN_LEFT;
				clDependencia.VerticalAlignment = Element.ALIGN_MIDDLE;
				clDependencia.UseAscender = true;
				clDependencia.FixedHeight = 20f;

				PdfPCell clDependenciaDato = new PdfPCell(new Phrase(aceAna[idA].dependencia, fontDato)) { Colspan = 5 };
				clDependenciaDato.BorderWidthBottom = 1;
				clDependenciaDato.BorderWidthLeft = 0;
				clDependenciaDato.BorderWidthRight = 0;
				clDependenciaDato.BorderWidthTop = 0;
				clDependenciaDato.HorizontalAlignment = Element.ALIGN_LEFT;
				clDependenciaDato.VerticalAlignment = Element.ALIGN_MIDDLE;
				clDependenciaDato.UseAscender = true;

				PdfPCell clAdscripcion = new PdfPCell(new Phrase("Adscripción:", fonEiqueta));
				clAdscripcion.BorderWidth = 0;
				clAdscripcion.HorizontalAlignment = Element.ALIGN_LEFT;
				clAdscripcion.VerticalAlignment = Element.ALIGN_MIDDLE;
				clAdscripcion.UseAscender = true;
				clAdscripcion.FixedHeight = 20f;

				PdfPCell clAdscripcionDato = new PdfPCell(new Phrase(aceAna[idA].adscripcion, fontDato)) { Colspan = 5 };
				clAdscripcionDato.BorderWidthBottom = 1;
				clAdscripcionDato.BorderWidthLeft = 0;
				clAdscripcionDato.BorderWidthRight = 0;
				clAdscripcionDato.BorderWidthTop = 0;
				clAdscripcionDato.HorizontalAlignment = Element.ALIGN_LEFT;
				clAdscripcionDato.VerticalAlignment = Element.ALIGN_MIDDLE;
				clAdscripcionDato.UseAscender = true;

				PdfPCell clPuesto = new PdfPCell(new Phrase("Puesto:", fonEiqueta));
				clPuesto.BorderWidth = 0;
				clPuesto.HorizontalAlignment = Element.ALIGN_LEFT;
				clPuesto.VerticalAlignment = Element.ALIGN_MIDDLE;
				clPuesto.UseAscender = true;
				clPuesto.FixedHeight = 20f;

				PdfPCell clPuestoDato = new PdfPCell(new Phrase(aceAna[idA].puesto, fontDato)) { Colspan = 5 };
				clPuestoDato.BorderWidthBottom = 1;
				clPuestoDato.BorderWidthLeft = 0;
				clPuestoDato.BorderWidthRight = 0;
				clPuestoDato.BorderWidthTop = 0;
				clPuestoDato.HorizontalAlignment = Element.ALIGN_LEFT;
				clPuestoDato.VerticalAlignment = Element.ALIGN_MIDDLE;
				clPuestoDato.UseAscender = true;

				PdfPCell clEvaluacion = new PdfPCell(new Phrase("Tipo de evaluación:", fonEiqueta));
				clEvaluacion.BorderWidth = 0;
				clEvaluacion.HorizontalAlignment = Element.ALIGN_LEFT;
				clEvaluacion.VerticalAlignment = Element.ALIGN_MIDDLE;
				clEvaluacion.UseAscender = true;
				clEvaluacion.FixedHeight = 20f;

				PdfPCell clEvaluacionDato = new PdfPCell(new Phrase(aceAna[idA].evaluacion, fontDato)) { Colspan = 5 };
				clEvaluacionDato.BorderWidthBottom = 1;
				clEvaluacionDato.BorderWidthLeft = 0;
				clEvaluacionDato.BorderWidthRight = 0;
				clEvaluacionDato.BorderWidthTop = 0;
				clEvaluacionDato.HorizontalAlignment = Element.ALIGN_LEFT;
				clEvaluacionDato.VerticalAlignment = Element.ALIGN_MIDDLE;
				clEvaluacionDato.UseAscender = true;

				PdfPCell clLugar = new PdfPCell(new Phrase("Lugar de evaluación:", fonEiqueta));
				clLugar.BorderWidth = 0;
				clLugar.HorizontalAlignment = Element.ALIGN_LEFT;
				clLugar.VerticalAlignment = Element.ALIGN_MIDDLE;
				clLugar.UseAscender = true;
				clLugar.FixedHeight = 20f;

				PdfPCell clLugarDato = new PdfPCell(new Phrase("CENTRO ESTATAL DE CONTROL DE CONFIANZA CERTIFICADO", fontDato)) { Colspan = 5 };
				clLugarDato.BorderWidthBottom = 1;
				clLugarDato.BorderWidthLeft = 0;
				clLugarDato.BorderWidthRight = 0;
				clLugarDato.BorderWidthTop = 0;
				clLugarDato.HorizontalAlignment = Element.ALIGN_LEFT;
				clLugarDato.VerticalAlignment = Element.ALIGN_MIDDLE;
				clLugarDato.UseAscender = true;

				tblDatosPersonales.AddCell(clNombre);
				tblDatosPersonales.AddCell(clNombreDato);

				tblDatosPersonales.AddCell(clRfc);
				tblDatosPersonales.AddCell(clRFCDatos);
				tblDatosPersonales.AddCell(clEdad);
				tblDatosPersonales.AddCell(clEdadDatos);
				tblDatosPersonales.AddCell(clGenero);
				tblDatosPersonales.AddCell(clGeneroDatos);

				tblDatosPersonales.AddCell(clDependencia);
				tblDatosPersonales.AddCell(clDependenciaDato);

				tblDatosPersonales.AddCell(clAdscripcion);
				tblDatosPersonales.AddCell(clAdscripcionDato);

				tblDatosPersonales.AddCell(clPuesto);
				tblDatosPersonales.AddCell(clPuestoDato);

				tblDatosPersonales.AddCell(clEvaluacion);
				tblDatosPersonales.AddCell(clEvaluacionDato);

				tblDatosPersonales.AddCell(clLugar);
				tblDatosPersonales.AddCell(clLugarDato);

				docAceAna.Add(tblDatosPersonales);
				#endregion

				#region Fundamento
				Paragraph fundamento_a = new Paragraph();
				fundamento_a.Alignment = Element.ALIGN_JUSTIFIED;
				fundamento_a.Add(new Phrase("Con fundamento en el Artículo 21 de la Constitución Política de los Estados Unidos Mexicanos; Artículo 7 fracción VI y 40 fracción XV de la Ley General del Sistema Nacional de Seguridad Publica, Artículo 7 fracción VI, y 33 fracción XV y 56 fracción I y II de la Ley del Sistema Estatal de Seguridad Publica y Artículo 27 fracción I del Reglamento Interior del Centro Estatal de Control de Confianza Certificado del Estado de Chiapas, otorgo la más amplia autorización al personal adscrito a la Dirección Médica y Toxicológico del Centro Estatal de Control de Confianza Certificado del Estado de Chiapas, para que realice mi examen médico.", fontDato));
				fundamento_a.Add(Chunk.NEWLINE); fundamento_a.Add(Chunk.NEWLINE);
				fundamento_a.Add(new Phrase("Declaro que me fue explicado la naturaleza y características de los análisis clínicos, autorizando se me realice una punción venosa para la obtención de dos muestras de sangre, para los fines que se requieran.", fontDato));
				fundamento_a.Add(Chunk.NEWLINE); fundamento_a.Add(Chunk.NEWLINE);
				fundamento_a.Add(new Phrase("Bajo protesta de decir verdad, me someto a la evaluación médica de manera voluntaria y sin que medie presión alguna, toda vez que como servidor público en materia de seguridad es mi obligación; asi mismo, estoy conforme que el resultado se notifique al Titular de mi Dependencia de adscripción al ser considerado como información confidencial, por lo que no tengo inconveniente alguno en que los datos obtenidos durante el proceso de evaluación, así como los documentos se destruya en el momento que el Centro lo considere conveniente.", fontDato));
				fundamento_a.Add(Chunk.NEWLINE); fundamento_a.Add(Chunk.NEWLINE);

				docAceAna.Add(fundamento_a);
				#endregion

				#region firmas
				PdfPTable tblFirmaMedico = new PdfPTable(4)
				{
					TotalWidth = 560,
					LockedWidth = true
				};
				float[] valuesFirmaMedico = new float[4] { 350, 55, 100, 55 };
				tblFirmaMedico.SetWidths(valuesFirmaMedico);
				tblFirmaMedico.HorizontalAlignment = 0;
				tblFirmaMedico.SpacingBefore = 1f;
				tblFirmaMedico.SpacingAfter = 1f;
				tblFirmaMedico.DefaultCell.Border = 0;

				PdfPCell clFirmaNombre = new PdfPCell();
				clFirmaNombre.BorderWidthBottom = 1;
				clFirmaNombre.BorderWidthLeft = 0;
				clFirmaNombre.BorderWidthRight = 0;
				clFirmaNombre.BorderWidthTop = 0;
				clFirmaNombre.FixedHeight = 80f;

				PdfPCell clVacio_a = new PdfPCell();
				clVacio_a.BorderWidth = 0;

				PdfPCell clHuela = new PdfPCell();
				clHuela.BorderWidthBottom = 1;
				clHuela.BorderWidthLeft = 1;
				clHuela.BorderWidthRight = 1;
				clHuela.BorderWidthTop = 1;

				PdfPCell clVacio_b = new PdfPCell();
				clVacio_b.BorderWidth = 0;

				PdfPCell clFirmaNombre_b = new PdfPCell(new Phrase(aceAna[idA].evaluado, fontDato));
				clFirmaNombre_b.HorizontalAlignment = Element.ALIGN_CENTER;
				clFirmaNombre_b.BorderWidth = 0;

				PdfPCell clVacio_bb = new PdfPCell();
				clVacio_bb.BorderWidth = 0;

				PdfPCell clHuela_b = new PdfPCell(new Phrase("Huella digital del evaluado", fontDatosmall));
				clHuela_b.HorizontalAlignment = Element.ALIGN_CENTER;
				clHuela_b.BorderWidth = 0;

				PdfPCell clVacio_bbb = new PdfPCell();
				clVacio_bbb.BorderWidth = 0;

				tblFirmaMedico.AddCell(clFirmaNombre);
				tblFirmaMedico.AddCell(clVacio_a);
				tblFirmaMedico.AddCell(clHuela);
				tblFirmaMedico.AddCell(clVacio_b);

				tblFirmaMedico.AddCell(clFirmaNombre_b);
				tblFirmaMedico.AddCell(clVacio_bb);
				tblFirmaMedico.AddCell(clHuela_b);
				tblFirmaMedico.AddCell(clVacio_bbb);

				docAceAna.Add(tblFirmaMedico);
				#endregion

				#region pie pagina
				Paragraph pie = new Paragraph(new Phrase("Toda información contenida en este formato está clasificada como confidencial de conformidad con lo dispuesto por el artículo 3°, Fracción IV y XII y 33 de la Ley que Garantiza la Transparencia y el Derecho a la información Pública para el Estado de Chiapas.", fontDatosmall));
				pie.Alignment = Element.ALIGN_CENTER;

				docAceAna.Add(pie);
				#endregion

				#region fin
				PdfPTable fin = new PdfPTable(2)
				{
					TotalWidth = 560,
					LockedWidth = true
				};
				float[] final = new float[2] { 280, 280 };
				fin.SetWidths(final);
				fin.HorizontalAlignment = 0;
				fin.SpacingBefore = 10f;
				fin.SpacingAfter = 5f;
				fin.DefaultCell.Border = 0;

				PdfPCell clfolio = new PdfPCell(new Phrase(aceAna[idA].folio, fonEiqueta));
				clfolio.BorderWidth = 0;
				clfolio.HorizontalAlignment = Element.ALIGN_LEFT;

				PdfPCell clCodigo_c = new PdfPCell(new Phrase(aceAna[idA].codigoevaluado, fonEiqueta));
				clCodigo_c.BorderWidth = 0;
				clCodigo_c.HorizontalAlignment = Element.ALIGN_RIGHT;

				fin.AddCell(clfolio);
				fin.AddCell(clCodigo_c);

				docAceAna.Add(fin);
				#endregion

				docAceAna.NewPage();
			}

			docAceAna.Close();
			byte[] byteStream = msAna.ToArray();
			msAna = new MemoryStream();
			msAna.Write(byteStream, 0, byteStream.Length);
			msAna.Position = 0;

			return new FileStreamResult(msAna, "application/pdf");
		}

		public IActionResult cadenasCustodia(string fecha)
		{
			var datosC3 = repo.Get<ConsultasModel>("sp_general_obtener_certificacion_acreditacion").FirstOrDefault();
			var cadenas = repo.Getdosparam1<ConsultasModel>("sp_medicos_rep_cabeceras_fecha", new { @fecha = fecha }).ToList();

			var _totalCadena = cadenas.Count();

			var fonEiqueta = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK);
			var fontDato = FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK);
			var fontDatosmall = FontFactory.GetFont("Arial", 9, Font.NORMAL, BaseColor.BLACK);

			var fontDatosCelda = FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLACK);

			MemoryStream msCad = new MemoryStream();
			Document docCadena = new Document(PageSize.LETTER, 30f, 20f, 20f, 40f);
			PdfWriter pwCadena = PdfWriter.GetInstance(docCadena, msCad);
			docCadena.Open();

			for (int id = 0; id < _totalCadena; id++)
			{
				#region encabezado
				//-------------------------------------------------------------------------------------------------------- 1a linea
				string imageizq = @"C:/inetpub/wwwroot/fotoUser/gobedohor.png";
				iTextSharp.text.Image jpgSupIzq = iTextSharp.text.Image.GetInstance(imageizq);
				jpgSupIzq.ScaleToFit(80f, 80f);

				PdfPCell clLogoSupIzq = new PdfPCell();
				clLogoSupIzq.BorderWidth = 0;
				clLogoSupIzq.VerticalAlignment = Element.ALIGN_BOTTOM;
				clLogoSupIzq.AddElement(jpgSupIzq);

				string imageder = @"C:/inetpub/wwwroot/fotoUser/nuevoCeccc.png";
				iTextSharp.text.Image jpgSupDer = iTextSharp.text.Image.GetInstance(imageder);
				jpgSupDer.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
				jpgSupDer.ScaleToFit(100f, 100f);

				PdfPCell clLogoSupDer = new PdfPCell();
				clLogoSupDer.BorderWidth = 0;
				clLogoSupDer.VerticalAlignment = Element.ALIGN_BOTTOM;
				clLogoSupDer.AddElement(jpgSupDer);

				Chunk chkTit = new Chunk("Dirección Médica y Toxicológica", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
				Paragraph paragraph = new Paragraph();
				paragraph.Alignment = Element.ALIGN_CENTER;
				paragraph.Add(chkTit);

				Chunk chkSub = new Chunk("Cadena de Custodia", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
				Paragraph paragraph1 = new Paragraph();
				paragraph1.Alignment = Element.ALIGN_CENTER;
				paragraph1.Add(chkSub);

				PdfPCell clTitulo = new PdfPCell();
				clTitulo.BorderWidth = 0;
				clTitulo.AddElement(paragraph);

				PdfPCell clSubTit = new PdfPCell();
				clSubTit.BorderWidth = 0;
				clSubTit.AddElement(paragraph1);

				PdfPTable tblTitulo = new PdfPTable(1);
				tblTitulo.WidthPercentage = 100;
				tblTitulo.AddCell(clTitulo);
				tblTitulo.AddCell(clSubTit);

				PdfPCell clTablaTitulo = new PdfPCell();
				clTablaTitulo.BorderWidth = 0;
				clTablaTitulo.VerticalAlignment = Element.ALIGN_MIDDLE;
				clTablaTitulo.AddElement(tblTitulo);

				PdfPTable tblEncabezado = new PdfPTable(3);
				tblEncabezado.WidthPercentage = 100;
				float[] widths = new float[] { 20f, 60f, 20f };
				tblEncabezado.SetWidths(widths);

				tblEncabezado.AddCell(clLogoSupIzq);
				tblEncabezado.AddCell(clTablaTitulo);
				tblEncabezado.AddCell(clLogoSupDer);

				docCadena.Add(tblEncabezado);
				#endregion

				#region emision - revision - codigo
				Paragraph paragraphemision = new Paragraph(new Phrase("EMISION", fonEiqueta));
				paragraphemision.Alignment = Element.ALIGN_CENTER;

				PdfPCell clEmision = new PdfPCell();
				clEmision.BorderWidth = 0;
				clEmision.AddElement(paragraphemision);

				Paragraph paragrarevision = new Paragraph(new Phrase("REVISION", fonEiqueta));
				paragrarevision.Alignment = Element.ALIGN_CENTER;

				PdfPCell clrevision = new PdfPCell();
				clrevision.BorderWidth = 0;
				clrevision.AddElement(paragrarevision);

				Paragraph paragracodigo = new Paragraph(new Phrase("CODIGO", fonEiqueta));
				paragracodigo.Alignment = Element.ALIGN_LEFT;

				PdfPCell clcodigo = new PdfPCell();
				clcodigo.BorderWidth = 0;
				clcodigo.AddElement(paragracodigo);

				Paragraph paragraphemision_b = new Paragraph(new Phrase(DateTime.Now.Year.ToString(), fonEiqueta));
				paragraphemision_b.Alignment = Element.ALIGN_CENTER;

				PdfPCell clEmision_b = new PdfPCell();
				clEmision_b.BorderWidth = 0;
				clEmision_b.VerticalAlignment = Element.ALIGN_MIDDLE;
				clEmision_b.UseAscender = true;
				clEmision_b.AddElement(paragraphemision_b);

				Paragraph paragrarevision_b = new Paragraph(new Phrase("1.1", fonEiqueta));
				paragrarevision_b.Alignment = Element.ALIGN_CENTER;

				PdfPCell clrevision_b = new PdfPCell();
				clrevision_b.BorderWidth = 0;
				clrevision_b.VerticalAlignment = Element.ALIGN_MIDDLE;
				clrevision_b.UseAscender = true;
				clrevision_b.AddElement(paragrarevision_b);

				Paragraph paragracodigo_b = new Paragraph(new Phrase("CECCC/DMT/06", fonEiqueta));
				paragracodigo_b.Alignment = Element.ALIGN_LEFT;

				PdfPCell clcodigo_b = new PdfPCell();
				clcodigo_b.BorderWidth = 0;
				clcodigo_b.VerticalAlignment = Element.ALIGN_MIDDLE;
				clcodigo_b.UseAscender = true;
				clcodigo_b.AddElement(paragracodigo_b);

				PdfPCell clLinea = new PdfPCell(new Phrase("", fontDato)) { Colspan = 3 };
				clLinea.BorderWidthBottom = 1;
				clLinea.BorderWidthTop = 0;
				clLinea.BorderWidthLeft = 0;
				clLinea.BorderWidthRight = 0;

				PdfPTable tblemision = new PdfPTable(3);
				tblemision.WidthPercentage = 100;
				float[] widthsemision = new float[] { 33f, 34f, 33f };
				tblemision.SetWidths(widthsemision);

				tblemision.AddCell(clLinea);

				tblemision.AddCell(clEmision);
				tblemision.AddCell(clrevision);
				tblemision.AddCell(clcodigo);

				tblemision.AddCell(clEmision_b);
				tblemision.AddCell(clrevision_b);
				tblemision.AddCell(clcodigo_b);

				docCadena.Add(tblemision);
				#endregion

				Byte[] _laFoto = (Byte[])cadenas[id].laFoto;
				iTextSharp.text.Image _laFotita = iTextSharp.text.Image.GetInstance(_laFoto);
				_laFotita.ScalePercent(45f);

				Paragraph derecha = new Paragraph();
				derecha.Alignment = Element.ALIGN_RIGHT;

				_laFotita.SetAbsolutePosition(520f, 645f);
				derecha.Add(_laFotita);
				docCadena.Add(derecha);

				Paragraph laFecha = new Paragraph(new Phrase("Tuxtla Gutiérrez; Chiapas a " + DateTime.Now.ToString("dd MMMM yyyy"), fontDato));
				laFecha.Alignment = Element.ALIGN_LEFT;
				laFecha.Add(Chunk.NEWLINE);
				docCadena.Add(laFecha);

				Paragraph DatosPersonales = new Paragraph(new Phrase("Datos personales ", fonEiqueta));
				DatosPersonales.Alignment = Element.ALIGN_LEFT;
				DatosPersonales.Add(Chunk.NEWLINE);
				docCadena.Add(DatosPersonales);

				#region tabla datos personales
				PdfPTable tblDatosPersonales = new PdfPTable(6)
				{
					TotalWidth = 560,
					LockedWidth = true
				};
				float[] valuesDatosPersonales = new float[6] { 120, 90, 90, 80, 90, 90 };
				tblDatosPersonales.SetWidths(valuesDatosPersonales);
				tblDatosPersonales.HorizontalAlignment = 0;
				tblDatosPersonales.SpacingBefore = 5f;
				tblDatosPersonales.SpacingAfter = 5f;
				tblDatosPersonales.DefaultCell.Border = 0;

				PdfPCell clNombre = new PdfPCell(new Phrase("Nombre:", fonEiqueta));
				clNombre.BorderWidth = 0;
				clNombre.HorizontalAlignment = Element.ALIGN_LEFT;
				clNombre.VerticalAlignment = Element.ALIGN_MIDDLE;
				clNombre.UseAscender = true;
				clNombre.FixedHeight = 20f;

				PdfPCell clNombreDato = new PdfPCell(new Phrase(cadenas[id].evaluado, fontDato)) { Colspan = 5 };
				clNombreDato.BorderWidthBottom = 1;
				clNombreDato.BorderWidthLeft = 0;
				clNombreDato.BorderWidthRight = 0;
				clNombreDato.BorderWidthTop = 0;
				clNombreDato.HorizontalAlignment = Element.ALIGN_LEFT;
				clNombreDato.VerticalAlignment = Element.ALIGN_MIDDLE;
				clNombreDato.UseAscender = true;

				PdfPCell clRfc = new PdfPCell(new Phrase("RFC:", fonEiqueta));
				clRfc.BorderWidth = 0;
				clRfc.HorizontalAlignment = Element.ALIGN_LEFT;
				clRfc.VerticalAlignment = Element.ALIGN_MIDDLE;
				clRfc.UseAscender = true;
				clRfc.FixedHeight = 20f;

				PdfPCell clRFCDatos = new PdfPCell(new Phrase(cadenas[id].rfc, fontDato));
				clRFCDatos.BorderWidthBottom = 1;
				clRFCDatos.BorderWidthLeft = 0;
				clRFCDatos.BorderWidthRight = 0;
				clRFCDatos.BorderWidthTop = 0;
				clRFCDatos.HorizontalAlignment = Element.ALIGN_CENTER;
				clRFCDatos.VerticalAlignment = Element.ALIGN_MIDDLE;
				clRFCDatos.UseAscender = true;

				PdfPCell clEdad = new PdfPCell(new Phrase("Edad:", fonEiqueta));
				clEdad.BorderWidth = 0;
				clEdad.HorizontalAlignment = Element.ALIGN_CENTER;
				clEdad.VerticalAlignment = Element.ALIGN_MIDDLE;
				clEdad.UseAscender = true;

				PdfPCell clEdadDatos = new PdfPCell(new Phrase(cadenas[id].edad.ToString(), fontDato));
				clEdadDatos.BorderWidthBottom = 1;
				clEdadDatos.BorderWidthLeft = 0;
				clEdadDatos.BorderWidthRight = 0;
				clEdadDatos.BorderWidthTop = 0;
				clEdadDatos.HorizontalAlignment = Element.ALIGN_CENTER;
				clEdadDatos.VerticalAlignment = Element.ALIGN_MIDDLE;
				clEdadDatos.UseAscender = true;

				PdfPCell clGenero = new PdfPCell(new Phrase("Género:", fonEiqueta));
				clGenero.BorderWidth = 0;
				clGenero.HorizontalAlignment = Element.ALIGN_CENTER;
				clGenero.VerticalAlignment = Element.ALIGN_MIDDLE;
				clGenero.UseAscender = true;

				PdfPCell clGeneroDatos = new PdfPCell(new Phrase(cadenas[id].sexo, fontDato));
				clGeneroDatos.BorderWidthBottom = 1;
				clGeneroDatos.BorderWidthLeft = 0;
				clGeneroDatos.BorderWidthRight = 0;
				clGeneroDatos.BorderWidthTop = 0;
				clGeneroDatos.HorizontalAlignment = Element.ALIGN_CENTER;
				clGeneroDatos.VerticalAlignment = Element.ALIGN_MIDDLE;
				clGeneroDatos.UseAscender = true;

				PdfPCell clDependencia = new PdfPCell(new Phrase("Dependencia:", fonEiqueta));
				clDependencia.BorderWidth = 0;
				clDependencia.HorizontalAlignment = Element.ALIGN_LEFT;
				clDependencia.VerticalAlignment = Element.ALIGN_MIDDLE;
				clDependencia.UseAscender = true;
				clDependencia.FixedHeight = 20f;

				PdfPCell clDependenciaDato = new PdfPCell(new Phrase(cadenas[id].dependencia, fontDato)) { Colspan = 5 };
				clDependenciaDato.BorderWidthBottom = 1;
				clDependenciaDato.BorderWidthLeft = 0;
				clDependenciaDato.BorderWidthRight = 0;
				clDependenciaDato.BorderWidthTop = 0;
				clDependenciaDato.HorizontalAlignment = Element.ALIGN_LEFT;
				clDependenciaDato.VerticalAlignment = Element.ALIGN_MIDDLE;
				clDependenciaDato.UseAscender = true;

				PdfPCell clPuesto = new PdfPCell(new Phrase("Puesto:", fonEiqueta));
				clPuesto.BorderWidth = 0;
				clPuesto.HorizontalAlignment = Element.ALIGN_LEFT;
				clPuesto.VerticalAlignment = Element.ALIGN_MIDDLE;
				clPuesto.UseAscender = true;
				clPuesto.FixedHeight = 20f;

				PdfPCell clPuestoDato = new PdfPCell(new Phrase(cadenas[id].puesto, fontDato)) { Colspan = 5 };
				clPuestoDato.BorderWidthBottom = 1;
				clPuestoDato.BorderWidthLeft = 0;
				clPuestoDato.BorderWidthRight = 0;
				clPuestoDato.BorderWidthTop = 0;
				clPuestoDato.HorizontalAlignment = Element.ALIGN_LEFT;
				clPuestoDato.VerticalAlignment = Element.ALIGN_MIDDLE;
				clPuestoDato.UseAscender = true;

				PdfPCell clEvaluacion = new PdfPCell(new Phrase("Tipo de evaluación:", fonEiqueta));
				clEvaluacion.BorderWidth = 0;
				clEvaluacion.HorizontalAlignment = Element.ALIGN_LEFT;
				clEvaluacion.VerticalAlignment = Element.ALIGN_MIDDLE;
				clEvaluacion.UseAscender = true;
				clEvaluacion.FixedHeight = 20f;

				PdfPCell clEvaluacionDato = new PdfPCell(new Phrase(cadenas[id].evaluacion, fontDato)) { Colspan = 5 };
				clEvaluacionDato.BorderWidthBottom = 1;
				clEvaluacionDato.BorderWidthLeft = 0;
				clEvaluacionDato.BorderWidthRight = 0;
				clEvaluacionDato.BorderWidthTop = 0;
				clEvaluacionDato.HorizontalAlignment = Element.ALIGN_LEFT;
				clEvaluacionDato.VerticalAlignment = Element.ALIGN_MIDDLE;
				clEvaluacionDato.UseAscender = true;

				PdfPCell clLugar = new PdfPCell(new Phrase("Lugar de evaluación:", fonEiqueta));
				clLugar.BorderWidth = 0;
				clLugar.HorizontalAlignment = Element.ALIGN_LEFT;
				clLugar.VerticalAlignment = Element.ALIGN_MIDDLE;
				clLugar.UseAscender = true;
				clLugar.FixedHeight = 20f;

				PdfPCell clLugarDato = new PdfPCell(new Phrase("CENTRO ESTATAL DE CONTROL DE CONFIANZA CERTIFICADO", fontDato)) { Colspan = 5 };
				clLugarDato.BorderWidthBottom = 1;
				clLugarDato.BorderWidthLeft = 0;
				clLugarDato.BorderWidthRight = 0;
				clLugarDato.BorderWidthTop = 0;
				clLugarDato.HorizontalAlignment = Element.ALIGN_LEFT;
				clLugarDato.VerticalAlignment = Element.ALIGN_MIDDLE;
				clLugarDato.UseAscender = true;

				tblDatosPersonales.AddCell(clNombre);
				tblDatosPersonales.AddCell(clNombreDato);

				tblDatosPersonales.AddCell(clRfc);
				tblDatosPersonales.AddCell(clRFCDatos);
				tblDatosPersonales.AddCell(clEdad);
				tblDatosPersonales.AddCell(clEdadDatos);
				tblDatosPersonales.AddCell(clGenero);
				tblDatosPersonales.AddCell(clGeneroDatos);

				tblDatosPersonales.AddCell(clDependencia);
				tblDatosPersonales.AddCell(clDependenciaDato);

				tblDatosPersonales.AddCell(clLugar);
				tblDatosPersonales.AddCell(clLugarDato);

				tblDatosPersonales.AddCell(clEvaluacion);
				tblDatosPersonales.AddCell(clEvaluacionDato);

				tblDatosPersonales.AddCell(clPuesto);
				tblDatosPersonales.AddCell(clPuestoDato);

				docCadena.Add(tblDatosPersonales);
				#endregion

				Paragraph temAna = new Paragraph(new Phrase("TEMPERATURA:________________________           ANALITO:_______________________________________________", fontDato));
				temAna.Alignment = Element.ALIGN_LEFT;
				docCadena.Add(temAna);

				#region cadena
				PdfPTable tblCadCus = new PdfPTable(4)
				{
					TotalWidth = 560,
					LockedWidth = true
				};
				float[] valCadCus = new float[4] { 100, 90, 185, 185 };
				tblCadCus.SetWidths(valCadCus);
				//tblCadCus.HorizontalAlignment = 0;
				tblCadCus.SpacingBefore = 5f;
				tblCadCus.SpacingAfter = 5f;
				tblCadCus.DefaultCell.Border = 1;

				//--------------------------------------------------------------------------------------- 1a Linea : Titulos
				PdfPCell clFecha = new PdfPCell(new Phrase("FECHA, HORA Y LUGAR", fontDatosCelda));
				clFecha.HorizontalAlignment = Element.ALIGN_CENTER;
				clFecha.VerticalAlignment = Element.ALIGN_MIDDLE;
				clFecha.UseAscender = true;
				clFecha.FixedHeight = 15f;
				tblCadCus.AddCell(clFecha);

				PdfPCell clActividad = new PdfPCell(new Phrase("ACTIVIDAD", fontDatosCelda));
				clActividad.HorizontalAlignment = Element.ALIGN_CENTER;
				clActividad.VerticalAlignment = Element.ALIGN_MIDDLE;
				clActividad.UseAscender = true;
				tblCadCus.AddCell(clActividad);

				PdfPCell clEntrega = new PdfPCell(new Phrase("QUIEN ENTREGA", fontDatosCelda));
				clEntrega.HorizontalAlignment = Element.ALIGN_CENTER;
				clEntrega.VerticalAlignment = Element.ALIGN_MIDDLE;
				clEntrega.UseAscender = true;
				tblCadCus.AddCell(clEntrega);

				PdfPCell clRecibe = new PdfPCell(new Phrase("QUIEN RECIBE", fontDatosCelda));
				clRecibe.HorizontalAlignment = Element.ALIGN_CENTER;
				clRecibe.VerticalAlignment = Element.ALIGN_MIDDLE;
				clRecibe.UseAscender = true;
				tblCadCus.AddCell(clRecibe);

				//--------------------------------------------------------------------------------------- 2 Linea
				PdfPCell clFecha_b = new PdfPCell();
				clFecha_b.AddElement(new Phrase("SANITARIO", fontDatosCelda));
				clFecha_b.AddElement(new Phrase("FECHA:____________", fontDatosCelda));
				clFecha_b.AddElement(new Phrase("HORA:_____________", fontDatosCelda));
				clFecha_b.HorizontalAlignment = Element.ALIGN_LEFT;
				clFecha_b.FixedHeight = 50f;
				tblCadCus.AddCell(clFecha_b);

				PdfPCell clActividad_b = new PdfPCell(new Phrase("RECOLECCION DE LA MUESTRA", fontDatosCelda));
				clActividad_b.HorizontalAlignment = Element.ALIGN_CENTER;
				clActividad_b.VerticalAlignment = Element.ALIGN_MIDDLE;
				clActividad_b.UseAscender = true;
				tblCadCus.AddCell(clActividad_b);

				PdfPCell clEntrega_b = new PdfPCell(new Phrase(cadenas[id].evaluado + "\nNOMBRE Y FIRMA DEL EVALUADO", fontDatosCelda));
				clEntrega_b.HorizontalAlignment = Element.ALIGN_CENTER;
				clEntrega_b.VerticalAlignment = Element.ALIGN_BOTTOM;
				tblCadCus.AddCell(clEntrega_b);

				PdfPCell clRecibe_b = new PdfPCell(new Phrase(cadenas[id].userquim + "\nNOMBRE Y FIRMA DEL SUPERVISOR OCULAR", fontDatosCelda));
				clRecibe_b.VerticalAlignment = Element.ALIGN_BOTTOM;
				clRecibe_b.HorizontalAlignment = Element.ALIGN_CENTER;
				tblCadCus.AddCell(clRecibe_b);

				//--------------------------------------------------------------------------------------- 3 Linea
				PdfPCell clFecha_c = new PdfPCell();
				clFecha_c.AddElement(new Phrase("LABORATORIO", fontDatosCelda));
				clFecha_c.AddElement(new Phrase("FECHA:____________", fontDatosCelda));
				clFecha_c.AddElement(new Phrase("HORA:____________", fontDatosCelda));
				clFecha_c.HorizontalAlignment = Element.ALIGN_LEFT;
				clFecha_c.FixedHeight = 50f;
				tblCadCus.AddCell(clFecha_c);

				PdfPCell clActividad_c = new PdfPCell(new Phrase("ENTREGA DE LA MUESTRA", fontDatosCelda));
				clActividad_c.HorizontalAlignment = Element.ALIGN_CENTER;
				clActividad_c.VerticalAlignment = Element.ALIGN_MIDDLE;
				clActividad_c.UseAscender = true;
				tblCadCus.AddCell(clActividad_c);

				PdfPCell clEntrega_c = new PdfPCell(new Phrase(cadenas[id].userquim + "\nNOMBRE Y FIRMA DEL SUPERVISOR OCULAR", fontDatosCelda));
				clEntrega_c.HorizontalAlignment = Element.ALIGN_CENTER;
				clEntrega_c.VerticalAlignment = Element.ALIGN_BOTTOM;
				tblCadCus.AddCell(clEntrega_c);

				PdfPCell clRecibe_c = new PdfPCell(new Phrase(cadenas[id].usertox + "\nNOMBRE Y FIRMA DEL ANALISTA", fontDatosCelda));
				clRecibe_c.VerticalAlignment = Element.ALIGN_BOTTOM;
				clRecibe_c.HorizontalAlignment = Element.ALIGN_CENTER;
				tblCadCus.AddCell(clRecibe_c);

				//--------------------------------------------------------------------------------------- 4 Linea
				PdfPCell clFecha_d = new PdfPCell();
				clFecha_d.AddElement(new Phrase("LABORATORIO", fontDatosCelda));
				clFecha_d.AddElement(new Phrase("FECHA:____________", fontDatosCelda));
				clFecha_d.AddElement(new Phrase("HORA:____________", fontDatosCelda));
				clFecha_d.HorizontalAlignment = Element.ALIGN_LEFT;
				clFecha_d.FixedHeight = 50f;
				tblCadCus.AddCell(clFecha_d);

				PdfPCell clActividad_d = new PdfPCell(new Phrase("ENTREGA PARA EL RESGUARDO DE LA MUESTRA", fontDatosCelda));
				clActividad_d.HorizontalAlignment = Element.ALIGN_CENTER;
				clActividad_d.VerticalAlignment = Element.ALIGN_MIDDLE;
				clActividad_d.UseAscender = true;
				tblCadCus.AddCell(clActividad_d);

				PdfPCell clEntrega_d = new PdfPCell(new Phrase(cadenas[id].usertox + "\nNOMBRE Y FIRMA DEL ANALISTA", fontDatosCelda));
				clEntrega_d.HorizontalAlignment = Element.ALIGN_CENTER;
				clEntrega_d.VerticalAlignment = Element.ALIGN_BOTTOM;
				tblCadCus.AddCell(clEntrega_d);

				PdfPCell clRecibe_d = new PdfPCell(new Phrase("______________________________________\nNOMBRE Y FIRMA DE QUIEN RESGUARDA LA MUESTRA", fontDatosCelda));
				clRecibe_d.VerticalAlignment = Element.ALIGN_BOTTOM;
				clRecibe_d.HorizontalAlignment = Element.ALIGN_CENTER;
				tblCadCus.AddCell(clRecibe_d);

				//--------------------------------------------------------------------------------------- 5 Linea
				PdfPCell clFecha_e = new PdfPCell();
				clFecha_e.AddElement(new Phrase("LABORATORIO", fontDatosCelda));
				clFecha_e.AddElement(new Phrase("FECHA:____________", fontDatosCelda));
				clFecha_e.AddElement(new Phrase("HORA:____________", fontDatosCelda));
				clFecha_e.HorizontalAlignment = Element.ALIGN_LEFT;
				clFecha_e.FixedHeight = 50f;
				tblCadCus.AddCell(clFecha_e);

				PdfPCell clActividad_e = new PdfPCell(new Phrase("ENTREGA DE LA MUESTRA PARA EL ENVIO A ESTUDIO CONFIRMATORIO", fontDatosCelda));
				clActividad_e.HorizontalAlignment = Element.ALIGN_CENTER;
				clActividad_e.VerticalAlignment = Element.ALIGN_MIDDLE;
				clActividad_e.UseAscender = true;
				tblCadCus.AddCell(clActividad_e);

				PdfPCell clEntrega_e = new PdfPCell(new Phrase("______________________________________\nNOMBRE Y FIRMA DE QUIEN RESGUARDA LA MUESTRA", fontDatosCelda));
				clEntrega_e.HorizontalAlignment = Element.ALIGN_CENTER;
				clEntrega_e.VerticalAlignment = Element.ALIGN_BOTTOM;
				tblCadCus.AddCell(clEntrega_e);

				PdfPCell clRecibe_e = new PdfPCell(new Phrase("______________________________________\nNOMBRE Y FIRMA DE QUIEN LLEVA LA MUESTRA", fontDatosCelda));
				clRecibe_e.VerticalAlignment = Element.ALIGN_BOTTOM;
				clRecibe_e.HorizontalAlignment = Element.ALIGN_CENTER;
				tblCadCus.AddCell(clRecibe_e);

				//--------------------------------------------------------------------------------------- 6 Linea
				PdfPCell clFecha_f = new PdfPCell();
				clFecha_f.AddElement(new Phrase("LABORATORIO", fontDatosCelda));
				clFecha_f.AddElement(new Phrase("FECHA:____________", fontDatosCelda));
				clFecha_f.AddElement(new Phrase("HORA:____________", fontDatosCelda));
				clFecha_f.HorizontalAlignment = Element.ALIGN_LEFT;
				clFecha_f.FixedHeight = 50f;
				tblCadCus.AddCell(clFecha_f);

				PdfPCell clActividad_f = new PdfPCell(new Phrase("ENTREGA DE MUESTRAS PARA EL ESTUDIO CONFIRMATORIO", fontDatosCelda));
				clActividad_f.HorizontalAlignment = Element.ALIGN_CENTER;
				clActividad_f.VerticalAlignment = Element.ALIGN_MIDDLE;
				clActividad_f.UseAscender = true;
				tblCadCus.AddCell(clActividad_f);

				PdfPCell clEntrega_f = new PdfPCell(new Phrase("______________________________________\nNOMBRE Y FIRMA DE QUIEN LLEVA LA MUESTRA", fontDatosCelda));
				clEntrega_f.HorizontalAlignment = Element.ALIGN_CENTER;
				clEntrega_f.VerticalAlignment = Element.ALIGN_BOTTOM;
				tblCadCus.AddCell(clEntrega_f);

				PdfPCell clRecibe_f = new PdfPCell(new Phrase("______________________________________\nNOMBRE Y FIRMA DE REPRESENTANTE DEL LABORATORIO CONFIRMATORIO", fontDatosCelda));
				clRecibe_f.VerticalAlignment = Element.ALIGN_BOTTOM;
				clRecibe_f.HorizontalAlignment = Element.ALIGN_CENTER;
				tblCadCus.AddCell(clRecibe_f);

				//--------------------------------------------------------------------------------------- 7 Linea
				PdfPCell clFecha_h = new PdfPCell();
				clFecha_h.AddElement(new Phrase("LABORATORIO", fontDatosCelda));
				clFecha_h.AddElement(new Phrase("FECHA:____________", fontDatosCelda));
				clFecha_h.AddElement(new Phrase("HORA:____________", fontDatosCelda));
				clFecha_h.HorizontalAlignment = Element.ALIGN_LEFT;
				clFecha_h.FixedHeight = 50f;
				tblCadCus.AddCell(clFecha_h);

				PdfPCell clActividad_h = new PdfPCell(new Phrase("RECEPCION DE RESULTADOS", fontDatosCelda));
				clActividad_h.HorizontalAlignment = Element.ALIGN_CENTER;
				clActividad_h.VerticalAlignment = Element.ALIGN_MIDDLE;
				clActividad_h.UseAscender = true;
				tblCadCus.AddCell(clActividad_h);

				PdfPCell clEntrega_h = new PdfPCell(new Phrase("______________________________________\nNOMBRE Y FIRMA DE REPRESENTANTE DEL LABORATORIO CONFIRMATORIO", fontDatosCelda));
				clEntrega_h.HorizontalAlignment = Element.ALIGN_CENTER;
				clEntrega_h.VerticalAlignment = Element.ALIGN_BOTTOM;
				tblCadCus.AddCell(clEntrega_h);

				PdfPCell clRecibe_h = new PdfPCell(new Phrase("______________________________________\nNOMBRE Y FIRMA DE QUIEN RECIBE RESULTADOS CONFIRMATORIOS", fontDatosCelda));
				clRecibe_h.VerticalAlignment = Element.ALIGN_BOTTOM;
				clRecibe_h.HorizontalAlignment = Element.ALIGN_CENTER;
				tblCadCus.AddCell(clRecibe_h);

				docCadena.Add(tblCadCus);

				#endregion

				#region Resultados - observaciones
				Paragraph resObs = new Paragraph();
				resObs.Add(new Phrase("Resultado de adulterantes en orina: _______________________________________________________________________", fontDato));
				resObs.Add(Chunk.NEWLINE);
				resObs.Add(new Phrase("OBSERVACIONES ____________________________________________________________________________________", fontDato));
				resObs.Add(Chunk.NEWLINE);
				resObs.Add(new Phrase("____________________________________________________________________________________________________", fontDato));
				resObs.Alignment = Element.ALIGN_LEFT;

				docCadena.Add(resObs);
				#endregion

				#region Resultados - observaciones
				Paragraph finCus = new Paragraph();
				finCus.Add(new Phrase("Este formato acompañara a la muestra en todo momento, toda persona que la maneje deberá llenar los espacios correspondientes del formato.", fontDatosCelda));
				finCus.Add(Chunk.NEWLINE);
				finCus.Add(new Phrase("Este formato se sustenta en el de emisión de resultados CECCC/CMT/04", fontDatosCelda));
				finCus.Alignment = Element.ALIGN_LEFT;

				docCadena.Add(finCus);
				#endregion

				#region final centro
				Paragraph finCusCentro = new Paragraph(new Phrase("ESTE FORMATO ES DE CARÁCTER CONFIDENCIAL.", fontDatosCelda));
				finCusCentro.Alignment = Element.ALIGN_CENTER;
				docCadena.Add(finCusCentro);
				#endregion

				#region finfinal
				PdfPTable fin = new PdfPTable(2)
				{
					TotalWidth = 560,
					LockedWidth = true
				};
				float[] final = new float[2] { 280, 280 };
				fin.SetWidths(final);
				fin.HorizontalAlignment = 0;
				fin.SpacingBefore = 10f;
				fin.SpacingAfter = 5f;
				fin.DefaultCell.Border = 0;

				PdfPCell clfolio = new PdfPCell(new Phrase(cadenas[id].folio, fonEiqueta));
				clfolio.BorderWidth = 0;
				clfolio.HorizontalAlignment = Element.ALIGN_LEFT;

				PdfPCell clCodigo_c = new PdfPCell(new Phrase(cadenas[id].codigoevaluado, fonEiqueta));
				clCodigo_c.BorderWidth = 0;
				clCodigo_c.HorizontalAlignment = Element.ALIGN_RIGHT;

				fin.AddCell(clfolio);
				fin.AddCell(clCodigo_c);

				docCadena.Add(fin);
				#endregion

				docCadena.NewPage();
			}

			docCadena.Close();
			byte[] byteStream = msCad.ToArray();
			msCad = new MemoryStream();
			msCad.Write(byteStream, 0, byteStream.Length);
			msCad.Position = 0;

			return new FileStreamResult(msCad, "application/pdf");
		}

		public IActionResult cadenaCustodiaIndividual(int idhistorico)
		{
			var datosC3 = repo.Get<ConsultasModel>("sp_general_obtener_certificacion_acreditacion").FirstOrDefault();
			var cadenaIndividual = repo.Getdosparam1<ConsultasModel>("sp_medicos_rep_cabeceras_idhistorico", new { @idhistorico = idhistorico }).FirstOrDefault();

            var fonEiqueta = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK);
            var fontDato = FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK);
            var fontDatosmall = FontFactory.GetFont("Arial", 9, Font.NORMAL, BaseColor.BLACK);

            var fontDatosCelda = FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLACK);

            MemoryStream msCad = new MemoryStream();
            Document docCadena = new Document(PageSize.LETTER, 30f, 20f, 20f, 40f);
            PdfWriter pwCadena = PdfWriter.GetInstance(docCadena, msCad);
            docCadena.Open();

            #region encabezado
            string imageizq = @"C:/inetpub/wwwroot/fotoUser/gobedohor.png";
            iTextSharp.text.Image jpgSupIzq = iTextSharp.text.Image.GetInstance(imageizq);
            jpgSupIzq.ScaleToFit(80f, 80f);

            PdfPCell clLogoSupIzq = new PdfPCell();
            clLogoSupIzq.BorderWidth = 0;
            clLogoSupIzq.VerticalAlignment = Element.ALIGN_BOTTOM;
            clLogoSupIzq.AddElement(jpgSupIzq);

            string imageder = @"C:/inetpub/wwwroot/fotoUser/nuevoCeccc.png";
            iTextSharp.text.Image jpgSupDer = iTextSharp.text.Image.GetInstance(imageder);
            jpgSupDer.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
            jpgSupDer.ScaleToFit(100f, 100f);

            PdfPCell clLogoSupDer = new PdfPCell();
            clLogoSupDer.BorderWidth = 0;
            clLogoSupDer.VerticalAlignment = Element.ALIGN_BOTTOM;
            clLogoSupDer.AddElement(jpgSupDer);

            Chunk chkTit = new Chunk("Dirección Médica y Toxicológica", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
            Paragraph paragraph = new Paragraph();
            paragraph.Alignment = Element.ALIGN_CENTER;
            paragraph.Add(chkTit);

            Chunk chkSub = new Chunk("Cadena de Custodia", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
            Paragraph paragraph1 = new Paragraph();
            paragraph1.Alignment = Element.ALIGN_CENTER;
            paragraph1.Add(chkSub);

            PdfPCell clTitulo = new PdfPCell();
            clTitulo.BorderWidth = 0;
            clTitulo.AddElement(paragraph);

            PdfPCell clSubTit = new PdfPCell();
            clSubTit.BorderWidth = 0;
            clSubTit.AddElement(paragraph1);

            PdfPTable tblTitulo = new PdfPTable(1);
            tblTitulo.WidthPercentage = 100;
            tblTitulo.AddCell(clTitulo);
            tblTitulo.AddCell(clSubTit);

            PdfPCell clTablaTitulo = new PdfPCell();
            clTablaTitulo.BorderWidth = 0;
            clTablaTitulo.VerticalAlignment = Element.ALIGN_MIDDLE;
            clTablaTitulo.AddElement(tblTitulo);

            PdfPTable tblEncabezado = new PdfPTable(3);
            tblEncabezado.WidthPercentage = 100;
            float[] widths = new float[] { 20f, 60f, 20f };
            tblEncabezado.SetWidths(widths);

            tblEncabezado.AddCell(clLogoSupIzq);
            tblEncabezado.AddCell(clTablaTitulo);
            tblEncabezado.AddCell(clLogoSupDer);

            docCadena.Add(tblEncabezado);
            #endregion

            #region emision - revision - codigo
            Paragraph paragraphemision = new Paragraph(new Phrase("EMISION", fonEiqueta));
            paragraphemision.Alignment = Element.ALIGN_CENTER;

            PdfPCell clEmision = new PdfPCell();
            clEmision.BorderWidth = 0;
            clEmision.AddElement(paragraphemision);

            Paragraph paragrarevision = new Paragraph(new Phrase("REVISION", fonEiqueta));
            paragrarevision.Alignment = Element.ALIGN_CENTER;

            PdfPCell clrevision = new PdfPCell();
            clrevision.BorderWidth = 0;
            clrevision.AddElement(paragrarevision);

            Paragraph paragracodigo = new Paragraph(new Phrase("CODIGO", fonEiqueta));
            paragracodigo.Alignment = Element.ALIGN_LEFT;

            PdfPCell clcodigo = new PdfPCell();
            clcodigo.BorderWidth = 0;
            clcodigo.AddElement(paragracodigo);

            Paragraph paragraphemision_b = new Paragraph(new Phrase(DateTime.Now.Year.ToString(), fonEiqueta));
            paragraphemision_b.Alignment = Element.ALIGN_CENTER;

            PdfPCell clEmision_b = new PdfPCell();
            clEmision_b.BorderWidth = 0;
            clEmision_b.VerticalAlignment = Element.ALIGN_MIDDLE;
            clEmision_b.UseAscender = true;
            clEmision_b.AddElement(paragraphemision_b);

            Paragraph paragrarevision_b = new Paragraph(new Phrase("1.1", fonEiqueta));
            paragrarevision_b.Alignment = Element.ALIGN_CENTER;

            PdfPCell clrevision_b = new PdfPCell();
            clrevision_b.BorderWidth = 0;
            clrevision_b.VerticalAlignment = Element.ALIGN_MIDDLE;
            clrevision_b.UseAscender = true;
            clrevision_b.AddElement(paragrarevision_b);

            Paragraph paragracodigo_b = new Paragraph(new Phrase("CECCC/DMT/06", fonEiqueta));
            paragracodigo_b.Alignment = Element.ALIGN_LEFT;

            PdfPCell clcodigo_b = new PdfPCell();
            clcodigo_b.BorderWidth = 0;
            clcodigo_b.VerticalAlignment = Element.ALIGN_MIDDLE;
            clcodigo_b.UseAscender = true;
            clcodigo_b.AddElement(paragracodigo_b);

            PdfPCell clLinea = new PdfPCell(new Phrase("", fontDato)) { Colspan = 3 };
            clLinea.BorderWidthBottom = 1;
            clLinea.BorderWidthTop = 0;
            clLinea.BorderWidthLeft = 0;
            clLinea.BorderWidthRight = 0;

            PdfPTable tblemision = new PdfPTable(3);
            tblemision.WidthPercentage = 100;
            float[] widthsemision = new float[] { 33f, 34f, 33f };
            tblemision.SetWidths(widthsemision);

            tblemision.AddCell(clLinea);

            tblemision.AddCell(clEmision);
            tblemision.AddCell(clrevision);
            tblemision.AddCell(clcodigo);

            tblemision.AddCell(clEmision_b);
            tblemision.AddCell(clrevision_b);
            tblemision.AddCell(clcodigo_b);

            docCadena.Add(tblemision);
            #endregion

            //Byte[] _laFoto = (Byte[])cadenas[id].laFoto;
            Byte[] _laFoto = (Byte[])cadenaIndividual.laFoto;
            iTextSharp.text.Image _laFotita = iTextSharp.text.Image.GetInstance(_laFoto);
            _laFotita.ScalePercent(45f);

            Paragraph derecha = new Paragraph();
            derecha.Alignment = Element.ALIGN_RIGHT;

            _laFotita.SetAbsolutePosition(520f, 645f);
            derecha.Add(_laFotita);
            docCadena.Add(derecha);

            Paragraph laFecha = new Paragraph(new Phrase("Tuxtla Gutiérrez; Chiapas a " + DateTime.Now.ToString("dd MMMM yyyy"), fontDato));
            laFecha.Alignment = Element.ALIGN_LEFT;
            laFecha.Add(Chunk.NEWLINE);
            docCadena.Add(laFecha);

            Paragraph DatosPersonales = new Paragraph(new Phrase("Datos personales ", fonEiqueta));
            DatosPersonales.Alignment = Element.ALIGN_LEFT;
            DatosPersonales.Add(Chunk.NEWLINE);
            docCadena.Add(DatosPersonales);

            #region tabla datos personales
            PdfPTable tblDatosPersonales = new PdfPTable(6)
            {
                TotalWidth = 560,
                LockedWidth = true
            };

            float[] valuesDatosPersonales = new float[6] { 120, 90, 90, 80, 90, 90 };
            tblDatosPersonales.SetWidths(valuesDatosPersonales);
            tblDatosPersonales.HorizontalAlignment = 0;
            tblDatosPersonales.SpacingBefore = 5f;
            tblDatosPersonales.SpacingAfter = 5f;
            tblDatosPersonales.DefaultCell.Border = 0;

            PdfPCell clNombre = new PdfPCell(new Phrase("Nombre:", fonEiqueta));
            clNombre.BorderWidth = 0;
            clNombre.HorizontalAlignment = Element.ALIGN_LEFT;
            clNombre.VerticalAlignment = Element.ALIGN_MIDDLE;
            clNombre.UseAscender = true;
            clNombre.FixedHeight = 20f;

            PdfPCell clNombreDato = new PdfPCell(new Phrase(cadenaIndividual.evaluado, fontDato)) { Colspan = 5 };
            clNombreDato.BorderWidthBottom = 1;
            clNombreDato.BorderWidthLeft = 0;
            clNombreDato.BorderWidthRight = 0;
            clNombreDato.BorderWidthTop = 0;
            clNombreDato.HorizontalAlignment = Element.ALIGN_LEFT;
            clNombreDato.VerticalAlignment = Element.ALIGN_MIDDLE;
            clNombreDato.UseAscender = true;

            PdfPCell clRfc = new PdfPCell(new Phrase("RFC:", fonEiqueta));
            clRfc.BorderWidth = 0;
            clRfc.HorizontalAlignment = Element.ALIGN_LEFT;
            clRfc.VerticalAlignment = Element.ALIGN_MIDDLE;
            clRfc.UseAscender = true;
            clRfc.FixedHeight = 20f;

            PdfPCell clRFCDatos = new PdfPCell(new Phrase(cadenaIndividual.rfc, fontDato));
            clRFCDatos.BorderWidthBottom = 1;
            clRFCDatos.BorderWidthLeft = 0;
            clRFCDatos.BorderWidthRight = 0;
            clRFCDatos.BorderWidthTop = 0;
            clRFCDatos.HorizontalAlignment = Element.ALIGN_CENTER;
            clRFCDatos.VerticalAlignment = Element.ALIGN_MIDDLE;
            clRFCDatos.UseAscender = true;

            PdfPCell clEdad = new PdfPCell(new Phrase("Edad:", fonEiqueta));
            clEdad.BorderWidth = 0;
            clEdad.HorizontalAlignment = Element.ALIGN_CENTER;
            clEdad.VerticalAlignment = Element.ALIGN_MIDDLE;
            clEdad.UseAscender = true;

            PdfPCell clEdadDatos = new PdfPCell(new Phrase(cadenaIndividual.edad.ToString(), fontDato));
            clEdadDatos.BorderWidthBottom = 1;
            clEdadDatos.BorderWidthLeft = 0;
            clEdadDatos.BorderWidthRight = 0;
            clEdadDatos.BorderWidthTop = 0;
            clEdadDatos.HorizontalAlignment = Element.ALIGN_CENTER;
            clEdadDatos.VerticalAlignment = Element.ALIGN_MIDDLE;
            clEdadDatos.UseAscender = true;

            PdfPCell clGenero = new PdfPCell(new Phrase("Género:", fonEiqueta));
            clGenero.BorderWidth = 0;
            clGenero.HorizontalAlignment = Element.ALIGN_CENTER;
            clGenero.VerticalAlignment = Element.ALIGN_MIDDLE;
            clGenero.UseAscender = true;

            PdfPCell clGeneroDatos = new PdfPCell(new Phrase(cadenaIndividual.sexo, fontDato));
            clGeneroDatos.BorderWidthBottom = 1;
            clGeneroDatos.BorderWidthLeft = 0;
            clGeneroDatos.BorderWidthRight = 0;
            clGeneroDatos.BorderWidthTop = 0;
            clGeneroDatos.HorizontalAlignment = Element.ALIGN_CENTER;
            clGeneroDatos.VerticalAlignment = Element.ALIGN_MIDDLE;
            clGeneroDatos.UseAscender = true;

            PdfPCell clDependencia = new PdfPCell(new Phrase("Dependencia:", fonEiqueta));
            clDependencia.BorderWidth = 0;
            clDependencia.HorizontalAlignment = Element.ALIGN_LEFT;
            clDependencia.VerticalAlignment = Element.ALIGN_MIDDLE;
            clDependencia.UseAscender = true;
            clDependencia.FixedHeight = 20f;

            PdfPCell clDependenciaDato = new PdfPCell(new Phrase(cadenaIndividual.dependencia, fontDato)) { Colspan = 5 };
            clDependenciaDato.BorderWidthBottom = 1;
            clDependenciaDato.BorderWidthLeft = 0;
            clDependenciaDato.BorderWidthRight = 0;
            clDependenciaDato.BorderWidthTop = 0;
            clDependenciaDato.HorizontalAlignment = Element.ALIGN_LEFT;
            clDependenciaDato.VerticalAlignment = Element.ALIGN_MIDDLE;
            clDependenciaDato.UseAscender = true;

            PdfPCell clPuesto = new PdfPCell(new Phrase("Puesto:", fonEiqueta));
            clPuesto.BorderWidth = 0;
            clPuesto.HorizontalAlignment = Element.ALIGN_LEFT;
            clPuesto.VerticalAlignment = Element.ALIGN_MIDDLE;
            clPuesto.UseAscender = true;
            clPuesto.FixedHeight = 20f;

            PdfPCell clPuestoDato = new PdfPCell(new Phrase(cadenaIndividual.puesto, fontDato)) { Colspan = 5 };
            clPuestoDato.BorderWidthBottom = 1;
            clPuestoDato.BorderWidthLeft = 0;
            clPuestoDato.BorderWidthRight = 0;
            clPuestoDato.BorderWidthTop = 0;
            clPuestoDato.HorizontalAlignment = Element.ALIGN_LEFT;
            clPuestoDato.VerticalAlignment = Element.ALIGN_MIDDLE;
            clPuestoDato.UseAscender = true;

            PdfPCell clEvaluacion = new PdfPCell(new Phrase("Tipo de evaluación:", fonEiqueta));
            clEvaluacion.BorderWidth = 0;
            clEvaluacion.HorizontalAlignment = Element.ALIGN_LEFT;
            clEvaluacion.VerticalAlignment = Element.ALIGN_MIDDLE;
            clEvaluacion.UseAscender = true;
            clEvaluacion.FixedHeight = 20f;

            PdfPCell clEvaluacionDato = new PdfPCell(new Phrase(cadenaIndividual.evaluacion, fontDato)) { Colspan = 5 };
            clEvaluacionDato.BorderWidthBottom = 1;
            clEvaluacionDato.BorderWidthLeft = 0;
            clEvaluacionDato.BorderWidthRight = 0;
            clEvaluacionDato.BorderWidthTop = 0;
            clEvaluacionDato.HorizontalAlignment = Element.ALIGN_LEFT;
            clEvaluacionDato.VerticalAlignment = Element.ALIGN_MIDDLE;
            clEvaluacionDato.UseAscender = true;

            PdfPCell clLugar = new PdfPCell(new Phrase("Lugar de evaluación:", fonEiqueta));
            clLugar.BorderWidth = 0;
            clLugar.HorizontalAlignment = Element.ALIGN_LEFT;
            clLugar.VerticalAlignment = Element.ALIGN_MIDDLE;
            clLugar.UseAscender = true;
            clLugar.FixedHeight = 20f;

            PdfPCell clLugarDato = new PdfPCell(new Phrase("CENTRO ESTATAL DE CONTROL DE CONFIANZA CERTIFICADO", fontDato)) { Colspan = 5 };
            clLugarDato.BorderWidthBottom = 1;
            clLugarDato.BorderWidthLeft = 0;
            clLugarDato.BorderWidthRight = 0;
            clLugarDato.BorderWidthTop = 0;
            clLugarDato.HorizontalAlignment = Element.ALIGN_LEFT;
            clLugarDato.VerticalAlignment = Element.ALIGN_MIDDLE;
            clLugarDato.UseAscender = true;

            tblDatosPersonales.AddCell(clNombre);
            tblDatosPersonales.AddCell(clNombreDato);

            tblDatosPersonales.AddCell(clRfc);
            tblDatosPersonales.AddCell(clRFCDatos);
            tblDatosPersonales.AddCell(clEdad);
            tblDatosPersonales.AddCell(clEdadDatos);
            tblDatosPersonales.AddCell(clGenero);
            tblDatosPersonales.AddCell(clGeneroDatos);

            tblDatosPersonales.AddCell(clDependencia);
            tblDatosPersonales.AddCell(clDependenciaDato);

            tblDatosPersonales.AddCell(clLugar);
            tblDatosPersonales.AddCell(clLugarDato);

            tblDatosPersonales.AddCell(clEvaluacion);
            tblDatosPersonales.AddCell(clEvaluacionDato);

            tblDatosPersonales.AddCell(clPuesto);
            tblDatosPersonales.AddCell(clPuestoDato);

            docCadena.Add(tblDatosPersonales);
            #endregion

            Paragraph temAna = new Paragraph(new Phrase("TEMPERATURA:________________________           ANALITO:_______________________________________________", fontDato));
            temAna.Alignment = Element.ALIGN_LEFT;
            docCadena.Add(temAna);

            #region cadena
            PdfPTable tblCadCus = new PdfPTable(4)
            {
                TotalWidth = 560,
                LockedWidth = true
            };
            float[] valCadCus = new float[4] { 100, 90, 185, 185 };
            tblCadCus.SetWidths(valCadCus);
            //tblCadCus.HorizontalAlignment = 0;
            tblCadCus.SpacingBefore = 5f;
            tblCadCus.SpacingAfter = 5f;
            tblCadCus.DefaultCell.Border = 1;

            //--------------------------------------------------------------------------------------- 1a Linea : Titulos
            PdfPCell clFecha = new PdfPCell(new Phrase("FECHA, HORA Y LUGAR", fontDatosCelda));
            clFecha.HorizontalAlignment = Element.ALIGN_CENTER;
            clFecha.VerticalAlignment = Element.ALIGN_MIDDLE;
            clFecha.UseAscender = true;
            clFecha.FixedHeight = 15f;
            tblCadCus.AddCell(clFecha);

            PdfPCell clActividad = new PdfPCell(new Phrase("ACTIVIDAD", fontDatosCelda));
            clActividad.HorizontalAlignment = Element.ALIGN_CENTER;
            clActividad.VerticalAlignment = Element.ALIGN_MIDDLE;
            clActividad.UseAscender = true;
            tblCadCus.AddCell(clActividad);

            PdfPCell clEntrega = new PdfPCell(new Phrase("QUIEN ENTREGA", fontDatosCelda));
            clEntrega.HorizontalAlignment = Element.ALIGN_CENTER;
            clEntrega.VerticalAlignment = Element.ALIGN_MIDDLE;
            clEntrega.UseAscender = true;
            tblCadCus.AddCell(clEntrega);

            PdfPCell clRecibe = new PdfPCell(new Phrase("QUIEN RECIBE", fontDatosCelda));
            clRecibe.HorizontalAlignment = Element.ALIGN_CENTER;
            clRecibe.VerticalAlignment = Element.ALIGN_MIDDLE;
            clRecibe.UseAscender = true;
            tblCadCus.AddCell(clRecibe);

            //--------------------------------------------------------------------------------------- 2 Linea
            PdfPCell clFecha_b = new PdfPCell();
            clFecha_b.AddElement(new Phrase("SANITARIO", fontDatosCelda));
            clFecha_b.AddElement(new Phrase("FECHA:____________", fontDatosCelda));
            clFecha_b.AddElement(new Phrase("HORA:_____________", fontDatosCelda));
            clFecha_b.HorizontalAlignment = Element.ALIGN_LEFT;
            clFecha_b.FixedHeight = 50f;
            tblCadCus.AddCell(clFecha_b);

            PdfPCell clActividad_b = new PdfPCell(new Phrase("RECOLECCION DE LA MUESTRA", fontDatosCelda));
            clActividad_b.HorizontalAlignment = Element.ALIGN_CENTER;
            clActividad_b.VerticalAlignment = Element.ALIGN_MIDDLE;
            clActividad_b.UseAscender = true;
            tblCadCus.AddCell(clActividad_b);

            PdfPCell clEntrega_b = new PdfPCell(new Phrase(cadenaIndividual.evaluado + "\nNOMBRE Y FIRMA DEL EVALUADO", fontDatosCelda));
            clEntrega_b.HorizontalAlignment = Element.ALIGN_CENTER;
            clEntrega_b.VerticalAlignment = Element.ALIGN_BOTTOM;
            tblCadCus.AddCell(clEntrega_b);

            PdfPCell clRecibe_b = new PdfPCell(new Phrase(cadenaIndividual.userquim + "\nNOMBRE Y FIRMA DEL SUPERVISOR OCULAR", fontDatosCelda));
            clRecibe_b.VerticalAlignment = Element.ALIGN_BOTTOM;
            clRecibe_b.HorizontalAlignment = Element.ALIGN_CENTER;
            tblCadCus.AddCell(clRecibe_b);

            //--------------------------------------------------------------------------------------- 3 Linea
            PdfPCell clFecha_c = new PdfPCell();
            clFecha_c.AddElement(new Phrase("LABORATORIO", fontDatosCelda));
            clFecha_c.AddElement(new Phrase("FECHA:____________", fontDatosCelda));
            clFecha_c.AddElement(new Phrase("HORA:____________", fontDatosCelda));
            clFecha_c.HorizontalAlignment = Element.ALIGN_LEFT;
            clFecha_c.FixedHeight = 50f;
            tblCadCus.AddCell(clFecha_c);

            PdfPCell clActividad_c = new PdfPCell(new Phrase("ENTREGA DE LA MUESTRA", fontDatosCelda));
            clActividad_c.HorizontalAlignment = Element.ALIGN_CENTER;
            clActividad_c.VerticalAlignment = Element.ALIGN_MIDDLE;
            clActividad_c.UseAscender = true;
            tblCadCus.AddCell(clActividad_c);

            PdfPCell clEntrega_c = new PdfPCell(new Phrase(cadenaIndividual.userquim + "\nNOMBRE Y FIRMA DEL SUPERVISOR OCULAR", fontDatosCelda));
            clEntrega_c.HorizontalAlignment = Element.ALIGN_CENTER;
            clEntrega_c.VerticalAlignment = Element.ALIGN_BOTTOM;
            tblCadCus.AddCell(clEntrega_c);

            PdfPCell clRecibe_c = new PdfPCell(new Phrase(cadenaIndividual.usertox + "\nNOMBRE Y FIRMA DEL ANALISTA", fontDatosCelda));
            clRecibe_c.VerticalAlignment = Element.ALIGN_BOTTOM;
            clRecibe_c.HorizontalAlignment = Element.ALIGN_CENTER;
            tblCadCus.AddCell(clRecibe_c);

            //--------------------------------------------------------------------------------------- 4 Linea
            PdfPCell clFecha_d = new PdfPCell();
            clFecha_d.AddElement(new Phrase("LABORATORIO", fontDatosCelda));
            clFecha_d.AddElement(new Phrase("FECHA:____________", fontDatosCelda));
            clFecha_d.AddElement(new Phrase("HORA:____________", fontDatosCelda));
            clFecha_d.HorizontalAlignment = Element.ALIGN_LEFT;
            clFecha_d.FixedHeight = 50f;
            tblCadCus.AddCell(clFecha_d);

            PdfPCell clActividad_d = new PdfPCell(new Phrase("ENTREGA PARA EL RESGUARDO DE LA MUESTRA", fontDatosCelda));
            clActividad_d.HorizontalAlignment = Element.ALIGN_CENTER;
            clActividad_d.VerticalAlignment = Element.ALIGN_MIDDLE;
            clActividad_d.UseAscender = true;
            tblCadCus.AddCell(clActividad_d);

            PdfPCell clEntrega_d = new PdfPCell(new Phrase(cadenaIndividual.usertox + "\nNOMBRE Y FIRMA DEL ANALISTA", fontDatosCelda));
            clEntrega_d.HorizontalAlignment = Element.ALIGN_CENTER;
            clEntrega_d.VerticalAlignment = Element.ALIGN_BOTTOM;
            tblCadCus.AddCell(clEntrega_d);

            PdfPCell clRecibe_d = new PdfPCell(new Phrase("______________________________________\nNOMBRE Y FIRMA DE QUIEN RESGUARDA LA MUESTRA", fontDatosCelda));
            clRecibe_d.VerticalAlignment = Element.ALIGN_BOTTOM;
            clRecibe_d.HorizontalAlignment = Element.ALIGN_CENTER;
            tblCadCus.AddCell(clRecibe_d);

            //--------------------------------------------------------------------------------------- 5 Linea
            PdfPCell clFecha_e = new PdfPCell();
            clFecha_e.AddElement(new Phrase("LABORATORIO", fontDatosCelda));
            clFecha_e.AddElement(new Phrase("FECHA:____________", fontDatosCelda));
            clFecha_e.AddElement(new Phrase("HORA:____________", fontDatosCelda));
            clFecha_e.HorizontalAlignment = Element.ALIGN_LEFT;
            clFecha_e.FixedHeight = 50f;
            tblCadCus.AddCell(clFecha_e);

            PdfPCell clActividad_e = new PdfPCell(new Phrase("ENTREGA DE LA MUESTRA PARA EL ENVIO A ESTUDIO CONFIRMATORIO", fontDatosCelda));
            clActividad_e.HorizontalAlignment = Element.ALIGN_CENTER;
            clActividad_e.VerticalAlignment = Element.ALIGN_MIDDLE;
            clActividad_e.UseAscender = true;
            tblCadCus.AddCell(clActividad_e);

            PdfPCell clEntrega_e = new PdfPCell(new Phrase("______________________________________\nNOMBRE Y FIRMA DE QUIEN RESGUARDA LA MUESTRA", fontDatosCelda));
            clEntrega_e.HorizontalAlignment = Element.ALIGN_CENTER;
            clEntrega_e.VerticalAlignment = Element.ALIGN_BOTTOM;
            tblCadCus.AddCell(clEntrega_e);

            PdfPCell clRecibe_e = new PdfPCell(new Phrase("______________________________________\nNOMBRE Y FIRMA DE QUIEN LLEVA LA MUESTRA", fontDatosCelda));
            clRecibe_e.VerticalAlignment = Element.ALIGN_BOTTOM;
            clRecibe_e.HorizontalAlignment = Element.ALIGN_CENTER;
            tblCadCus.AddCell(clRecibe_e);

            //--------------------------------------------------------------------------------------- 6 Linea
            PdfPCell clFecha_f = new PdfPCell();
            clFecha_f.AddElement(new Phrase("LABORATORIO", fontDatosCelda));
            clFecha_f.AddElement(new Phrase("FECHA:____________", fontDatosCelda));
            clFecha_f.AddElement(new Phrase("HORA:____________", fontDatosCelda));
            clFecha_f.HorizontalAlignment = Element.ALIGN_LEFT;
            clFecha_f.FixedHeight = 50f;
            tblCadCus.AddCell(clFecha_f);

            PdfPCell clActividad_f = new PdfPCell(new Phrase("ENTREGA DE MUESTRAS PARA EL ESTUDIO CONFIRMATORIO", fontDatosCelda));
            clActividad_f.HorizontalAlignment = Element.ALIGN_CENTER;
            clActividad_f.VerticalAlignment = Element.ALIGN_MIDDLE;
            clActividad_f.UseAscender = true;
            tblCadCus.AddCell(clActividad_f);

            PdfPCell clEntrega_f = new PdfPCell(new Phrase("______________________________________\nNOMBRE Y FIRMA DE QUIEN LLEVA LA MUESTRA", fontDatosCelda));
            clEntrega_f.HorizontalAlignment = Element.ALIGN_CENTER;
            clEntrega_f.VerticalAlignment = Element.ALIGN_BOTTOM;
            tblCadCus.AddCell(clEntrega_f);

            PdfPCell clRecibe_f = new PdfPCell(new Phrase("______________________________________\nNOMBRE Y FIRMA DE REPRESENTANTE DEL LABORATORIO CONFIRMATORIO", fontDatosCelda));
            clRecibe_f.VerticalAlignment = Element.ALIGN_BOTTOM;
            clRecibe_f.HorizontalAlignment = Element.ALIGN_CENTER;
            tblCadCus.AddCell(clRecibe_f);

            //--------------------------------------------------------------------------------------- 7 Linea
            PdfPCell clFecha_h = new PdfPCell();
            clFecha_h.AddElement(new Phrase("LABORATORIO", fontDatosCelda));
            clFecha_h.AddElement(new Phrase("FECHA:____________", fontDatosCelda));
            clFecha_h.AddElement(new Phrase("HORA:____________", fontDatosCelda));
            clFecha_h.HorizontalAlignment = Element.ALIGN_LEFT;
            clFecha_h.FixedHeight = 50f;
            tblCadCus.AddCell(clFecha_h);

            PdfPCell clActividad_h = new PdfPCell(new Phrase("RECEPCION DE RESULTADOS", fontDatosCelda));
            clActividad_h.HorizontalAlignment = Element.ALIGN_CENTER;
            clActividad_h.VerticalAlignment = Element.ALIGN_MIDDLE;
            clActividad_h.UseAscender = true;
            tblCadCus.AddCell(clActividad_h);

            PdfPCell clEntrega_h = new PdfPCell(new Phrase("______________________________________\nNOMBRE Y FIRMA DE REPRESENTANTE DEL LABORATORIO CONFIRMATORIO", fontDatosCelda));
            clEntrega_h.HorizontalAlignment = Element.ALIGN_CENTER;
            clEntrega_h.VerticalAlignment = Element.ALIGN_BOTTOM;
            tblCadCus.AddCell(clEntrega_h);

            PdfPCell clRecibe_h = new PdfPCell(new Phrase("______________________________________\nNOMBRE Y FIRMA DE QUIEN RECIBE RESULTADOS CONFIRMATORIOS", fontDatosCelda));
            clRecibe_h.VerticalAlignment = Element.ALIGN_BOTTOM;
            clRecibe_h.HorizontalAlignment = Element.ALIGN_CENTER;
            tblCadCus.AddCell(clRecibe_h);

            docCadena.Add(tblCadCus);
            #endregion

            #region Resultados - observaciones
            Paragraph resObs = new Paragraph();
            resObs.Add(new Phrase("Resultado de adulterantes en orina: _______________________________________________________________________", fontDato));
            resObs.Add(Chunk.NEWLINE);
            resObs.Add(new Phrase("OBSERVACIONES ____________________________________________________________________________________", fontDato));
            resObs.Add(Chunk.NEWLINE);
            resObs.Add(new Phrase("____________________________________________________________________________________________________", fontDato));
            resObs.Alignment = Element.ALIGN_LEFT;

            docCadena.Add(resObs);
            #endregion

            #region Resultados - observaciones
            Paragraph finCus = new Paragraph();
            finCus.Add(new Phrase("Este formato acompañara a la muestra en todo momento, toda persona que la maneje deberá llenar los espacios correspondientes del formato.", fontDatosCelda));
            finCus.Add(Chunk.NEWLINE);
            finCus.Add(new Phrase("Este formato se sustenta en el de emisión de resultados CECCC/CMT/04", fontDatosCelda));
            finCus.Alignment = Element.ALIGN_LEFT;

            docCadena.Add(finCus);
            #endregion

            #region final centro
            Paragraph finCusCentro = new Paragraph(new Phrase("ESTE FORMATO ES DE CARÁCTER CONFIDENCIAL.", fontDatosCelda));
            finCusCentro.Alignment = Element.ALIGN_CENTER;
            docCadena.Add(finCusCentro);
            #endregion

            #region finfinal
            PdfPTable fin = new PdfPTable(2)
            {
                TotalWidth = 560,
                LockedWidth = true
            };
            float[] final = new float[2] { 280, 280 };
            fin.SetWidths(final);
            fin.HorizontalAlignment = 0;
            fin.SpacingBefore = 10f;
            fin.SpacingAfter = 5f;
            fin.DefaultCell.Border = 0;

            PdfPCell clfolio = new PdfPCell(new Phrase(cadenaIndividual.folio, fonEiqueta));
            clfolio.BorderWidth = 0;
            clfolio.HorizontalAlignment = Element.ALIGN_LEFT;

            PdfPCell clCodigo_c = new PdfPCell(new Phrase(cadenaIndividual.codigoevaluado, fonEiqueta));
            clCodigo_c.BorderWidth = 0;
            clCodigo_c.HorizontalAlignment = Element.ALIGN_RIGHT;

            fin.AddCell(clfolio);
            fin.AddCell(clCodigo_c);

            docCadena.Add(fin);
            #endregion

            docCadena.Close();
            byte[] byteStream = msCad.ToArray();
            msCad = new MemoryStream();
            msCad.Write(byteStream, 0, byteStream.Length);
            msCad.Position = 0;

            return new FileStreamResult(msCad, "application/pdf");
        }
    }
}
