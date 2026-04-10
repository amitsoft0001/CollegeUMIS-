
var app1 = angular.module('empSallistApp', ['chieffancypants.loadingBar', /*'ngAnimate',*/ 'ui.bootstrap']);
app1.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;
});
app1.controller('empsallistCtrl', function ($scope, $http, cfpLoadingBar) {
    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected = 100; // Maximum number of items per page.
    $scope.EmployeeID = "";
    $scope.PaybandID = "";
    $scope.Name = "";
    $scope.EmployeedetailList = function () {
        cfpLoadingBar.start();
        // debugger;
        var EmployeeID = $scope.EmployeeID === undefined ? '' : $scope.EmployeeID;
        var PaybandID = $scope.PaybandID === undefined ? '' : $scope.PaybandID;
        var Name = $scope.Name === undefined ? '' : $scope.Name;
        showloader();
        $http.get(Url + "api/empSalaryList", {
            params: { pageIndex: $scope.pageIndex, pageSize: $scope.pageSizeSelected, EmployeeID: EmployeeID, PaybandID: PaybandID, Name: Name },
            headers: { 'Authorization': 'Basic ' + btoa(username + ':' + password) }
        }
        ).then(
            function (response) {
                hideloader();
                //console.log(response.data.qlist);
                $scope.qlist = response.data.qlist;
                $scope.totalCount = response.data.totalCount;

            },
            function (err) {
                hideloader();
                var error = err;
            });
    }
 
    $scope.GetFilterUsers = function (Name) {
        $scope.EmployeedetailList();
    }
    $scope.EmployeedetailList();

    $scope.pageChanged = function () {
        $scope.EmployeedetailList();
    };
    $scope.dataTableOpt = {      
        "aLengthMenu": [[10, 50, 100, -1], [10, 50, 100, 'All']],
    };
    //This method is calling from dropDown
    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.EmployeedetailList();
    };

    $scope.ShowQueryListDiv = function (varQueryID, sid) {       
        $(".hidevid").hide();
        $(".showid").show();
       // debugger;
        $scope.filteredArray = {};
        $("#trQueryComments" + varQueryID).show();
        $("#trhide" + varQueryID).show();
        $("#show_" + varQueryID).hide();
        var httpreq = {
            method: 'POST',
            url: '/College/PayRoll/EmployeeSalaryDetail',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { sid: sid }
        }
        $http(httpreq).success(
             
            function (response) {
                debugger;
               // console.log(response);
               // console.log(response.data);
                $scope.netTotal = response.data.NetTotal;
                $scope.grossTotal = response.data.GrossTotal;
                $scope.allowanceList = response.data.Allowance_MasterList;
                $scope.deductionList = response.data.Deduction_MasterList;
                $scope.TotalAllow = 0.0;
                for (var i = 0; i < response.data.Allowance_MasterList.length; i++)
                {
                    $scope.TotalAllow = $scope.TotalAllow + parseFloat(response.data.Allowance_MasterList[i].Amount);
                }
                $scope.TotalDec = 0.0;
                for (var i = 0; i < response.data.Deduction_MasterList.length; i++) {
                    console.log(response.data.Deduction_MasterList[i].Amount);
                    $scope.TotalDec = $scope.TotalDec + parseFloat(response.data.Deduction_MasterList[i].Amount);
                }
                console.log($scope.TotalDec);
            })
.error(function () {
    $scope.allowanceList = {};
    $scope.deductionList = {};
});
    }
    $scope.hideQueryListDiv = function (varQueryID) {
        //debugger;
        //console.log(varQueryID);
        $(".hidevid").hide();
        $("#trQueryComments" + varQueryID).hide();
        $("#trhide" + varQueryID).hide();
        $("#show_" + varQueryID).show();
    }   
    $scope.propertyName = null;
    $scope.reverse = true;

    $scope.sortBy = function (propertyName) {
        $scope.reverse = ($scope.propertyName === propertyName) ? !$scope.reverse : false;
        $scope.propertyName = propertyName;
    };

    $scope.numDifferentiation = function (val) {
        if (val >= 10000000) val = (val / 10000000).toFixed(2) + ' Cr';
        else if (val >= 100000) val = (val / 100000).toFixed(2) + ' Lac';
        else if (val >= 1000) val = (val / 1000).toFixed(2) + ' K';
        return val;
    }
});


