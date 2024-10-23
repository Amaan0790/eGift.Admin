using eGift.Admin.MVC.Common;
using eGift.Admin.MVC.Helpers;
using eGift.Admin.MVC.Models;
using eGift.Admin.MVC.Models.ListViewModels;
using eGift.Admin.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eGift.Admin.MVC.Controllers
{
    public class CountryController : Controller
    {
        #region Constructors

        public CountryController()
        {
        }

        #endregion

        #region Country Default CRUD Actions

        // GET: CountryController
        public ActionResult Index()
        {
            var list = new CountryListViewModel();

            // Web client api call
            string response = WebAPIHelper.GetWebAPIClient("Country").Result;
            if (!string.IsNullOrEmpty(response))
            {
                list.CountryList = JsonConvert.DeserializeObject<List<CountryViewModel>>(response);
            }
            return View(list);
        }

        // GET: CountryController/Details/5
        public ActionResult Details(int id)
        {
            var model = new CountryViewModel();

            // Web client api call
            string response = WebAPIHelper.GetWebAPIClient($"Country/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<CountryViewModel>(response);
            }

            return View(model);
        }

        // GET: CountryController/Create
        public ActionResult Create()
        {
            var model = new CountryViewModel();
            return View(model);
        }

        // POST: CountryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CountryViewModel model)
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
                        Message = ToastrMessages.CountryCreateError.GetEnumDescription<ToastrMessages>()
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

                // Web client api call
                string response = WebAPIHelper.PostWebAPIClient("Country", modelData).Result;
                if (!string.IsNullOrEmpty(response))
                {
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Success,
                        Message = ToastrMessages.CountryCreateSuccess.GetEnumDescription<ToastrMessages>()
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
                Message = ToastrMessages.CountryCreateError.GetEnumDescription<ToastrMessages>()
            };

            //TempData["ToastrModel"] = tosterModel;
            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

            return View();
        }

        // GET: CountryController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = new CountryViewModel();

            // Web client api call
            string response = WebAPIHelper.GetWebAPIClient($"Country/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<CountryViewModel>(response);
            }

            return View(model);
        }

        // POST: CountryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CountryViewModel model)
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
                        Message = ToastrMessages.CountryEditError.GetEnumDescription<ToastrMessages>()
                    };

                    //TempData["ToastrModel"] = tosterModel;
                    // Serialize the model to JSON before storing it in TempData
                    TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

                    return View(model);
                }

                // For edit
                if (model.ID > 0)
                {
                    model.UpdatedBy = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
                    model.UpdatedDate = DateTime.Now;
                }

                // Serialize model to json string
                var modelData = JsonConvert.SerializeObject(model);

                // Web client api call
                string response = WebAPIHelper.PutWebAPIClient($"Country/{model.ID}", modelData).Result;
                if (!string.IsNullOrEmpty(response))
                {
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Success,
                        Message = ToastrMessages.CountryEditSuccess.GetEnumDescription<ToastrMessages>()
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
                Message = ToastrMessages.CountryEditError.GetEnumDescription<ToastrMessages>()
            };

            //TempData["ToastrModel"] = tosterModel;
            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

            return View();
        }

        // GET: CountryController/Delete/5
        public ActionResult Delete(int id)
        {
            // pass session variable to view
            ViewBag.UserID = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));

            var model = new CountryViewModel();
            string response = WebAPIHelper.GetWebAPIClient($"Country/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<CountryViewModel>(response);
            }

            return View(model);
        }

        // POST: CountryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int loginUserId)
        {
            ToastrViewModel tosterModel = null;
            try
            {
                string response = WebAPIHelper.DeleteWebAPIClient($"Country/{id}?loginUserId={loginUserId}").Result;
                if (!string.IsNullOrEmpty(response))
                {
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Success,
                        Message = ToastrMessages.CountryDeleteSuccess.GetEnumDescription<ToastrMessages>()
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
                Message = ToastrMessages.CountryDeleteError.GetEnumDescription<ToastrMessages>()
            };

            //TempData["ToastrModel"] = tosterModel;
            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

            return View();
        }

        #endregion

        #region Remote Validation Actions

        // For country name
        public IActionResult VerifyCountryName(int ID, string CountryName)
        {
            // Web client api call
            string isExistResponse = WebAPIHelper.GetWebAPIClient($"Country/VerifyCountryName?id={ID}&countryName={CountryName}").Result;

            if (Convert.ToBoolean(isExistResponse))
            {
                return Json($"Country name {CountryName} is already exist.");
            }

            return Json(true);
        }

        // For country code
        public IActionResult VerifyCountryCode(int ID, string CountryCode)
        {
            // Web client api call
            string isExistResponse = WebAPIHelper.GetWebAPIClient($"Country/VerifyCountryCode?id={ID}&countryCode={CountryCode}").Result;

            if (Convert.ToBoolean(isExistResponse))
            {
                return Json($"Country code {CountryCode} is already exist.");
            }

            return Json(true);
        }

        #endregion
    }
}
