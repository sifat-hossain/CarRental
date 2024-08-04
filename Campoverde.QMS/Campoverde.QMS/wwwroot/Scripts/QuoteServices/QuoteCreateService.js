
    $('#firstName').on('input', updateFullName);
    $('#lastName').on('input', updateFullName);

    function updateFullName() {
        var firstName = $('#firstName').val();
        var lastName = $('#lastName').val();
        $('#receiver').text(firstName + ' ' + lastName);
    }

    $('#email').on('input', function () {
        var email = $('#email').val();
        $('#qEmail').text(email);
    });

    $('#phone').on('input', function () {
        var phone = $('#phone').val();
        $('#qPhone').text(phone);
    });

    $('#vehicleType').on('input', function () {
        var vehicleType = $('#vehicleType option:selected').text();
        $('#qVehicleType').text(vehicleType);
    });

    $('#startDate').change(function () {
        var startDate = $('#startDate').val();
        $('#qFromDate').text(startDate);       
    });

    $('#endDate').change(function () {
        var endDate = $('#endDate').val();
        $('#qToDate').text(endDate);
    });
    $('#vehicle, #startDate, #endDate').change(function () {
        var vehicleModelId = $('#vehicle').val();
        var startDate = new Date($('#startDate').val());
        var endDate = new Date($('#endDate').val());

        if (vehicleModelId && startDate && endDate) {
            calculatePrice();
            GetVehicleModel();
        }
    });

    function calculatePrice() {

        var vehicleModelId = $('#vehicle').val();
        var startDate = new Date($('#startDate').val());
        var endDate = new Date($('#endDate').val());

        $.ajax({
            url: '/Quote/GetPrice',
            type: 'GET',
            data: { vehicleModelId: vehicleModelId },
            success: function (response) {
                debugger;
                if (response !== null) {
                    var price = response;
                    var hoursDifference = Math.abs(endDate-startDate) / 36e5; // Convert milliseconds to hours
                    var totalPrice = price * hoursDifference;
                    if (!isNaN(totalPrice)) {
                        $('#price').text(totalPrice.toFixed(2));
                        $('#quotePrice').val(totalPrice.toFixed(2));
                    }
                } else {
                    $('#price').text('');
                }
            },
            error: function () {
                $('#price').text('');
            }
        });
    }
    function GetVehicleModel() {

        var vehicleModelId = $('#vehicle').val();

        $.ajax({
            url: '/Quote/GetModel',
            type: 'GET',
            data: { vehicleModelId: vehicleModelId },
            success: function (response) {

                if (response !== null) {
                    var vehicleModel = response;

                    $('#qVehicle').text(vehicleModel);
                } else {
                    $('#qVehicle').text('');
                }
            },
            error: function () {
                $('#qVehicle').text('Error occurred');
            }
        });
    }