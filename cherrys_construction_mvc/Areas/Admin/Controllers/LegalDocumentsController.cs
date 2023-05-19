using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cherrys_construction_mvc.Areas.Admin.Controllers
{
    [Area(Breadcrumb.AdminArea)]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class LegalDocumentsController : Controller
    {
        private readonly ILogger<LegalDocumentsController> _logger;
        private readonly ILegalDocumentService _legalDocService;
        public LegalDocumentsController(ILegalDocumentService legalDocService, 
            ILogger<LegalDocumentsController> logger)
        {
            _legalDocService = legalDocService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var docs = await _legalDocService.GetLegalDocumentsAsync();

            return View(docs);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LegalDocumentRequest doc)
        {
            if (ModelState.IsValid)
            {
                await _legalDocService.CreateLegalDocumentAsync(doc);
                TempData["success"] = "Created Document";
            }
            else
            {
                TempData["error"] = "Error Creating Document";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != 0)
            {
                var item = await _legalDocService.GetLegalDocumentByIdAsync(id);
                if (item != null)
                {
                    LegalDocumentRequest doc = new();
                    if (!string.IsNullOrEmpty(item.Title))
                    {
                        doc.Title = item.Title.Trim();
                    }
                    else { }
                    if (!string.IsNullOrWhiteSpace(item.Body))
                    {
                        doc.Body = item.Body.Trim();
                    }
                    else { }
                    return View(doc);
                }
                else
                {
                    _logger.LogWarning("Could not find existing legal document to display in edit form, in - LegalDocumentController");
                    TempData["error"] = "Document Not Found";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                _logger.LogWarning("Could not receive Id to display edit form, in - LegalDocument Controller");
                TempData["error"] = "Failed To Load Document";
                TempData["error"] = "Contact Website Developer Team";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LegalDocumentRequest doc, int id)
        {
            if (ModelState.IsValid)
            {
                var docExists = await _legalDocService.GetLegalDocumentByIdAsync(id);
                if (docExists != null)
                {
                    await _legalDocService.UpdateLegalDocumentAsync(id, doc);
                    TempData["success"] = "Document Updated";
                }
                else
                {
                    TempData["error"] = "Document Was Not Found";
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Document Failed To Update";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id != 0)
            {
                var item = await _legalDocService.GetLegalDocumentByIdAsync(id);
                if (item != null)
                {
                    return View(item);
                }
                else
                {
                    TempData["error"] = "Document Not Found";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["error"] = "Failed To Load Document";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDoc(int id)
        {
            if (id != 0)
            {
                var item = await _legalDocService.GetLegalDocumentByIdAsync(id);
                if (item != null)
                {
                    await _legalDocService.DeleteLegalDocumentAsync(id);
                    TempData["success"] = "Deleted Document";
                }
                else
                {
                    TempData["error"] = "Failed To Find Document To Delete";
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Failed To Find Document To Delete";
                return RedirectToAction(nameof(Index));
            }
        }


    }
}
