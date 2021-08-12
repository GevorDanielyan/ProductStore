using System.Linq;
using System.Web.Mvc;
using Store.Domain.Abstract;
using Store.Domain.Entities;
using Store.WebUI.Models;

namespace Store.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int pageSize = 4;

        // GET: Product
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int page = 1)
        {
            ProductListViewModel model = new ProductListViewModel
            {
                Products = repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(product => product.ProductId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                        repository.Products.Count() :
                        repository.Products.Where(product => product.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }

        public FileContentResult GetImage(int productId)
        {
            Product product = repository.Products.FirstOrDefault(g => g.ProductId == productId);

            if (product != null)
            {
                return File(product.ImageData, product.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}