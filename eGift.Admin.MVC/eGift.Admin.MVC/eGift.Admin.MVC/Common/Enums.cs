using System.ComponentModel;

namespace eGift.Admin.MVC.Common
{
    #region General

    public enum Gender
    {
        [Description("Male")]
        Male = 1,

        [Description("Female")]
        Female = 2
    }

    #endregion

    #region Project Specific

    public enum Role
    {
        [Description("Employee")]
        Employee = 1,

        [Description("Customer")]
        Customer = 2
    }

    #endregion

    #region Toastr

    #region Toastr Type

    public enum ToastrType
    {
        [Description("Success")]
        Success = 1,

        [Description("Info")]
        Info = 2,

        [Description("Warning")]
        Warning = 3,

        [Description("Error")]
        Error = 4
    }

    #endregion

    #region Toastr Messages

    public enum ToastrMessages
    {
        // Employee CRUD
        [Description("Employee created successfully.")]
        EmployeeCreateSuccess = 1,

        [Description("Unable to create employee.")]
        EmployeeCreateError = 2,

        [Description("Employee edited successfully.")]
        EmployeeEditSuccess = 3,

        [Description("Unable to edit employee.")]
        EmployeeEditError = 4,

        [Description("Employee deleted successfully.")]
        EmployeeDeleteSuccess = 5,

        [Description("Unable to delete employee.")]
        EmployeeDeleteError = 6,

        // Customer CRUD
        [Description("Customer created successfully.")]
        CustomerCreateSuccess = 7,

        [Description("Unable to create customer.")]
        CustomerCreateError = 8,

        [Description("Customer edited successfully.")]
        CustomerEditSuccess = 9,

        [Description("Unable to edit customer.")]
        CustomerEditError = 10,

        [Description("Customer deleted successfully.")]
        CustomerDeleteSuccess = 11,

        [Description("Unable to delete customer.")]
        CustomerDeleteError = 12
    }

    #endregion

    #endregion
}
