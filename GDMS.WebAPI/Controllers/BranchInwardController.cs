using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Kotak.Entities;
using Kotak.Repository.Interfaces;
using Kotak.WebAPI.Extensions;
using System.Web.Http.Cors;
using System.Configuration;
using System.IO;
using System.Web;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace Kotak.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BranchInwardController : ApiController
    {
		IBranchInward<BranchInwardEntity> objRepositary;
		public BranchInwardController(IBranchInward<BranchInwardEntity> obj)
		{
			objRepositary = obj;
		}
		[HttpGet]
		[Route("api/BranchInward/GetList")]
		public HttpResponseMessage GetList([FromUri] string user_Token)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var objData = objRepositary.Get();
					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Internal server error"); ;
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}
	 

		[HttpGet]
		[Route("api/BranchInward/GetBatchDetails")]
		public HttpResponseMessage GetBatchDetails([FromUri]  string BatchNo, string USERId, string user_Token)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var objData = objRepositary.GetBatchDetails(BatchNo, USERId);
					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}



		[HttpPost]
		[Route("api/BranchInward/GetAppacDetails")]
		public HttpResponseMessage GetAppacDetails([FromBody] BranchInwardEntity objEntity)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
				{
					var objData = objRepositary.GetAppacDetails(objEntity);
					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}


		[HttpGet]
		[Route("api/BranchInward/getDocumentdetails")]
		public HttpResponseMessage getDocumentdetails([FromUri] string user_Token, string appl, string apac)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var objData = objRepositary.getDocumentdetails(appl, apac);
					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}


		[HttpPost]
		[Route("api/BranchInward/AddEditAppacdetails")]
		public HttpResponseMessage AddEditAppacdetails([FromBody] BranchInwardEntity objEntity)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
				{
					var objData = objRepositary.Add(objEntity);
					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}

		private void LogError(Exception ex)
		{
			var TempPath = (ConfigurationManager.AppSettings["temp"].ToString());
			string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
			message += Environment.NewLine;
			message += "-----------------------------------------------------------";
			message += Environment.NewLine;
			message += string.Format("Message: {0}", ex.Message);
			message += Environment.NewLine;
			message += string.Format("StackTrace: {0}", ex.StackTrace);
			message += Environment.NewLine;
			message += string.Format("Source: {0}", ex.Source);
			message += Environment.NewLine;
			message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
			message += Environment.NewLine;
			message += "-----------------------------------------------------------";
			message += Environment.NewLine;
			string path = Path.Combine(TempPath, "ErrorLog.txt");
			using (StreamWriter writer = new StreamWriter(path, true))
			{
				writer.WriteLine(message);
				writer.Close();
			}
		}

		[HttpPost]
		[Route("api/BranchInward/UpdateBatchNo")]
		public HttpResponseMessage UpdateBatchNo([FromBody] BranchInwardEntity objEntity)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
				{
					var objData = objRepositary.UpdateBatchNo(objEntity);
					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}

		[HttpGet]
		[Route("api/BranchInward/getPODdetails")]
		public HttpResponseMessage getPODdetails([FromUri]  string UserID, string user_Token)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var objData = objRepositary.getPODdetails(UserID);
					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}


		[HttpPost]
		[Route("api/BranchInward/PODdetailsEntry")]
		public HttpResponseMessage PODdetailsEntry([FromBody] BranchInwardEntity objEntity)
		{
			try
			{

				if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
				{
					string val = objRepositary.PODdetailsEntry(objEntity);
					return Request.CreateResponse(HttpStatusCode.OK, val);
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}



		[HttpGet]
		[Route("api/BranchInward/DownloadFile")]
		public HttpResponseMessage DownloadFile([FromUri] string BatchNo, string user_Token)
		{
			try
			{

				if (ValidateTokenExtension.ValidateToken(user_Token))
				{

					var objData = objRepositary.GetBatchdetailsByBatchNo(BatchNo);
					//	string Filetype = Path.GetExtension(objData);


					var TempPath = Path.Combine(ConfigurationManager.AppSettings["temp"].ToString(), Guid.NewGuid().ToString() + ".pdf");

					if (objData.Count() > 0)
					{
						foreach (var file1 in objData)
						{

							GeneratePDFFile(file1.location, TempPath, file1.batch_no, file1.pod_no, file1.pod_dispatch_date, file1.appl_apac, file1.courier_name, file1.location, file1.sub_lcoation);

						}
					}
					 
					HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
					byte[] bytes = System.IO.File.ReadAllBytes(TempPath);
					result.Content = new ByteArrayContent(bytes);

					result.Content.Headers.ContentLength = bytes.LongLength;

					var dataStream = new MemoryStream(bytes);
					result.Content = new StreamContent(dataStream);
					result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
					result.Content.Headers.ContentDisposition.FileName = "123123.pdf";
					result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
					return result;


				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);

				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}



		[HttpGet]
		[Route("api/BranchInward/getPODAckPending")]
		public HttpResponseMessage getPODAckPending([FromUri] string UserID, string user_Token)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var objData = objRepositary.getPODAckPending(UserID);
					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}


		[HttpPost]
		[Route("api/BranchInward/PODAckdetailsEntry")]
		public HttpResponseMessage PODAckdetailsEntry([FromBody] BranchInwardEntity objEntity)
		{
			try
			{

				if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
				{
					string val = objRepositary.PODAckdetailsEntry(objEntity);
					return Request.CreateResponse(HttpStatusCode.OK, val);
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}

		[HttpPost]
		[Route("api/BranchInward/Delete")]
		public HttpResponseMessage Delete([FromBody] BranchInwardEntity objEntity)
		{
			var response = new HttpResponseMessage();
			if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
			{
				if (objRepositary.DeleteApack(objEntity.BatchNo, objEntity.appl, objEntity.apac))
				{
					response = Request.CreateResponse(HttpStatusCode.OK, "Succsessfuly Deleted!!!");
				}
				else
				{
					response = Request.CreateResponse(HttpStatusCode.BadRequest, "Delete Failed!!!");
				}
			}
			else
			{
				response = Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid token!!!");
			}
			return response;
		}

		protected void GeneratePDFFile(string ToAddress, string PDFFilepath, string BatchNo, string PODNo, string date, string LanCount, string CourierName, string location, string sub_lcoation)
		{

			//string FromAddress = "Branch Code :- "+ BranchCode + " , Branch Name :- "+ BranchName;
			//string TOAddress= "Crown Records Management ,  119 / 1B 1K , Moopanar Street, Pandur Village , Permattunallur, Guduvancherry – 603202., Phone N0. 04435102751";



			//Create document  
			Document doc = new Document();
			//Create PDF Table  
			PdfPTable tableLayout = new PdfPTable(2);
			PdfPTable tableLayout1 = new PdfPTable(1);
			//Create a PDF file in specific path  
			PdfWriter.GetInstance(doc, new FileStream(PDFFilepath, FileMode.Create));
			//Open the PDF document  
			doc.Open();
			//Add Content to PDF  
			doc.Add(Add_Content_To_PDF(tableLayout, BatchNo, PODNo, date, LanCount.ToString(), CourierName, location, sub_lcoation));

			doc.Add(Add_Address(tableLayout1, ToAddress));



			// Closing the document  
			doc.Close();
			//		btnOpenPDFFile.Enabled = true;
			//	btnGeneratePDFFile.Enabled = false;
		}

		private PdfPTable Add_Address(PdfPTable tableLayout, string ToAddress)
		{
			float[] headers = {

		100
	}; //Header Widths  
			tableLayout.SetWidths(headers); //Set the pdf headers  
			tableLayout.WidthPercentage = 100; //Set the PDF File witdh percentage  
											   //Add Title to the PDF file at the top  
			tableLayout.AddCell(new PdfPCell(new Phrase("Address", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 25, 1, iTextSharp.text.BaseColor.BLACK)))

			//tableLayout.AddCell(new PdfPCell(new Phrase("Creating PDF file using iTextsharp", new iTextSharp.text.Font(Font.Bold, 13, 1, new iTextSharp.text.BaseColor(153, 51, 0))))
			{
				Colspan = 6,
				Border = 1,
				PaddingBottom = 20,
				HorizontalAlignment = Element.ALIGN_CENTER
			});


			//{
			//    //Colspan = 6,
			//    //Border = 1,
			//    //PaddingBottom = 20,
			//    HorizontalAlignment = Element.ALIGN_CENTER;
			//});




			//content.AddCell(cell);

			//Add header  
			//AddCellToHeader(tableLayout, "Batch Reference Barcode");
			//AddCellToBody(tableLayout, "To Address");
			//AddCellToHeader(tableLayout, "From Address");

			AddCellToBodyAddress(tableLayout, ToAddress);
			//	AddCellToBody(tableLayout, FromAddress);

			return tableLayout;
		}
		private PdfPTable Add_Content_To_PDF(PdfPTable tableLayout, string BatchNo, string PODNo, string Ddate, string LanCount, string CourierName, string BranchName, string  BranchCode)
		{
			float[] headers = {
		40,
		50
	}; //Header Widths  
			tableLayout.SetWidths(headers); //Set the pdf headers  
			tableLayout.WidthPercentage = 100; //Set the PDF File witdh percentage  

			//tableLayout.                                //Add Title to// the PDF file at the top    //C:\temp\Kotak

			//iTextSharp.text.Image myImage = iTextSharp.text.Image.GetInstance("c:\\temp\\Kotak\\logo.png");
			//PdfPCell cell = new PdfPCell(myImage);

			//tableLayout.AddCell(new PdfPCell(myImage));

			tableLayout.AddCell(new PdfPCell(new Phrase("Kotak File Dispatch Details", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 14, 1, iTextSharp.text.BaseColor.BLACK)))

			//tableLayout.AddCell(new PdfPCell(new Phrase("Creating PDF file using iTextsharp", new iTextSharp.text.Font(Font.Bold, 13, 1, new iTextSharp.text.BaseColor(153, 51, 0))))
			{
				Colspan = 6,
				Border = 0,
				PaddingBottom = 20,
				HorizontalAlignment = Element.ALIGN_CENTER
			});
			//Add header  
			//AddCellToHeader(tableLayout, "Batch Reference Barcode");
			AddCellToHeader(tableLayout, "Name");
			AddCellToHeader(tableLayout, "Value");

			//AddCellToBody(tableLayout, "Batch Reference Barcode");
			//AddCellToBody(tableLayout, BatchNo);

			AddCellToBody(tableLayout, "Batch Reference ID");
			AddCellToBody(tableLayout, BatchNo);

			AddCellToBody(tableLayout, "Location");
			AddCellToBody(tableLayout, BranchName);

			AddCellToBody(tableLayout, "SubLocation");
			AddCellToBody(tableLayout, BranchCode);

			AddCellToBody(tableLayout, "Consignment No");
			AddCellToBody(tableLayout, PODNo);

			AddCellToBody(tableLayout, "Courier Name");
			AddCellToBody(tableLayout, CourierName);
			AddCellToBody(tableLayout, "Dispatch Date");

			AddCellToBody(tableLayout, Ddate);
			AddCellToBody(tableLayout, "No. of Account No.");
			AddCellToBody(tableLayout, LanCount.ToString());



			return tableLayout;
		}
		// Method to add single cell to the header  
		private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
		{
			tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 18, 1, iTextSharp.text.BaseColor.WHITE)))
			{
				HorizontalAlignment = Element.ALIGN_LEFT,
				Padding = 5,
				BackgroundColor = new iTextSharp.text.BaseColor(0, 51, 102)
			});
		}
		// Method to add single cell to the body  
		private static void AddCellToBody(PdfPTable tableLayout, string cellText)
		{
			tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 18, 1, iTextSharp.text.BaseColor.BLACK)))
			{
				HorizontalAlignment = Element.ALIGN_LEFT,
				Padding = 5,
				BackgroundColor = iTextSharp.text.BaseColor.WHITE
			});
		}

		private static void AddCellToBodyAddress(PdfPTable tableLayout, string cellText)
		{
			tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 25, 1, iTextSharp.text.BaseColor.BLACK)))
			{
				HorizontalAlignment = Element.ALIGN_LEFT,
				Padding = 5,
				BackgroundColor = iTextSharp.text.BaseColor.WHITE
			});
		}

	}
}
