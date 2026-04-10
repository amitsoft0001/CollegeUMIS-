
var app1 = angular.module('recruitmentlistApp', ['chieffancypants.loadingBar', 'ngAnimate', 'ui.bootstrap']);
app1.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;
});
app1.controller('recruitmentlistCtrl', function ($scope, $http, cfpLoadingBar) {

    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected = 100; // Maximum number of items per page.
    $scope.Coursetype = {};
    $scope.Subject = {};
    $scope.session = {};
    $scope.EducationTypeID = {};
    $scope.CollegeID = {};
    $scope.Cast = {};
    $scope.Applicationno = {};
    $scope.SeatType = "2";
    $scope.CounsellingNo = {};
    $scope.RecruitmentdetailList = function () {
        cfpLoadingBar.start();
       debugger;

        var Coursetype = $scope.Coursetype.search === undefined ? '' : $scope.Coursetype.search;
        if (Coursetype == "")
        {
            Coursetype = courseid54;
        }

        var Subject = $scope.Subject.search === undefined ? '' : $scope.Subject.search;
        var session = $scope.session.search === undefined ? '' : $scope.session.search;
        var EducationTypeID1 = $scope.EducationTypeID.search === undefined ? '' : $scope.EducationTypeID.search;
        var CollegeID = $scope.CollegeID.search === undefined ? '' : $scope.CollegeID.search;
        var Cast = $scope.Cast.search === undefined ? '' : $scope.Cast.search;
        var Applicationno = $scope.Applicationno.search === undefined ? '' : $scope.Applicationno.search;
        var CounsellingNo = $scope.CounsellingNo.search === undefined ? '' : $scope.CounsellingNo.search;
        showloader();
        $http.get(Url + "api/recruitmentList", {
            params: {
                pageIndex: $scope.pageIndex, pageSize: $scope.pageSizeSelected, coursetype: Coursetype, subject: Subject, session: session, collegeID: CollegeID, cast: Cast, seatType: $scope.SeatType, Applicationno: Applicationno, CounsellingNo: CounsellingNo, EducationTypeID: EducationTypeID1
            },
            headers: { 'Authorization': 'Basic ' + btoa(username + ':' + password) }
        }
        )
       /* $http.get(Url + "api/recruitmentList?pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected + "&coursetype=" + Coursetype + "&subject=" + Subject + "&session=" + session + "&collegeID=" + CollegeID + "&cast=" + Cast + "&seatType=" + $scope.SeatType  +"")*/.then(
            function (response) {
                hideloader();
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
        $scope.RecruitmentdetailList();
    }
    //$scope.RecruitmentdetailList();

    $scope.GetStatusList = function (Status) {

        $scope.RecruitmentdetailList();
    }
    $scope.QualificationdetailList = function (Date, DateTo) {

        $scope.RecruitmentdetailList();
    }

    //Loading employees list on first time

    //  //This method is calling from pagination number
    $scope.pageChanged = function () {
        //debugger;
        $scope.RecruitmentdetailList();
    };
    $scope.dataTableOpt = {
        //custom datatable options 
        // or load data through ajax call also
        "aLengthMenu": [[10, 50, 100, -1], [10, 50, 100, 'All']],
    };
    //This method is calling from dropDown
    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.RecruitmentdetailList();
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


