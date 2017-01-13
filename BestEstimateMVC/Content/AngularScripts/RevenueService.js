app.service("RevService", function ($http) {
    var strUrl = "";
    
    this.getAllRevDetails = function () {
        if (rootDir != "/") {
            strUrl = rootDir + 'Estimate/GetRevenueData';
        }
        else {
            strUrl = '/Estimate/GetRevenueData';
        }
        return $http.get(strUrl);
    };
    this.getQtrDetails = function () {
        if (rootDir != "/") {
            strUrl = rootDir + 'Estimate/GetQtrDetails';
        }
        else {
            strUrl = '/Estimate/GetQtrDetails';
        }
        return $http.get(strUrl);
    };
    this.updateRevDetails = function (revdetails) {
        if (rootDir != "/") {
            strUrl = rootDir + 'Estimate/UpdateRevenue';
        }
        else {
            strUrl = '/Estimate/UpdateRevenue';
        }

        var response = $http({
            method: "post",
            url: strUrl,
            data: JSON.stringify(revdetails),
            dataType: "json"
        });
        return response;

    }
});