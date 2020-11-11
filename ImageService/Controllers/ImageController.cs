using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ImageService.Models;
using System;

namespace ImageService.Controllers
{
    //[ApiController]
    //[Route("[controller]")]
    public class ImageController : Controller
    {
        private readonly ImageContext _imageContext;
        public ImageController(ImageContext imageContext)
            {
            _imageContext = imageContext;
            }
        //public static Guid TryStrToGuid(String s, out Guid value)
        //{
        //    try
        //    {
        //        value = new Guid(s);
        //        return value;
        //    }
        //    catch (FormatException)
        //    {
        //        value = Guid.Empty;
        //        return value;
        //    }
        //}
        public async Task<IActionResult> Index()
        {
            return View(await _imageContext.Images.ToListAsync());
        }
       
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Image image)
        {
            _imageContext.Images.Add(image);
            await _imageContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(Guid id)
        {
            if (id != null)
            {
                Image image = await _imageContext.Images.FirstOrDefaultAsync(p => p.Id == id);
                if (image != null)
                    return View(image);
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id != null)
            {
                Image image = await _imageContext.Images.FirstOrDefaultAsync(p => p.Id == id);
                if (image != null)
                    return View(image);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Image image)
        {
            _imageContext.Images.Update(image);
            await _imageContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(Guid id)
        {
            if (id != null)
            {
                Image image = await _imageContext.Images.FirstOrDefaultAsync(p => p.Id == id);
                if (image != null)
                    return View(image);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id != null)
            {
                Image image = await _imageContext.Images.FirstOrDefaultAsync(p => p.Id == id);
                if (image != null)
                {
                    _imageContext.Images.Remove(image);
                    await _imageContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}
