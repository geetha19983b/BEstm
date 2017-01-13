app.filter('sumOfValue', function () {
    return function (data, key, PU, PuVal) {
        if (angular.isUndefined(data) || angular.isUndefined(key))
            return 0;
        var sum = 0;
        angular.forEach(data, function (value) {
            if (value[PU].indexOf(PuVal) !== -1) {
                //debugger
                //sum = sum + (value[key]);
                if (key == 'PQAM1' || key == 'CQMCOBE' || key == 'CQDHBE' || key == 'NQMCOBE' || key == 'NQDHBE') {
                    if (value['BVFR'] == "1")
                        sum = sum + (value[key]);
                }
                else {
                    sum = sum + (value[key]);
                }

            }
        });
        return sum;
    }
})
.filter('ifEmpty', function () {
    return function (input, defaultValue) {
        if (angular.isUndefined(input) || input === null || input === '') {
            return defaultValue;
        }

        return input;
    }
});
//    .filter('startFrom', function () {
//    return function (input, start) {
//        if (!input || !input.length) { return; }
//        start = +start; //parse to int
//        return input.slice(start);
//    }
//});
