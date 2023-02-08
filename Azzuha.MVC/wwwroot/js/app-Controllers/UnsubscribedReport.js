
angular.module("fitCntr").requires.push('ui.bootstrap');
angular.module("fitCntr").requires.push('ui.select');
app.controller("UnsubscribedReport", ["$scope", "$http", "GetService", "commonService", function ($scope, $http, GetService, commonService) {
    //#region DatePicker
    //============================ Date picker pop up start 
    $scope.dateRang = {
        str: "",
        end: ""
    };

    $scope.dateOptions = {
        //dateDisabled: disabled,
        dateDisabled: null,
        formatYear: 'yy',
        //maxDate: new Date(2020, 5, 22),
        minDate: new Date(2000, 1, 1),
        startingDay: 1
    };

    $scope.format = "dd/MM/yyyy";

    $scope.open1 = function () {
        $scope.popup1.opened = true;
    };

    $scope.popup1 = {
        opened: false
    };

    $scope.open2 = function () {
        $scope.popup2.opened = true;
    };

    $scope.popup2 = {
        opened: false
    };

    $scope.recordDetail = null;
    //============================ Date picker pop up start 
    // #endregion DatePicker


    //============================ Pagination Start
    $scope.forwardedSearch = "";

    $scope.forwardedPagination = { Draw: 0, Page: 1, PageSize: 10, TotalRecords: 0, Search: "", SortByFieldName: "", SortBy: 'asc' };

    $scope.forwardedCases = [];


    //===================================== Load Cases

    $scope.findForwardedCases = function () {
        $scope.forwardedPagination = { Draw: 0, Page: 1, PageSize: 10, TotalRecords: 0, Search: "", SortByFieldName: "", SortBy: 'asc' };
        $scope.forwardedPagination.Search = $scope.forwardedSearch;
        $scope.loadForwardedCases();
    };


    $scope.loadForwardedCases = function () {
        $("#loader").show();
        GetService.getUnsubsribedList(setPaginationOffset($scope.forwardedPagination), $scope.dateRang.str, $scope.dateRang.end, "/Report/GetUnSubscribedData").then(function (data) {
            $scope.forwardedPagination.TotalRecords = data.data.totalRecords;
            $("#loader").hide();
            $scope.forwardedCases = data.data.entities;
        });
    };

    $scope.loadAllCases = function () {
        $scope.loadForwardedCases();

    };

    // =============================== End Cases
    // =============================== Sorting techniques

    $scope.sortByForwardedCases = function (index) {
        $scope.forwardedPagination = { Draw: 0, Page: 1, PageSize: 10, TotalRecords: 0, Search: "", SortIndex: index, SortBy: $scope.forwardedPagination.SortBy };
        $scope.forwardedPagination.SortBy = $scope.forwardedPagination.SortBy === "asc" ? "desc" : "asc";
        $scope.loadForwardedCases();
    };

    $scope.getClassForwardedCases = function (index) {
        if ($scope.forwardedPagination.SortIndex === index) {
            return $scope.forwardedPagination.SortBy === "asc" ? "fa fa-caret-up" : "fa fa-caret-down";
        }
        return '';
    };
    // End Sorting

    // Local Functions
    function setPaginationOffset(paginationObj) {
        var pagination = _.clone(paginationObj);
        pagination.Page = (pagination.Page - 1) * pagination.PageSize;
        return pagination;
    }
    //============================ Pagination End

    //================================ Document Ready
    angular.element(document).ready(function () {
        $scope.dateRang.end = new Date();
        $scope.dateRang.str = new Date();
        $scope.dateRang.str.setDate($scope.dateRang.str.getDate() - 30);

        //pagination
        $scope.loadAllCases();
    });

    //Convert Date
    $scope.convertToDate = function (data) {
        return commonService.convertToDate(data);
    };


}]);

function recordAdded(message, type) {
    if (message === "Saved Successfully!" || message === "Delete Successfully!" || message === "Backup Saved Successfully!") {
        $.notify(" " + message, { type: type, icon: "check", align: "right", verticalAlign: "top" });
    } else {
        $.notify(" " + message, { type: type, icon: "close", align: "right", verticalAlign: "top", background: "#ffbebe", color: "#6a0000" });
    }
}

