
var app1 = angular.module('leavependinglistApp', ['chieffancypants.loadingBar', /*'ngAnimate',*/ 'ui.bootstrap']);
app1.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;
});
app1.controller('leavependinglistCtrl', function ($scope, $http, cfpLoadingBar) {

    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected = 100; // Maximum number of items per page.
    $scope.Month = "";
    $scope.Year = "";
    $scope.Name = "";
    $scope.LeaveStatus = "3";
    $scope.EmployeePendingLeaveList = function () {
        cfpLoadingBar.start();
       // debugger;
        var Month = $scope.Month === undefined ? '' : $scope.Month;
        var Year = $scope.Year === undefined ? '' : $scope.Year;
        var Name = $scope.Name === undefined ? '' : $scope.Name;
        $http.get(Url + "api/leaveList", {
            params: { pageIndex: $scope.pageIndex, pageSize: $scope.pageSizeSelected, Month: Month, Year: Year, Name: Name, LeaveStatus: $scope.LeaveStatus },
            headers: { 'Authorization': 'Basic ' + btoa(username + ':' + password) }
        }
        ).then(
            function (response) {

                //console.log(response.data.qlist);
                $scope.qlist = response.data.qlist;
                $scope.totalCount = response.data.totalCount;

            },
            function (err) {
                var error = err;
            });
    }
    // console.log($scope.DSAdataList)

    $scope.GetFilterUsers = function (Name) {
        $scope.EmployeePendingLeaveList();
    }
    $scope.EmployeePendingLeaveList();



    //Loading employees list on first time

    //  //This method is calling from pagination number
    $scope.pageChanged = function () {
        $scope.EmployeePendingLeaveList();
    };
    $scope.dataTableOpt = {
        //custom datatable options 
        // or load data through ajax call also
        "aLengthMenu": [[10, 50, 100, -1], [10, 50, 100, 'All']],
    };
    //This method is calling from dropDown
    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.EmployeePendingLeaveList();
    };

    // Sorting by Table head
    $scope.propertyName = null;
    $scope.reverse = true;


    $scope.sortBy = function (propertyName) {
        $scope.reverse = ($scope.propertyName === propertyName) ? !$scope.reverse : false;
        $scope.propertyName = propertyName;
    };
    //


    $scope.numDifferentiation = function (val) {
        if (val >= 10000000) val = (val / 10000000).toFixed(2) + ' Cr';
        else if (val >= 100000) val = (val / 100000).toFixed(2) + ' Lac';
        else if (val >= 1000) val = (val / 1000).toFixed(2) + ' K';
        return val;
    }


});


