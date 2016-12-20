using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
using MVC.Models.Interface;
using MVC.Models.Repository;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class StructureController : Controller
    {
        private IStructureRepository structureRepository;

        public StructureController()
        {
            this.structureRepository = new StructureRepository();
        }

        // GET: Structure
        public ActionResult Index()
        {
            var structures = structureRepository.GetAll().ToList();
            var viewModel = new StructureListViewModel();
            viewModel.StructureList = structures;

            return View(viewModel);
        }

        public ActionResult Create()
        {
            var viewModel = new StructureViewModel();

            return View(viewModel);
        }

        public ActionResult Edit(Guid id)
        {
            var viewModel = new StructureViewModel();
            var model = structureRepository.Get(id);
            viewModel.Id = id;
            viewModel.Name = model.Name;

            return View("Create", viewModel);
        }

        [HttpPost]
        public ActionResult Save(StructureViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Id == Guid.Empty)
                {
                    var model = new Structure();
                    model.Id = Guid.NewGuid();
                    model.Name = viewModel.Name;
                    structureRepository.Create(model);
                }
                else
                {
                    var model = structureRepository.Get(viewModel.Id);
                    model.Name = viewModel.Name;
                    structureRepository.Update(model);
                }
                return RedirectToAction("Index");
            }
            return View("Create", viewModel);
        }

        public ActionResult Delete(Guid id)
        {
            var model = structureRepository.Get(id);
            structureRepository.Delete(model);

            return RedirectToAction("Index");
        }
    }
}