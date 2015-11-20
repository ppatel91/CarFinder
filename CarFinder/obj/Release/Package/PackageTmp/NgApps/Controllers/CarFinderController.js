var app = angular.module('CarFinderApp');


app.controller('CarFinderController', ['$scope', 'CarFinderFactory', function ($scope, CarFinderFactory) {

    $scope.years = [];
    $scope.makes = [];
    $scope.models = [];
    $scope.trims = [];
    $scope.cars = [];

    $scope.selectedBeginYear = '';
    $scope.selectedEndYear = '';
    $scope.selectedMake = '';
    $scope.selectedModel = '';
    $scope.selectedTrim = '';
    $scope.isCollapsed = true;

    $scope.init = function () {
        //$('#select_year1').attr("disabled", false)
        //$('#select_year2').attr("disabled", true)
        //$('#select_make').attr("disabled", true)
        //$('#select_model').attr("disabled", true)
        //$('#select_trim').attr("disabled", true)
        $scope.getBeginYears();
    };


    $scope.getBeginYears = function () {
        CarFinderFactory.getYears().then(function (data) {
            $scope.years = data;
        }).then(function () {
            //$('#select_year2').attr("disabled", false)
        });
    };

    $scope.getEndYears = function () {
        //$('#select_make').attr("disabled", true)
        //$('#select_model').attr("disabled", true)
        //$('#select_trim').attr("disabled", true)
    };

    $scope.getMakes = function () {
        CarFinderFactory.getMakes($scope.selectedBeginYear, $scope.selectedEndYear).then(function (data) {
            $scope.makes = data;
        }).then(function () {
            //$('#select_model').attr("disabled", false)
            //$('#select_trim').attr("disabled", true)
        });
    };

    $scope.getModels = function () {
        CarFinderFactory.getModels($scope.selectedBeginYear, $scope.selectedEndYear, $scope.selectedMake).then(function (data) {
            $scope.models = data;
        }).then(function () {
            //   $('#select_trim').attr("disabled", false)
        });
    };

    $scope.getTrims = function () {
        CarFinderFactory.getTrims($scope.selectedBeginYear, $scope.selectedEndYear, $scope.selectedMake, $scope.selectedModel).then(function (data) {
            $scope.trims = data;
        }).then(function () {
            //   $('#select_year2').attr("disabled", false)
        });
    };

    $scope.getCars = function () {
        CarFinderFactory.getCars($scope.selectedBeginYear, $scope.selectedEndYear, $scope.selectedMake, $scope.selectedModel, $scope.selectedTrim).then(function (data) {
            $scope.cars = data;
            $('#myModal').modal('show');
        });
    };

    $scope.hasRecalls = function (numRecalls) {
        if (numRecalls > 0) {
            return true;
        } else {
            return false
        }
    };



    $scope.init();
}]);