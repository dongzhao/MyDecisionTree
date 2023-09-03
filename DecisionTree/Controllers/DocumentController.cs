using DecisionTree.Mapper;
using DecisionTree.Models.Documents;
using DecisionTree.MVC.Application.Document;
using DecisionTree.MVC.Core.Entities;
using DecisionTree.MVC.Core.Enums;
using DecisionTree.MVC.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DecisionTree.Controllers
{
    public class DocumentController : Controller
    {
        private readonly IDocumentService _document;

        public DocumentController(IDocumentService doc)
        {
            this._document = doc;
        }

        public async Task<ActionResult> Index()
        {
            var items = await _document.GetAllDocuments();
            var modelViews = items?.Select(c => new DocumentViewModel { Id = c.Id, ParentId = c.ParentId, Title = c.Title, ItemType = c.ItemType }).OrderByDescending(d => d.ParentId).OrderByDescending(d => d.Id);
            return View(modelViews);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WorkflowController/View/5
        public async Task<ActionResult> DocmentListView(int id)
        {
            var item = await _document.GetDocument(id);
            var model = TreeViewModelMapper.Map(item);
            return View(model);
        }

        public ActionResult Create(int? id = null)
        {
            var model = new DocumentViewModel
            {
                ParentId = id,
            };

            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DocumentViewModel model)
        {
            try
            {
                var item = new HierarchyItem
                {
                    ParentId = model.ParentId,
                    Title = model.Title,
                    ItemType = model.ItemType,
                };

                await _document.SaveOrUpdate(item);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var item = await _document.HierarchyItemRepository.GetAsync(id);
            var model = new DocumentViewModel
            {
                Id = id,
                ParentId = item.ParentId,
                Title = item.Title,
                ItemType = item.ItemType,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DocumentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var id = model.Id;
                    var item = await _document.HierarchyItemRepository.GetAsync(id);
                    var modelChanged = false;
                    if (model.Title != item.Title)
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
                        await _document.HierarchyItemRepository.UpdateAsync(item);
                        await _document.CommitChangesAsync();
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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _document.HierarchyItemRepository.DeleteAsync(id);
                await _document.CommitChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
