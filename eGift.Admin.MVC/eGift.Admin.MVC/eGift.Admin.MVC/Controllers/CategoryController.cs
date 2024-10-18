using eGift.Admin.MVC.Common;
using eGift.Admin.MVC.Helpers;
using eGift.Admin.MVC.Models;
using eGift.Admin.MVC.Models.ListViewModels;
using eGift.Admin.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eGift.Admin.MVC.Controllers
{
    public class CategoryController : Controller
    {
        #region Constructors

        public CategoryController()
        {
        }

        #endregion

        #region Category Default CRUD Actions

        // GET: CategoryController
        public ActionResult Index()
        {
            var list = new CategoryListViewModel();
            string response = WebAPIHelper.GetWebAPIClient("Category").Result;
            if (!string.IsNullOrEmpty(response))
            {
                list.CategoryList = JsonConvert.DeserializeObject<List<CategoryViewModel>>(response);
            }
            return View(list);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            var model = new CategoryViewModel();
            string response = WebAPIHelper.GetWebAPIClient($"Category/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<CategoryViewModel>(response);
            }

            return View(model);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            var model = new CategoryViewModel();
            return View(model);
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel model)
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
                        Message = ToastrMessages.CategoryCreateError.GetEnumDescription<ToastrMessages>()
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

                string response = WebAPIHelper.PostWebAPIClient("Category", modelData).Result;
                if (!string.IsNullOrEmpty(response))
                {
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Success,
                        Message = ToastrMessages.CategoryCreateSuccess.GetEnumDescription<ToastrMessages>()
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
                Message = ToastrMessages.CategoryCreateError.GetEnumDescription<ToastrMessages>()
            };

            //TempData["ToastrModel"] = tosterModel;
            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);
            return View(model);
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = new CategoryViewModel();
            string response = WebAPIHelper.GetWebAPIClient($"Category/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<CategoryViewModel>(response);
            }

            return View(model);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoryViewModel model)
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
                        Message = ToastrMessages.CategoryEditError.GetEnumDescription<ToastrMessages>()
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
                string response = WebAPIHelper.PutWebAPIClient($"Category/{model.ID}", modelData).Result;
                if (!string.IsNullOrEmpty(response))
                {
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Success,
                        Message = ToastrMessages.CategoryEditSuccess.GetEnumDescription<ToastrMessages>()
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
                Message = ToastrMessages.CategoryEditError.GetEnumDescription<ToastrMessages>()
            };

            //TempData["ToastrModel"] = tosterModel;
            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

            return View(model);
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            // pass session variable to view
            ViewBag.UserID = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));

            var model = new CategoryViewModel();
            string response = WebAPIHelper.GetWebAPIClient($"Category/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<CategoryViewModel>(response);
            }

            return View(model);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int loginUserId)
        {
            ToastrViewModel tosterModel = null;
            try
            {
                string response = WebAPIHelper.DeleteWebAPIClient($"Category/{id}?loginUserId={loginUserId}").Result;
                if (!string.IsNullOrEmpty(response))
                {
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Success,
                        Message = ToastrMessages.CategoryDeleteSuccess.GetEnumDescription<ToastrMessages>()
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
                Message = ToastrMessages.CategoryDeleteError.GetEnumDescription<ToastrMessages>()
            };

            //TempData["ToastrModel"] = tosterModel;
            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

            return View();
        }

        #endregion

        #region Remote Validation Actions

        public IActionResult VerifyCategoryName(int ID, string CategoryName)
        {
            // Web client api call
            string isExistResponse = WebAPIHelper.GetWebAPIClient($"Category/VerifyCategoryName?id={ID}&categoryName={CategoryName}").Result;

            if (Convert.ToBoolean(isExistResponse))
            {
                return Json($"Category name {CategoryName} is already in use.");
            }

            return Json(true);
        }

        #endregion
    }
}
