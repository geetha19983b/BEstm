app.controller("RevCntrl", function ($scope, RevService) {
   
    GetAllRevDeatils();
    GetQtrDetails();
    
    $scope.sortType = 'MasterCustomerCode'; // set the default sort type
    $scope.sortReverse = false;  // set the default sort order
    $scope.searchCustCode = '';     // set the default search/filter term


    var listOfChangedRows = [];
  
    $scope.onChange = function (rev, key,index) {
        var cqflag = 'N';
        var nqflag = 'N';
        
        if (key == 'CQM1NATIVE') {
            if (isNaN(rev.CQM1NATIVE) || rev.CQM1NATIVE.length == 0)
                rev.CQM1NATIVE = 0;
            rev.CQM1USD = rev.CQM1NATIVE * rev.ER;
            cqflag = 'Y';
        }
        if (key == 'CQM2NATIVE') {
            if (isNaN(rev.CQM2NATIVE) || rev.CQM2NATIVE.length == 0)
                rev.CQM2NATIVE = 0;
            rev.CQM2USD = rev.CQM2NATIVE * rev.ER;
            cqflag = 'Y';
        }
        if (key == 'CQM3NATIVE') {
            if (isNaN(rev.CQM3NATIVE) || rev.CQM3NATIVE.length == 0)
                rev.CQM3NATIVE = 0;
            rev.CQM3USD = rev.CQM3NATIVE * rev.ER;
            cqflag = 'Y';
        }
        if (cqflag = 'Y') {
            rev.CQREVNATIVE = parseFloat(rev.CQM1NATIVE) + parseFloat(rev.CQM2NATIVE) + parseFloat(rev.CQM3NATIVE);
            rev.CQREVUSD = rev.CQM1USD + rev.CQM2USD + rev.CQM3USD;
        }
        if (key == 'NQM1NATIVE') {
            if (isNaN(rev.NQM1NATIVE) || rev.NQM1NATIVE.length == 0)
                rev.NQM1NATIVE = 0;
            rev.NQM1USD = rev.NQM1NATIVE * rev.ER;
            nqflag = 'Y';
        }
        if (key == 'NQM2NATIVE') {
            if (isNaN(rev.NQM2NATIVE) || rev.NQM2NATIVE.length == 0)
                rev.NQM2NATIVE = 0;
            rev.NQM2USD = rev.NQM2NATIVE * rev.ER;
            nqflag = 'Y';
        }
        if (key == 'NQM3NATIVE') {
            if (isNaN(rev.NQM3NATIVE) || rev.NQM3NATIVE.length == 0)
                rev.NQM3NATIVE = 0;
            rev.NQM3USD = rev.NQM3NATIVE * rev.ER;
            nqflag = 'Y';
        }
        if (nqflag = 'Y') {
            rev.NQREVNATIVE = parseFloat(rev.NQM1NATIVE) + parseFloat(rev.NQM2NATIVE) + parseFloat(rev.NQM3NATIVE);
            rev.NQREVUSD = rev.NQM1USD + rev.NQM2USD + rev.NQM3USD;
        }
        //debugger;

        var startPos = ($scope.page - 1) * 50;
        //if ($scope.currentPage == "0")
        //{
        //    index = index;
        //}
        //else
        //{
        //    index = $scope.pageSize * $scope.currentPage + index;
        //}
        if ($scope.page == "1") {
            index = index;
        }
        else {
            index = startPos + index;
        }
        if(listOfChangedRows.indexOf(index)==-1)
        {
            listOfChangedRows.push(index);
        }
        $scope.changedindx = listOfChangedRows;
    }
    $scope.saveEditedRows = function () {
        var updatedItems = [];
        var j = 0;
        for (var i = 0; i < listOfChangedRows.length; i++) {
            j = listOfChangedRows[i];
            updatedItems.push($scope.revdetails[j]);
        }
        listOfChangedRows = [];
        //Now send the updatedItems array to web service
        //$scope.changed = updatedItems;
        var updtrev = RevService.updateRevDetails(updatedItems);
        updtrev.then(function (msg) {
           // GetAllRevDeatils();
           // $scope.$apply();
            alert(msg.data);
           
        }, function () {
            alert('Error in updating record');
        });
    }
    $scope.pageChanged = function () {
        var startPos = ($scope.page - 1) * 50;
        //$scope.displayItems = $scope.totalItems.slice(startPos, startPos + 3);
       // alert(startPos);
        //console.log($scope.page);
    };
    function GetAllRevDeatils() {
        var getRevData = RevService.getAllRevDetails();
        getRevData.then(function (rev) {
            // debugger;
            $scope.revdetails = rev.data.revdata;
            $scope.admtotal = rev.data.admtotal;
            $scope.bpototal = rev.data.bpototal;
            // GetTotals($scope.revdetails);

            // Pagination in controller
            $scope.page = 1;

            //$scope.currentPage = 0;
            //$scope.pageSize = 75;
            //$scope.setCurrentPage = function (currentPage) {
            //    $scope.currentPage = currentPage;
            //}

            //$scope.getNumberAsArray = function (num) {
            //    return new Array(num);
            //};

            //$scope.numberOfPages = function () {
            //    return Math.ceil($scope.revdetails.length / $scope.pageSize);
            //};

            //pagination end code

        }, function () {
            alert('Error in getting records');
        });
    }
    function GetQtrDetails() {
        var qtrData = RevService.getQtrDetails();
        qtrData.then(function (qtr) {
            $scope.qtrData = qtr.data;
        }, function () {
            alert('Error in getting qtr details');
        });
    }
    //function GetTotals(data) {
    //    var pqm1admsum = 0, cqmadmCOBE = 0, cqadmDHBE = 0, nqmadmCOBE = 0, nqadmDHBE = 0, cqm1admActuals = 0, cqm2admActuals = 0, cqm3admActuals = 0;
    //    var cqm1admNative = 0, cqm1admUSD = 0, cqm2admNative = 0, cqm2admUSD = 0, cqm3admNative = 0, cqm3admUSD = 0;
    //    var cqrevadmNATIVE = 0, cqadmREVUSD = 0, cqadmRTBRNATIVE = 0, cqadmRTBRUSDACTUALS = 0, nqm1admNATIVE = 0, nqm1USD = 0, nqm2admNATIVE = 0, nqm2USD = 0, nqm3admNATIVE = 0, nqm3USD = 0;
    //    var nqadmREVNATIVE = 0, nqadmREVUSD = 0, nqadmRTBRNATIVE = 0, nqadmRTBRUSD = 0;

    //    var pqm1bposum = 0, cqmbpoCOBE = 0, cqbpoDHBE = 0, nqmbpoCOBE = 0, nqmbpoDHBE = 0;
    //    var cqm1bpoActuals = 0, cqm2bpoActuals = 0, cqm3bpoActuals = 0, cqm1bpoNative = 0, cqm1bpoUSD = 0, cqm2bpoNative = 0, cqm2bpoUSD = 0, cqm3bpoNative = 0, cqm3bpoUSD = 0, cqrevbpoNATIVE = 0, cqbpoREVUSD = 0, cqbpoRTBRNATIVE = 0, cqbpoRTBRUSDACTUALS = 0;

    //    var nqm1bpoNATIVE = 0, nqm1bpoUSD = 0, nqm2bpoNATIVE = 0, nqm2bpoUSD = 0, nqm3bpoNATIVE = 0, nqm3bpoUSD = 0, nqbpoREVNATIVE = 0, nqbpoREVUSD = 0, nqbpoRTBRNATIVE = 0, nqbpoRTBRUSD = 0;


        
    //    angular.forEach(data, function (value, key) {
    //        //debugger;
    //        if (value.PU.indexOf('ADM') !== -1) {
    //            if (value.BVFR == '1') {
    //                pqm1admsum = pqm1admsum + (value.PQAM1);
    //                cqmadmCOBE = cqmadmCOBE + (value.CQMCOBE);
    //                cqadmDHBE = cqadmDHBE + (value.CQDHBE);
    //                nqmadmCOBE = nqmadmCOBE + (value.NQMCOBE);
    //                nqadmDHBE = nqadmDHBE + (value.NQDHBE);
    //            }
    //            cqm1admActuals = cqm1admActuals + (value.CQM1Actuals);
    //            cqm2admActuals = cqm2admActuals + (value.CQM2Actuals);
    //            cqm3admActuals = cqm3admActuals + (value.CQM3Actuals);

    //            cqm1admNative = cqm1admNative + value.CQM1NATIVE;
    //            cqm1admUSD = cqm1admUSD + (value.CQM1USD);
    //            cqm2admNative = cqm2admNative + value.CQM2NATIVE;
    //            cqm2admUSD = cqm2admUSD + (value.CQM2USD);
    //            cqm3admNative = cqm3admNative + value.CQM3NATIVE;
    //            cqm3admUSD = cqm3admUSD + (value.CQM3USD);

    //            cqrevadmNATIVE = cqrevadmNATIVE + (value.CQREVNATIVE);
    //            cqadmREVUSD = cqadmREVUSD + (value.CQREVUSD);
    //            cqadmRTBRNATIVE = cqadmRTBRNATIVE + (value.CQRTBRNATIVE);
    //            cqadmRTBRUSDACTUALS = cqadmRTBRUSDACTUALS + (value.CQRTBRUSDACTUALS);

    //            nqm1admNATIVE = nqm1admNATIVE + (value.NQM1NATIVE);
    //            nqm1USD = nqm1USD + (value.NQM1USD);
    //            nqm2admNATIVE = nqm2admNATIVE + (value.NQM2NATIVE);
    //            nqm2USD = nqm2USD + (value.NQM2USD);
    //            nqm3admNATIVE = nqm3admNATIVE + (value.NQM3NATIVE);
    //            nqm3USD = nqm3USD + (value.NQM3USD);

    //            nqadmREVNATIVE = nqadmREVNATIVE + (value.NQREVNATIVE);
    //            nqadmREVUSD = nqadmREVUSD + (value.NQREVUSD);
    //            nqadmRTBRNATIVE = nqadmRTBRNATIVE + (value.NQRTBRNATIVE)
    //            nqadmRTBRUSD = nqadmRTBRUSD + (value.NQRTBRUSD);


    //        }
    //        else {
    //            if (value.BVFR == "1") {
    //                pqm1bposum = pqm1bposum + (value.PQAM1);
    //                cqmbpoCOBE = cqmbpoCOBE + (value.CQMCOBE);
    //                cqbpoDHBE = cqbpoDHBE + (value.CQDHBE);
    //                nqmbpoCOBE = nqmbpoCOBE + (value.NQMCOBE);
    //                nqmbpoDHBE = nqmbpoDHBE + (value.NQDHBE);
    //            }
    //            cqm1bpoActuals = cqm1bpoActuals + (value.CQM1Actuals);
    //            cqm2bpoActuals = cqm2bpoActuals + (value.CQM2Actuals);
    //            cqm3bpoActuals = cqm3bpoActuals + (value.CQM3Actuals);

    //            cqm1bpoNative = cqm1bpoNative + value.CQM1NATIVE;
    //            cqm1bpoUSD = cqm1bpoUSD + (value.CQM1USD);
    //            cqm2bpoNative = cqm2bpoNative + value.CQM2NATIVE;
    //            cqm2bpoUSD = cqm2bpoUSD + (value.CQM2USD);
    //            cqm3bpoNative = cqm3bpoNative + value.CQM3NATIVE;
    //            cqm3bpoUSD = cqm3bpoUSD + (value.CQM3USD);

    //            cqrevbpoNATIVE = cqrevbpoNATIVE + (value.CQREVNATIVE);
    //            cqbpoREVUSD = cqbpoREVUSD + (value.CQREVUSD);
    //            cqbpoRTBRNATIVE = cqbpoRTBRNATIVE + (value.CQRTBRNATIVE);
    //            cqbpoRTBRUSDACTUALS = cqbpoRTBRUSDACTUALS + (value.CQRTBRUSDACTUALS);

    //            nqm1bpoNATIVE = nqm1bpoNATIVE + (value.NQM1NATIVE);
    //            nqm1bpoUSD = nqm1bpoUSD + (value.NQM1USD);
    //            nqm2bpoNATIVE = nqm2bpoNATIVE + (value.NQM2NATIVE);
    //            nqmbpo2USD = nqm2bpoUSD + (value.NQM2USD);
    //            nqm3bpoNATIVE = nqm3bpoNATIVE + (value.NQM3NATIVE);
    //            nqm3bpoUSD = nqm3bpoUSD + (value.NQM3USD);

    //            nqbpoREVNATIVE = nqbpoREVNATIVE + (value.NQREVNATIVE);
    //            nqbpoREVUSD = nqbpoREVUSD + (value.NQREVUSD);
    //            nqbpoRTBRNATIVE = nqbpoRTBRNATIVE + (value.NQRTBRNATIVE)
    //            nqbpoRTBRUSD = nqbpoRTBRUSD + (value.NQRTBRUSD);


    //        }
           
    //    });
        
    //    //$scope.totals = {
    //    //    pqm1admsum,cqmadmCOBE, cqadmDHBE, nqmadmCOBE, nqadmDHBE, cqm1admActuals, cqm2admActuals, cqm3admActuals,
    //    //    cqm1admNative, cqm1admUSD, cqm2admNative, cqm2admUSD, cqm3admNative, cqm3admUSD,
    //    //    cqrevadmNATIVE, cqadmREVUSD, cqadmRTBRNATIVE, cqadmRTBRUSDACTUALS, nqm1admNATIVE, nqm1USD, nqm2admNATIVE, nqm2USD, nqm3admNATIVE, nqm3USD,
    //    //    nqadmREVNATIVE, nqadmREVUSD, nqadmRTBRNATIVE, nqadmRTBRUSD,

    //    //    pqm1bposum, cqmbpoCOBE, cqbpoDHBE, nqmbpoCOBE, nqmbpoDHBE,
    //    //    cqm1bpoActuals, cqm2bpoActuals, cqm3bpoActuals, cqm1bpoNative, cqm1bpoUSD, cqm2bpoNative, cqm2bpoUSD, cqm3bpoNative, cqm3bpoUSD, cqrevbpoNATIVE, cqbpoREVUSD, cqbpoRTBRNATIVE, cqbpoRTBRUSDACTUALS,

    //    //    nqm1bpoNATIVE, nqm1bpoUSD, nqm2bpoNATIVE, nqm2bpoUSD, nqm3bpoNATIVE, nqm3bpoUSD, nqbpoREVNATIVE, nqbpoREVUSD, nqbpoRTBRNATIVE, nqbpoRTBRUSD

    //    //}
    //}

});