
angular.module("fitCntr").requires.push('ui.bootstrap');
angular.module("fitCntr").requires.push('ui.select');
app.controller("EmailSubscriptions", ["$scope", "$http", "GetService", "commonService", function ($scope, $http, GetService, commonService) {
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

    $scope.open3 = function () {
        $scope.popup3.opened = true;
    };

    $scope.popup3 = {
        opened: false
    };

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
        GetService.getVouchers(setPaginationOffset($scope.forwardedPagination), $scope.dateRang.str, $scope.dateRang.end, "/EmailSubscription/Get").then(function (data) {
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
        $("#show-detail-modal").modal('hide');
        $scope.dateRang.end = new Date();
        $scope.dateRang.str = new Date();
        $scope.dateRang.str.setDate($scope.dateRang.str.getDate() - 30);

        //pagination
        $scope.loadAllCases();
    });

    $scope.EmailSubscriptionsModel = {
        Name: "",
        Code: "",
        Expiry: new Date(),
        ExpiryMonth: 0,
        Discount: 0
    };

    $scope.dateOptionForForm = {
        //dateDisabled: disabled,
        dateDisabled: null,
        formatYear: 'yy',
        //maxDate: new Date(2020, 5, 22),
        minDate: new Date(),
        startingDay: 1
    };

    $scope.changeStatus = function (id, isActive) {
        GetService.changeStatusEmailSubscriptions(id, isActive).then(function (data) {
            $scope.loadAllCases();
        });
    }

    $scope.openCreateModel = function () {
        //Init
        $scope.EmailSubscriptionsModel = {
            Name: "",
            Code: "",
            Expiry: new Date(),
            ExpiryMonth: "",
            Discount: ""
        };

        $("#show-detail-modal").modal('show');
    };

    $scope.saveEmailSubscriptions = function () {
        $("#loader").show();
        $(".form-control").removeClass("validation");
        $("#DE").html("");
        $("#CE").html("");



        var isAllowed1 = false;
        var isAllowed2 = false;
        var isAllowed3 = false;
        var isAllowed4 = false;
        var isAllowed5 = false;

        isAllowed1 = validationcheck($scope.EmailSubscriptionsModel.Name) ? $("#VN").addClass("validation") : true;
        isAllowed2 = validationcheck($scope.EmailSubscriptionsModel.Code) ? $("#VC").addClass("validation") : true;
        isAllowed3 = validationcheck($scope.EmailSubscriptionsModel.Discount) ? $("#VD").addClass("validation") : true;
        isAllowed5 = validationcheck($scope.EmailSubscriptionsModel.ExpiryMonth) ? $("#VEM").addClass("validation") : $scope.EmailSubscriptionsModel.ExpiryMonth < 0 ? $("#VEM").addClass("validation") : true;

        if ($scope.EmailSubscriptionsModel.Discount > 100) {
            $("#VD").addClass("validation");
            $("#DE").html("discount must be lesser than 100");
            $("#loader").hide();
            return false;
        } else {
            isAllowed4 = true;
        }

        if (isAllowed1 === true && isAllowed2 === true && isAllowed3 === true && isAllowed4 === true && isAllowed5 === true) {
            GetService.isCodeExist($scope.EmailSubscriptionsModel.Code).then(function (data) {
                if (data.data) {
                    $("#VC").addClass("validation")
                    $("#CE").html("Code already exists!");
                    $("#loader").hide();
                    return false;
                } else {
                    GetService.createEmailSubscriptions($scope.EmailSubscriptionsModel).then(function (data) {
                        recordAdded("Saved Successfully!", "success");
                        $scope.loadAllCases();
                        $("#show-detail-modal").modal('hide');
                        $("#loader").hide();
                    });
                }
            });

        } else {
            $("#loader").hide();
        }
    }

    //check validation
    function validationcheck(elem) {
        if (elem === "" || elem === undefined || elem === null) {
            return true;
        } else {
            return false;
        }
    }

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

