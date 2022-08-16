using AbbyWeb.Data;
using AbbyWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category category { get; set; }
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            category = _db.Categories.Find(id);

        }
        public async Task<IActionResult> OnPost()
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError(String.Empty,"The DisplayOrder cannot exactly match the name.");
            }
            if (ModelState.IsValid)
            {
                 _db.Categories.Update(category);
                await _db.SaveChangesAsync();
                TempData["success"] = "Category updated successfully";

                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
