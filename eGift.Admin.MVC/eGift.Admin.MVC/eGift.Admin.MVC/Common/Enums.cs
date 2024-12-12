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
        Customer = 2,

        [Description("Admin")]
        Admin = 3
    }

    public enum Size
    {
        [Description("Extra Small")]
        XS = 1,

        [Description("Small")]
        S = 2,

        [Description("Medium")]
        M = 3,

        [Description("Large")]
        L = 4,

        [Description("Extra Large")]
        XL = 5,

        [Description("2X Large")]
        XXL = 6,

        [Description("3X Large")]
        XXXL = 7,

        [Description("4X Large")]
        XXXXL = 8,

        [Description("5X Large")]
        XXXXXL = 9,

        [Description("One Size")]
        OneSize = 10
    }

    public enum Status
    {
        [Description("New")]
        New = 1,

        [Description("Dispatched")]
        Dispatched = 2,

        [Description("Shipped")]
        Shipped = 3,

        [Description("Delivered")]
        Delivered = 4,

        [Description("Cancelled")]
        Cancelled = 5,

        [Description("Completed")]
        Completed = 6,
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
        CustomerDeleteError = 12,

        // Login
        [Description("Login successfully.")]
        LoginSuccess = 13,

        [Description("Invalid credentials.")]
        LoginInvalid = 14,

        [Description("This user is inctive.")]
        LoginInactive = 15,

        [Description("An error occured while login.")]
        LoginError = 16,

        // Logout
        [Description("Logout successfully.")]
        LogoutSuccess = 17,

        // Category
        [Description("Category created successfully.")]
        CategoryCreateSuccess = 18,

        [Description("Unable to create category.")]
        CategoryCreateError = 19,

        [Description("Category edited successfully.")]
        CategoryEditSuccess = 20,

        [Description("Unable to edit category.")]
        CategoryEditError = 21,

        [Description("Category deleted successfully.")]
        CategoryDeleteSuccess = 22,

        [Description("Unable to delete category.")]
        CategoryDeleteError = 23,

        // Sub Category
        [Description("Sub category created successfully.")]
        SubCategoryCreateSuccess = 24,

        [Description("Unable to create sub category.")]
        SubCategoryCreateError = 25,

        [Description("Sub category edited successfully.")]
        SubCategoryEditSuccess = 26,

        [Description("Unable to edit sub category.")]
        SubCategoryEditError = 27,

        [Description("Sub category deleted successfully.")]
        SubCategoryDeleteSuccess = 28,

        [Description("Unable to delete sub category.")]
        SubCategoryDeleteError = 29,

        // Country
        [Description("Country created successfully.")]
        CountryCreateSuccess = 30,

        [Description("Unable to create country.")]
        CountryCreateError = 31,

        [Description("Country edited successfully.")]
        CountryEditSuccess = 32,

        [Description("Unable to edit country.")]
        CountryEditError = 33,

        [Description("Country deleted successfully.")]
        CountryDeleteSuccess = 34,

        [Description("Unable to delete country.")]
        CountryDeleteError = 35,

        // State
        [Description("State created successfully.")]
        StateCreateSuccess = 36,

        [Description("Unable to create state.")]
        StateCreateError = 37,

        [Description("State edited successfully.")]
        StateEditSuccess = 38,

        [Description("Unable to edit state.")]
        StateEditError = 39,

        [Description("State deleted successfully.")]
        StateDeleteSuccess = 40,

        [Description("Unable to delete state.")]
        StateDeleteError = 41,

        // City
        [Description("City created successfully.")]
        CityCreateSuccess = 42,

        [Description("Unable to create city.")]
        CityCreateError = 43,

        [Description("City edited successfully.")]
        CityEditSuccess = 44,

        [Description("Unable to edit city.")]
        CityEditError = 45,

        [Description("City deleted successfully.")]
        CityDeleteSuccess = 46,

        [Description("Unable to delete city.")]
        CityDeleteError = 47,

        // Address
        [Description("Address created successfully.")]
        AddressCreateSuccess = 48,

        [Description("Unable to create address.")]
        AddressCreateError = 49,

        [Description("Address edited successfully.")]
        AddressEditSuccess = 50,

        [Description("Unable to edit address.")]
        AddressEditError = 51,

        [Description("Address deleted successfully.")]
        AddressDeleteSuccess = 52,

        [Description("Unable to delete address.")]
        AddressDeleteError = 53,

        // Product
        [Description("Product created successfully.")]
        ProductCreateSuccess = 54,

        [Description("Unable to create product.")]
        ProductCreateError = 55,

        [Description("Product edited successfully.")]
        ProductEditSuccess = 56,

        [Description("Unable to edit product.")]
        ProductEditError = 57,

        [Description("Product deleted successfully.")]
        ProductDeleteSuccess = 58,

        [Description("Unable to delete product.")]
        ProductDeleteError = 59,

        // Order
        [Description("Order updated successfully.")]
        OrderUpdateSuccess = 60,

        [Description("Unable to update order.")]
        OrderUpdateError = 61,
    }

    #endregion

    #endregion
}
