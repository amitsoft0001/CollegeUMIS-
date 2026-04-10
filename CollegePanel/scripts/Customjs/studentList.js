
var app1 = angular.module('studentlistApp', ['chieffancypants.loadingBar', 'ngAnimate', 'ui.bootstrap']);
app1.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;
});
app1.controller('studentlistCtrl', function ($scope, $http, cfpLoadingBar) {

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
    $scope.ApplicationStatus = "2";
    $scope.CounsellingNo = {};
    $scope.studentdetailList = function () {
        cfpLoadingBar.start();
        //debugger;

        var Coursetype = $scope.Coursetype.search === undefined ? '' : $scope.Coursetype.search;
        var Subject = $scope.Subject.search === undefined ? '' : $scope.Subject.search;
        var session = $scope.session.search === undefined ? '' : $scope.session.search;
        var CollegeID = $scope.CollegeID.search === undefined ? '' : $scope.CollegeID.search;
        var Cast = $scope.Cast.search === undefined ? '' : $scope.Cast.search;
        var Application = $scope.Application.search === undefined ? '' : $scope.Application.search;
        var CounsellingNo = $scope.CounsellingNo.search === undefined ? '' : $scope.CounsellingNo.search;
        $http.get(Url + "api/studentList", {
            params: {
                pageIndex: $scope.pageIndex, pageSize: $scope.pageSizeSelected, coursetype: Coursetype, subject: Subject, session: session, cast: Cast, seatType: $scope.SeatType, application: Application, ApplicationStatus: $scope.ApplicationStatus, CounsellingNo: CounsellingNo
            },
            headers: { 'Authorization': 'Basic ' + btoa(username + ':' + password) }
        }
        )
       /* $http.get(Url + "api/recruitmentList?pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected + "&coursetype=" + Coursetype + "&subject=" + Subject + "&session=" + session + "&collegeID=" + CollegeID + "&cast=" + Cast + "&seatType=" + $scope.SeatType  +"")*/.then(
            function (response) {

                $scope.qlist = response.data.qlist;
                $scope.totalCount = response.data.totalCount;

            },
            function (err) {
                var error = err;
            });
    }
    // console.log($scope.DSAdataList)

    $scope.GetFilterUsers = function (Name) {
        $scope.studentdetailList();
    }
    $scope.studentdetailList();

    $scope.GetStatusList = function (Status) {

        $scope.studentdetailList();
    }
    $scope.QualificationdetailList = function (Date, DateTo) {

        $scope.studentdetailList();
    }

    //Loading employees list on first time

    //  //This method is calling from pagination number
    $scope.pageChanged = function () {
        //debugger;
        $scope.studentdetailList();
    };
    $scope.dataTableOpt = {
        //custom datatable options 
        // or load data through ajax call also
        "aLengthMenu": [[10, 50, 100, -1], [10, 50, 100, 'All']],
    };
    //This method is calling from dropDown
    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.studentdetailList();
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


