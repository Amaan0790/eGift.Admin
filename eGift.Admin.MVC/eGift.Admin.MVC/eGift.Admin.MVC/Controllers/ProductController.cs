using eGift.Admin.MVC.Common;
using eGift.Admin.MVC.Helpers;
using eGift.Admin.MVC.Models;
using eGift.Admin.MVC.Models.ListViewModels;
using eGift.Admin.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace eGift.Admin.MVC.Controllers
{
    public class ProductController : Controller
    {
        #region Constructors

        public ProductController()
        {
        }

        #endregion

        #region Product Default CRUD Actions

        // GET: ProductController
        public ActionResult Index()
        {
            var list = new ProductListViewModel();

            // Web api call
            var response = WebAPIHelper.GetWebAPIClient("Product").Result;
            if (!string.IsNullOrEmpty(response))
            {
                list.ProductList = JsonConvert.DeserializeObject<List<ProductViewModel>>(response);
                foreach (var product in list.ProductList)
                {
                    GetCategoryName(product);
                    GetSubCategoryName(product);
                }
            }
            return View(list);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var model = new ProductViewModel();

            // Web api call
            var response = WebAPIHelper.GetWebAPIClient($"Product/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<ProductViewModel>(response);

                model.SizeName = model.SizeId.HasValue ?
                    ((Size)model.SizeId.Value).GetEnumDescription() : string.Empty; // Or any default value if SizeId is null

                GetCategoryName(model);
                GetSubCategoryName(model);
            }
            return View(model);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            var model = new ProductViewModel();
            GetAllCategory(model);
            return View(model);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel model)
        {
            ToastrViewModel tosterModel = null;
            try
            {
                //Server side validation
                if (ModelState.IsValid)
                {
                    // For create
                    if (model.ID == 0)
                    {
                        // For product profile image
                        // Check if the file is not null and has content
                        if (model.ImageUrl != null && model.ImageUrl.Length > 0)
                        {
                            // Get the file name
                            model.ProductImagePath = Path.GetFileName(model.ImageUrl.FileName);

                            using (var memoryStream = new MemoryStream())
                            {
                                model.ImageUrl.CopyTo(memoryStream);
                                model.ProductImageData = memoryStream.ToArray(); // Convert to byte array
                            }
                        }
                        model.ImageUrl = null;

                        // For picture 1
                        // Check if the file is not null and has content
                        if (model.Picture1 != null && model.Picture1.Length > 0)
                        {
                            // Get the file name
                            model.PicturePath1 = Path.GetFileName(model.Picture1.FileName);

                            using (var memoryStream = new MemoryStream())
                            {
                                model.Picture1.CopyTo(memoryStream);
                                model.PictureData1 = memoryStream.ToArray(); // Convert to byte array
                            }
                        }
                        model.Picture1 = null;

                        // For picture 2
                        // Check if the file is not null and has content
                        if (model.Picture2 != null && model.Picture2.Length > 0)
                        {
                            // Get the file name
                            model.PicturePath2 = Path.GetFileName(model.Picture2.FileName);

                            using (var memoryStream = new MemoryStream())
                            {
                                model.Picture2.CopyTo(memoryStream);
                                model.PictureData2 = memoryStream.ToArray(); // Convert to byte array
                            }
                        }
                        model.Picture2 = null;

                        // For picture 3
                        // Check if the file is not null and had content
                        if (model.Picture3 != null && model.Picture3.Length > 0)
                        {
                            // Get the file name
                            model.PicturePath3 = Path.GetFileName(model.Picture3.FileName);

                            using (var memoryStream = new MemoryStream())
                            {
                                model.Picture3.CopyTo(memoryStream);
                                model.PictureData3 = memoryStream.ToArray(); // Convert to byte array
                            }
                        }
                        model.Picture3 = null;

                        // For picture 4
                        // Check if the file is not null and had content
                        if (model.Picture4 != null && model.Picture4.Length > 0)
                        {
                            model.PicturePath4 = Path.GetFileName(model.Picture4.FileName);

                            using (var memoryStream = new MemoryStream())
                            {
                                model.Picture4.CopyTo(memoryStream);
                                model.PictureData4 = memoryStream.ToArray(); // Convert to byte array
                            }
                        }
                        model.Picture4 = null;

                        model.IsDeleted = false;
                        model.CreatedBy = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
                        model.CreatedDate = DateTime.Now;
                    }

                    // Return to view if product image is not submitted
                    if (model.ProductImagePath == null && model.ProductImageData == null)
                    {
                        ModelState.AddModelError("ImageUrl", "Please select product image.");
                    }
                    else
                    {
                        // Model to json string
                        var modelData = JsonConvert.SerializeObject(model);

                        // Web API call
                        string response = WebAPIHelper.PostWebAPIClient("Product", modelData).Result;
                        if (!string.IsNullOrEmpty(response))
                        {
                            tosterModel = new ToastrViewModel()
                            {
                                Type = (int)ToastrType.Success,
                                Message = ToastrMessages.ProductCreateSuccess.GetEnumDescription<ToastrMessages>()
                            };

                            //TempData["ToastrModel"] = tosterModel;
                            // Serialize the model to JSON before storing it in TempData
                            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
            }
            catch
            {
            }

            tosterModel = new ToastrViewModel()
            {
                Type = (int)ToastrType.Error,
                Message = ToastrMessages.ProductCreateError.GetEnumDescription<ToastrMessages>()
            };

            //TempData["ToastrModel"] = tosterModel;
            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);
            GetAllCategory(model);
            return View(model);
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = new ProductViewModel();

            // Web api call
            var response = WebAPIHelper.GetWebAPIClient($"Product/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<ProductViewModel>(response);
                GetAllCategory(model);
            }
            return View(model);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductViewModel model)
        {
            ToastrViewModel tosterModel = null;
            try
            {
                // Server side validation
                if (ModelState.IsValid)
                {
                    // For edit
                    if (model.ID > 0)
                    {
                        // For product profile image
                        // Check if the file is not null and has content
                        if (model.ImageUrl != null && model.ImageUrl.Length > 0)
                        {
                            // Get the file name
                            model.ProductImagePath = Path.GetFileName(model.ImageUrl.FileName);

                            using (var memoryStream = new MemoryStream())
                            {
                                model.ImageUrl.CopyTo(memoryStream);
                                model.ProductImageData = memoryStream.ToArray(); // Convert to byte array
                            }
                        }
                        else if (model.IsClear)
                        {
                            model.ProductImagePath = null;
                            model.ProductImageData = null;
                        }
                        model.ImageUrl = null;

                        // For picture 1
                        // Check if the file is not null and has content
                        if (model.Picture1 != null && model.Picture1.Length > 0)
                        {
                            // Get the file name
                            model.PicturePath1 = Path.GetFileName(model.Picture1.FileName);

                            using (var memoryStream = new MemoryStream())
                            {
                                model.Picture1.CopyTo(memoryStream);
                                model.PictureData1 = memoryStream.ToArray(); // Convert to byte array
                            }
                        }
                        model.Picture1 = null;

                        // For picture 2
                        // Check if the file is not null and has content
                        if (model.Picture2 != null && model.Picture2.Length > 0)
                        {
                            // Get the file name
                            model.PicturePath2 = Path.GetFileName(model.Picture2.FileName);

                            using (var memoryStream = new MemoryStream())
                            {
                                model.Picture2.CopyTo(memoryStream);
                                model.PictureData2 = memoryStream.ToArray(); // Convert to byte array
                            }
                        }
                        model.Picture2 = null;

                        // For picture 3
                        // Check if the file is not null and had content
                        if (model.Picture3 != null && model.Picture3.Length > 0)
                        {
                            // Get the file name
                            model.PicturePath3 = Path.GetFileName(model.Picture3.FileName);

                            using (var memoryStream = new MemoryStream())
                            {
                                model.Picture3.CopyTo(memoryStream);
                                model.PictureData3 = memoryStream.ToArray(); // Convert to byte array
                            }
                        }
                        model.Picture3 = null;

                        // For picture 4
                        // Check if the file is not null and had content
                        if (model.Picture4 != null && model.Picture4.Length > 0)
                        {
                            model.PicturePath4 = Path.GetFileName(model.Picture4.FileName);

                            using (var memoryStream = new MemoryStream())
                            {
                                model.Picture4.CopyTo(memoryStream);
                                model.PictureData4 = memoryStream.ToArray(); // Convert to byte array
                            }
                        }
                        model.Picture4 = null;

                        model.IsDeleted = false;
                        model.UpdatedBy = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
                        model.UpdatedDate = DateTime.Now;
                    }

                    // Return to view if product image is not submitted
                    if (model.ProductImagePath == null && model.ProductImageData == null)
                    {
                        ModelState.AddModelError("ImageUrl", "Please select product image.");
                    }
                    else
                    {
                        // Serialize model to json string
                        var modelData = JsonConvert.SerializeObject(model);

                        // Web API call
                        string response = WebAPIHelper.PutWebAPIClient($"Product/{model.ID}", modelData).Result;
                        if (!string.IsNullOrEmpty(response))
                        {
                            tosterModel = new ToastrViewModel()
                            {
                                Type = (int)ToastrType.Success,
                                Message = ToastrMessages.ProductEditSuccess.GetEnumDescription<ToastrMessages>()
                            };

                            // Serialize the model to JSON before storing it in TempData
                            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
            }
            catch
            {
            }

            tosterModel = new ToastrViewModel()
            {
                Type = (int)ToastrType.Error,
                Message = ToastrMessages.ProductEditError.GetEnumDescription<ToastrMessages>()
            };

            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);
            GetAllCategory(model);
            return View(model);
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            // pass session variable to view
            ViewBag.UserID = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));

            var model = new ProductViewModel();

            // Web api call
            var response = WebAPIHelper.GetWebAPIClient($"Product/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<ProductViewModel>(response);

                model.SizeName = model.SizeId.HasValue ?
                    ((Size)model.SizeId.Value).GetEnumDescription() : string.Empty; // Or any default value if SizeId is null

                GetCategoryName(model);
                GetSubCategoryName(model);
            }
            return View(model);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int loginUserId)
        {
            ToastrViewModel tosterModel = null;
            try
            {
                string response = WebAPIHelper.DeleteWebAPIClient($"Product/{id}?loginUserId={loginUserId}").Result;
                if (!string.IsNullOrEmpty(response))
                {
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Success,
                        Message = ToastrMessages.ProductDeleteSuccess.GetEnumDescription<ToastrMessages>()
                    };

                    // Serialize the model to JSON before storing it in TempData
                    TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
            }
            tosterModel = new ToastrViewModel()
            {
                Type = (int)ToastrType.Error,
                Message = ToastrMessages.ProductDeleteError.GetEnumDescription<ToastrMessages>()
            };

            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);
            return View();
        }

        #endregion

        #region Ajax Actions

        // GET: ProductController/GetSubCategoriesByCategory/5
        public ActionResult GetSubCategoriesByCategory(int categoryId)
        {
            List<SubCategoryViewModel> subCategoryList = null;

            // Web API call
            string response = WebAPIHelper.GetWebAPIClient($"SubCategory/GetSubCategoriesByCategory/{categoryId}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                subCategoryList = JsonConvert.DeserializeObject<List<SubCategoryViewModel>>(response);
                return Json(subCategoryList);
            }

            return Json(subCategoryList);
        }

        #endregion

        #region Private Methods

        // Get All Category
        private void GetAllCategory(ProductViewModel model)
        {
            // Web api call
            var response = WebAPIHelper.GetWebAPIClient("Category").Result;
            if (response != null)
            {
                var categoryList = JsonConvert.DeserializeObject<List<CategoryViewModel>>(response);

                // Create a SelectList
                model.CategoryList = new SelectList(categoryList, "ID", "CategoryName");
            }
        }

        // Get Category Name
        private void GetCategoryName(ProductViewModel model)
        {
            // Web api call
            var response = WebAPIHelper.GetWebAPIClient($"Category/{model.CategoryId}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                var category = JsonConvert.DeserializeObject<CategoryViewModel>(response);

                if (category != null)
                {
                    model.CategoryName = category.CategoryName;
                }
                else
                {
                    model.CategoryName = null;
                }
            }
        }

        // Get Sub Category Name
        private void GetSubCategoryName(ProductViewModel model)
        {
            // Web api call
            var response = WebAPIHelper.GetWebAPIClient($"SubCategory/{model.SubCategoryId}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                var subCategory = JsonConvert.DeserializeObject<SubCategoryViewModel>(response);

                if (subCategory != null)
                {
                    model.SubCategoryName = subCategory.SubCategoryName;
                }
                else
                {
                    model.SubCategoryName = null;
                }
            }
        }

        #endregion

        #region Remote Validation Actions

        public ActionResult VerifyProductName(int ID, int CategoryId, int SubCategoryId, string Name)
        {
            // Web client api call
            string isExistResponse = WebAPIHelper.GetWebAPIClient($"Product/VerifyProductName?id={ID}&categoryId={CategoryId}&subCategoryId={SubCategoryId}&Name={Name}").Result;

            if (Convert.ToBoolean(isExistResponse))
            {
                return Json($"Product name {Name} is already exist.");
            }

            return Json(true);
        }

        #endregion
    }
}
