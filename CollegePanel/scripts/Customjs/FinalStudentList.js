
var app1 = angular.module('feesubmitstudentlistApp', ['chieffancypants.loadingBar', 'ngAnimate', 'ui.bootstrap']);
app1.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;
});
app1.controller('feesubmitstudentlistCtrl', function ($scope, $http, cfpLoadingBar) {

    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected = 100; // Maximum number of items per page.
    $scope.Coursetype = {};
    $scope.Subject = {};
    $scope.session = {};
    $scope.CollegeID = {};
    $scope.Cast = {};
    $scope.Application = {};
    $scope.SeatType = "2";
    $scope.ApplicationStatus = "3";
    $scope.CounsellingNo = {};
    $scope.paymentStatus = "3";
    $scope.CourseYearID = {};
    $scope.EducationTypeID = "";
    $scope.Registration = "";


    $scope.studentListafterfeeSubmit = function () {
        cfpLoadingBar.start();
        //debugger;

        var Coursetype = $scope.Coursetype.search === undefined ? '' : $scope.Coursetype.search;
        var Subject = $scope.Subject.search === undefined ? '' : $scope.Subject.search;
        var session = $scope.session.search === undefined ? '' : $scope.session.search;
        var CollegeID = $scope.CollegeID.search === undefined ? '' : $scope.CollegeID.search;
        var Cast = $scope.Cast.search === undefined ? '' : $scope.Cast.search;
        var Application = $scope.Application.search === undefined ? '' : $scope.Application.search;
        var CounsellingNo = $scope.CounsellingNo.search === undefined ? '' : $scope.CounsellingNo.search;      
        var paymentStatus = $scope.paymentStatus === undefined ? '' : $scope.paymentStatus;       
        var CourseYearID = $scope.CourseYearID.search === undefined ? '' : $scope.CourseYearID.search;
        var EducationType = $scope.EducationTypeID === undefined ? '' : $scope.EducationTypeID;
        var Registration = $scope.Registration === undefined ? '' : $scope.Registration;
        
        //ApplicationStatus: $scope.ApplicationStatus,
        showloader();
        $http.get(Url + "api/finalstudentList", {
            params: {
                pageIndex: $scope.pageIndex, pageSize: $scope.pageSizeSelected, coursetype: Coursetype, subject: Subject, session: session, cast: Cast, seatType: $scope.SeatType, application: Application, CounsellingNo: CounsellingNo, paymentStatus: paymentStatus, CourseYearID: CourseYearID, EducationType: EducationType, Registration: Registration
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
  
    $scope.GetFilterUsers = function (Name) {
        $scope.studentListafterfeeSubmit();
    }
  //  $scope.studentListafterfeeSubmit();

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
    $scope.ShowQueryListDiv = function (varQueryID, sid) {
        // debugger;
        //console.log(varQueryID);
        $(".hidevid").hide();
        $(".showid").show();
       
        $scope.filteredArray = {};
        $("#trQueryComments" + varQueryID).show();
        $("#trhide" + varQueryID).show();
        $("#show_" + varQueryID).hide();
        var httpreq = {
            method: 'POST',
            url: '/College/Home/view1studentDetail',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { sid: sid }
        }
        $http(httpreq).success(
            function (response) {
                // console.log(response);
                //console.log(response.data);
                $scope.filteredArray = response.data;
            })
.error(function () {
    $scope.filteredArray = {};
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


