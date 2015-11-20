var app = angular.module('CarFinderApp');

app.filter('offset', function () {
    return function (input, start) {
        if (input == null) {
            return null;
        }
        else {
            start = parseInt(start, 10);
            return input.slice(start);
        }
    };
});