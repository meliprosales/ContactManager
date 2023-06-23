// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    $(document).on('click', '.delete-btn', function () {
        var itemId = $(this).data('item-id');
        showDeleteConfirmation(itemId);
    });

    function showDeleteConfirmation(itemId) {
        if (confirm('Are you sure you want to delete this item?')) {
            deleteItem(itemId);
        }
    }

    function deleteItem(itemId) {
        $.ajax({
            url: '/Contacts/Delete',
            type: 'POST',
            data: { id: itemId, __RequestVerificationToken: $("input[name=__RequestVerificationToken]").val() },
            success: function () {
                // Optional: Remove the deleted item from the DOM
                $('[data-item-id="' + itemId + '"]').closest('tr').remove();
            },
            error: function () {
                alert('An error occurred while deleting the item.');
            }
        });
    }

    $(document).on('click', '#modal-link', function () {
        $('#exampleModal').modal('show');
    });

    $(document).on('click', '.details-btn', function () {
        var itemId = $(this).data('item-id');
        loadItemDetails(itemId);
        openModal();
    });

    $(document).on('click', '.close', function () {
        closeModal();
    });

    function loadItemDetails(itemId) {
        $.ajax({
            url: '/Contacts/Details/' + itemId,
            type: 'GET',
            success: function (data) {
                $('#modal-content-details').html(data);
            },
            error: function () {
                alert('An error occurred while loading item details.');
            }
        });
    }

    function openModal() {
        $('#modal-container').modal('show');
    }

    function closeModal() {
        $('#modal-container').modal('hide');
    }

    var typingTimer;
    var doneTypingInterval = 100; // milliseconds

    $('#searchInput').keyup(function () {
        clearTimeout(typingTimer);
        var searchTerm = $(this).val();
        typingTimer = setTimeout(function () {
            searchItems(searchTerm);
        }, doneTypingInterval);
    });

    function searchItems(searchTerm) {
        $.ajax({
            url: '/Contacts/Search',
            type: 'GET',
            data: { searchTerm: searchTerm },
            success: function (data) {
                $('#searchResults').html(data);
            },
            error: function () {
                alert('An error occurred while performing the search.');
            }
        });
    }

    searchItems("");

    $.ajax({
        type: "GET",
        url: "/Configuration/GetConfigurationValue"
  
    }).done(
        function (parameterValue) {
            (async () => {
                let script = document.createElement("script");
                let bingKey = parameterValue.parameter;
                script.setAttribute("src", `https://www.bing.com/api/maps/mapcontrol?callback=GetMap&key=${bingKey}`);
                document.body.appendChild(script);
            })();
        });

 

});



