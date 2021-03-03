using DocumentRegistration.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using DocumentRegistration.DAL;

namespace DocumentRegistration.Controllers
{
    public class DocumentController : Controller
    {

        static IList<DocumentModel> documentList = new List<DocumentModel>
        {
            new DocumentModel() {Id = 1, Title = "Teste1", Code = 12345, Process = "process1", Category = "Support"},
            new DocumentModel() {Id = 2, Title = "Teste2", Code = 12345, Process = "process2", Category = "Delopment"},
            new DocumentModel() {Id = 3, Title = "Teste3", Code = 12345, Process = "process3", Category = "Test"},
            new DocumentModel() {Id = 4, Title = "Teste4", Code = 12345, Process = "process4", Category = "Call"}
        };

        // GET: Documents
        public ActionResult Index()
        {
            return View(documentList);
        }

        // GET: Document/Details/5
        public ActionResult Details(long Id)
        {
            var document = documentList.Where(d => d.Id == Id).FirstOrDefault();

            return View(document);
        }

        // GET: Document/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Document/Create
        [HttpPost]
        public ActionResult Create(FormCollection Collection, HttpPostedFileBase PostedFile)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    //using (var context = new DocumentContext())
                    //{
                            if (PostedFile == null)
                        {
                            TempData["Error"] = "Arquivo ausente";
                            return View();
                        }

                        if (!FileExtensionIsValid(PostedFile))
                        {
                            TempData["Error"] = "Arquivo invalido";

                            return View();
                        }



                        //Get Name and Extension of posted file
                        var UploadedFileName = Path.GetFileName(PostedFile.FileName);
                        var UploadedFileExtension = Path.GetExtension(PostedFile.FileName);
                        UploadedFileName = UploadedFileName.Trim() + UploadedFileExtension;
                        var UploadedFilePath = Path.Combine(Server.MapPath("~/App_Data/Files"), UploadedFileName);

                        DocumentModel Document = new DocumentModel
                        {
                            Category = Collection["Category"],
                            Title = Collection["Title"],
                            Process = Collection["Process"],
                            Code = Int32.Parse(Collection["Code"]),
                            FilePath = UploadedFilePath
                        };

                        //context.Documents.Add(Document);
                        //context.SaveChanges();
                        PostedFile.SaveAs(UploadedFilePath);

                  //  }

                    //documentList.Add(Document);

                    TempData["Success"] = "Documento cadastrado!";
                    return RedirectToAction("Index");
                }
                catch
                {
                    TempData["Error"] = "Tente novamente.";
                    return View();
                }

            }
            else
            {
                TempData["Error"] = "Formulario inválido.";
                return View();
            }

        }

        // GET: Document/Edit/5
        public ActionResult Edit(long Id)
        {
            var document = documentList.Where(d => d.Id == Id).FirstOrDefault();

            return View(document);
        }

        // POST: Document/Edit/5
        [HttpPost]
        public ActionResult Edit(DocumentModel Document, FormCollection Collection, HttpPostedFileBase PostedFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //update student in DB using EntityFramework in real-life application
                    //update list by removing old student and adding updated student for demo purpose

                    var oldDocument = documentList.Where(d => d.Id == Document.Id).FirstOrDefault();

                    if (PostedFile != null)
                    {
                        if (!FileExtensionIsValid(PostedFile))
                        {
                            TempData["Error"] = "Arquivo invalido";
                            return View();
                        }

                        var UploadedFileName = Path.GetFileName(PostedFile.FileName);
                        var UploadedFileExtension = Path.GetExtension(PostedFile.FileName);
                        UploadedFileName = UploadedFileName.Trim() + UploadedFileExtension;
                        var UploadedFilePath = Path.Combine(Server.MapPath("~/App_Data/Files"), UploadedFileName);
                        PostedFile.SaveAs(UploadedFilePath);
                        Document.FilePath = UploadedFilePath;
                    }
                    else
                    {
                        Document.FilePath = oldDocument.FilePath;
                    }

                    documentList.Remove(oldDocument);
                    documentList.Add(Document);

                    TempData["Success"] = "Documento editado!";
                    return RedirectToAction("Index");
                }
                catch
                {
                    TempData["Error"] = "Tente novamente.";
                    return View(Document);
                }
            }

            ViewData["Error"] = "Formulario inválido.";
            return View(Document);
        }

        // GET: Document/Delete/5
        public ActionResult Delete(int id)
        {
            var document = documentList.Where(d => d.Id == id).FirstOrDefault();

            return View(document);
        }

        // POST: Document/Delete/5
        [HttpPost]
        public ActionResult Delete(DocumentModel document, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                documentList.Remove(document);

                TempData["Success"] = "Documento deletado!";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Error"] = "Tente novamente.";
                return View();
            }
        }

        public FileResult Download(string FilePath)
        {
            //var FileVirtualPath = "~/App_Data/Files/" + FileName;
            return File(FilePath, "application/force-download", Path.GetFileName(FilePath));
        }

        public Boolean FileExtensionIsValid(HttpPostedFileBase PostedFile)
        {
            var SupportedTypes = new[] { "pdf", "doc", "docx", "xls", "xlsx" };
            var FileExtension = System.IO.Path.GetExtension(PostedFile.FileName).Substring(1);

            return SupportedTypes.Contains(FileExtension);
        }
    }


}
