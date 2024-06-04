let currentStep = 0;

function showStep(step) {
    let steps = document.querySelectorAll('.step');
    let progressbarItems = document.querySelectorAll('.progressbar div');
    steps.forEach((stepElement, index) => {
        stepElement.classList.remove('active');
        if (index === step) {
            stepElement.classList.add('active');
        }
    });
    progressbarItems.forEach((item, index) => {
        item.classList.remove('active');
        if (index <= step) {
            item.classList.add('active');
        }
    });
}

function nextStep() {
    currentStep++;
    if (currentStep >= document.querySelectorAll('.step').length) {
        currentStep = document.querySelectorAll('.step').length - 1;
    }
    showStep(currentStep);
    if (currentStep === 2) {
        updateSummary();
    }
}

function prevStep() {
    currentStep--;
    if (currentStep < 0) {
        currentStep = 0;
    }
    showStep(currentStep);
}

function goToStep(step) {
    if (step <= currentStep) {
        currentStep = step;
        showStep(currentStep);
    }
}

function searchVehicles() {
    let startDate = $('#startDate').val();
    let endDate = $('#endDate').val();

    $.ajax({
        url: '/Vehicle/GetAvailableVehicles',
        type: 'POST',
        data: { startDate: startDate, endDate: endDate },
        success: function (data) {

            let vehicleList = $('#availableVehicles');
            vehicleList.empty();
            if (data.length > 0) {
                data.forEach(vehicle => {
                    vehicleList.append(`
                          <div class="row vehicle-item mb-4 p-3 border rounded">
                            <div class="col-md-4">
                            <img src="${vehicle.photoUrl}" alt="Vehicle Image" class="vehicle-image"/>
                            </div>
                            <div class="col-md-2">
                            <h3>${vehicle.model}</h3>
                            <ul class="list-unstyled">
                            <li>Transmission: ${vehicle.vehicleType}</li>
                            <li>Size: ${vehicle.vehicleSize}</li>                           
                            </ul>
                            </div>
                             <div class="col-md-6">
        <div class="premium p-1 bg-light rounded d-flex flex-column">
            <div>
                <span class="text-warning fw-bold">Premium</span>
                <ul class="list-unstyled mt-2 mb-2">
                    <li><i class="bi bi-check-circle-fill"></i> Full cover without excess</li>
                    <li><i class="bi bi-check-circle-fill"></i> No deposit required</li>
                    <li><i class="bi bi-check-circle-fill"></i> Full-Full</li>
                    <li><i class="bi bi-check-circle-fill"></i> Express check-in</li>
                    <li><i class="bi bi-check-circle-fill"></i> Additional driver</li>
                </ul>
            </div>
            <button type="button" class="discount align-items-center rounded-pill p-2 text-center" onclick="selectVehicle('${vehicle.id}','${vehicle.model}', '${vehicle.vehicleType}', '${vehicle.vehicleSize}', '${vehicle.price}', '${vehicle.photoUrl}')">

                <h3>Price: €${vehicle.price} </h3>
            </button>
        </div>
    </div>
                            </div>
                            </div>
                            </div>                            
                            </div>
                        `);
                });
            } else {
                vehicleList.append('<div>No vehicles available for the selected dates.</div>');
            }
            nextStep();
        }
    });
}
function selectVehicle(vehicleId, model, transmission, size, price, imageUrl) {
    selectedVehicle = { model, transmission, size, price, imageUrl };
    document.getElementById('vehicleId').value = vehicleId;
    document.getElementById('price').value = price;
    nextStep(); // Navigate to step 3
}
function updateSummary() {
    $('#pickupLocation').text($('#picOffice').val());
    $('#dropLocation').text($('#dropOffice').val());
    $('#fromDate').text($('#startDate').val());
    $('#toDate').text($('#endDate').val());
    $('#vehicleImage').attr('src', selectedVehicle.imageUrl);
    $('#vehicleModel').text(selectedVehicle.model);
    $('#vehicleTransmission').text(selectedVehicle.transmission);
    $('#vehicleSize').text(selectedVehicle.size);
    $('#vehiclePrice').text(selectedVehicle.price);
}
document.getElementById('multiStepForm').addEventListener('submit', function (e) {
    e.preventDefault();
    alert('Form submitted successfully!');
});