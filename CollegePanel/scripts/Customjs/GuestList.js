
var app1 = angular.module('guestlistApp', ['chieffancypants.loadingBar', /*'ngAnimate',*/ 'ui.bootstrap']);
app1.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;
});
app1.controller('guestlistCtrl', function ($scope, $http, cfpLoadingBar) {

    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected = 100; // Maximum number of items per page.  
    $scope.EventID = "";
    $scope.MobileNo = "";
    $scope.Name = "";
    $scope.GetEventList = function () {
        cfpLoadingBar.start();
        showloader();
        // debugger;      
        var EventID = $scope.EventID === undefined ? '' : $scope.EventID;
        var MobileNo = $scope.MobileNo === undefined ? '' : $scope.MobileNo;
        var Name = $scope.Name === undefined ? '' : $scope.Name;
        $http.get(Url + "api/guestList", {
            params: { pageIndex: $scope.pageIndex, pageSize: $scope.pageSizeSelected, EventID: EventID, MobileNo: MobileNo, Name: Name },
            headers: { 'Authorization': 'Basic ' + btoa(username + ':' + password) }
        }
        ).then(
            function (response) {
                hideloader();
                // console.log(response.data.qlist);
                $scope.qlist = response.data.qlist;
                $scope.totalCount = response.data.totalCount;

            },
            function (err) {
                hideloader();
                var error = err;
            });
    }
    // console.log($scope.DSAdataList)

    $scope.GetFilterUsers = function (Name) {
        $scope.GetEventList();
    }
    $scope.GetEventList();
    $scope.pageChanged = function () {
        $scope.GetEventList();
    };
    $scope.dataTableOpt = {
        //custom datatable options 
        // or load data through ajax call also
        "aLengthMenu": [[10, 50, 100, -1], [10, 50, 100, 'All']],
    };
    //This method is calling from dropDown
    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.GetEventList();
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


