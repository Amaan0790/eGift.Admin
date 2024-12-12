// On Page Load
$(document).ready(function () {

    // Order datatable
    var table = $("#order-table").DataTable({
        "paging": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "lengthMenu": [5, 10, 25, 50, 100],
        "language": {
            "emptyTable": "No records available"
        }
    });

    // To update total in table bifurcation
    function updateTotals() {
        let grandTotal = 0;

        // Calculate Grand Total (All Rows)
        table.rows().nodes().each(function (row) {
            let price = parseFloat($(row).find('td:eq(5)').text()) || 0;
            grandTotal += price;
        });

        // Update footer
        $('#grandtotal').html(grandTotal.toFixed(2));
    }

    // On table draw
    table.on('draw', function () {
        updateTotals();
    });

    // first time table load
    updateTotals();
});