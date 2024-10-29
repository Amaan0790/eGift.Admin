// On Page Load
$(document).ready(function () {

    // Variables
    var _countryId = $('#CountryId').val();
    var _stateId = $('#StateId').attr('data-stateid');

    // Call Bind State By Country Id
    BindStateByCountryId(_countryId, _stateId);

    // State datatable
    $("#city-table").DataTable({
        "paging": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "lengthMenu": [5, 10, 25, 50, 100],
        "language": {
            "emptyTable": "No records available"
        }
    });

    // State List On Country Change
    $('#CountryId').change(function () {
        var countryId = $(this).val();

        // Call Bind State By Country Id
        BindStateByCountryId(countryId, 0);
    });

    // Delete city
    $('#city-delete-button').on('click', function (event) {
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
                $('#city-delete-form').submit();
            }
        });
    });
});

// Bind State By Country Id
function BindStateByCountryId(countryId, stateId) {
    $.ajax({
        url: '/City/GetStatesByCountry',
        type: 'GET',
        data: { countryId: countryId },
        success: function (states) {
            var stateDropdown = $('#StateId');
            stateDropdown.empty(); // Clear existing options
            stateDropdown.append('<option value="">Select</option>');

            $.each(states, function (index, state) {
                stateDropdown.append('<option value="' + state.id + '">' + state.stateName + '</option>');
            });

            // Set State
            if (stateId > 0) {
                stateDropdown.val(stateId);
            }
        },
        error: function (error) {
            console.error("Error loading states:", error);
        }
    });
}
