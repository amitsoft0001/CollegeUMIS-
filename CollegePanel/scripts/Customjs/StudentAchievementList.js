
var app1 = angular.module('achievementlistApp', ['chieffancypants.loadingBar', /*'ngAnimate',*/ 'ui.bootstrap']);
app1.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;
});
app1.controller('achievementlistCtrl', function ($scope, $http, cfpLoadingBar) {

    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected = 100; // Maximum number of items per page.
    $scope.SID = "";
    //$scope.AchievementMasterID = "";
    $scope.session = "";
    $scope.EducationTypeID = "";
    $scope.Course = "";
    $scope.Name = "";
    $scope.RegistrationNo = "";
    $scope.achievementList = function () {
        cfpLoadingBar.start();
        showloader();
        // debugger;
        var SID = $scope.SID === undefined ? '' : $scope.SID;
        var session = $scope.session === undefined ? '' : $scope.session;
        var EducationTypeID = $scope.EducationTypeID === undefined ? '' : $scope.EducationTypeID;
        var Course = $scope.Course === undefined ? '' : $scope.Course;
        //var AchievementMasterID = $scope.AchievementMasterID === undefined ? '' : $scope.AchievementMasterID;
        var Name = $scope.Name === undefined ? '' : $scope.Name;
        var RegistrationNo = $scope.RegistrationNo === undefined ? '' : $scope.RegistrationNo;
        $http.get(Url + "api/achievementList", {
            params: { pageIndex: $scope.pageIndex, pageSize: $scope.pageSizeSelected, session: session, EducationTypeID: EducationTypeID, Course: Course, SID: SID, Name: Name, RegistrationNo: RegistrationNo },
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
        $scope.achievementList();
    }
    $scope.achievementList();
    $scope.pageChanged = function () {
        $scope.achievementList();
    };
    $scope.dataTableOpt = {
        //custom datatable options 
        // or load data through ajax call also
        "aLengthMenu": [[10, 50, 100, -1], [10, 50, 100, 'All']],
    };
    //This method is calling from dropDown
    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.achievementList();
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


