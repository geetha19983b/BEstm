var app = angular.module("BEApp", []);

//app.config(['$httpProvider', function ($httpProvider) {
//    //initialize get if not there
//    if (!$httpProvider.defaults.headers.get) {
//        $httpProvider.defaults.headers.get = {};
//    }

//    // Answer edited to include suggestions from comments
//    // because previous version of code introduced browser-related errors

       
//    // extra
//    $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
//    $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';
//    $httpProvider.defaults.headers.common['Pragma'] = 'no-cache';
//    $httpProvider.defaults.headers.get.Pragma = 'no-cache';
//}]);

//app.config(['$httpProvider', function ($httpProvider) {
//    $httpProvider.interceptors.push('noCacheInterceptor');
//}]).factory('noCacheInterceptor', function () {
//    return {
//        request: function (config) {
//            console.log(config.method);
//            console.log(config.url);
//            if (config.method == 'GET') {
//                var separator = config.url.indexOf('?') === -1 ? '?' : '&';
//                config.url = config.url + separator + 'noCache=' + new Date().getTime();
//            }
//            console.log(config.method);
//            console.log(config.url);
//            return config;
//        }
//    };
//});