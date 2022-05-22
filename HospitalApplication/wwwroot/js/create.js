var specialtiesUrl = 'GetSpecialties';
var insuranceProvidersUrl = 'GetInsuranceProviders';


var specialties = [];
$.get(specialtiesUrl, function (response) {
    specialties = response;

    var specialtiesDDL = $("#specialities");

    response.forEach(function (s) {
        var html = '<option value=' + s.id + '>' + s.name + '</option>';
        specialtiesDDL.append($(html));
    });

});


var insurances = [];
$.get(insuranceProvidersUrl, function (response) {
    insurances = response;

    var insuranceDDL = $("#insuranceProviders");

    response.forEach(function (i) {
        var html = '<option value=' + i.id + '>' + i.name + '</option>';
        insuranceDDL.append($(html));
    });

});

