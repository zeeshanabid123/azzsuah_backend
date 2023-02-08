angular.module("fitCntr").requires.push('ui.bootstrap');
angular.module("fitCntr").requires.push('ui.bootstrap.datetimepicker');
angular.module("fitCntr").requires.push('ui.select');
app.controller("AdminUserProfile", ["$scope", "$http", "$timeout", "$filter", "commonService", function ($scope, $http, $timeout, $filter, commonService) {


    $scope.init = function (id,profileId) {
        $scope.userId = id;
        $scope.profileTypeId = profileId
    };
    $scope.convertToDate = function (data) {
        return commonService.convertToDate(data);
    };

    $scope.GetProfileData = function () {
        var xsrf1 = $.param({ userId: $scope.userId, profileId: $scope.profileTypeId });
        $http({
            url: "/UserProfile/GetProfileDetail/",
            method: "POST",
            data: xsrf1,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).then(function (res) {
            $scope.detail = res.data.data;
            console.log($scope.detail);
            $scope.GetAllMessageofThread($scope.userId);
        }, function () {
            recordAdded("Something Went Wrong!", "success");
        });
    };

    $scope.GetAllMessageofThread = function () {
        var xsrf1 = $.param({ userId: $scope.userId});
        $http({
            url: "/UserProfile/GetMessageThread/",
            method: "POST",
            data: xsrf1,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).then(function (res) {
            $scope.allthread = res.data.data;
        }, function () {
            recordAdded("Something Went Wrong!", "success");
        });
    };

    $scope.GetAllMessageofUser = function (threadId, friendId) {
        debugger
        ;
        var xsrf1 = $.param({ userId: $scope.userId, thiredId: threadId, friendId: friendId });
        $http({
            url: "/UserProfile/GetMessageAll/",
            method: "POST",
            data: xsrf1,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).then(function (res) {
            debugger;
            $scope.allmessages = res.data.data;
         
            $("#modal-lg").modal('show');
        }, function () {
            recordAdded("Something Went Wrong!", "success");
        });
    };
    $scope.Warning = function (i) {
        swal({
            title: 'Are You Sure!',
            text: 'Do you want to remove it?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes!'
        }).then(function (result) {
            if (result.value) {
                $scope.DeleteRole(i);
            }
        });
    };

    //================================ Document Ready
    angular.element(document).ready(function () {
        $scope.GetProfileData();
    });

}]);

function recordAdded(message, type) {
    if (message === "Saved Successfully!" || message === "Delete Successfully!" || message === "Remove Successfully!") {
        $.notify(" " + message, { type: type, icon: "check", align: "right", verticalAlign: "top" });
    } else {
        $.notify(" " + message, { type: type, icon: "close", align: "right", verticalAlign: "top", background: "#ffbebe", color: "#6a0000" });
    }
}