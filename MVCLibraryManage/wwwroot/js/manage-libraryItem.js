$(document).ready(function () {
    // Initialize Bootstrap tooltips
    $('[data-toggle="tooltip"]').tooltip();

    // Format dates in table
    function formatDate(dateString) {
        const options = { year: 'numeric', month: 'short', day: 'numeric' };
        return new Date(dateString).toLocaleDateString(undefined, options);
    }

    document.querySelector("div.button__add").addEventListener("click", function () {
        var modal = document.querySelector(".modal-view-add");
        modal.classList.add("open");

    });
    document.querySelector(".close").addEventListener("click", function () {
        var modal = document.querySelector(".modal-view-add");
        modal.classList.remove("open");
    });

    document.getElementById('createItemType').addEventListener('change', function () {
        const itemType = this.value;

        document.getElementById('bookAddFields').style.display = 'none';
        document.getElementById('dvdAddFields').style.display = 'none';

        if (itemType === 'Book') {
            document.getElementById('bookAddFields').style.display = 'block';
        } else if (itemType === 'DVD') {
            document.getElementById('dvdAddFields').style.display = 'block';
        }
    });

    $(document).ready(function () {
        $(".fa-eye").click(function () {
            var id = $(this).data("id");  // Get the ID from the data-id attribute
            var modal = document.querySelector(".modal-view-read");  // Select the modal
            modal.classList.add("open");  // Open the modal by adding the 'open' class

            // Send AJAX request to get item details
            $.ajax({
                url: '/Staff/GetItem',  // URL to get item details
                data: { id: id },  // Pass the item ID
                type: 'GET',
                success: function (data) {
                    console.log("Received data:", data);  // Check the returned data

                    if (data.item.error) {
                        alert(data.item.error);  // Show error message if any
                        return;
                    }

                    if (data) {
                        $('#viewImage').attr('src', data.item.imagePath); 
                        $('#viewImage').attr('alt', data.item.title || 'Item Image');
                        $('#viewTitle').text(data.item.title);
                        $('#viewAuthor').text(data.item.author);
                        const date = new Date(data.item.publicationDate);
                        const formattedDate = date.toISOString().split('T')[0];
                        $('#viewPublicationDate').text(formattedDate);
                        $('#viewItemType').text(data.type);  
                            if (data.type === 'Book') {
                                $('#dvdFields').hide();
                                $('#bookFields').show();
                                $('#viewNumberOfPages').text(data.item.numberOfPage);
                            } else if (data.type === 'DVD') {
                                $('#bookFields').hide();
                                $('#dvdFields').show();
                                $('#viewRuntime').text(data.item.runtime);
                            } 
                        } else {
                            alert('Item not found');
                        }
                },
                error: function (xhr, status, error) {
                    console.error("AJAX Error:", status, error);  // Log error if any
                    alert('Failed to fetch item details');
                }
            });
        });
    });

    document.getElementById('updateImage').addEventListener('change', function (event) {
        const fileInput = event.target;
        const currentImage = document.getElementById('currentImage');

        if (fileInput.files && fileInput.files[0]) {
            const reader = new FileReader();

            reader.onload = function (e) {
                currentImage.src = e.target.result;
            };

            reader.readAsDataURL(fileInput.files[0]);
        }
    });

    // Edit Modal Handler
    $(".fa-pencil-alt").click(function () {
        const id = $(this).data("id");
        var modal = document.querySelector(".modal-view-edit");  // Select the modal
        modal.classList.add("open");  // Open the modal by adding the 'open' class

        $.ajax({
            url: '/Staff/GetItem',
            type: 'GET',
            data: { id: id },
            dataType: 'json',
            success: function (data) {
                console.log("Received data update:", data);
                if (data) {
                    $('#updateLibraryItemId').val(data.item.id || data.item.Id);
                    $('#updateTitle').val(data.item.title || data.item.Title);
                    $('#updateAuthor').val(data.item.author || data.item.Author);
                    const date = new Date(data.item.publicationDate);
                    const formattedDate = date.toISOString().split('T')[0];
                    $('#updatePublicationDate').val(formattedDate);
                    $('#updateItemType').val(data.type);
                    if (data.type === 'Book') {
                        console.log('Displaying book fields');
                        $('#bookUpdateFields').addClass('show');
                        $('#dvdUpdateFields').removeClass('show');
                        $('#updateNumberOfPages').val(data.item.numberOfPage);
                    } else if (data.type === 'DVD') {
                        console.log('Displaying DVD fields');
                        $('#bookUpdateFields').removeClass('show');
                        $('#dvdUpdateFields').addClass('show');
                        $('#updateRuntime').val(data.item.runtime);
                    }
                    const imageUrl = data.item.imagePath || '/img/default-placeholder.png';
                    $('#currentImage').attr('src', imageUrl);
                } else {
                    alert('No data received from server');
                }
            },
            error: function (xhr, status, error) {
                console.error('AJAX Error:', error);
                console.log('Status:', status);
                console.log('Response:', xhr.responseText);
                alert('Error fetching item details. Please try again.');
            }
        });
    });
    $(function () {
        (function ($) {
            $.fn.confirm = function (options) {
                var settings = $.extend({}, $.fn.confirm.defaults, options);
                return this.each(function () {
                    var element = this;

                    // Set modal content
                    $('.modal-title', this).html(settings.title);
                    $('.message', this).html(settings.message);
                    $('.confirm', this).html(settings.confirm);
                    $('.dismiss', this).html(settings.dismiss);

                    // Set up confirm button
                    $(this).on('click', '.confirm', function () {
                        $(element).data('confirm', true);
                    });

                    // Handle modal hide event
                    $(this).on('hide.bs.modal', function (event) {
                        if ($(this).data('confirm')) {
                            $(this).trigger('confirm', event);
                            $(this).removeData('confirm');
                        } else {
                            $(this).trigger('dismiss', event);
                        }
                        $(this).off('confirm dismiss'); // Unbind events
                    });
                });
            };

            // Default settings for the modal
            $.fn.confirm.defaults = {
                title: 'Modal title',
                message: 'One fine body&hellip;',
                confirm: 'OK',
                dismiss: 'Cancel'
            };
        })(jQuery);
    });

    $(".fa-ban").click(function () {
        var id = $(this).data('id');
        var modal = document.querySelector(".modal-delete");  // Select the modal
        modal.classList.add("open");  // Open the modal by adding the 'open' class
        $.ajax({
            url: '/Staff/GetItem',
            type: 'GET',
            data: { id: id },
            dataType: 'json',
            success: function (data) {
                if (data) {
                    $('#deleteLibraryItemId').val(data.item.id); // Store ID in hidden field
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                alert('Error loading item: ' + errorThrown);
            }
        });

        // Show confirmation modal
        $('#confirm').confirm({
            title: 'Xác nhận xóa',
            message: 'Bạn có chắc chắn muốn xóa sản phẩm này không?',
            confirm: 'Xác nhận',
            dismiss: 'Hủy bỏ'
        });
    });

    // Form Validation
    function validateForm(formId) {
        const form = $(formId);
        const title = form.find('[name="Title"]').val();
        const author = form.find('[name="Author"]').val();
        const publicationDate = form.find('[name="PublicationDate"]').val();

        let isValid = true;
        let errorMessage = '';

        if (!title) {
            errorMessage += 'Title is required\n';
            isValid = false;
        }
        if (!author) {
            errorMessage += 'Author is required\n';
            isValid = false;
        }
        if (!publicationDate) {
            errorMessage += 'Publication Date is required\n';
            isValid = false;
        }

        if (!isValid) {
            alert(errorMessage);
        }
        return isValid;
    }

    // Add Form Submit Handler
    $('form[action*="Create"]').submit(function (e) {
        if (!validateForm(this)) {
            e.preventDefault();
        }
    });

    // Update Form Submit Handler
    $('form[action*="Update"]').submit(function (e) {
        if (!validateForm(this)) {
            e.preventDefault();
        }
    });

    // Sort Handler
    $('#sort-select').change(function () {
        $(this).closest('form').submit();
    });

    // Search Handler with Debounce
    let searchTimeout;
    $('.search-user-box-input').on('input', function () {
        clearTimeout(searchTimeout);
        searchTimeout = setTimeout(() => {
            $(this).closest('form').submit();
        }, 500);
    });

    // Modal Animation
    $('.modal').on('show.bs.modal', function () {
        $(this).find('.modal-content')
            .css({
                transform: 'scale(0.7)',
                opacity: 0
            })
            .animate({
                transform: 'scale(1)',
                opacity: 1
            }, 200);
    });

    // Global AJAX Error Handler
    $(document).ajaxError(function (event, jqXHR, settings, thrownError) {
        console.error('Ajax error:', {
            event: event,
            jqXHR: jqXHR,
            settings: settings,
            thrownError: thrownError
        });
    });

    // Add loading indicator
    $(document).on('ajaxStart', function () {
        $('body').addClass('loading');
    }).on('ajaxStop', function () {
        $('body').removeClass('loading');
    });

    $(document).ready(function () {
        // Close modals by removing the 'open' class
        $(".close, .dismiss, .btn.btn-secondary").click(function () {
            $(this).closest(".modal-view-read, .modal-view-edit, .modal-delete, .modal-view-add").removeClass("open");
        });
    });
});