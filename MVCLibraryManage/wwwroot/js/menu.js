$(document).ready(function () {
    // Function to hide alerts
    function hideAlert(alertElement) {
        $(alertElement).fadeOut(500, function () {
            $(this).removeClass('show').addClass('fade').css('display', 'none');
        });
    }

    // Show success or error alert with a timeout
    function showAlert(alertSelector) {
        var alertElement = $(alertSelector);

        if (!alertElement.length) return;

        // Show the alert
        alertElement.stop(true, true).fadeIn(500).addClass('show').removeClass('fade');

        // Automatically hide the alert after 3 seconds
        setTimeout(function () {
            hideAlert(alertElement);
        }, 3000);
    }

    // Handle close button click to manually hide the alert
    $('.alert .btn-close').on('click', function () {
        var alertElement = $(this).closest('.alert');
        if (alertElement.length) {
            hideAlert(alertElement);
        }
    });

    // AJAX call on add button click
    $('.add-btn').on('click', function () {
        var id = $(this).data('id');
        $.ajax({
            type: 'POST',
            data: { id: id },
            url: '/Borrower/AddToCart',
            success: function (result) {
                if (result.redirectToPage === false) {
                    if (result.success === true) {
                        showAlert('.alert-success'); // Show success alert
                    } else {
                        var errorAlert = $('.alert-danger span');
                        errorAlert.text(result.message || 'An error occurred.');
                        showAlert('.alert-danger'); // Show error alert
                    }
                } else {
                    var url = "/Login/LoginRegister" + "?";
                    url += 'Message=* Bạn phải đăng nhập/ đăng ký';
                    window.location.href = url;
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                console.log('Error loading: ' + errorThrown);
                var errorAlert = $('.alert-danger span');
                errorAlert.text('An unexpected error occurred.');
                showAlert('.alert-danger'); // Show error alert
            }
        });
    });
    $('#sort-select').change(function () {
        $(this).closest('form').submit();
    });
});
