using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public ActionResult Index()
        {
            var projects = _context.Project.ToList();
            var viewModel = new ProjectListViewModel();
            viewModel.ProjectList = projects;

            return View(viewModel);
        }

        public ActionResult Create()
        {
            var viewModel = new ProjectViewModel();
            return View(viewModel);
        }

        public ActionResult Edit(Guid id)
        {
            var viewModel = new ProjectViewModel();
            var model = _context.Project.Find(id);
            viewModel.Id = id;
            viewModel.Name = model.Name;

            return View("Create", viewModel);
        }

        [HttpPost]
        public ActionResult Save(ProjectViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Id == Guid.Empty)
                {
                    var model = new Project();
                    model.Id = Guid.NewGuid();
                    model.Name = viewModel.Name;
                    _context.Project.Add(model);
                    _context.SaveChanges();
                }
                else
                {
                    var model = _context.Project.Find(viewModel.Id);
                    model.Name = viewModel.Name;
                    _context.Project.AddOrUpdate(model);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            return View("Create", viewModel);
        }

        public ActionResult Delete(Guid id)
        {
            var model = _context.Project.Find(id);
            _context.Project.Remove(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}