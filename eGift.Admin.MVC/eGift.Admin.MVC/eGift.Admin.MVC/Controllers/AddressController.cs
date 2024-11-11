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
    public class AddressController : Controller
    {
        #region Constructors
        public AddressController()
        {
        }
        #endregion

        #region Address Default CRUD Actions

        // GET: AddressController
        public ActionResult Index()
        {
            var list = new AddressListViewModel();

            // Web api call
            string response = WebAPIHelper.GetWebAPIClient("Address").Result;
            if (!string.IsNullOrEmpty(response))
            {
                list.AddressList = JsonConvert.DeserializeObject<List<AddressViewModel>>(response);
                foreach (var addressItem in list.AddressList)
                {
                    addressItem.CountryName = GetCountryName(addressItem.CountryId);
                    addressItem.StateName = GetStateName(addressItem.StateId);
                    addressItem.CityName = GetCityName(addressItem.CityId);
                }
            }

            return View(list);
        }

        // GET: AddressController/Details/5
        public ActionResult Details(int id)
        {
            var model = new AddressViewModel();

            // Web api call
            string response = WebAPIHelper.GetWebAPIClient($"Address/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<AddressViewModel>(response);
                model.CountryName = GetCountryName(model.CountryId);
                model.StateName = GetStateName(model.StateId);
                model.CityName = GetCityName(model.CityId);
            }
            return View(model);
        }

        // GET: AddressController/Create
        public ActionResult Create()
        {
            var model = new AddressViewModel();

            // Get all country
            GetAllCountry(model);
            return View(model);
        }

        // POST: AddressController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddressViewModel model)
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
                        Message = ToastrMessages.AddressCreateError.GetEnumDescription<ToastrMessages>()
                    };

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
                string response = WebAPIHelper.PostWebAPIClient("Address", modelData).Result;
                if (!string.IsNullOrEmpty(response))
                {
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Success,
                        Message = ToastrMessages.AddressCreateSuccess.GetEnumDescription<ToastrMessages>()
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
                Message = ToastrMessages.AddressCreateError.GetEnumDescription<ToastrMessages>()
            };

            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);
            return View();
        }

        // GET: AddressController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = new AddressViewModel();

            // Web api call
            string response = WebAPIHelper.GetWebAPIClient($"Address/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<AddressViewModel>(response);
                GetAllCountry(model);
            }
            return View(model);
        }

        // POST: AddressController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AddressViewModel model)
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
                        Message = ToastrMessages.AddressEditError.GetEnumDescription<ToastrMessages>()
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
                string response = WebAPIHelper.PutWebAPIClient($"Address/{model.ID}", modelData).Result;
                if (!string.IsNullOrEmpty(response))
                {
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Success,
                        Message = ToastrMessages.AddressEditSuccess.GetEnumDescription<ToastrMessages>()
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
                Message = ToastrMessages.AddressEditError.GetEnumDescription<ToastrMessages>()
            };

            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);
            return View();
        }

        // GET: AddressController/Delete/5
        public ActionResult Delete(int id)
        {
            // pass session variable to view
            ViewBag.UserID = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));

            var model = new AddressViewModel();

            // Web api call
            string response = WebAPIHelper.GetWebAPIClient($"Address/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<AddressViewModel>(response);
                model.CountryName = GetCountryName(model.CountryId);
                model.StateName = GetStateName(model.StateId);
                model.CityName = GetCityName(model.CityId);
            }
            return View(model);
        }

        // POST: AddressController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int loginUserId)
        {
            ToastrViewModel tosterModel = null;
            try
            {
                loginUserId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));

                // Web api call
                string response = WebAPIHelper.DeleteWebAPIClient($"Address/{id}?loginUserId={loginUserId}").Result;
                if (!string.IsNullOrEmpty(response))
                {
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Success,
                        Message = ToastrMessages.AddressDeleteSuccess.GetEnumDescription<ToastrMessages>()
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
                Message = ToastrMessages.AddressDeleteError.GetEnumDescription<ToastrMessages>()
            };

            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);
            return View();
        }
        #endregion

        #region Private Methods

        // Get all country
        private void GetAllCountry(AddressViewModel model)
        {
            // Web api call
            string response = WebAPIHelper.GetWebAPIClient("Country").Result;
            if (!string.IsNullOrEmpty(response))
            {
                var countryList = JsonConvert.DeserializeObject<List<CountryViewModel>>(response);

                // Create a SelectList
                model.CountryList = new SelectList(countryList, "ID", "CountryName");
            }
        }

        // Get country name
        private string GetCountryName(int countryId)
        {
            // Web api call
            string response = WebAPIHelper.GetWebAPIClient($"Country/{countryId}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                var country = JsonConvert.DeserializeObject<CountryViewModel>(response);

                // Return the country name
                return country.CountryName;
            }
            return null;
        }

        // Get state name
        private string GetStateName(int stateId)
        {
            // Web api call
            string response = WebAPIHelper.GetWebAPIClient($"State/{stateId}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                var state = JsonConvert.DeserializeObject<StateViewModel>(response);

                // Return the state name
                return state.StateName;
            }
            return null;
        }

        // Get city name
        private string GetCityName(int cityId)
        {
            // Web api call
            string response = WebAPIHelper.GetWebAPIClient($"City/{cityId}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                var city = JsonConvert.DeserializeObject<CityViewModel>(response);

                // Return the city name
                return city.CityName;
            }
            return null;
        }
        #endregion

        #region Ajax Actions

        // GET: AddressController/GetStatesByCountry/5
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

        // GET: AddressController/GetCitiesByState/5
        public ActionResult GetCitiesByState(int stateId)
        {
            List<CityViewModel> cityList = null;

            // Web API call
            string response = WebAPIHelper.GetWebAPIClient($"City/GetCitiesByState/{stateId}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                cityList = JsonConvert.DeserializeObject<List<CityViewModel>>(response);
                return Json(cityList);
            }

            return Json(cityList);
        }
        #endregion
    }
}
