// On Page Load
$(document).ready(function () {
    // Employee datatable
    $("#employee-table").DataTable({
        "paging": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "lengthMenu": [5, 10, 25, 50, 100],
        "language": {
            "emptyTable": "No records available"
        }
    });

    $("#DateOfBirth").change(function () {
        var dob = new Date($(this).val());
        if (!isNaN(dob.getTime())) {
            var today = new Date();
            var age = today.getFullYear() - dob.getFullYear();
            var monthDiff = today.getMonth() - dob.getMonth();
            // Adjust if the birthday hasn't happened yet this year
            if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < dob.getDate())) {
                age--;
            }
            $("#Age").val(age);
        }
    });

    // When the file input changes, display the selected image
    $('#profile-image').on('change', function () {
        var input = this;

        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#profile-image-preview').attr('src', e.target.result); // Set the image preview
            }

            reader.readAsDataURL(input.files[0]); // Read the file as a data URL
            $('#IsClear').val(false); // Set IsClear flag
        }
    });

    // When the "Clear" button is clicked, reset the image preview and clear the file input
    $('#clear-btn').on('click', function () {
        $('#profile-image-preview').attr('src', '/images/default/user_default_image.png'); // Reset to default image
        $('#profile-image').val(''); // Clear the file input
        $('#IsClear').val(true); // Set IsClear flag

    });

    // Delete employee record using sweetalert
    $("#employee-delete").on('click', function () {
        let id = $(this).attr('data-id')
        //v2.1.2
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
                let input = {
                    id: parseInt(id),
                    loginUserId: parseInt(id)
                }
                $.ajax({
                    url: '/Employee/Delete',
                    type: 'POST',
                    data: input,
                    dataType: 'json',
                    success: function (data) {
                        if (data) {
                            window.location.href = '/Employee/Index?deleteSuccess=true';
                        } else {
                            toastr.error("Unable to delete employee.", 'Error!');
                        }
                    },
                    error: function (xhr, status, error) {
                        toastr.error(error, 'Error!');
                    }
                });
            }
        });
    });
});