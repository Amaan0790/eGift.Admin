// On Page Load
$(document).ready(function () {
});

function ShowToastrMessages(type, message) {
    switch (type) {
        case 1:
            toastr.success(message, "Success!");
            break;
        case 2:
            toastr.info(message, "Info!");
            break;
        case 3:
            toastr.warning(message, "Warning!");
            break;
        case 4:
            toastr.error(message, "Error!");
            break;
    }
}