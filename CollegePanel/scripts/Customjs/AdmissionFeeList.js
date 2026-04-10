
var app1 = angular.module('admissionfeelistApp', ['chieffancypants.loadingBar', 'ngAnimate', 'ui.bootstrap']);
app1.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;
});
app1.controller('admissionfeelistCtrl', function ($scope, $http, cfpLoadingBar) {

    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected = 15; // Maximum number of items per page.
    $scope.EducationTypeID = "";
    $scope.CoursetypeID = "";
    $scope.session = "";
    //$scope.College = "";
    $scope.castecategory = "";
    $scope.Subject = "";
    $scope.FeeType = "";
    $scope.CourseYearID = "";
   
    $scope.FeestructureDetail = function () {
        cfpLoadingBar.start();
        var EducationTypeID = $scope.EducationTypeID === undefined ? '' : $scope.EducationTypeID;
        var CoursetypeID = $scope.CoursetypeID === undefined ? '' : $scope.CoursetypeID;
        var session = $scope.session === undefined ? '' : $scope.session;
        var Subject = $scope.Subject === undefined ? '' : $scope.Subject;
        var castecategory = $scope.CasteCategory === undefined ? "" : $scope.CasteCategory;
        var CourseYearID = $scope.CourseYearID === undefined ? "" : $scope.CourseYearID;
        var FeeType = $scope.FeeType === undefined ? '' : $scope.FeeType;
      
        $http.get(Url + "api/admissionfeeList", {
            params: { pageIndex: $scope.pageIndex, pageSize: $scope.pageSizeSelected, CourseCategory: CoursetypeID, Session: session, castecategory: castecategory, Subject: Subject, EducationTypeID: EducationTypeID, FeeType: FeeType, },
            headers: { 'Authorization': 'Basic ' + btoa(username + ':' + password) }
        }
        ).then(
            function (response) {
                console.log(response.data.qlist);
                $scope.qlist = response.data.qlist;
                $scope.totalCount = response.data.totalCount;

            },
            function (err) {
                var error = err;
            });
    }
  

    $scope.GetFilterUsers = function (Name) {
        $scope.FeestructureDetail();
    }
    $scope.FeestructureDetail();

  
    //Loading employees list on first time

    //  //This method is calling from pagination number
    $scope.pageChanged = function () {

        $scope.FeestructureDetail();
    };
    $scope.dataTableOpt = {
        //custom datatable options 
        // or load data through ajax call also
        "aLengthMenu": [[10, 50, 100, -1], [10, 50, 100, 'All']],
    };
    //This method is calling from dropDown
    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.FeestructureDetail();
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


