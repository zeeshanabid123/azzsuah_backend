app.controller("Dashboard", ["$scope", "$http", function ($scope, $http) {

    $scope.trainerCount;
    $scope.appCount;
    $scope.gymCount;
    $scope.neutrationAppCount;
    $scope.classCount;
    $scope.customerCount;

    $scope.feeTrainerCount;
    $scope.feeAppCount;
    $scope.feeGymCount;
    $scope.feeNeutrationAppCount;
    $scope.feeClassCount;
    $scope.feeCustomerCount;

    $scope.GetCountTrainer = function () {
        $(".L1").show();
        $http({
            url: "/Dashboard/GetCountTrainer/",
            method: "Get",
        }).then(function (res) {
            $(".L1").hide();
            $scope.trainerCount = res.data.data;
        }, function () {
            $(".L1").hide();
            recordAdded("Something Went Wrong!");
        });
    };

    $scope.GetCountApp = function () {
        $(".L2").show();
        $http({
            url: "/Dashboard/GetCountApp/",
            method: "Get",
        }).then(function (res) {
            $(".L2").hide();
            $scope.appCount = res.data.data;
        }, function () {
            $(".L2").hide();
            recordAdded("Something Went Wrong!");
        });
    };

    $scope.GetCountGym = function () {
        $(".L3").show();
        $http({
            url: "/Dashboard/GetCountGym/",
            method: "Get",
        }).then(function (res) {
            $(".L3").hide();
            $scope.gymCount = res.data.data;
        }, function () {
            $(".L3").hide();
            recordAdded("Something Went Wrong!");
        });
    };

    $scope.GetCountNeutrationApp = function () {
        $(".L4").show();
        $http({
            url: "/Dashboard/GetCountNeutrationApp/",
            method: "Get",
        }).then(function (res) {
            $(".L4").hide();
            $scope.neutrationAppCount = res.data.data;
        }, function () {
            $(".L4").hide();
            recordAdded("Something Went Wrong!");
        });
    };

    $scope.GetCountClass = function () {
        $(".L5").show();
        $http({
            url: "/Dashboard/GetCountClass/",
            method: "Get",
        }).then(function (res) {
            $(".L5").hide();
            $scope.classCount = res.data.data;
        }, function () {
            $(".L5").hide();
            recordAdded("Something Went Wrong!");
        });
    };

    $scope.GetCountCustomer = function () {
        $(".L6").show();
        $http({
            url: "/Dashboard/GetCountCustomer/",
            method: "Get",
        }).then(function (res) {
            $(".L6").hide();
            $scope.customerCount = res.data.data;
        }, function () {
            $(".L6").hide();
            recordAdded("Something Went Wrong!");
        });
    };

    $scope.GetFeeCountTrainer = function () {
        $(".F1").show();
        $http({
            url: "/Dashboard/GetFeeCountTrainer/",
            method: "Get",
        }).then(function (res) {
            $(".F1").hide();
            $scope.feeTrainerCount = res.data.data;
        }, function () {
            $(".F1").hide();
            recordAdded("Something Went Wrong!");
        });
    };

    $scope.GetFeeCountApp = function () {
        $(".F2").show();
        $http({
            url: "/Dashboard/GetFeeCountApp/",
            method: "Get",
        }).then(function (res) {
            $(".F2").hide();
            $scope.feeAppCount = res.data.data;
        }, function () {
            $(".F2").hide();
            recordAdded("Something Went Wrong!");
        });
    };

    $scope.GetFeeCountGym = function () {
        $(".F3").show();
        $http({
            url: "/Dashboard/GetFeeCountGym/",
            method: "Get",
        }).then(function (res) {
            $(".F3").hide();
            $scope.feeGymCount = res.data.data;
        }, function () {
            $(".F3").hide();
            recordAdded("Something Went Wrong!");
        });
    };

    $scope.GetFeeCountNeutrationApp = function () {
        $(".F4").show();
        $http({
            url: "/Dashboard/GetFeeCountNeutrationApp/",
            method: "Get",
        }).then(function (res) {
            $(".F4").hide();
            $scope.feeNeutrationAppCount = res.data.data;
        }, function () {
            $(".F4").hide();
            recordAdded("Something Went Wrong!");
        });
    };

    $scope.GetFeeCountClass = function () {
        $(".F5").show();
        $http({
            url: "/Dashboard/GetFeeCountClass/",
            method: "Get",
        }).then(function (res) {
            $(".F5").hide();
            $scope.feeClassCount = res.data.data;
        }, function () {
            $(".F5").hide();
            recordAdded("Something Went Wrong!");
        });
    };

    $scope.GetFeeCountCustomer = function () {
        $(".F6").show();
        $http({
            url: "/Dashboard/GetFeeCountCustomer/",
            method: "Get",
        }).then(function (res) {
            $(".F6").hide();
            $scope.feeCustomerCount = res.data.data;
        }, function () {
            $(".F6").hide();
            recordAdded("Something Went Wrong!");
        });
    };


    //================================ Document Ready
    angular.element(document).ready(function () {
        $scope.GetCountTrainer();
        $scope.GetCountApp();
        $scope.GetCountGym();
        $scope.GetCountNeutrationApp();
        $scope.GetCountClass();
        $scope.GetCountCustomer();

        $scope.GetFeeCountTrainer();
        $scope.GetFeeCountApp();
        $scope.GetFeeCountGym();
        $scope.GetFeeCountNeutrationApp();
        $scope.GetFeeCountClass();
        $scope.GetFeeCountCustomer();
    });
}]);

function recordAdded(message) {
    if (message === "Saved Successfully!" || message === "Delete Successfully!" || message === "Remove Successfully!") {
        $.notify(" " + message, { type: "success", icon: "check", align: "right", verticalAlign: "top" });
    } else {
        $.notify(" " + message, { type: "success", icon: "close", align: "right", verticalAlign: "top", background: "#ffbebe", color: "#6a0000" });
    }
}