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
    public class SubCategoryController : Controller
    {
        #region Constructors

        public SubCategoryController()
        {
        }

        #endregion

        #region SubCategory Default CRUD Actions

        // GET: SubCategoryController
        public ActionResult Index()
        {
            var list = new SubCategoryListViewModel();

            // Get all sub category
            string response = WebAPIHelper.GetWebAPIClient("SubCategory").Result;
            if (!string.IsNullOrEmpty(response))
            {
                list.SubCategoryList = JsonConvert.DeserializeObject<List<SubCategoryViewModel>>(response);
                list.SubCategoryList.ForEach(x => { x.CategoryName = GetCategoryName(x.CategoryId); });
            }

            return View(list);
        }

        // GET: SubCategoryController/Details/5
        public ActionResult Details(int id)
        {
            var model = new SubCategoryViewModel();
            string response = WebAPIHelper.GetWebAPIClient($"SubCategory/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<SubCategoryViewModel>(response);
                model.CategoryName = GetCategoryName(model.CategoryId);
            }

            return View(model);
        }

        // GET: SubCategoryController/Create
        public ActionResult Create()
        {
            var model = new SubCategoryViewModel();
            GetAllCategory(model);
            return View(model);
        }

        // POST: SubCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubCategoryViewModel model)
        {
            ToastrViewModel tosterModel = null;
            try
            {
                //Server side validation
                if (!ModelState.IsValid)
                {
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Error,
                        Message = ToastrMessages.SubCategoryCreateError.GetEnumDescription<ToastrMessages>()
                    };

                    //TempData["ToastrModel"] = tosterModel;
                    // Serialize the model to JSON before storing it in TempData
                    TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

                    return View(model);
                }

                // For create
                if (model.ID == 0)
                {
                    model.IsDeleted = false;
                    model.CreatedBy = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
                    model.CreatedDate = DateTime.Now;
                }

                // Model to json string
                var modelData = JsonConvert.SerializeObject(model);

                string response = WebAPIHelper.PostWebAPIClient("SubCategory", modelData).Result;
                if (!string.IsNullOrEmpty(response))
                {
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Success,
                        Message = ToastrMessages.SubCategoryCreateSuccess.GetEnumDescription<ToastrMessages>()
                    };

                    //TempData["ToastrModel"] = tosterModel;
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
                Message = ToastrMessages.SubCategoryCreateError.GetEnumDescription<ToastrMessages>()
            };

            //TempData["ToastrModel"] = tosterModel;
            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

            return View(model);
        }

        // GET: SubCategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = new SubCategoryViewModel();
            string response = WebAPIHelper.GetWebAPIClient($"SubCategory/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<SubCategoryViewModel>(response);
                GetAllCategory(model);
            }

            return View(model);
        }

        // POST: SubCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SubCategoryViewModel model)
        {
            ToastrViewModel tosterModel = null;
            try
            {
                // Server side validation
                if (!ModelState.IsValid)
                {
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Error,
                        Message = ToastrMessages.SubCategoryEditError.GetEnumDescription<ToastrMessages>()
                    };

                    //TempData["ToastrModel"] = tosterModel;
                    // Serialize the model to JSON before storing it in TempData
                    TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

                    return View(model);
                }

                if (model.ID > 0)
                {
                    model.UpdatedBy = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
                    model.UpdatedDate = DateTime.Now;
                }

                // Serialize model to json string
                var modelData = JsonConvert.SerializeObject(model);

                // Web client api call
                string response = WebAPIHelper.PutWebAPIClient($"SubCategory/{model.ID}", modelData).Result;
                if (!string.IsNullOrEmpty(response))
                {
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Success,
                        Message = ToastrMessages.SubCategoryEditSuccess.GetEnumDescription<ToastrMessages>()
                    };

                    //TempData["ToastrModel"] = tosterModel;
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
                Message = ToastrMessages.SubCategoryEditError.GetEnumDescription<ToastrMessages>()
            };

            //TempData["ToastrModel"] = tosterModel;
            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

            return View(model);
        }

        // GET: SubCategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            // pass session variable to view
            ViewBag.UserID = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));

            var model = new SubCategoryViewModel();
            string response = WebAPIHelper.GetWebAPIClient($"SubCategory/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<SubCategoryViewModel>(response);
                model.CategoryName = GetCategoryName(model.CategoryId);
            }

            return View(model);
        }

        // POST: SubCategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int loginUserId)
        {
            ToastrViewModel tosterModel = null;
            try
            {
                loginUserId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
                string response = WebAPIHelper.DeleteWebAPIClient($"SubCategory/{id}?loginUserId={loginUserId}").Result;
                if (!string.IsNullOrEmpty(response))
                {
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Success,
                        Message = ToastrMessages.SubCategoryDeleteSuccess.GetEnumDescription<ToastrMessages>()
                    };

                    //TempData["ToastrModel"] = tosterModel;
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
                Message = ToastrMessages.SubCategoryDeleteError.GetEnumDescription<ToastrMessages>()
            };

            //TempData["ToastrModel"] = tosterModel;
            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

            return View();
        }

        #endregion

        #region Private Methods

        private void GetAllCategory(SubCategoryViewModel model)
        {
            string response = WebAPIHelper.GetWebAPIClient("Category").Result;
            if (!string.IsNullOrEmpty(response))
            {
                var CategoryList = JsonConvert.DeserializeObject<List<CategoryViewModel>>(response);

                // Create a SelectList
                model.CategoryList = new SelectList(CategoryList, "ID", "CategoryName");
            }
        }

        private string GetCategoryName(int id)
        {
            string response = WebAPIHelper.GetWebAPIClient("Category").Result;
            if (!string.IsNullOrEmpty(response))
            {
                var categoryList = JsonConvert.DeserializeObject<List<CategoryViewModel>>(response);

                // Find the category with the matching ID
                var category = categoryList.FirstOrDefault(x => x.ID == id);

                // Return the category name if found, otherwise return null
                return category?.CategoryName;
            }
            return null;
        }

        #endregion

        #region Remote Validation Actions

        public IActionResult VerifySubCategoryName(int ID, int CategoryId, string SubCategoryName)
        {
            // Web client api call
            string isExistResponse = WebAPIHelper.GetWebAPIClient($"SubCategory/VerifySubCategoryName?id={ID}&categoryId={CategoryId}&subCategoryName={SubCategoryName}").Result;

            if (Convert.ToBoolean(isExistResponse))
            {
                return Json($"Sub category name {SubCategoryName} is already exist.");
            }

            return Json(true);
        }

        #endregion
    }
}
