using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Evaluacion_Tecnica_Web.Datos;
using Evaluacion_Tecnica_Web.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion_Tecnica_Web.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ProductoContext _context;

        public ProductoController(ProductoContext context)
        {
            _context = context;
        }   

        public async Task<IActionResult> Index(string category)
        {
            var productos = from p in _context.Productos
                            select p;

            if (!string.IsNullOrEmpty(category))
            {
                productos = productos.Where(p => p.categoria == category);
            }

            ViewBag.Categories = await _context.Productos
                .Select(p => p.categoria)
                .Distinct()
                .ToListAsync();

            ViewBag.AvaragePrice = await _context.Productos.AnyAsync() ?
                    await _context.Productos.AverageAsync(p => p.Precio) : 0;

            return View(await productos.ToListAsync());
        }
   

        // GET: ProductoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(producto);
            }
        }

        // GET: ProductoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductoController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FirstOrDefaultAsync(m => m.id == id);

            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
