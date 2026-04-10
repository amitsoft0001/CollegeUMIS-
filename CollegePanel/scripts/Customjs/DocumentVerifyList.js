
var app1 = angular.module('studentlistforverifyApp', ['chieffancypants.loadingBar', 'ngAnimate', 'ui.bootstrap']);
app1.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;
});
app1.controller('studentlistforverifyCtrl', function ($scope, $http, cfpLoadingBar) {

    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected = 100; // Maximum number of items per page.
    $scope.Coursetype = "";
    $scope.Subject = "";
    $scope.session = "";
    $scope.CollegeID = "";
    $scope.Cast = "";
    $scope.Application = "";
    $scope.SeatType = "2";
    $scope.ApplicationStatus = "3";
    $scope.FeeStatus = "2";
    $scope.CounsellingNo = "";
    $scope.IncomeStatus = "3";
    $scope.EducationType = "";

    $scope.studentdetailListForVerification = function () {
        cfpLoadingBar.start();
        //debugger;

        var Coursetype = $scope.Coursetype === undefined ? '' : $scope.Coursetype;
        var Subject = $scope.Subject === undefined ? '' : $scope.Subject;
        var session = $scope.session === undefined ? '' : $scope.session;
        var CollegeID = $scope.CollegeID === undefined ? '' : $scope.CollegeID;
        var Cast = $scope.Cast === undefined ? '' : $scope.Cast;
        var Application = $scope.Application === undefined ? '' : $scope.Application;
        var CounsellingNo = $scope.CounsellingNo === undefined ? '' : $scope.CounsellingNo;
        var varEducationtype = $scope.EducationType == undefined ? '' : $scope.EducationType;
        debugger;
        showloader();
        $http.get(Url + "api/studentListForVerified", {
            params: {
                pageIndex: $scope.pageIndex, pageSize: $scope.pageSizeSelected, coursetype: Coursetype, subject: Subject, session: session, cast: Cast, seatType: $scope.SeatType, application: Application, ApplicationStatus: $scope.ApplicationStatus, CounsellingNo: CounsellingNo, FeeStatus: $scope.FeeStatus, IncomeStatus: $scope.IncomeStatus, varEducationtype: varEducationtype
            },
            headers: { 'Authorization': 'Basic ' + btoa(username + ':' + password) }
        }
        )
       /* $http.get(Url + "api/recruitmentList?pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected + "&coursetype=" + Coursetype + "&subject=" + Subject + "&session=" + session + "&collegeID=" + CollegeID + "&cast=" + Cast + "&seatType=" + $scope.SeatType  +"")*/.then(
            function (response) {
                hideloader();
                console.log("migration data" + JSON.stringify(response.data.qlist));
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
        $scope.studentdetailListForVerification();
    }
  //  $scope.studentdetailListForVerification();

    $scope.GetStatusList = function (Status) {

        $scope.studentdetailListForVerification();
    }
    $scope.QualificationdetailList = function (Date, DateTo) {

        $scope.studentdetailListForVerification();
    }

    //Loading employees list on first time

    //  //This method is calling from pagination number
    $scope.pageChanged = function () {
        //debugger;
        $scope.studentdetailListForVerification();
    };
    $scope.dataTableOpt = {
        //custom datatable options 
        // or load data through ajax call also
        "aLengthMenu": [[10, 50, 100, -1], [10, 50, 100, 'All']],
    };
    //This method is calling from dropDown
    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.studentdetailListForVerification();
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


