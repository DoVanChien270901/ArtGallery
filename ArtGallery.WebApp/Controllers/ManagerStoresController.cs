using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.Catalog.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArtGallery.WebApp.Controllers
{
    public class ManagerStoresController : Controller
    {
        private readonly string url = "http://localhost:5000/api/";
        private HttpClient httpClient = new HttpClient();
        [Authorize]
        [HttpGet]
        public IActionResult GetProduct()
        {
            string UserId = "";
            foreach (var item in User.Claims.ToList().Where(c => c.Type.Equals("UserId")))
            {
                UserId = item.Value.ToString();
            }
            IEnumerable<Product> listProducts = JsonConvert.DeserializeObject<IEnumerable<Product>>(httpClient.GetStringAsync(url + "Products/AllProduct").Result);
            ViewBag.products = listProducts.Where(c => c.AccountId == UserId).ToList();
            return View();
        }
        [HttpGet]
        public IActionResult DeleteProduct(int id)
        {
            var result = httpClient.DeleteAsync(url + "Products/DeleteProduct/" + id).Result;
            return RedirectToAction("GetProduct", "ManagerStores");
        }
        [HttpGet]
        public IActionResult CreateProduct()
        {
            List<SelectListCate> Cate = JsonConvert.DeserializeObject<List<SelectListCate>>(httpClient.GetStringAsync(url + "CategoriesManager").Result);

            return View(new InsertProductRequest { Selecteds = Cate });
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] InsertProductRequest request)
        {
            //Get use id
            string UserId = "";
            foreach (var item in User.Claims.ToList().Where(c => c.Type.Equals("UserId")))
            {
                UserId = item.Value.ToString();
            }
            request.AccountId = UserId;
            //get list category id
            request.ListCategoryId = new List<int> { };
            foreach (var item in request.Selecteds.Where(c => c.Selescted == true))
            {
                request.ListCategoryId.Add(item.Id);
            }
            //requestcontent
            var requestcontent = new MultipartFormDataContent();
            byte[] data;
            using (var br = new BinaryReader(request.Thumbnail.OpenReadStream()))
            {
                data = br.ReadBytes((int)request.Thumbnail.OpenReadStream().Length);
            }
            ByteArrayContent bytes = new ByteArrayContent(data);
            requestcontent.Add(bytes, "Thumbnail", request.Thumbnail.FileName);
            //Images to byte[]
            foreach (var item in request.Images)
            {
                byte[] datai;
                using (var br = new BinaryReader(item.OpenReadStream()))
                {
                    datai = br.ReadBytes((int)item.OpenReadStream().Length);
                }
                ByteArrayContent bytesi = new ByteArrayContent(datai);
                requestcontent.Add(bytesi, "Images", item.FileName);
            }
            //list category to byte[]
            foreach (int item in request.ListCategoryId)
            {
                requestcontent.Add(new StringContent(item.ToString()), "ListCategoryId");
            }
            requestcontent.Add(new StringContent(request.Title.ToString()), "Title");
            requestcontent.Add(new StringContent(request.Price.ToString()), "Price");
            requestcontent.Add(new StringContent(request.Description.ToString()), "Description");
            requestcontent.Add(new StringContent(request.AccountId.ToString()), "AccountId");

            var result = httpClient.PostAsync(url + "Products/InsertProduct", requestcontent).Result;
            if (!result.IsSuccessStatusCode) return BadRequest();
            return RedirectToAction("GetProduct", "ManagerStores");
        }
        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            Product singlepro = JsonConvert.DeserializeObject<Product>(httpClient.GetStringAsync(url + "Products/GetProduct/" + id).Result);
            List<SelectListCate> listCate = JsonConvert.DeserializeObject<List<SelectListCate>>(httpClient.GetStringAsync(url + "CategoriesManager").Result);
            foreach (var proincate in singlepro.ProductInCategories)
            {
                foreach (var cate in listCate)
                {
                    if (proincate.CategoryId == cate.Id)
                    {
                        cate.Selescted = true;
                    }
                }
            }
            GetProductEdit model = new GetProductEdit
            {
                Id = singlepro.Id,
                Title = singlepro.Title,
                Description = singlepro.Description,
                Price = singlepro.Price,
                Images = singlepro.ProductImages,
                SelectListCates = listCate
            };
            ViewBag.editpro = model;
            return View();
        }
        [HttpPost]
        public IActionResult UpdateProduct(EditProductRequest request)
        {
            //
            Product singlepro = JsonConvert.DeserializeObject<Product>(httpClient.GetStringAsync(url + "Products/GetProduct/" + request.Id).Result);
            List<SelectListCate> listCate = JsonConvert.DeserializeObject<List<SelectListCate>>(httpClient.GetStringAsync(url + "CategoriesManager").Result);
            foreach (var proincate in singlepro.ProductInCategories)
            {
                foreach (var cate in listCate)
                {
                    if (proincate.CategoryId == cate.Id)
                    {
                        cate.Selescted = true;
                    }
                }
            }
            GetProductEdit model = new GetProductEdit
            {
                Id = singlepro.Id,
                Title = singlepro.Title,
                Description = singlepro.Description,
                Price = singlepro.Price,
                Images = singlepro.ProductImages,
                SelectListCates = listCate
            };
            ViewBag.editpro = model;
            //
            //

            request.ListCategoryId = new List<int> { };
            foreach (var item in request.Selecteds.Where(c => c.Selescted == true))
            {
                request.ListCategoryId.Add(item.Id);
            }
            //requestcontent
            var requestcontent = new MultipartFormDataContent();
            byte[] data;
            using (var br = new BinaryReader(request.Thumbnail.OpenReadStream()))
            {
                data = br.ReadBytes((int)request.Thumbnail.OpenReadStream().Length);
            }
            ByteArrayContent bytes = new ByteArrayContent(data);
            requestcontent.Add(bytes, "Thumbnail", request.Thumbnail.FileName);
            //Images to byte[]
            foreach (var item in request.Images)
            {
                byte[] datai;
                using (var br = new BinaryReader(item.OpenReadStream()))
                {
                    datai = br.ReadBytes((int)item.OpenReadStream().Length);
                }
                ByteArrayContent bytesi = new ByteArrayContent(datai);
                requestcontent.Add(bytesi, "Images", item.FileName);
            }
            //list category to byte[]
            foreach (int item in request.ListCategoryId)
            {
                requestcontent.Add(new StringContent(item.ToString()), "ListCategoryId");
            }
            requestcontent.Add(new StringContent(request.Id.ToString()), "Id");
            requestcontent.Add(new StringContent(request.Title.ToString()), "Title");
            requestcontent.Add(new StringContent(request.Price.ToString()), "Price");
            requestcontent.Add(new StringContent(request.Description.ToString()), "Description");

            var result = httpClient.PutAsync(url + "Products/UpdateProduct", requestcontent).Result;
            if (!result.IsSuccessStatusCode) return BadRequest();

            return RedirectToAction("GetProduct", "ManagerStores");
        }
        //[HttpGet]
        //public IActionResult GetAuction()
        //{
        //    string UserId = "";
        //    foreach (var item in User.Claims.ToList().Where(c => c.Type.Equals("UserId")))
        //    {
        //        UserId = item.Value.ToString();
        //    }
        //    IEnumerable<Product> listProducts = JsonConvert.DeserializeObject<IEnumerable<Product>>(httpClient.GetStringAsync(url + "Products/AllProduct").Result);
        //    ViewBag.products = listProducts.Where(c => c.AccountId == UserId).ToList();
        //    return View();
        //}
    }
}
