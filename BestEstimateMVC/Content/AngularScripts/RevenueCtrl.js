app.controller("RevCntrl", function ($scope, RevService) {

    GetAllRevDeatils();
    GetQtrDetails();

    $scope.sortType = 'MasterCustomerCode'; // set the default sort type
    $scope.sortReverse = false;  // set the default sort order
    $scope.searchCustCode = '';     // set the default search/filter term

    $scope.hghltRows = function(rev)
    {
        var actuals = rev.CQM1Actuals + rev.CQM2Actuals + rev.CQM3Actuals;
       
        actuals = isNaN(actuals) ? 0 : actuals;
        //console.log('actuals' + actuals + 'revnative' + rev.CQREVNATIVE);
        return parseInt(actuals) > parseInt(rev.CQREVNATIVE) ? 'hghlight' : 'ok';
      
    }
    var listOfChangedRows = [];

    $scope.onChange = function (rev, key, index) {
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
        if (listOfChangedRows.indexOf(index) == -1) {
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
            var isUpdate = 'Y';
            GetAllRevDeatils(isUpdate);
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
    function GetAllRevDeatils(isUpdate) {
        var getRevData = RevService.getAllRevDetails();
        getRevData.then(function (rev) {
            // debugger;
          
            $scope.revdetails = rev.data.revdata;

            $scope.admtotal = rev.data.admtotal;
            $scope.bpototal = rev.data.bpototal;
            // GetTotals($scope.revdetails);

            // Pagination in controller
            if (isUpdate != 'Y')
            $scope.page = 1;


           

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
  
});