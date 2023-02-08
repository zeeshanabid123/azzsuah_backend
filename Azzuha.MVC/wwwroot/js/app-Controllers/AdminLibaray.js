angular.module("fitCntr").requires.push('ui.bootstrap');
angular.module("fitCntr").requires.push('ui.bootstrap.datetimepicker');
angular.module("fitCntr").requires.push('ui.select');
app.controller("AdminLibaray", ["$scope", "$http", "$timeout", "$filter", "marktingservice", "commonService", function ($scope, $http, $timeout, $filter, marktingservice, commonService) {


    $scope.profileTypeId = [];
    $scope.pId = 0;
    $scope.Isused = false;

    $scope.usedUnuseed = function (i) {
        $scope.pId = i;
        $scope.loadAllCases();
    };
    $scope.CheckBoxSearchPhoto = function (data1,PId) {
        $scope.showloader = true;
        $scope.profileTypeId.push(data1.profileTypeId);
        $scope.pId = PId;
        if (data1.id === false) {
            $scope.profileTypeId = _.without($scope.profileTypeId, data1.profileTypeId);

        }
        $scope.loadAllCases();
    };
    $scope.onTabChange = function (i) {
        $scope.pId = i;
        $scope.loadAllCases();
    }
    $scope.CheckBoxSearchvideo = function (video, PId) {
        $scope.showloader = true;
        $scope.profileTypeId.push(video.profileTypeId);
        $scope.pId = PId;
        if (video.idvideo === false) {
            $scope.profileTypeId = _.without($scope.profileTypeId, video.profileTypeId);

        }
        $scope.loadAllCases();
    };
    $scope.popUpImage = "";
    $scope.profilePId = "";
    $scope.videoUrl = "";
    $scope.open = function (i) {
        debugger;
        $scope.popUpImage = "";
        $scope.profilePId = "";
        $scope.videoUrl = "";
    
        $scope.popUpImage = i.thumbnailUrl;
        $scope.profilePId = i.pid;
        $scope.videoUrl = i.videoUrl;
        $("#modal-default").modal('show');
    };
    $scope.getdata = function (profile, PId) {
        $scope.showloader = true;
        $scope.profileTypeId.push(profile.profileTypeId);
        $scope.pId = PId;
        if (profile.idpro === false) {
            $scope.profileTypeId = _.without($scope.profileTypeId, profile.profileTypeId);

        }
        $scope.loadAllCases();
    };

    $scope.checkTrue = function (itm) {
        debugger;
        var xsrf1 = $.param({ Id: itm.tableId, pid: itm.pid });
        $http({
            url: "/MarketingLibaray/UpdateIsUsedStatus/",
            method: "POST",
            data: xsrf1,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).then(function (res) {
            recordAdded("Saved Successfully!", "success");
       
        }, function () {
            recordAdded("Something Went Wrong!", "success");
        });
    }
    $scope.CheckBoxSearchfree = function (free, PId) {
        $scope.showloader = true;
        $scope.profileTypeId.push(free.profileTypeId);
        $scope.pId = PId;
        if (free.idfree === false) {
            $scope.profileTypeId = _.without($scope.profileTypeId, free.profileTypeId);

        }
        $scope.loadAllCases();
    };
    //============================ Pagination Start
    $scope.forwardedSearch = "";

    $scope.forwardedPagination = { Draw: 0, Page: 1, PageSize: 9, TotalRecords: 0, Search: "", SortByFieldName: "", SortBy: 'asc' };

    $scope.forwardedCases = [];


    //===================================== Load Cases

    $scope.findForwardedCases = function () {
        $scope.forwardedPagination = { Draw: 0, Page: 1, PageSize: 9, TotalRecords: 0, Search: "", SortByFieldName: "", SortBy: 'asc' };
        $scope.forwardedPagination.Search = $scope.forwardedSearch;
        $scope.loadForwardedCases();
    };
    $scope.dateRang = {
        str: "",
        end: ""
    };

    $scope.loadForwardedCases = function () {
        //App.startPageLoading({ message: 'Please wait...' });
        $("#Userslistloader").show();
        marktingservice.loadForwardedCases(setPaginationOffset($scope.forwardedPagination), $scope.dateRang.str, $scope.dateRang.end, "/MarketingLibaray/GetData", $scope.profileTypeId, $scope.pId, $scope.Isused).then(function (data) {
            $scope.forwardedPagination.TotalRecords = data.data.totalRecords;
            $("#loader").hide();
            $scope.forwardedCases = data.data.entities;
            console.log($scope.forwardedCases)
           
        });
    }

    $scope.loadAllCases = function () {
        $scope.loadForwardedCases();

    }

    // =============================== End Cases
    // =============================== Sorting techniques

    $scope.sortByForwardedCases = function (index) {
        $scope.forwardedPagination = { Draw: 0, Page: 1, PageSize: 10, TotalRecords: 0, Search: "", SortIndex: index, SortBy: $scope.forwardedPagination.SortBy };
        $scope.forwardedPagination.SortBy = $scope.forwardedPagination.SortBy === "asc" ? "desc" : "asc";
        $scope.loadForwardedCases();
    }

    $scope.getClassForwardedCases = function (index) {
        if ($scope.forwardedPagination.SortIndex === index) {
            return $scope.forwardedPagination.SortBy === "asc" ? "fa fa-caret-up" : "fa fa-caret-down";
        }
        return '';
    }
    // End Sorting

    // Local Functions
    function setPaginationOffset(paginationObj) {
        var pagination = _.clone(paginationObj);
        pagination.Page = (pagination.Page - 1) * pagination.PageSize;
        return pagination;
    }



    //Convert Date
    $scope.convertToDate = function (data) {
        return commonService.convertToDate(data);
    };






    $scope.GetPTypes = function () {
        $http({
            url: "/MarketingLibaray/GetProfileType/",
            method: "Get",
        }).then(function (res) {
            $scope.PtypesCount = res.data.data;
        }, function () {
            recordAdded("Something Went Wrong!");
        });
    };

    $scope.GetFreeClasses = function () {
        $http({
            url: "/MarketingLibaray/GeFreeClassesCate/",
            method: "Get",
        }).then(function (res) {
            $scope.Freeclasses = res.data.data;
        }, function () {
            recordAdded("Something Went Wrong!");
        });
    };




    ////////////////////////////////////Warning
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
        $scope.GetPTypes();
        $scope.GetFreeClasses();
        //pagination
        $scope.loadAllCases();
    });

}]);

function recordAdded(message, type) {
    if (message === "Saved Successfully!" || message === "Delete Successfully!" || message === "Remove Successfully!") {
        $.notify(" " + message, { type: type, icon: "check", align: "right", verticalAlign: "top" });
    } else {
        $.notify(" " + message, { type: type, icon: "close", align: "right", verticalAlign: "top", background: "#ffbebe", color: "#6a0000" });
    }
}

app.filter("trustUrl", function ($sce) {
    return function (Url) {
        console.log(Url);
        return $sce.trustAsResourceUrl(Url);
    };
});