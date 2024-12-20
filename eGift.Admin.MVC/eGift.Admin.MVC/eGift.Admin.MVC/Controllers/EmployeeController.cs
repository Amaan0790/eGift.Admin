﻿using eGift.Admin.MVC.Common;
using eGift.Admin.MVC.Helpers;
using eGift.Admin.MVC.Models;
using eGift.Admin.MVC.Models.ListViewModels;
using eGift.Admin.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace eGift.Admin.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        #region Constructors

        public EmployeeController()
        {
        }

        #endregion

        #region Employee Default CRUD Actions

        // GET: EmployeeController
        public ActionResult Index(string deleteSuccess = "")
        {
            var list = new EmployeeListViewModel();
            string response = WebAPIHelper.GetWebAPIClient("Employee").Result;
            if (!string.IsNullOrEmpty(response))
            {
                list.EmployeeList = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(response);
                list.EmployeeList.ForEach(x => { x.GenderName = ((Gender)x.GenderId).ToString(); x.RoleName = ((Role)x.RoleId).ToString(); });
            }

            // to display delete success toastr
            if (!string.IsNullOrEmpty(deleteSuccess))
            {
                var tosterModel = new ToastrViewModel()
                {
                    Type = (int)ToastrType.Success,
                    Message = ToastrMessages.EmployeeDeleteSuccess.GetEnumDescription<ToastrMessages>()
                };

                //TempData["ToastrModel"] = tosterModel;
                // Serialize the model to JSON before storing it in TempData
                TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);
            }
            return View(list);
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            var model = new EmployeeViewModel();
            string response = WebAPIHelper.GetWebAPIClient($"Employee/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<EmployeeViewModel>(response);
                model.GenderName = ((Gender)model.GenderId).ToString();
                model.RoleName = (((Role)model.RoleId).ToString());
                model.Age = CalculateAge(model.DateOfBirth);

                GetFullAddress(model);
            }

            return View(model);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            var model = new EmployeeViewModel();
            GetAllAddress(model);
            return View(model);
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeViewModel model)
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
                        Message = ToastrMessages.EmployeeCreateError.GetEnumDescription<ToastrMessages>()
                    };

                    //TempData["ToastrModel"] = tosterModel;
                    // Serialize the model to JSON before storing it in TempData
                    TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

                    GetAllAddress(model);
                    return View(model);
                }

                // For create
                if (model.ID == 0)
                {
                    // Check if the file is not null and has content
                    if (model.ProfileImage != null && model.ProfileImage.Length > 0)
                    {
                        // Get the file name
                        model.ProfileImagePath = Path.GetFileName(model.ProfileImage.FileName);

                        using (var memoryStream = new MemoryStream())
                        {
                            model.ProfileImage.CopyTo(memoryStream);
                            model.ProfileImageData = memoryStream.ToArray(); // Convert to byte array
                        }
                    }

                    model.RoleId = (int)Role.Employee;
                    model.IsDeleted = false;
                    model.CreatedBy = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
                    model.CreatedDate = DateTime.Now;
                }

                // Model to json string
                var modelData = JsonConvert.SerializeObject(model);

                // Web API call
                string response = WebAPIHelper.PostWebAPIClient("Employee", modelData).Result;
                if (!string.IsNullOrEmpty(response))
                {
                    var employeeModel = JsonConvert.DeserializeObject<EmployeeViewModel>(response);
                    if (employeeModel != null)
                    {
                        // Employee login model
                        var loginModel = new LoginViewModel()
                        {
                            IsActive = employeeModel.IsActive,
                            RefId = employeeModel.ID,
                            RefType = Role.Employee.ToString(),
                            UserName = model.UserName,
                            Password = model.LoginModel.Password,
                            RoleId = (int)Role.Employee
                        };

                        // Model to json string
                        var loginModelData = JsonConvert.SerializeObject(loginModel);

                        // Web API call
                        string loginResponse = WebAPIHelper.PostWebAPIClient("Login", loginModelData).Result;
                        if (!string.IsNullOrEmpty(loginResponse))
                        {
                            tosterModel = new ToastrViewModel()
                            {
                                Type = (int)ToastrType.Success,
                                Message = ToastrMessages.EmployeeCreateSuccess.GetEnumDescription<ToastrMessages>()
                            };

                            //TempData["ToastrModel"] = tosterModel;
                            // Serialize the model to JSON before storing it in TempData
                            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            tosterModel = new ToastrViewModel()
            {
                Type = (int)ToastrType.Error,
                Message = ToastrMessages.EmployeeCreateError.GetEnumDescription<ToastrMessages>()
            };

            //TempData["ToastrModel"] = tosterModel;
            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

            GetAllAddress(model);
            return View(model);
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = new EmployeeViewModel();
            string response = WebAPIHelper.GetWebAPIClient($"Employee/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<EmployeeViewModel>(response);
                model.Age = CalculateAge(model.DateOfBirth);

                // web client api call for login model
                string loginResponse = WebAPIHelper.GetWebAPIClient($"Login/GetEmployeeLogin/{id}").Result;
                if (!string.IsNullOrEmpty(loginResponse))
                {
                    var loginModel = JsonConvert.DeserializeObject<LoginViewModel>(loginResponse);
                    model.LoginModel = loginModel;
                    model.UserName = loginModel.UserName;
                }
                GetAllAddress(model);
            }
            return View(model);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EmployeeViewModel model)
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
                        Message = ToastrMessages.EmployeeEditError.GetEnumDescription<ToastrMessages>()
                    };

                    //TempData["ToastrModel"] = tosterModel;
                    // Serialize the model to JSON before storing it in TempData
                    TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

                    GetAllAddress(model);
                    return View(model);
                }

                if (model.ID > 0)
                {
                    // Check if the file is not null and has content
                    if (model.ProfileImage != null && model.ProfileImage.Length > 0)
                    {
                        // Get the file name
                        model.ProfileImagePath = Path.GetFileName(model.ProfileImage.FileName);

                        using (var memoryStream = new MemoryStream())
                        {
                            model.ProfileImage.CopyTo(memoryStream);
                            model.ProfileImageData = memoryStream.ToArray(); // Convert to byte array
                        }
                    }
                    else if (model.IsClear)
                    {
                        model.ProfileImagePath = null;
                        model.ProfileImageData = null;
                    }

                    model.RoleId = (int)Role.Employee;
                    model.UpdatedBy = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
                    model.UpdatedDate = DateTime.Now;
                }

                var modelData = JsonConvert.SerializeObject(model);

                //web client api call
                string response = WebAPIHelper.PutWebAPIClient($"Employee/{model.ID}", modelData).Result;
                if (!string.IsNullOrEmpty(response))
                {
                    var employeeModel = JsonConvert.DeserializeObject<EmployeeViewModel>(response);

                    //string loginResponse = WebAPIHelper.GetWebAPIClient($"Login?refId={id}&refType={Role.Employee.ToString()}").Result;
                    string loginResponse = WebAPIHelper.GetWebAPIClient($"Login/GetEmployeeLogin/{id}").Result;
                    if (!string.IsNullOrEmpty(loginResponse))
                    {
                        var existingLoginModel = JsonConvert.DeserializeObject<LoginViewModel>(loginResponse);
                        if (existingLoginModel != null)
                        {
                            existingLoginModel.IsActive = model.IsActive;
                            existingLoginModel.UserName = model.UserName;
                            existingLoginModel.Password = model.LoginModel.Password;

                            // Model to json string
                            var loginModelData = JsonConvert.SerializeObject(existingLoginModel);

                            // Web API call
                            string updateLoginResponse = WebAPIHelper.PutWebAPIClient($"Login/{existingLoginModel.ID}", loginModelData).Result;
                            if (!string.IsNullOrEmpty(updateLoginResponse))
                            {
                                tosterModel = new ToastrViewModel()
                                {
                                    Type = (int)ToastrType.Success,
                                    Message = ToastrMessages.EmployeeEditSuccess.GetEnumDescription<ToastrMessages>()
                                };

                                //TempData["ToastrModel"] = tosterModel;
                                // Serialize the model to JSON before storing it in TempData
                                TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

                                return RedirectToAction(nameof(Index));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            tosterModel = new ToastrViewModel()
            {
                Type = (int)ToastrType.Error,
                Message = ToastrMessages.EmployeeEditError.GetEnumDescription<ToastrMessages>()
            };

            //TempData["ToastrModel"] = tosterModel;
            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

            GetAllAddress(model);
            return View(model);
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = new EmployeeViewModel();
            string response = WebAPIHelper.GetWebAPIClient($"Employee/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<EmployeeViewModel>(response);
                model.Age = CalculateAge(model.DateOfBirth);
                model.RoleName = (((Role)model.RoleId).ToString());
                model.GenderName = ((Gender)model.GenderId).ToString();

                // Fetch login model
                string loginResponse = WebAPIHelper.GetWebAPIClient($"Login/GetEmployeeLogin/{id}").Result;
                if (!string.IsNullOrEmpty(loginResponse))
                {
                    var loginModel = JsonConvert.DeserializeObject<LoginViewModel>(loginResponse);

                    model.LoginModel = loginModel;

                    // Call Get Full Address method
                    GetFullAddress(model);
                }
            }
            return View(model);
        }

        // POST: EmployeeController/Delete
        [HttpPost]
        public JsonResult Delete(int id, int loginUserId)
        {
            try
            {
                string response = WebAPIHelper.DeleteWebAPIClient($"Employee/{id}?loginUserId={loginUserId}").Result;
                if (!string.IsNullOrEmpty(response))
                {
                    return Json(true);
                }
            }
            catch
            {
            }
            return Json(false);
        }

        #endregion

        #region Private Functions

        // function to calculate age based on date of birth
        private int CalculateAge(DateTime? dateOfBirth)
        {
            if (dateOfBirth == null)
            {
                return 0;
            }
            else
            {
                var today = DateTime.Today;
                var age = today.Year - dateOfBirth.Value.Year;

                // Check if the birthday has occurred this year; if not, subtract one year from the age
                if (dateOfBirth.Value.Date > today.AddYears(-age))
                {
                    age--;
                }

                return age;
            }
        }

        // Get All Address
        private void GetAllAddress(EmployeeViewModel model)
        {
            // Web api call
            var response = WebAPIHelper.GetWebAPIClient("Address").Result;
            if (response != null)
            {
                var addressList = JsonConvert.DeserializeObject<List<AddressViewModel>>(response);

                // Create a SelectList
                model.AddressList = new SelectList(addressList, "ID", "FullAddress");
            }
        }

        // Get Full Address
        private void GetFullAddress(EmployeeViewModel model)
        {
            if (model.AddressId != null)
            {
                // Web api call
                var response = WebAPIHelper.GetWebAPIClient($"Address/{model.AddressId}").Result;
                if (response != null)
                {
                    var addressModel = JsonConvert.DeserializeObject<AddressViewModel>(response);

                    // Assign full address
                    model.AddressName = addressModel?.FullAddress;
                }
            }
        }

        #endregion

        #region Remote Validation Actions

        public IActionResult VerifyUserName(int ID, string UserName)
        {
            // Web client api call
            string isExistResponse = WebAPIHelper.GetWebAPIClient($"Employee/VerifyUserName?id={ID}&userName={UserName}").Result;

            if (Convert.ToBoolean(isExistResponse))
            {
                return Json($"Username {UserName} is already in use.");
            }

            return Json(true);
        }

        #endregion
    }
}
