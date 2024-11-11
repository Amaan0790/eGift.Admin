// On Page Load
$(document).ready(function () {
    var _categoryId = $('#CategoryId').val();
    var _subCategoryId = $('#SubCategoryId').attr('data-subCategoryId');

    // Call Bind SubCategory By Category
    BindSubCategoryOnCategory(_categoryId, _subCategoryId);

    // Product datatable
    $("#product-table").DataTable({
        "paging": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "lengthMenu": [5, 10, 25, 50, 100],
        "language": {
            "emptyTable": "No records available"
        }
    });

    // SubCategory List On Category Change
    $('#CategoryId').change(function () {
        var categoryId = $(this).val();

        // Call Bind SubCategory By Category
        BindSubCategoryOnCategory(categoryId, 0);
    });

    // When the file input changes, display the selected image
    $('#product-profile-image').on('change', function () {
        var input = this;

        // Call  Display Image On Input Change
        DisplayImageOnInputChange(input, 'product-profile-image-preview');

        // set IsClear field
        $('#IsClear').val(false);
    });

    // When the "Clear" button is clicked, reset the image preview and clear the file input
    $('#product-clear-btn').on('click', function () {
        $('#product-profile-image-preview').attr('src', '/images/default/default-product-image.png'); // Reset to default image
        $('#product-profile-image').val(''); // Clear the file input
        $('#IsClear').val(true);
    });

    // For Picture 1 input change
    $('#picture1').on('change', function () {
        var input = this;

        // Call  Display Image On Input Change
        DisplayImageOnInputChange(input, 'picture1-preview');
    });

    // For picture 1 image clear button
    $('#picture1-btn').on('click', function () {

        // Call Image Clear
        ImageClear('picture1-preview', 'picture1');
    });

    // For Picture 2 input change
    $('#picture2').on('change', function () {
        var input = this;

        // Call  Display Image On Input Change
        DisplayImageOnInputChange(input, 'picture2-preview');
    });

    // For picture 2 image clear button
    $('#picture2-btn').on('click', function () {

        // Call Image Clear
        ImageClear('picture2-preview', 'picture2');
    });

    // For Picture 3 input change
    $('#picture3').on('change', function () {
        var input = this;

        // Call  Display Image On Input Change
        DisplayImageOnInputChange(input, 'picture3-preview');
    });

    // For picture 3 image clear button
    $('#picture3-btn').on('click', function () {

        // Call Image Clear
        ImageClear('picture3-preview', 'picture3');
    });

    // For Picture 4 input change
    $('#picture4').on('change', function () {
        var input = this;

        // Call  Display Image On Input Change
        DisplayImageOnInputChange(input, 'picture4-preview');
    });

    // For picture 4 image clear button
    $('#picture4-btn').on('click', function () {

        // Call Image Clear
        ImageClear('picture4-preview', 'picture4');
    });

    // Delete Product
    $('#btn-product-delete').on('click', function (event) {
        swal({
            title: 'Are you sure?',
            text: 'You will not be able to undo this!',
            icon: 'warning',  // Updated from 'type' to 'icon'
            buttons: {
                cancel: 'No, keep it',
                confirm: 'Yes, delete it!'
            }
        }).then(function (result) {
            if (result) {

                // Submit the form programmatically
                $('#product-delete-form').submit();
            }
        });
    });
});

// Display Image On Input Change
function DisplayImageOnInputChange(input, imagePreviewId) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $(`#${imagePreviewId}`).attr('src', e.target.result); // Set the image preview
        }

        reader.readAsDataURL(input.files[0]); // Read the file as a data URL
    }
}

// For image clear button
function ImageClear(imagePreviewId, imageInputId) {
    $(`#${imagePreviewId}`).attr('src', '/images/default/default-product-image.png'); // Reset to default image
    $(`#${imageInputId}`).val(''); // Clear the file input
}

// Bind SubCategory List On Category Change
function BindSubCategoryOnCategory(categoryId, subCategoryId) {
    $.ajax({
        url: '/Product/GetSubCategoriesByCategory',
        type: 'GET',
        data: { categoryId: categoryId },
        success: function (subCategories) {
            var subCategoryDropdown = $('#SubCategoryId');
            subCategoryDropdown.empty(); // Clear existing options
            subCategoryDropdown.append('<option value="">Select</option>');

            $.each(subCategories, function (index, subCategory) {
                subCategoryDropdown.append('<option value="' + subCategory.id + '">' + subCategory.subCategoryName + '</option>');
            });

            // Set SubCategory
            if (subCategoryId > 0) {
                subCategoryDropdown.val(subCategoryId);
            }
        },
        error: function (error) {
            console.error("Error loading states:", error);
        }
    });
}