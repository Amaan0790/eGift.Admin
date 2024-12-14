using eGift.Admin.MVC.Common;
using eGift.Admin.MVC.Helpers;
using eGift.Admin.MVC.Models;
using eGift.Admin.MVC.Models.ListViewModels;
using eGift.Admin.MVC.Models.ResponseModels;
using eGift.Admin.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eGift.Admin.MVC.Controllers
{
    public class OrderController : Controller
    {
        #region Constructors

        public OrderController()
        {
        }

        #endregion

        #region Order Index/Details Actions

        // GET: OrderController
        public ActionResult Index()
        {
            var list = new OrderListViewModel();

            // Web api call
            var response = WebAPIHelper.GetWebAPIClient("Order").Result;
            if (!string.IsNullOrEmpty(response))
            {
                list.OrderList = JsonConvert.DeserializeObject<List<OrderViewModel>>(response);
            }
            return View(list);
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            var orderResponseModel = new OrderResponseModel();

            // Web api call
            var response = WebAPIHelper.GetWebAPIClient($"Order/GetOrderResponse/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                orderResponseModel = JsonConvert.DeserializeObject<OrderResponseModel>(response);
            }
            return View(orderResponseModel);
        }

        // POST: OrderController/Details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(OrderResponseModel orderResponseModel)
        {
            ToastrViewModel tosterModel = null;
            try
            {
                // User Id
                var userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));

                if (orderResponseModel != null)
                {
                    var today = DateTime.Now;
                    if (orderResponseModel.OrderModel.StatusId == 6)
                    {
                        orderResponseModel.OrderModel.CompletedDate = today;
                    }
                    else if (orderResponseModel.OrderModel.StatusId == 5)
                    {
                        orderResponseModel.OrderModel.CancelDate = today;
                    }
                    else if (orderResponseModel.OrderModel.StatusId == 4)
                    {
                        orderResponseModel.OrderModel.DeliveryDate = today;
                    }
                    else if (orderResponseModel.OrderModel.StatusId == 3)
                    {
                        orderResponseModel.OrderModel.ShippedDate = today;
                    }
                    else if (orderResponseModel.OrderModel.StatusId == 2)
                    {
                        orderResponseModel.OrderModel.DispatchedDate = today;
                    }

                    orderResponseModel.OrderModel.UpdatedBy = userId;
                    orderResponseModel.OrderModel.UpdatedDate = today;

                    // Web api call
                    var orderDetailResponse = WebAPIHelper.GetWebAPIClient($"OrderDetails/GetByCustomerId/{orderResponseModel.OrderModel.CustomerId}").Result;
                    if (!string.IsNullOrEmpty(orderDetailResponse))
                    {
                        var orderDetailList = JsonConvert.DeserializeObject<List<OrderDetailsViewModel>>(orderDetailResponse);

                        // updating UpdatedBy and UpdatedDate
                        foreach (var orderDetail in orderDetailList)
                        {
                            orderDetail.UpdatedBy = userId;
                            orderDetail.UpdatedDate = today;
                        }

                        orderResponseModel.OrderDetailList = orderDetailList;
                    }

                    // Serialize the model
                    var orderResponseData = JsonConvert.SerializeObject(orderResponseModel);

                    // Web api call
                    var response = WebAPIHelper.PutWebAPIClient($"Order/{orderResponseModel.OrderModel.ID}", orderResponseData).Result;
                    if (!string.IsNullOrEmpty(response))
                    {
                        tosterModel = new ToastrViewModel()
                        {
                            Type = (int)ToastrType.Success,
                            Message = ToastrMessages.OrderUpdateSuccess.GetEnumDescription<ToastrMessages>()
                        };

                        // Serialize the model to JSON before storing it in TempData
                        TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch
            {
            }
            tosterModel = new ToastrViewModel()
            {
                Type = (int)ToastrType.Error,
                Message = ToastrMessages.OrderUpdateError.GetEnumDescription<ToastrMessages>()
            };

            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);
            return View();
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        #endregion
    }
}
