
var app1 = angular.module('qualificationlistApp', ['chieffancypants.loadingBar', /*'ngAnimate',*/ 'ui.bootstrap']);
app1.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;
});

var segment_str = window.location.pathname; // return segment1/segment2/segment3/segment4
var segment_array = segment_str.split ('/') ;
var last_segment = segment_array[segment_array.length - 1];
last_segment = last_segment.trim();
app1.controller('qualificationlistCtrl', function ($scope, $http, cfpLoadingBar) {

    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected = 100; // Maximum number of items per page.
   
    $scope.QualificationdetailList = function () {
        cfpLoadingBar.start();     
        debugger;
       // $http.get(Url + "api/qualifiationList", {
       //     params: {
       //         Id: last_segment, pageIndex: $scope.pageIndex, pageSize: $scope.pageSizeSelected 
       //     },
       //     headers: { 'Authorization': 'Basic ' + btoa(username + ':' + password) }
       // }
       //).then(
        $http.get(Url + "api/qualifiationList?pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected +"&Id="+ last_segment+"").then(
            function (response) {

                console.log(response.data.qlist);
                $scope.qlist = response.data.qlist;
                $scope.totalCount = response.data.totalCount;
              
            },
            function (err) {
                var error = err;
            });
    }
    // console.log($scope.DSAdataList)

    $scope.GetFilterUsers = function (Name) {
        $scope.QualificationdetailList();
    }
    $scope.QualificationdetailList();

    $scope.GetStatusList = function (Status) {

        $scope.QualificationdetailList();
    }
    $scope.QualificationdetailList = function (Date, DateTo) {

        $scope.QualificationdetailList();
    }

    //Loading employees list on first time

    //  //This method is calling from pagination number
    $scope.pageChanged = function () {
        $scope.QualificationdetailList();
    };
    $scope.dataTableOpt = {
        //custom datatable options 
        // or load data through ajax call also
        "aLengthMenu": [[10, 50, 100, -1], [10, 50, 100, 'All']],
    };
    //This method is calling from dropDown
    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.QualificationdetailList();
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


