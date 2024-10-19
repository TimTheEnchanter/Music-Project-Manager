using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc;

namespace MusicProjectManager.Controllers
{
    public class MusicProjectManagerController : Controller
    {
        // To create View of this Action result
        public ActionResult create()
        {
            return View();
        }

        // Specify the type of attribute i.e.
        // it will add the record to the database
        [HttpPost]
        public ActionResult create(Project model)
        {

            // To open a connection to the database
            using (var context = new musicCRUDEntities())
            {
                // Add data to the particular table
                context.Project.Add(model);

                // save the changes
                context.SaveChanges();
            }
            string message = "Created the record successfully";

            // To display the message on the screen
            // after the record is created successfully
            ViewBag.Message = message;

            // write @Viewbag.Message in the created
            // view at the place where you want to
            // display the message
            return View();
        }

        [HttpGet] // Set the attribute to Read
        public ActionResult Read()
        {
            using (var context = new musicCRUDEntities())
            {
                var data = context.Project.ToList(); // Return the list of data from the database
                return View(data);
            }

        }


        // To fill data in the form 
        // to enable easy editing
        public ActionResult Update(int Projectid)
        {
            using (var context = new musicCRUDEntities())
            {
                var data = context.Project.Where(x => x.ProjectNo == Projectid).SingleOrDefault();
                return View(data);
            }
        }

        // To specify that this will be 
        // invoked when post method is called
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int Projectid, Project model)
        {
            using (var context = new musicCRUDEntities())
            {

                // Use of lambda expression to access
                // particular record from a database
                var data = context.Project.FirstOrDefault(x => x.ProjectNo == Projectid);

                // Checking if any such record exist 
                if (data != null)
                {
                    data.Name = model.Name;
                    data.Type = model.Type;
                    data.StartDate = model.StartDate;
                    data.Description = model.Description;
                    context.SaveChanges();

                    // It will redirect to 
                    // the Read method
                    return RedirectToAction("Read");
                }
                else
                    return View();
            }
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int Projectid)
        {
            using (var context = new musicCRUDEntities())
            {
                var data = context.Project.FirstOrDefault(x => x.ProjectNo == Projectid);
                if (data != null)
                {
                    context.Project.Remove(data);
                    context.SaveChanges();
                    return RedirectToAction("Read");
                }
                else
                    return View();
            }
        }
    }
}
