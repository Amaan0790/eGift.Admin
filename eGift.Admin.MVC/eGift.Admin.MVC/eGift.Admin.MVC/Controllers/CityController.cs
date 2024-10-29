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
    public class CityController : Controller
    {
        #region Constructors

        public CityController()
        {
        }

        #endregion

        #region City Default CRUD Actions

        // GET: CityController
        public ActionResult Index()
        {
            var list = new CityListViewModel();

            // Get all city
            string response = WebAPIHelper.GetWebAPIClient("City").Result;
            if (!string.IsNullOrEmpty(response))
            {
                list.CityList = JsonConvert.DeserializeObject<List<CityViewModel>>(response);

                // Web api call
                string stateResponse = WebAPIHelper.GetWebAPIClient("State").Result;
                if (!string.IsNullOrEmpty(stateResponse))
                {
                    var stateList = JsonConvert.DeserializeObject<List<StateViewModel>>(stateResponse);

                    list.CityList.ForEach(x =>
                    {
                        x.StateName = GetStateName(x.StateId);
                        var countryId = stateList.Where(s => s.ID == x.StateId).FirstOrDefault().CountryId;
                        x.CountryId = countryId;
                        x.CountryName = GetCountryName(countryId);
                    });
                }
            }

            return View(list);
        }

        // GET: CityController/Details/5
        public ActionResult Details(int id)
        {
            var model = new CityViewModel();
            string response = WebAPIHelper.GetWebAPIClient($"City/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<CityViewModel>(response);

                // Web api call
                string stateResponse = WebAPIHelper.GetWebAPIClient($"State/{model.StateId}").Result;
                if (!string.IsNullOrEmpty(stateResponse))
                {
                    // Deserialize model
                    var state = JsonConvert.DeserializeObject<StateViewModel>(stateResponse);
                    model.CountryId = state.CountryId;
                    model.StateName = GetStateName(model.StateId);
                    model.CountryName = GetCountryName(state.CountryId);
                }
            }

            return View(model);
        }

        // GET: CityController/Create
        public ActionResult Create()
        {
            var model = new CityViewModel();
            GetAllCountry(model);

            //GetAllState(model);
            return View(model);
        }

        // POST: CityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CityViewModel model)
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
                        Message = ToastrMessages.CityCreateError.GetEnumDescription<ToastrMessages>()
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

                // Web API call
                string response = WebAPIHelper.PostWebAPIClient("City", modelData).Result;
                if (!string.IsNullOrEmpty(response))
                {
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Success,
                        Message = ToastrMessages.CityCreateSuccess.GetEnumDescription<ToastrMessages>()
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
                Message = ToastrMessages.CityCreateError.GetEnumDescription<ToastrMessages>()
            };

            //TempData["ToastrModel"] = tosterModel;
            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

            return View();
        }

        // GET: CityController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = new CityViewModel();

            // Web api call
            string response = WebAPIHelper.GetWebAPIClient($"City/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<CityViewModel>(response);

                // Web api call
                string stateResponse = WebAPIHelper.GetWebAPIClient($"State/{model.StateId}").Result;
                if (!string.IsNullOrEmpty(stateResponse))
                {
                    // Web api call
                    var state = JsonConvert.DeserializeObject<StateViewModel>(stateResponse);
                    model.CountryId = state.CountryId;
                }

                GetAllCountry(model);
            }
            return View(model);
        }

        // POST: CityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CityViewModel model)
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
                        Message = ToastrMessages.CityEditError.GetEnumDescription<ToastrMessages>()
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
                string response = WebAPIHelper.PutWebAPIClient($"City/{model.ID}", modelData).Result;
                if (!string.IsNullOrEmpty(response))
                {
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Success,
                        Message = ToastrMessages.CityEditSuccess.GetEnumDescription<ToastrMessages>()
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
                Message = ToastrMessages.CityEditError.GetEnumDescription<ToastrMessages>()
            };

            //TempData["ToastrModel"] = tosterModel;
            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);
            return View();
        }

        // GET: CityController/Delete/5
        public ActionResult Delete(int id)
        {
            // pass session variable to view
            ViewBag.UserID = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));

            var model = new CityViewModel();
            string response = WebAPIHelper.GetWebAPIClient($"City/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<CityViewModel>(response);

                // Web api call
                string stateResponse = WebAPIHelper.GetWebAPIClient($"State/{model.StateId}").Result;
                if (!string.IsNullOrEmpty(stateResponse))
                {
                    // Deserialize model
                    var state = JsonConvert.DeserializeObject<StateViewModel>(stateResponse);
                    model.CountryId = state.CountryId;
                    model.StateName = GetStateName(model.StateId);
                    model.CountryName = GetCountryName(state.CountryId);
                }
            }

            return View(model);
        }

        // POST: CityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int loginUserId)
        {
            ToastrViewModel tosterModel = null;
            try
            {
                loginUserId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
                string response = WebAPIHelper.DeleteWebAPIClient($"City/{id}?loginUserId={loginUserId}").Result;
                if (!string.IsNullOrEmpty(response))
                {
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Success,
                        Message = ToastrMessages.CityDeleteSuccess.GetEnumDescription<ToastrMessages>()
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
                Message = ToastrMessages.CityDeleteError.GetEnumDescription<ToastrMessages>()
            };

            //TempData["ToastrModel"] = tosterModel;
            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

            return View();
        }

        #endregion

        #region Private Methods

        // Get all country
        private void GetAllCountry(CityViewModel model)
        {
            string response = WebAPIHelper.GetWebAPIClient("Country").Result;
            if (!string.IsNullOrEmpty(response))
            {
                var countryList = JsonConvert.DeserializeObject<List<CountryViewModel>>(response);

                // Create a SelectList
                model.CountryList = new SelectList(countryList, "ID", "CountryName");
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

        // Get all state
        private void GetAllState(CityViewModel model)
        {
            string response = WebAPIHelper.GetWebAPIClient("State").Result;
            if (!string.IsNullOrEmpty(response))
            {
                var stateList = JsonConvert.DeserializeObject<List<StateViewModel>>(response);

                // Create a SelectList
                model.StateList = new SelectList(stateList, "ID", "StateName");
            }
        }

        // Get state name
        private string GetStateName(int id)
        {
            string response = WebAPIHelper.GetWebAPIClient("State").Result;
            if (!string.IsNullOrEmpty(response))
            {
                var stateList = JsonConvert.DeserializeObject<List<StateViewModel>>(response);

                // Find the state with the matching ID
                var state = stateList.FirstOrDefault(x => x.ID == id);

                // Return the state name if found, otherwise return null
                return state?.StateName;
            }
            return null;
        }

        #endregion

        #region Ajax Actions

        // GET: CityController/Details/5
        public ActionResult GetStatesByCountry(int countryId)
        {
            List<StateViewModel> stateList = null;

            // Web API call
            string response = WebAPIHelper.GetWebAPIClient($"State/GetStatesByCountry/{countryId}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                stateList = JsonConvert.DeserializeObject<List<StateViewModel>>(response);
                return Json(stateList);
            }

            return Json(stateList);
        }

        #endregion

        #region Remote Validation Actions

        // For city name
        public IActionResult VerifyCityName(int ID, int StateId, string CityName)
        {
            // Web client api call
            string isExistResponse = WebAPIHelper.GetWebAPIClient($"City/VerifyCityName?id={ID}&stateId={StateId}&cityName={CityName}").Result;

            if (Convert.ToBoolean(isExistResponse))
            {
                return Json($"City name {CityName} is already exist.");
            }

            return Json(true);
        }

        // For city code
        public IActionResult VerifyCityCode(int ID, int StateId, string CityCode)
        {
            // Web client api call
            string isExistResponse = WebAPIHelper.GetWebAPIClient($"City/VerifyCityCode?id={ID}&stateId={StateId}&cityCode={CityCode}").Result;

            if (Convert.ToBoolean(isExistResponse))
            {
                return Json($"City code {CityCode} is already exist.");
            }

            return Json(true);
        }

        #endregion
    }
}
