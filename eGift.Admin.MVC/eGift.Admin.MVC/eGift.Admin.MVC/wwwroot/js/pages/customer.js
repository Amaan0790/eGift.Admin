// On Page Load
$(document).ready(function () {

    // Customer datatable
    $("#customer-table").DataTable({
        "paging": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "lengthMenu": [5, 10, 25, 50, 100],
        "language": {
            "emptyTable": "No records available"
        }
    });

    // Auto fill age based on date of birth
    $("#customer-date-of-birth").change(function () {
        var dob = new Date($(this).val());
        if (!isNaN(dob.getTime())) {
            var today = new Date();
            var age = today.getFullYear() - dob.getFullYear();
            var monthDiff = today.getMonth() - dob.getMonth();

            // Adjust if the birthday hasn't happened yet this year
            if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < dob.getDate())) {
                age--;
            }
            $("#customer-age").val(age);
        }
    });

    // When the file input changes, display the selected image
    $('#customer-profile-image').on('change', function () {
        var input = this;

        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#customer-profile-image-preview').attr('src', e.target.result); // Set the image preview
            }

            reader.readAsDataURL(input.files[0]); // Read the file as a data URL
            $('#IsClear').val(false); // Set IsClear flag
        }
    });

    // When the "Clear" button is clicked, reset the image preview and clear the file input
    $('#customer-clear-btn').on('click', function () {
        $('#customer-profile-image-preview').attr('src', '/images/default/user_default_image.png'); // Reset to default image
        $('#customer-profile-image').val(''); // Clear the file input
        $('#IsClear').val(true); // Set IsClear flag
    });

    $('#customer-delete-form').submit(function (event) {
        event.preventDefault(); // Prevent the default form submission

        // Get the submit button that was clicked
        let submitButton = $(this).find('input[type="submit"]');

        // Access the data-id and data-login-id attributes
        let id = submitButton.data('id');
        let loginUserId = submitButton.data('login-id');

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
                //// Add hidden fields for the IDs before submitting
                //$('<input>').attr({
                //    type: 'hidden',
                //    name: 'id',
                //    value: id
                //}).appendTo('#customer-delete-form');

                //$('<input>').attr({
                //    type: 'hidden',
                //    name: 'loginUserId',
                //    value: loginUserId
                //}).appendTo('#customer-delete-form');

                // Submit the form programmatically
                $('#customer-delete-form').off('submit').submit();  // Unbind the original submit handler and then submit the form

                //$('#customer-delete').submit();
            }
        })
    })
});