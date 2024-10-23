// On Page Load
$(document).ready(function () {

    // Category datatable
    $("#country-table").DataTable({
        "paging": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "lengthMenu": [5, 10, 25, 50, 100],
        "language": {
            "emptyTable": "No records available"
        }
    });

    // Delete country
    $('#country-delete-button').on('click', function (event) {
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
                $('#country-delete-form').submit();
            }
        })
    });
});