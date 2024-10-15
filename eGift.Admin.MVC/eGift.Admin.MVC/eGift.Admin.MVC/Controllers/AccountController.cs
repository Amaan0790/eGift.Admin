using eGift.Admin.MVC.Common;
using eGift.Admin.MVC.Helpers;
using eGift.Admin.MVC.Models;
using eGift.Admin.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eGift.Admin.MVC.Controllers
{
    public class AccountController : Controller
    {
        #region Constructors

        public AccountController()
        {
        }

        #endregion

        #region Account Actions

        // GET: AccountController
        public ActionResult Index()
        {
            return View(new SignInViewModel());
        }

        // POST: AccountController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(SignInViewModel model)
        {
            ToastrViewModel tosterModel = null;
            try
            {
                // Server side validation
                if (ModelState.IsValid)
                {
                    // Web client api call
                    string response = WebAPIHelper.GetWebAPIClient($"Login/AdminEmployeeLogin?userName={model.UserName}&password={model.Password}").Result;
                    if (!string.IsNullOrEmpty(response))
                    {
                        var loginModel = JsonConvert.DeserializeObject<LoginViewModel>(response);
                        if (loginModel != null)
                        {
                            if (loginModel.IsActive)
                            {
                                // Set user sessions
                                HttpContext.Session.SetInt32("UserID", loginModel.RefId);
                                HttpContext.Session.SetString("UserName", loginModel.UserName);

                                // Last Login
                                DateTime loginDateTime = DateTime.Now;

                                loginModel.LogInDate = loginDateTime;

                                // Serialize model to json string
                                var modelData = JsonConvert.SerializeObject(loginModel);

                                // Web client api call
                                string lastLoginResponse = WebAPIHelper.PostWebAPIClient($"Login/UpdateLastLogin", modelData).Result;
                                if (!string.IsNullOrEmpty(lastLoginResponse))
                                {
                                    var lastLoginModel = JsonConvert.DeserializeObject<LoginViewModel>(lastLoginResponse);

                                    // Set last login sessions
                                    HttpContext.Session.SetString("LoginDateTime", lastLoginModel.LogInDate.ToDateTimeString());
                                    HttpContext.Session.SetString("LastLogin", lastLoginModel.LastLogInDate == null ? lastLoginModel.LogInDate.ToDateTimeString() : lastLoginModel.LastLogInDate.ToDateTimeString());
                                }

                                tosterModel = new ToastrViewModel()
                                {
                                    Type = (int)ToastrType.Success,
                                    Message = ToastrMessages.LoginSuccess.GetEnumDescription<ToastrMessages>()
                                };

                                // Serialize the model to JSON before storing it in TempData
                                TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                tosterModel = new ToastrViewModel()
                                {
                                    Type = (int)ToastrType.Warning,
                                    Message = ToastrMessages.LoginInactive.GetEnumDescription<ToastrMessages>()
                                };

                                // Serialize the model to JSON before storing it in TempData
                                TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);
                                return RedirectToAction(nameof(Index));
                            }
                        }
                    }
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Warning,
                        Message = ToastrMessages.LoginInvalid.GetEnumDescription<ToastrMessages>()
                    };

                    // Serialize the model to JSON before storing it in TempData
                    TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);
                }
                else
                {
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Error,
                        Message = ToastrMessages.LoginError.GetEnumDescription<ToastrMessages>()
                    };

                    // Serialize the model to JSON before storing it in TempData
                    TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);
                }
            }
            catch (Exception ex)
            {
                tosterModel = new ToastrViewModel()
                {
                    Type = (int)ToastrType.Error,
                    Message = ToastrMessages.LoginError.GetEnumDescription<ToastrMessages>()
                };

                // Serialize the model to JSON before storing it in TempData
                TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: AccountController/Logout
        public ActionResult Logout()
        {
            // Reset all session
            HttpContext.Session.Clear();

            // Toastr Message
            var tosterModel = new ToastrViewModel()
            {
                Type = (int)ToastrType.Success,
                Message = ToastrMessages.LogoutSuccess.GetEnumDescription<ToastrMessages>()
            };

            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
