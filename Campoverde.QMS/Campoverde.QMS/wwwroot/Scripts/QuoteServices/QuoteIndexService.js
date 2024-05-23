﻿$(document).ready(function () {

    let selectedRowId;

    $('.open-modal').on('click', function () {
        selectedRowId = $(this).closest('tr').data('id');
    });
    $('#saveButtonForStatus').on('click', function () {
        let selectedValue = $('#status').val();

        $.ajax({
            url: '/Quote/ChangeStatus',
            type: 'POST',
            data: {
                quoteId: selectedRowId,
                status: selectedValue
            },
            success: function (response) {
                Swal.fire({
                    title: 'Success!',
                    text: "Successfully status updated",
                    icon: 'success',
                    confirmButtonText: 'OK'
                }).then((result) => {
                    if (result.isConfirmed) {
                        window.location.href = '/Quote/Index';
                    }
                });

            },
            error: function (error) {
                Swal.fire({
                    title: 'Error!',
                    text: 'Failed to update status',
                    icon: 'error',
                    confirmButtonText: 'OK'
                });
            }
        });
    });

    $('#saveButtonForNote').on('click', function () {
        debugger;
        var note = $('#note').val();

        $.ajax({
            url: '/QuoteNote/CreateNote',
            type: 'POST',
            data: {
                quoteId: selectedRowId,
                quoteNote: note
            },
            success: function (response) {
                Swal.fire({
                    title: 'Success!',
                    text: "Successfully create Note",
                    icon: 'success',
                    confirmButtonText: 'OK'
                }).then((result) => {
                    if (result.isConfirmed) {
                        window.location.href = '/Quote/Index';
                    }
                });
            }, error: function (error) {
                // handle error
                Swal.fire({
                    title: 'Error!',
                    text: 'Failed to create note',
                    icon: 'error',
                    confirmButtonText: 'OK'
                });
            }
        });
    });

    $('#quoteNotesTable tbody').empty();

    $('.open-modal').on('click', function () {
        var quoteId = $(this).closest('tr').data('id');
   
    // Make an AJAX call to get the quote notes
    $.ajax({
        url: '/QuoteNote/GetQuoteNotes',
        type: 'GET',
        data: {
            quoteId: quoteId
        },
        
        success: function (response) {
            debugger;
            // Populate the table with the received data
            response.forEach(function (note) {
                $('#quoteNotesTable tbody').append(
                    '<tr><td>' + note.notes + '</td></tr>'
                );
            });
        },
        error: function (error) {
            alert('An error occurred while fetching the notes');
        }
    });
    });
})