using eGift.Admin.MVC.Common;
using eGift.Admin.MVC.Helpers;
using eGift.Admin.MVC.Models;
using eGift.Admin.MVC.Models.ListViewModels;
using eGift.Admin.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eGift.Admin.MVC.Controllers
{
    public class CustomerController : Controller
    {
        #region Constructors

        public CustomerController()
        {
        }

        #endregion

        #region Customer Default CRUD Actions

        // GET: CustomerController
        public ActionResult Index()
        {
            var list = new CustomerListViewModel();
            string response = WebAPIHelper.GetWebAPIClient("Customer").Result;
            if (!string.IsNullOrEmpty(response))
            {
                list.CustomerList = JsonConvert.DeserializeObject<List<CustomerViewModel>>(response);
                list.CustomerList.ForEach(x => { x.GenderName = ((Gender)x.GenderId).ToString(); x.RoleName = ((Role)x.RoleId).ToString(); });
            }

            return View(list);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            var model = new CustomerViewModel();
            string response = WebAPIHelper.GetWebAPIClient($"Customer/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<CustomerViewModel>(response);
                model.GenderName = ((Gender)model.GenderId).ToString();
                model.RoleName = (((Role)model.RoleId).ToString());
                model.Age = CalculateAge(model.DateOfBirth);
            }

            return View(model);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            var model = new CustomerViewModel();
            return View(model);
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel model)
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
                        Message = ToastrMessages.CustomerCreateError.GetEnumDescription<ToastrMessages>()
                    };

                    //TempData["ToastrModel"] = tosterModel;
                    // Serialize the model to JSON before storing it in TempData
                    TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

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

                    model.RoleId = (int)Role.Customer;
                    model.IsDeleted = false;
                    model.CreatedBy = 1;//After login from session
                    model.CreatedDate = DateTime.Now;
                }

                // Model to json string
                var modelData = JsonConvert.SerializeObject(model);

                // Web API call
                string response = WebAPIHelper.PostWebAPIClient("Customer", modelData).Result;
                if (!string.IsNullOrEmpty(response))
                {
                    var customerModel = JsonConvert.DeserializeObject<EmployeeViewModel>(response);
                    if (customerModel != null)
                    {
                        // Employee login model
                        var loginModel = new LoginViewModel()
                        {
                            IsActive = customerModel.IsActive,
                            RefId = customerModel.ID,
                            RefType = Role.Customer.ToString(),
                            UserName = model.LoginModel.UserName,
                            Password = model.LoginModel.Password,
                            RoleId = (int)Role.Customer
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
                                Message = ToastrMessages.CustomerCreateSuccess.GetEnumDescription<ToastrMessages>()
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
                Message = ToastrMessages.CustomerCreateError.GetEnumDescription<ToastrMessages>()
            };

            //TempData["ToastrModel"] = tosterModel;
            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

            return View(model);
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = new CustomerViewModel();
            string response = WebAPIHelper.GetWebAPIClient($"Customer/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<CustomerViewModel>(response);
                model.Age = CalculateAge(model.DateOfBirth);

                // web client api call for login model
                string loginResponse = WebAPIHelper.GetWebAPIClient($"Login/GetCustomerLogin/{id}").Result;
                if (!string.IsNullOrEmpty(loginResponse))
                {
                    var loginModel = JsonConvert.DeserializeObject<LoginViewModel>(loginResponse);
                    model.LoginModel = loginModel;
                }
            }
            return View(model);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CustomerViewModel model)
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
                        Message = ToastrMessages.CustomerEditError.GetEnumDescription<ToastrMessages>()
                    };

                    //TempData["ToastrModel"] = tosterModel;
                    // Serialize the model to JSON before storing it in TempData
                    TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

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

                    model.RoleId = (int)Role.Customer;
                    model.UpdatedBy = 1;//After login from session
                    model.UpdatedDate = DateTime.Now;
                }

                // Serialize model to json string
                var modelData = JsonConvert.SerializeObject(model);

                // Web client api call
                string response = WebAPIHelper.PutWebAPIClient($"Customer/{model.ID}", modelData).Result;
                if (!string.IsNullOrEmpty(response))
                {
                    var customerModel = JsonConvert.DeserializeObject<CustomerViewModel>(response);

                    //string loginResponse = WebAPIHelper.GetWebAPIClient($"Login?refId={id}&refType={Role.Employee.ToString()}").Result;
                    string loginResponse = WebAPIHelper.GetWebAPIClient($"Login/GetCustomerLogin/{id}").Result;
                    if (!string.IsNullOrEmpty(loginResponse))
                    {
                        var existingLoginModel = JsonConvert.DeserializeObject<LoginViewModel>(loginResponse);
                        if (existingLoginModel != null)
                        {
                            existingLoginModel.IsActive = model.IsActive;
                            existingLoginModel.UserName = model.LoginModel.UserName;
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
                                    Message = ToastrMessages.CustomerEditSuccess.GetEnumDescription<ToastrMessages>()
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
            catch
            {
            }
            tosterModel = new ToastrViewModel()
            {
                Type = (int)ToastrType.Error,
                Message = ToastrMessages.CustomerEditError.GetEnumDescription<ToastrMessages>()
            };

            //TempData["ToastrModel"] = tosterModel;
            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

            return View(model);
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = new CustomerViewModel();
            string response = WebAPIHelper.GetWebAPIClient($"Customer/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                model = JsonConvert.DeserializeObject<CustomerViewModel>(response);
                model.Age = CalculateAge(model.DateOfBirth);
                model.RoleName = (((Role)model.RoleId).ToString());
                model.GenderName = ((Gender)model.GenderId).ToString();

                // Fetch login model
                string loginResponse = WebAPIHelper.GetWebAPIClient($"Login/GetCustomerLogin/{id}").Result;
                if (!string.IsNullOrEmpty(loginResponse))
                {
                    var loginModel = JsonConvert.DeserializeObject<LoginViewModel>(loginResponse);

                    model.LoginModel = loginModel;
                }
            }
            return View(model);
        }

        // POST: CustomerController/Delete/5
        [HttpPost]

        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int loginUserId)
        {
            ToastrViewModel tosterModel = null;
            try
            {
                string response = WebAPIHelper.DeleteWebAPIClient($"Customer/{id}?loginUserId={loginUserId}").Result;
                if (!string.IsNullOrEmpty(response))
                {
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Success,
                        Message = ToastrMessages.CustomerDeleteSuccess.GetEnumDescription<ToastrMessages>()
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
                Message = ToastrMessages.CustomerDeleteError.GetEnumDescription<ToastrMessages>()
            };

            //TempData["ToastrModel"] = tosterModel;
            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

            return View();
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

        #endregion
    }
}
