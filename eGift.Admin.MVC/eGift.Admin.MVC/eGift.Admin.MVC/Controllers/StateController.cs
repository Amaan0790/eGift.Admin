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
    public class StateController : Controller
    {
        #region Constructors

        public StateController()
        {
        }

        #endregion

        #region State Default CRUD Actions

        // GET: StateController
        public ActionResult Index()
        {
            var list = new StateListViewModel();

            // Get all sub category
            string response = WebAPIHelper.GetWebAPIClient("State").Result;
            if (!string.IsNullOrEmpty(response))
            {
                list.StateList = JsonConvert.DeserializeObject<List<StateViewModel>>(response);
                list.StateList.ForEach(x => { x.CountryName = GetCountryName(x.CountryId); });
            }

            return View(list);
        }

        // GET: StateController/Details/5
        public ActionResult Details(int id)
        {
            var model = new StateViewModel();
            string response = WebAPIHelper.GetWebAPIClient($"State/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<StateViewModel>(response);
                model.CountryName = GetCountryName(model.CountryId);
            }

            return View(model);
        }

        // GET: StateController/Create
        public ActionResult Create()
        {
            var model = new StateViewModel();
            GetAllCountry(model);
            return View(model);
        }

        // POST: StateController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StateViewModel model)
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
                        Message = ToastrMessages.StateCreateError.GetEnumDescription<ToastrMessages>()
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

                string response = WebAPIHelper.PostWebAPIClient("State", modelData).Result;
                if (!string.IsNullOrEmpty(response))
                {
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Success,
                        Message = ToastrMessages.StateCreateSuccess.GetEnumDescription<ToastrMessages>()
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
                Message = ToastrMessages.StateCreateError.GetEnumDescription<ToastrMessages>()
            };

            //TempData["ToastrModel"] = tosterModel;
            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

            return View();
        }

        // GET: StateController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = new StateViewModel();
            string response = WebAPIHelper.GetWebAPIClient($"State/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<StateViewModel>(response);
                GetAllCountry(model);
            }

            return View(model);
        }

        // POST: StateController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, StateViewModel model)
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
                        Message = ToastrMessages.StateEditError.GetEnumDescription<ToastrMessages>()
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
                string response = WebAPIHelper.PutWebAPIClient($"State/{model.ID}", modelData).Result;
                if (!string.IsNullOrEmpty(response))
                {
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Success,
                        Message = ToastrMessages.StateEditSuccess.GetEnumDescription<ToastrMessages>()
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
                Message = ToastrMessages.StateEditError.GetEnumDescription<ToastrMessages>()
            };

            //TempData["ToastrModel"] = tosterModel;
            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

            return View();
        }

        // GET: StateController/Delete/5
        public ActionResult Delete(int id)
        {
            // pass session variable to view
            ViewBag.UserID = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));

            var model = new StateViewModel();
            string response = WebAPIHelper.GetWebAPIClient($"State/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<StateViewModel>(response);
                model.CountryName = GetCountryName(model.CountryId);
            }

            return View(model);
        }

        // POST: StateController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int loginUserId)
        {
            ToastrViewModel tosterModel = null;
            try
            {
                loginUserId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
                string response = WebAPIHelper.DeleteWebAPIClient($"State/{id}?loginUserId={loginUserId}").Result;
                if (!string.IsNullOrEmpty(response))
                {
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Success,
                        Message = ToastrMessages.StateDeleteSuccess.GetEnumDescription<ToastrMessages>()
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
                Message = ToastrMessages.StateDeleteError.GetEnumDescription<ToastrMessages>()
            };

            //TempData["ToastrModel"] = tosterModel;
            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

            return View();
        }

        #endregion

        #region Private Methods

        // Get all country
        private void GetAllCountry(StateViewModel model)
        {
            string response = WebAPIHelper.GetWebAPIClient("Country").Result;
            if (!string.IsNullOrEmpty(response))
            {
                var CountryList = JsonConvert.DeserializeObject<List<CountryViewModel>>(response);

                // Create a SelectList
                model.CountryList = new SelectList(CountryList, "ID", "CountryName");
            }
        }

        // Get country name
        private string GetCountryName(int id)
        {
            string response = WebAPIHelper.GetWebAPIClient("Country").Result;
            if (!string.IsNullOrEmpty(response))
            {
                var countryList = JsonConvert.DeserializeObject<List<CountryViewModel>>(response);

                // Find the country with the matching ID
                var country = countryList.FirstOrDefault(x => x.ID == id);

                // Return the country name if found, otherwise return null
                return country?.CountryName;
            }
            return null;
        }

        #endregion

        #region Remote Validation Actions

        // For state name
        public IActionResult VerifyStateName(int ID, int CountryId, string StateName)
        {
            // Web client api call
            string isExistResponse = WebAPIHelper.GetWebAPIClient($"State/VerifyStateName?id={ID}&countryId={CountryId}&stateName={StateName}").Result;

            if (Convert.ToBoolean(isExistResponse))
            {
                return Json($"State name {StateName} is already exist.");
            }

            return Json(true);
        }

        // For state code
        public IActionResult VerifyStateCode(int ID, int CountryId, string StateCode)
        {
            // Web client api call
            string isExistResponse = WebAPIHelper.GetWebAPIClient($"State/VerifyStateCode?id={ID}&countryId={CountryId}&stateCode={StateCode}").Result;

            if (Convert.ToBoolean(isExistResponse))
            {
                return Json($"State code {StateCode} is already exist.");
            }

            return Json(true);
        }

        #endregion
    }
}
