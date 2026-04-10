
var app1 = angular.module('collegeuserlistApp', ['chieffancypants.loadingBar', /*'ngAnimate',*/ 'ui.bootstrap']);
app1.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;
});
app1.controller('collegeuserlistCtrl', function ($scope, $http, cfpLoadingBar) {

    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected = 100; // Maximum number of items per page.
    $scope.Usertype = {};
    $scope.MobileNo = {};
    $scope.Email = {};

    $scope.UserdetailList = function () {
        cfpLoadingBar.start();
      //  debugger;
        var Usertypes = $scope.Usertype.search === undefined ? '' : $scope.Usertype.search;
        var MobileNo = $scope.MobileNo.search === undefined ? '' : $scope.MobileNo.search;
        var Email = $scope.Email.search === undefined ? '' : $scope.Email.search;
        $http.get(Url + "api/collegeuserList", {
            params: { pageIndex: $scope.pageIndex, pageSize: $scope.pageSizeSelected,  MobileNo: MobileNo, Email: Email },
            headers: { 'Authorization': 'Basic ' + btoa(username + ':' + password) }
        }
        )
        /*$http.get(Url + "api/userList?pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected + "")*/.then(
            function (response) {

               // console.log(response.data.qlist);
                $scope.qlist = response.data.qlist;
                $scope.totalCount = response.data.totalCount;

            },
            function (err) {
                var error = err;
            });
    }
    // console.log($scope.DSAdataList)

    $scope.GetFilterUsers = function (Name) {
        $scope.UserdetailList();
    }
    $scope.UserdetailList();

    

    //Loading employees list on first time

    //  //This method is calling from pagination number
    $scope.pageChanged = function () {
        $scope.UserdetailList();
    };
    $scope.dataTableOpt = {
        //custom datatable options 
        // or load data through ajax call also
        "aLengthMenu": [[10, 50, 100, -1], [10, 50, 100, 'All']],
    };
    //This method is calling from dropDown
    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.UserdetailList();
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


