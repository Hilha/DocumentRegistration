using DocumentRegistration.DAL;
using DocumentRegistration.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocumentRegistration.Controllers
{
    public class DocumentController : Controller
    {
        // GET: Documents
        public ActionResult Index()
        {
            using (var Context = new DocumentContext())
            {
                var Documents = Context.DocumentModel.ToList();
                var DocumentsInAlfabeticOrder = Documents.OrderBy(d => d.Title);

                return View(DocumentsInAlfabeticOrder);
            }
        }

        // GET: Document/Details/5
        public ActionResult Details(long Id)
        {
            using (var Context = new DocumentContext())
            {
                var Document = Context.DocumentModel.Where(x => x.Id == Id).SingleOrDefault();

                if (Document == null)
                {
                    TempData["Error"] = "Documento inexistente!";
                    return RedirectToAction("Index");
                }

                return View(Document);
            }
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
                    using (var Context = new DocumentContext())
                    {
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

                        DocumentModel Document = new DocumentModel();

                        LoadAndSaveDocumentFile(Document, PostedFile);
                        UpdateDocumentData(Document, Collection);

                        Context.DocumentModel.Add(Document);
                        Context.SaveChanges();
                    }

                    TempData["Success"] = "Documento cadastrado!";
                    return RedirectToAction("Index");
                }
                catch
                {
                    TempData["Error"] = "Tente novamente.";
                    return View();
                }

            }
            
            TempData["Error"] = "Formulario inválido.";
            return View();
        }

        // GET: Document/Edit/5
        public ActionResult Edit(long Id)
        {
            using (var Context = new DocumentContext())
            {
                var Document = Context.DocumentModel.Where(x => x.Id == Id).SingleOrDefault();

                if (Document == null)
                {
                    TempData["Error"] = "Documento Inexistente!";
                    return RedirectToAction("Index");
                }

                return View(Document);
            }
        }

        // POST: Document/Edit/5
        [HttpPost]
        public ActionResult Edit(long Id, DocumentModel Document, FormCollection Collection, HttpPostedFileBase PostedFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var Context = new DocumentContext())
                    {
                        var DocumentToUpdate = Context.DocumentModel.Where(x => x.Id == Id).SingleOrDefault();

                        if (DocumentToUpdate != null)
                        {
                            if (PostedFile != null)
                            {
                                if (!FileExtensionIsValid(PostedFile))
                                {
                                    TempData["Error"] = "Arquivo invalido";
                                    return View();
                                }

                                LoadAndSaveDocumentFile(DocumentToUpdate, PostedFile);
                            }

                            UpdateDocumentData(DocumentToUpdate, Collection);
                            Context.SaveChanges();

                            TempData["Success"] = "Documento editado!";
                            return RedirectToAction("Index");
                        }

                        TempData["Error"] = "Documento inexistente!";
                        return RedirectToAction("Index");
                    }
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
        public ActionResult Delete(int Id)
        {
            using (var Context = new DocumentContext())
            {
                var Document = Context.DocumentModel.Where(x => x.Id == Id).SingleOrDefault();

                if (Document == null)
                {
                    TempData["Error"] = "Documento inexistente!";
                    return RedirectToAction("Index");
                }

                return View(Document);
            }
        }

        // POST: Document/Delete/5
        [HttpPost]
        public ActionResult Delete(DocumentModel Document, long Id)
        {
            try
            {
                using (var Context = new DocumentContext())
                {
                    var DocumentToDelete = Context.DocumentModel.Where(x => x.Id == Id).SingleOrDefault();
                    Context.DocumentModel.Remove(DocumentToDelete);
                    Context.SaveChanges();

                    TempData["Success"] = "Documento deletado!";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                TempData["Error"] = "Tente novamente.";
                return View();
            }
        }

        public FileResult Download(string FilePath)
        {
            return File(FilePath, "application/force-download", Path.GetFileName(FilePath));
        }

        private Boolean FileExtensionIsValid(HttpPostedFileBase PostedFile)
        {
            var SupportedTypes = new[] { "pdf", "doc", "docx", "xls", "xlsx" };
            var FileExtension = System.IO.Path.GetExtension(PostedFile.FileName).Substring(1);

            return SupportedTypes.Contains(FileExtension);
        }

        public JsonResult VerifyCode(string Code, long ? Id)
        {
            return new DocumentContext().DocumentModel.FirstOrDefault(x => x.Code == Code && x.Id != Id) == null
                ? Json(true, JsonRequestBehavior.AllowGet) : Json(false, JsonRequestBehavior.AllowGet);
        }

        private void UpdateDocumentData(DocumentModel DocumentToUpdate, FormCollection Collection)
        {
            DocumentToUpdate.Code = Collection["Code"];
            DocumentToUpdate.Title = Collection["Title"];
            DocumentToUpdate.Process = Collection["Process"];
            DocumentToUpdate.Category = Collection["Category"];
        }

        private void LoadAndSaveDocumentFile(DocumentModel Document, HttpPostedFileBase PostedFile)
        {
            var UploadedFileName = Path.GetFileName(PostedFile.FileName);
            var UploadedFileExtension = Path.GetExtension(PostedFile.FileName);
            UploadedFileName = UploadedFileName.Trim() + UploadedFileExtension;
            var UploadedFilePath = Path.Combine(Server.MapPath("~/App_Data/Files"), UploadedFileName);
            PostedFile.SaveAs(UploadedFilePath);
            Document.FilePath = UploadedFilePath;
        }
    }


}
