angular.module("fitCntr").requires.push('ui.bootstrap');
angular.module("fitCntr").requires.push('ui.bootstrap.datetimepicker');
angular.module("fitCntr").requires.push('ui.select');
angular.module("fitCntr").requires.push('angularUtils.directives.dirPagination');
app.controller("Users", ["$scope", "$http", "$timeout", "$filter", "scrutinyService", function ($scope, $http, $timeout, $filter, scrutinyService) {
    $scope.Register = "Register Now";
    $scope.Sdrp = {
        code: "+92",
        name: "United States",
        url: "~/img/Flags/US.png"
    };
    $scope.qAll = "";
    $scope.CountryCode = function () {
        $http({
            url: "/ManageRoles/GetCountryCode/",
            method: "Get",
        }).then(function (res) {
            $scope.drp = res.data.data;
        }, function (res) {
            recordAdded("Something Went Wrong!");
        });
    };

    $scope.init = function (roleId) {
        $scope.userRoleIdd = roleId;
    };


    $scope.GetRolesForRights = function () {
        $http({
            url: "/Account/GetRoles/",
            method: "Get"
        }).then(function (responce) {
            $scope.roleofrights = responce.data.res;
            $.each($scope.roleofrights, function (i) {
                if ($scope.roleofrights[i].Name === "SuperAdmin") {
                    $scope.roleofrights.splice(i, 1);
                    return false;
                }
            });
        });
    };

    $scope.GetRoles = function () {
        
        $http({
            url: "/Account/GetRoles/",
            method: "Get"
        }).then(function (res) {
            $scope.role = res.data.data;
            $.each($scope.role, function (i) {
                if ($scope.role[i].name === "SuperAdmin" || $scope.role[i].id === "210afb2f-62e8-4806-9572-b9274bcabd97") {
                    $scope.role.splice(i, 1);
                    return false;
                }
            });
        });
    };
    $scope.user = {
        Id: "",
        UserName: "",
        FullName: "",
        Designation: "",
        Email: "",
        PhoneNumber: "",
        Password: "",
        Role: "",
        EnableFlag: "",
    };

    $scope.Roles = {
        Name: "",
        Id: ""
    };
    /// Genrate Random Password 
    $scope.GenrateFalg = false;
    $scope.passGenrateer = function () {
        $scope.GenrateFalg = true;
        var newspassowrd = randomPass(7, true, true, true);
        $scope.Password = newspassowrd;

        $scope.myVar = newspassowrd;
    };
    ///
    $scope.save = function () {
        $scope.Register = "Register Now";

        var fd = new FormData();
        fd.append('UserName', $scope.user.UserName);
        fd.append('stringId', $scope.user.Id);

        fd.append('FullName', $scope.user.FullName);
        fd.append('Designation', $scope.user.Designation);
        fd.append('Email', $scope.user.Email);
      
        fd.append('Password', $scope.user.Password);
        fd.append('Role', $scope.user.Role);
        fd.append('EnableFlag', $scope.user.EnableFlag);
     
        if ($scope.user.Id === '' || $scope.user.Id === undefined) {
            var i = 1;
        } else if ($scope.user.Id !== '') {
            i = 2;
        }
        fd.append('id', i);
        $scope.user.PhoneNumber = $scope.Sdrp.code + "-" + $scope.user.PhoneNumber;
        fd.append('PhoneNumber', $scope.user.PhoneNumber);
        $("#hello-loader").show();
        $http({
            url: "/Account/RegisterUser/",
            method: "POST",
            data: fd,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
            //method: "POST",
            //data: { model: $scope.user, id: i }
        }).then(function (res) {
            $("#hello-loader").hide();
            $("#modal-lg").modal('hide');
            recordAdded("Saved Successfully!", "success");
            $scope.GetData($scope.SearchFilter, $scope.skip, $scope.take);
            $scope.user = {
                Id: "",
                UserName: "",
                FullName: "",
                Designation: "",
                Email: "",
                PhoneNumber: "",
                Password: "",
                Role: "",
                EnableFlag: "",
            };
            $scope.Sdrp = {
                code: "+1",
                Name: "United States",
                Url: "/Content/Flags/US.png"
            };
        }, function () {
            $("#hello-loader").hide();
            recordAdded("Something Went Wrong!", "success");
        });
    };

    $scope.GetEditUser = function (i) {
        $("#hello-loader").show();
        var xsrf = $.param({ id: i});
        $http({
            url: "/Account/GetEditUser/",
            method: "POST",
            data: xsrf,
             headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).then(function (res) {
            $("#hello-loader").hide();
            $scope.CountryCode();
            $scope.user.FullName = res.data.data.res.fullName;
            $scope.user.Id = res.data.data.res.id;

            $scope.user.Designation = res.data.data.res.designation;
            $scope.user.Email = res.data.data.res.email;
            $scope.user.PhoneNumber = parseFloat(res.data.data.res.phoneNumber);
            //$scope.user.PhoneNumber = res.data.data.res.phoneNumber;
            $scope.user.UserName = res.data.data.res.userName;
            $scope.user.Password = res.data.data.res.password;
            $scope.user.Role = res.data.data.res.role;

            
            $scope.Sdrp.code = res.data.data.drp.code;
            $scope.Sdrp.name = res.data.data.drp.name;
            $scope.Sdrp.url = res.data.data.drp.url;

           
            $scope.flguserbtn = true;
            $scope.flgemailbtn = true;
            $scope.Register = "Update User";
            $("#modal-lg").modal('show');
        }, function () {
            $("#hello-loader").hide();
            recordAdded("Something Went Wrong!", "success");
        });
    };

    $scope.Status = function (i, flg) {
        var xsrf1 = $.param({ id: i, flg: flg });
        $http({
            url: "/Account/ChangeStatus/",
            method: "POST",
            data: xsrf1,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).then(function (res) {
            recordAdded("Saved Successfully!", "success");
            $scope.GetData($scope.SearchFilter, $scope.skip, $scope.take);
        }, function () {
            recordAdded("Something Went Wrong!", "success");
        });
    };

    $scope.showAddUser = function () {
        $scope.frm.$setPristine();
        $scope.CountryCode();
        $scope.Sdrp = {
            code: "+1",
            name: "United States",
            url: "/Content/Flags/US.png"
        };
        $scope.Register = "Register Now";
        $("#p").val("");
        $scope.user = null;
        $scope.user = {
            Id: "",
            UserName: "",
            FullName: "",
            Designation: "",
            Email: "",
            PhoneNumber: "",
            Password: "",
            Role: "",
            EnableFlag: ""
        };
        $scope.GetRoles();
        $scope.flguserbtn = false;
        $scope.flgemailbtn = false;
        $("#modal-lg").modal('show');
        $scope.usernamealready = "none";
    };

    $scope.GetRolesList = function () {
        $http({
            url: "/ManageRoles/RolesView/",
            method: "Get"
        }).then(function (res) {
            $scope.RolesList = res.data.data;
        }, function () {
            recordAdded("Something Went Wrong!", "success");
        });
    };

    $scope.saveRole = function () {
        var xsrf23 = $.param({ Name: $scope.Roles.Name, id: $scope.Roles.Id  });
        if ($scope.Roles === null) {
            recordAdded("Role is Required", "success");
            return false;
        }
        if ($scope.Roles.Name === "") {
            recordAdded("Role is Required", "success");
            return false;
        }
        $("#hello-loader").show();
        $http({
            url: "/ManageRoles/SaveRole/",
            method: "POST",
            data: xsrf23,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
           
        }).then(function (res) {
            $("#hello-loader").hide();
            $scope.GetRolesList();
            $scope.Roles = {
                Name: "",
                Id: ""
            };
            if (res.data.data) {
                recordAdded("Saved Successfully!", "success");
            } else {
                recordAdded("Role Already Exist!", "success");
            }
        }, function () {
            $("#hello-loader").hide();
            recordAdded("Something Went Wrong!", "success");
        });
    };
    $scope.currentPage = 1;
    $scope.pageSize = 10;
    $scope.EditRole = function (i) {
        var xsrf1 = $.param({ nameParam: i});
        $http({
            url: "/ManageRoles/EditRole/",
            method: "POST",
            data: xsrf1,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).then(function (res) {
            $scope.Roles.Name = res.data.data.name;
            $scope.Roles.Id = res.data.data.id;

        }, function () {
            recordAdded("Something Went Wrong!", "success");
        });
    };

    $scope.DeleteRole = function (i) {
        var xsrf6 = $.param({ nameParam: i });
        $http({
            url: "/ManageRoles/deleteRole/",
            method: "POST",
            data: xsrf6,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).then(function (res) {
            $scope.GetRolesList();
            $scope.Roles = {
                Name: "",
                Id: ""
            };
            if (res.data.data) {
                recordAdded("Remove Successfully!", "success");
            } else {
                recordAdded("Ops! Sorry we cant Delete It. Some Data Against this role", "success");
            }
        }, function () {
            recordAdded("Something Went Wrong!", "success");
        });
    };

    $scope.showAddRole = function () {
        $scope.Roles = {
            Name: "",
            Id: ""
        };
        $scope.GetRolesList();
        $("#myModalRole").modal('show');
    };
    //////////////////////////////////////////////
    $scope.flag = "";

    $scope.Rights = {
        DashBorad: "",
        UserManager: "",
        AdminBlog: "",
        MarketingLibaray: "",
        BackupLog: "",
        EmailSubscription: ""
    };

    $scope.showAddrights = function () {
        $scope.Roles.Id = '-1';
        $scope.roleofrights = null;
        $scope.GetRolesForRights();
        $scope.flag = false;
        $("#myModalA").modal('show');
    };

    $('#myModalRole').on('hidden.bs.modal', function () {
        $scope.Roles.Id = '-1';
    });

    $('#myModalA').on('hidden.bs.modal', function () {
        $scope.Roles.Id = '-1';
    });


    $scope.GetRights = function (id) {
        var xsrf6 = $.param({ Role: id });
        $http({
            url: "/RightsManager/GetRigts/",
            method: "POST",
            data: xsrf6,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).then(function (res) {
            $scope.Rights = res.data.data;
        }, function () {
            recordAdded("Something Went Wrong!", "success");
        });
    };

    $scope.showRights = function () {
        if ($scope.Roles.Id !== "" && $scope.Roles.Id !== null) {
            $scope.GetRights($scope.Roles.Id);
            $scope.flag = true;
        } else {
            $scope.flag = false;
        }
    };

    $scope.saveRights = function (r_id) {
        var fd = new FormData();
        fd.append('saveroleid', $scope.Roles.Id);
        fd.append('saverightid', r_id);
        $http({
            url: "/RightsManager/SaveRight/",
            method: "POST",
            data: fd,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (res) {
            recordAdded("Saved Successfully!", "success");
        }, function () {
            recordAdded("Something Went Wrong!", "success");
        });
    };

    $scope.rmvRights = function (r_id) {
        var fd = new FormData();
        fd.append('Roleid', $scope.Roles.Id);
        fd.append('rightid', r_id);
        $http({
            url: "/RightsManager/RemoveRight/",
            method: "POST",
            data: fd,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (res) {
            recordAdded("Remove Successfully!", "success");
        }, function () {
            recordAdded("Something Went Wrong!", "success");
        });
    };

    $scope.brighSaveRights = function (r_id, flg) {
        if (flg === true) {
            $scope.saveRights(r_id);
        } else if (flg === false) {
            $scope.rmvRights(r_id);
        }
    };
    ///////////////////////////////////////////
    $scope.flguserbtn = "";
    $scope.flgemailbtn = "";


    $scope.CheckUserName = function (name) {
        if (name === undefined) {
            recordAdded("User Name is Required", "success");
            $scope.flguserbtn = false;
            return false;
        }
        if (name === "") {
            recordAdded("User Name is Required", "success");
            $scope.flguserbtn = false;
            return false;
        }
        if ($scope.user.Id !== undefined && $scope.user.Id !== "") {
            $scope.flguserbtn = true;
            return false;
        }
        $http({
            url: "/Account/UserName/",
            method: "POST",
            data: { Name: name }
        }).then(function (res) {
            if (res.data.data) {
                $scope.usernamealready = "none";
                $scope.flguserbtn = true;
            } else {
                $scope.usernamealready = "block";
                //recordAdded("User Name Already Exist", "success");
                $scope.flguserbtn = false;
            }
        }, function () {
            recordAdded("Something Went Wrong!", "success");
        });
    };

    $scope.CheckEmail = function (Email) {
        if (Email === undefined) {
            recordAdded("Email is Required", "success");
            $scope.flgemailbtn = false;
            return false;
        }
        if (Email === "") {
            recordAdded("Email is Required", "success");
            $scope.flgemailbtn = false;
            return false;
        }
        if ($scope.user.Id !== undefined && $scope.user.Id !== "") {
            $scope.flgemailbtn = true;
            return false;
        }
        $http({
            url: "/Account/EmailChk/",
            method: "POST",
            data: { email: Email }
        }).then(function (res) {
            if (res.data.data) {
                $scope.flgemailbtn = true;
            } else {
                recordAdded("Email Already Exist", "success");
                $scope.flgemailbtn = false;
            }
        }, function () {
            recordAdded("Something Went Wrong!", "success");
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
    $scope.RoledrpFlag = false;
    $scope.blogslist = [];
    $scope.SearchFilter = "";
    $scope.skip = 0;
    $scope.take = 50;
    $scope.totalBlog = 0;
    $scope.usersPerPage = 25;
    $scope.GetData = function (i, k, m) {
        $('#loadtable').attr('style', '');
        var fd = new FormData();
        fd.append('SearchFilter', i);
        fd.append('skip', k);
        fd.append('take', m);


        $http({
            url: "/Account/GetUserList/",
            method: "POST",
            data: fd,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (res) {
            $('#loadtable').attr('style', 'display: none  !important');
            $scope.blogslist = res.data.data;
        }, function (res) {
            recordAdded("Something Went Wrong!", "success");
        });
    }

    //================================ Document Ready
    angular.element(document).ready(function () {
        $scope.CountryCode();
        $scope.GetRoles();
        //pagination
        $scope.GetData($scope.SearchFilter, $scope.skip, $scope.take);

    });

}]);

function recordAdded(message, type) {
    if (message === "Saved Successfully!" || message === "Delete Successfully!" || message === "Remove Successfully!") {
        $.notify(" " + message, { type: type, icon: "check", align: "right", verticalAlign: "top" });
    } else {
        $.notify(" " + message, { type: type, icon: "close", align: "right", verticalAlign: "top", background: "#ffbebe", color: "#6a0000" });
    }
}