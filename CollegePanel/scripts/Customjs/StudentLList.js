var app = angular.module('studentlistApp', ['chieffancypants.loadingBar', 'ui.bootstrap']);
app.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;
});
app.controller('studentlistCtrl', function ($scope, $http, cfpLoadingBar) {
    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected = 100; // Maximum number of items per page.
    $scope.getCustomer = {};
    $scope.ClubData = {};
    $scope.ListCustomer = {};
    $scope.Status = "2";
    $scope.isPayStatus = "2";
    $scope.abc = "xya";

    var Url = window.location.origin + '/';

    $scope.GetStudentList = function () {

       
        cfpLoadingBar.start();
      
        var Searching = $scope.ClubData.search === undefined ? '' : $scope.ClubData.search;
        var Searchname = $scope.ClubData.name === undefined ? '' : $scope.ClubData.name;
        var SApplicationNo = $scope.ClubData.ApplicationNo === undefined ? '' : $scope.ClubData.ApplicationNo;
        var sIsFeeSubmit = $scope.ClubData.IsFeeSubmit === undefined ? 2 : $scope.ClubData.IsFeeSubmit;
        var ssession = $scope.ClubData.session === undefined ? '' : $scope.ClubData.session;
        var sCourseCategory = $scope.ClubData.Coursetype === '' ? 0 : $scope.ClubData.Coursetype;
        var sfromdate = $scope.ClubData.fromdate === '' ? '' : $scope.ClubData.fromdate;
        var stodate = $scope.ClubData.todate === '' ? '' : $scope.ClubData.todate;
       
        var sSubject = $scope.ClubData.Subject === undefined ? 0 : $scope.ClubData.Subject === '' ? 0 : $scope.ClubData.Subject;
        var sCollege = $scope.ClubData.CollegeID === undefined ? 0 : $scope.ClubData.CollegeID === '' ? 0 : $scope.ClubData.CollegeID;
        var sEducationType = $scope.ClubData.EducationType === '' ? 0 : $scope.ClubData.EducationType;
        showloader();
        $http.get(Url + "api/get-student-list", {
            params: { pageIndex1: $scope.pageIndex, pageSize1: $scope.pageSizeSelected, ApplicationNo: SApplicationNo, session: ssession, IsFeeSubmit: sIsFeeSubmit, name: Searchname, CourseCategory: sCourseCategory, Subject: sSubject, CollegeID: sCollege, fromdate: sfromdate, todate: stodate, EducationType: sEducationType },
            headers: { 'Authorization': 'Basic ' + btoa(username + ':' + password) }
        }
        )
       /* $http.get(Url + "api/get-student-list?pageIndex1=" + $scope.pageIndex + "&pageSize1=" + $scope.pageSizeSelected + "&ApplicationNo=" + SApplicationNo + "&session=" + ssession + "&IsFeeSubmit=" + sIsFeeSubmit + "&name=" + Searchname + "&CourseCategory=" + sCourseCategory+"&Subject=" + sSubject + "&CollegeID=" + sCollege)*/.then(
            function (response) {
                hideloader();
               debugger;  
                //console.log(response.data.qlist);
                $scope.qlist = response.data.qlist;
                $scope.totalCount = response.data.totalCount;

            },
            function (err) {
                var error = err;
            });
    }

    $scope.GetFilterStates = function (responseData) {
        // alert(JSON.stringify(userData));
        $scope.GetStudentList();
    }
    $scope.GetFilterUsers = function (ClubData) {
        $scope.GetStudentList();
    }

    //Loading employees list on first time
    //$scope.GetStudentList();

    $scope.GetStatusUsers = function (Status) {

        $scope.GetStudentList();
    }
    $scope.GetIsPayStatus = function (isPayStatus) {

        $scope.GetStudentList();
    }

    //  //This method is calling from pagination number
    $scope.pageChanged = function () {
        $scope.GetStudentList();
    };
    $scope.dataTableOpt = {
        //custom datatable options 
        // or load data through ajax call also
        "aLengthMenu": [[10, 50, 100, -1], [10, 50, 100, 'All']],
    };
    //This method is calling from dropDown
    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.GetStudentList();
    };
    $scope.GetStatusUsers = function () {
        $scope.Status = 1;
        $scope.GetStudentList();
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
app.controller('listCtrl', function ($scope, $http, $timeout, Excel) {
    $http.get("cate.json").then(function (response) { $scope.items = response.data; });
    $scope.exportData = function () {
        $('#customers').tableExport({ type: 'json', escape: 'false' });
    };

    $scope.exportToExcel = function (tableId) { // ex: '#my-table'
        var exportHref = Excel.tableToExcel(tableId, 'WireWorkbenchDataExport');
        $timeout(function () { location.href = exportHref; }, 100); // trigger download
    }
});
app.factory('Excel', function ($window) {
    var uri = 'data:application/vnd.ms-excel;base64,',
        template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>',
        base64 = function (s) { return $window.btoa(unescape(encodeURIComponent(s))); },
        format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) };
    return {
        tableToExcel: function (tableId, worksheetName) {
            var table = $(tableId),
                ctx = { worksheet: worksheetName, table: table.html() },
                href = uri + base64(format(template, ctx));
            return href;
        }
    };
})