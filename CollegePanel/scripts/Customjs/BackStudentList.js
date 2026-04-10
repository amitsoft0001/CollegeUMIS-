
var app1 = angular.module('examfeelistApp', ['chieffancypants.loadingBar', 'ngAnimate', 'ui.bootstrap']);
app1.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;
});
app1.controller('examfeelistCtrl', function ($scope, $http, cfpLoadingBar) {

    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected = 100; // Maximum number of items per page.
    $scope.CourseCategoryID = "";
    $scope.Subject = "";
    $scope.session = "";
    $scope.Application = ""; 
    $scope.paymentStatus = "3";
    $scope.ApplicationStatus = "3";
    $scope.CourseYearID = "";
    $scope.CollegeID = "";
    $scope.EducationTypeID = "";
    $scope.enrollmentno = "";
    $scope.studentListafterfeeSubmit = function () {
        cfpLoadingBar.start();
        debugger;

        var CourseCategoryID = $scope.CourseCategoryID === undefined ? '' : $scope.CourseCategoryID;
        var Subject = $scope.Subject === undefined ? '' : $scope.Subject;
        var session = $scope.session === undefined ? '' : $scope.session;      
        var Application = $scope.Application === undefined ? '' : $scope.Application;      
        var paymentStatus = $scope.paymentStatus === undefined ? '' : $scope.paymentStatus;
        var CourseYearID = $scope.CourseYearID === undefined ? '' : $scope.CourseYearID;
        var EducationType = $scope.EducationTypeID === undefined ? '' : $scope.EducationTypeID;
        var ApplicationStatus = $scope.ApplicationStatus === undefined ? '' : $scope.ApplicationStatus;
        var CollegeID = $scope.CollegeID === undefined ? '' : $scope.CollegeID;
        var Enrollmentno = $scope.enrollmentno === undefined ? '' : $scope.enrollmentno;
        if (session == "") {
            alert('Please Select Session  !!');
            return false;
        }
        if (EducationType == "") {
            alert('Please Select Education Type  !!');
            return false;
        }
        showloader();
        $http.get(Url + "api/backstudentList", {
            params: {
                pageIndex: $scope.pageIndex, pageSize: $scope.pageSizeSelected, CollegeID: CollegeID, session: session, CourseCategoryID: CourseCategoryID, CourseYearID: CourseYearID, EducationTypeID: EducationType, Enrollmentno: Enrollmentno
            },
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


    $scope.onClearQlist = function () {        
        $scope.qlist = "";
        $scope.totalCount = 0;
    }

    $scope.GetFilterUsers = function (Name) {
        $scope.studentListafterfeeSubmit();
    }
    //$scope.studentListafterfeeSubmit();

    $scope.GetStatusList = function (Status) {

        $scope.studentListafterfeeSubmit();
    }
    $scope.QualificationdetailList = function (Date, DateTo) {

        $scope.studentListafterfeeSubmit();
    }

    //Loading employees list on first time

    //  //This method is calling from pagination number
    $scope.pageChanged = function () {
        //debugger;
        $scope.studentListafterfeeSubmit();
    };
    $scope.dataTableOpt = {
        //custom datatable options 
        // or load data through ajax call also
        "aLengthMenu": [[10, 50, 100, -1], [10, 50, 100, 'All']],
    };
    //This method is calling from dropDown
    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.studentListafterfeeSubmit();
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


