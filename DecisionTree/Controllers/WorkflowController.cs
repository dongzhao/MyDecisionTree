using DecisionTree.Models;
using DecisionTree.MVC.Core.Entities;
using DecisionTree.MVC.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DecisionTree.Controllers
{
    public class WorkflowController : Controller
    {
        private readonly IUnitOfWork _uow; 

        public WorkflowController(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        public async Task<ActionResult> Index()
        {
            var items = await _uow.HierarchyItemRepository.GetManyAsync(null);
            var modelViews = items?.Select(c => new WorkflowViewModel{ Id = c.Id, ParentId = c.ParentId, Title = c.Title, ItemType = c.ItemType}).OrderByDescending(d => d.ParentId).OrderByDescending(d => d.Id);
            return View(modelViews);
        }

        // GET: WorkflowController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WorkflowController/Create
        public ActionResult Create(int? id = null)
        {
            var model = new WorkflowViewModel
            {
                ParentId = id,
            };

            return View(model);
        }

        // POST: WorkflowController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(WorkflowViewModel model)
        {
            try
            {
                var item = new HierarchyItem
                {
                    ParentId = model.ParentId,
                    Title = model.Title,
                    ItemType = model.ItemType,
                };

                await _uow.HierarchyItemRepository.AddAsync(item);
                await _uow.CommitChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WorkflowController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var item = await _uow.HierarchyItemRepository.GetAsync(id);
            var model = new WorkflowViewModel
            {
                Id = id,
                ParentId = item.ParentId,
                Title = item.Title,
                ItemType = item.ItemType,
            };
            return View(model);
        }

        // POST: WorkflowController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(WorkflowViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var id = model.Id;
                    var item = await _uow.HierarchyItemRepository.GetAsync(id);
                    var modelChanged = false;
                    if(model.Title != item.Title)
                    {
                        item.Title = model.Title;
                        modelChanged = true;
                    }
                    if (model.ItemType != item.ItemType)
                    {
                        item.ItemType = model.ItemType;
                        modelChanged = true;
                    }

                    if (modelChanged)
                    {
                        await _uow.HierarchyItemRepository.UpdateAsync(item);
                        await _uow.CommitChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // POST: WorkflowController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _uow.HierarchyItemRepository.DeleteAsync(id);
                await _uow.CommitChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
