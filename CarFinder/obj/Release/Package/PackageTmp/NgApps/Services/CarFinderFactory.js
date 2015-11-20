var app = angular.module('CarFinderApp');

app.factory('CarFinderFactory', ['$http', function ($http) {
    var factory = {};

    factory.getYears = function () {
        return $http.get('/api/years').then(function (response) { return response.data; });
    };

    factory.getMakes = function (selectedBeginYear, selectedEndYear) {
        var options = { params: { year1: selectedBeginYear, year2: selectedEndYear } };
        return $http.get('/api/make', options).then(function (response) { return response.data; });
    };

    factory.getModels = function (selectedBeginYear, selectedEndYear, selectedMake) {
        var options = { params: { year1: selectedBeginYear, year2: selectedEndYear, make: selectedMake } };
        return $http.get('/api/model', options).then(function (response) { return response.data; });
    };

    factory.getTrims = function (selectedBeginYear, selectedEndYear, selectedMake, selectedModel) {
        var options = { params: { year1: selectedBeginYear, year2: selectedEndYear, make: selectedMake, model: selectedModel } };
        return $http.get('/api/trim', options).then(function (response) { return response.data; });
    };


    factory.getCars = function (selectedBeginYear, selectedEndYear, selectedMake, selectedModel, selectedTrim) {
        if (selectedTrim == null) {
            var options = { params: { year1: selectedBeginYear, year2: selectedEndYear, make: selectedMake, model: selectedModel } };
        } else {
            var options = { params: { year1: selectedBeginYear, year2: selectedEndYear, make: selectedMake, model: selectedModel, trim: selectedTrim } };
        }
        return retval = $http.get('/api/car', options).then(function (response) { return response.data; });
    };

    return factory;
}]);