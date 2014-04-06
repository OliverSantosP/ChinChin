using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using chinchini.Models;
using chinchini.ViewModels;

namespace chinchini.Controllers
{
    public class ProjectController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Project/
        public ActionResult Index()
        {
            var project = db.Project.Include(p => p.Pitch).Include(p => p.ProjectType).Include(p => p.Status);
            return View(project.ToList());
        }

        // GET: /Project/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Project.Include("Loan.Lenders.User").SingleOrDefault(x => x.ProjectID == id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: /Project/Create
        public ActionResult Create()
        {
            //ViewBag.PitchID = new SelectList(db.Pitch, "PitchID", "Name");
            ViewBag.ProjectTypeID = new SelectList(db.ProjectType, "ProjectTypeID", "Description");
            ViewBag.CategoryID = new SelectList(db.Category, "CategoryID", "Name");
            return View();
        }

        // POST: /Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project project)
        {

            if (project.ProjectTypeID == 1)
            {
                var user = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                var usergroup = db.UserGroup.Where(x=>x.GroupID == db.UserGroup.FirstOrDefault(ug=>ug.User_Id == user.Id).GroupID);

                if (usergroup.Count() < 2)
                {
                    ModelState.AddModelError("ProjectTypeID", "El grupo al que perteneces debe de tener al menos 3 integrantes!");
                }
            }

            if (ModelState.IsValid)
            {
                project.StatusID = 3;
                var pitch = project.Pitch;
                db.Pitch.Add(pitch);
                db.SaveChanges();
                project.PitchID = pitch.PitchID;
                db.Project.Add(project);
                db.SaveChanges();


                return RedirectToAction("Index");
            }

            ViewBag.PitchID = new SelectList(db.Pitch, "PitchID", "Name", project.PitchID);
            ViewBag.ProjectTypeID = new SelectList(db.ProjectType, "ProjectTypeID", "Description", project.ProjectTypeID);
            ViewBag.CategoryID = new SelectList(db.Category, "CategoryID", "Name");
            return View(project);
        }

        // GET: /Project/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.PitchID = new SelectList(db.Pitch, "PitchID", "Name", project.PitchID);
            ViewBag.ProjectTypeID = new SelectList(db.ProjectType, "ProjectTypeID", "Description", project.ProjectTypeID);
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Description", project.StatusID);
            return View(project);
        }

        // POST: /Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectID,Title,Description,Amount,StatusID,PitchID,ProjectTypeID")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PitchID = new SelectList(db.Pitch, "PitchID", "Name", project.PitchID);
            ViewBag.ProjectTypeID = new SelectList(db.ProjectType, "ProjectTypeID", "Description", project.ProjectTypeID);
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Description", project.StatusID);
            return View(project);
        }

        // GET: /Project/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: /Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Project.Find(id);
            db.Project.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpPost]
        public ActionResult UploadImages(int ProjectID)
        {
            var r = new List<ModelViewDataUploadFilesResult>();

            foreach (string file in Request.Files)
            {
                var statuses = new List<ModelViewDataUploadFilesResult>();
                var headers = Request.Headers;

                if (string.IsNullOrEmpty(headers["X-File-Name"]))
                {
                    UploadWholeFile(Request, statuses, ProjectID);
                }
                else
                {
                    UploadPartialFile(headers["X-File-Name"], Request, statuses, ProjectID);
                }

                JsonResult result = Json(statuses);
                result.ContentType = "text/plain";

                return result;
            }

            return Json(r);
        }



        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        //Credit to i-e-b and his ASP.Net uploader for the bulk of the upload helper methods - https://github.com/i-e-b/jQueryFileUpload.Net
        private void UploadPartialFile(string fileName, HttpRequestBase request, List<ModelViewDataUploadFilesResult> statuses, int ProjectID)
        {
            if (request.Files.Count != 1) throw new HttpRequestValidationException("Attempt to upload chunked file containing more than one fragment per request");
            var file = request.Files[0];
            var inputStream = file.InputStream;

            file.SaveAs(Server.MapPath("~/Content/Images"));
            var project = db.Project.Single(x => x.ProjectID == ProjectID);
            project.Pitch.PitchGallery.Add(new PitchGallery
            {
                ImageURL = "~/Images/" + file.FileName,
                StatusID = 2
            });
            db.SaveChanges();

            statuses.Add(new ModelViewDataUploadFilesResult()
            {
                name = fileName,
                size = file.ContentLength,
                type = file.ContentType,
                url = "/Home/Download/" + fileName,
                delete_url = Url.Action("DeleteFile", "Task", new { projectId = ProjectID, filename = fileName }),
                thumbnail_url = "",//@"data:image/png;base64," + EncodeFile(fullName),
                delete_type = "GET",
            });
        }

        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        //Credit to i-e-b and his ASP.Net uploader for the bulk of the upload helper methods - https://github.com/i-e-b/jQueryFileUpload.Net
        private void UploadWholeFile(HttpRequestBase request, List<ModelViewDataUploadFilesResult> statuses, int ProjectID)
        {
            for (int i = 0; i < request.Files.Count; i++)
            {
                var file = request.Files[i];


                file.SaveAs(Server.MapPath("~/Content/Images"));
                var project = db.Project.Single(x => x.ProjectID == ProjectID);
                project.Pitch.PitchGallery.Add(new PitchGallery
                {
                    ImageURL = "~/Images/" + file.FileName,
                    StatusID = 2
                });
                db.SaveChanges();


                statuses.Add(new ModelViewDataUploadFilesResult()
                {
                    name = file.FileName,
                    size = file.ContentLength,
                    type = file.ContentType,
                    url = "/Home/Download/" + file.FileName,
                    delete_url = Url.Action("DeleteFile", "Task", new { projectID = ProjectID, filename = file.FileName }),
                    thumbnail_url = "",//@"data:image/png;base64," + EncodeFile(fullPath),
                    delete_type = "GET",
                });
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
