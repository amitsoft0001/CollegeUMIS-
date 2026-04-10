
var app1 = angular.module('enrollmentlistApp', ['chieffancypants.loadingBar', /*'ngAnimate',*/ 'ui.bootstrap']);
app1.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;
});
app1.controller('enrollmentlistCtrl', function ($scope, $http, cfpLoadingBar) {

    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected = 100; // Maximum number of items per page.
    $scope.RegistrationNo = "";
    $scope.Name = ""; 
    $scope.EnrollmentStatus = "";
    $scope.EducationTypeID = "";
    $scope.CourseCategoryID = ""; 
    $scope.session = "";
    $scope.CollegeID = "";

    $scope.EnrollmentDetailList = function () {
        cfpLoadingBar.start();
        // debugger;
        showloader();
        var EducationTypeID = $scope.EducationTypeID === undefined ? '' : $scope.EducationTypeID;
        var CourseCategoryID = $scope.CourseCategoryID === undefined ? '' : $scope.CourseCategoryID;
        var Name = $scope.Name === undefined ? '' : $scope.Name;
        var RegistrationNo = $scope.RegistrationNo === undefined ? '' : $scope.RegistrationNo;
        var session = $scope.session === undefined ? '' : $scope.session;
       // var CollegeID = $scope.CollegeID === undefined ? '' : $scope.CollegeID;       
        $http.get(Url + "api/EnrollmentRequestDetailList", {
            params: { pageIndex: $scope.pageIndex, pageSize: $scope.pageSizeSelected, collegeID: '', EducationTypeID: EducationTypeID, CourseCategoryID: CourseCategoryID, Name: Name, RegistrationNo: RegistrationNo, session: session },
            headers: { 'Authorization': 'Basic ' + btoa(username + ':' + password) }
        })
        /*$http.get(Url + "api/userList?pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected + "")*/.then(
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
    // console.log($scope.DSAdataList)

    $scope.GetFilterUsers = function (Name) {
        $scope.EnrollmentDetailList();
    }
   // $scope.EnrollmentDetailList();



    //Loading employees list on first time

    //  //This method is calling from pagination number
    $scope.pageChanged = function () {
        $scope.EnrollmentDetailList();
    };
    $scope.dataTableOpt = {
        //custom datatable options 
        // or load data through ajax call also
        "aLengthMenu": [[10, 50, 100, -1], [10, 50, 100, 'All']],
    };
    //This method is calling from dropDown
    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.EnrollmentDetailList();
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


