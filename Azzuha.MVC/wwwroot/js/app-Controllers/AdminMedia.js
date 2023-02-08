angular.module("fitCntr").requires.push('ui.bootstrap');
angular.module("fitCntr").requires.push('ui.bootstrap.datetimepicker');
angular.module("fitCntr").requires.push('ui.select');
angular.module("fitCntr").requires.push('angularUtils.directives.dirPagination');
app.controller("AdminMedia", ["$scope", "$http", "$timeout", "$filter", "scrutinyService", function ($scope, $http, $timeout, $filter, scrutinyService) {
    $scope.Register = "Register Now";
    $scope.RoledrpFlag = false;
    $scope.disable = true;
    $scope.media = {
        Id: "",
        name: "",
        mediaTypeId: "",
        isAudio: false,
        fileUrl: "",
        isEnabled: false,
    };
    $scope.file = {
        Id: 0,
        a: null,
        ImageAlt: "",
        ImageUrl: "",
        Base64Image: ""

    };
    $scope.courseTypes = ["Quran Courses", "Workshops"];
    $scope.ShowModel = function () {
        $("#modal-default").modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        $scope.myCroppedImage = '';
        $scope.myImage = '';
        $('#icon12').attr('src', '');
        $scope.media = {
            Id: "",
            name: "",
            mediaTypeId: "",
            isAudio: false,
            fileUrl: "",
            isEnabled: false,
        };
        $scope.file = {
            Id: 0,
            a: null,
            ImageAlt: "",
            ImageUrl: "",
            Base64Image: ""

        };
        $("#detail").val("");
        $scope.message = "Add Media";
        $("#submit").prop('disabled', true);
        $timeout(function () { angular.element(document.querySelector('#fileInput')).on('change', handleFileSelect); }, 1000, false);
    }
    $scope.myCroppedImage = '';
    $scope.myImage = '';
    $scope.dateRang = {
        str: "",
        end: ""
    };

    $scope.dateOptions = {
        date: new Date()
    };
    $scope.format = "dd/MM/yyyy HH: mm";

    $scope.open1 = function () {
        $scope.popup1.opened = true;
    };

    $scope.findSearch = function () {

        $scope.GetData($scope.SearchFilter, $scope.skip, $scope.take);
    }
    $scope.popup1 = {
        opened: false
    };
    var handleFileSelect = function (evt) {
        debugger;
        $scope.file.a = evt.currentTarget.files[0];
        var reader = new FileReader();
        reader.onload = function (evt) {
            $scope.$apply(function ($scope) {
                $scope.myImage = evt.target.result;
            });
        };
        reader.readAsDataURL($scope.file.a);
        return true;
    };
    $timeout(function () { angular.element(document.querySelector('#fileInput')).on('change', handleFileSelect); }, 1000, false);


    $scope.currentPage = 1;
    $scope.pageSize = 10;


    $scope.submit = function () {
        debugger;
       
        $scope.save($scope.file.a);
       
    };
    $('#loading').attr('style', 'display: none  !important');
    $('#loadtable').attr('style', 'display: none  !important');


    $scope.save = function (file) {
        $('#loading').attr('style', '');
        var fd = new FormData();
        debugger;
        fd.append('file', $scope.file.a);
        fd.append('name', $scope.media.name);
        fd.append('Id', $scope.media.Id);
        fd.append('mediaTypeId', $scope.media.mediaTypeId);
        fd.append('base64', $scope.file.a);
        fd.append('fileUrl', $scope.media.fileUrl);
        fd.append('isAudio', $scope.media.isAudio);
        fd.append('isEnabled', $scope.media.isEnabled);
        $http({
            url: "/AdminMedia/SaveBlogs/",
            method: "Post",
            data: fd,
            processData: false,
            contentType: false,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (res) {
            if (res.data) {
                $scope.media = {
                    Id: "",
                    name: "",
                    mediaTypeId: "",
                    isAudio: false,
                    fileUrl: "",
                    isEnabled: false,
                };
                $scope.file = {
                    Id: 0,
                    a: null,
                    Base64Image: ""

                };
                $scope.myCroppedImage = '';
                $scope.myImage = '';
                $("#modal-default").modal("hide");
                $("#detail").val("");
                $("#submit").prop('disabled', true);
                $('#loading').attr('style', 'display: none  !important');
              
                $scope.blogForm.$setPristine();
                $scope.blogForm.$setUntouched();
                $scope.GetData($scope.SearchFilter, $scope.skip, $scope.take);
                recordAdded("Saved Successfully!", "success");
            } else {
                recordAdded(res.data.error, "success");
            }
        }, function (res) {
            recordAdded("Something Went Wrong!", "success");
        });
    }

    $scope.editImage = function () {
        $("#cropper-model").modal('show');
    };
    $scope.removeImage = function () {
        $scope.file = {
            Id: 0,
            a: null,
            Base64Image: ""

        };
        $scope.myCroppedImage = '';
        $scope.myImage = '';
        $("#fileInput").val("");
    }
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
            url: "/AdminMedia/GetBlogs/",
            method: "POST",
            data: fd,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (res) {
            $('#loadtable').attr('style', 'display: none  !important');
            $scope.blogslist = res.data.data;
            $scope.totalBlog = res.data.data.Count
        }, function (res) {

            recordAdded("Something Went Wrong!", "success");
        });
    }


    $scope.GetMediaType = function () {
        $('#loadtable').attr('style', '');
        $http({
            url: "/AdminMedia/GetMediaType/",
            method: "POST",
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (res) {
            debugger;
            $('#loadtable').attr('style', 'display: none  !important');
            $scope.courseTypes = res.data.data;
        }, function (res) {

            recordAdded("Something Went Wrong!", "success");
        });
    }
    $scope.dataURItoBlob = function (dataURI) {
        var byteString = atob(dataURI.toString().split(',')[1]);

        //var mimeString = dataURI.split(',')[0].split(':')[1].split(';')[0];

        var ab = new ArrayBuffer(byteString.length);
        var ia = new Uint8Array(ab);
        for (var i = 0; i < byteString.length; i++) {
            ia[i] = byteString.charCodeAt(i);
        }
        var blob = new Blob([ab], { type: 'image/png' }); //or mimeString if you want
        return blob;
    }

    $scope.GetEditData = function (i) {
        $('#loading').attr('style', '');
        $("#modal-default").modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var fd = new FormData();
        fd.append('id', i);
        $http({
            url: "/AdminMedia/EditBlog/",
            method: "POST",
            data: fd,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (res) {
            $scope.message = "Edit Blog";
            $scope.media.Id = res.data.data.id;
            $scope.media.name = res.data.data.name;
            $scope.media.mediaTypeId = res.data.data.mediaTypeId;
            $scope.media.isAudio = res.data.data.isAudio;
            $scope.media.fileUrl = res.data.data.fileUrl;
            $scope.media.isEnabled = res.data.data.isEnabled;

            $scope.disable = false;
            $('#loading').attr('style', 'display: none  !important');
            $("#submit").prop('disabled', false);
            $scope.myCroppedImage = res.data.data.fileUrl;
            $("#modal-default").modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });
        }, function () {
            recordAdded("Something Went Wrong!", "success");
        });
    };

    $scope.PostStatus = function (id, flag) {
        var fd = new FormData();
        fd.append('id', id);
        fd.append('flag', flag);

        $http({
            url: "/AdminMedia/PostStatus/",
            method: "Post",
            data: fd,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (res) {
            recordAdded("Saved Successfully!", "success");
            $scope.GetData($scope.SearchFilter, $scope.skip, $scope.take);
        }, function () {
            recordAdded("Something Went Wrong!", "success");
        });
    }

    $scope.DeleteBlog = function (id) {
        var fd = new FormData();
        fd.append('id', id);
        $http({
            url: "/AdminMedia/DeleteBlog/",
            method: "Post",
            data: fd,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (res) {
            $scope.GetData($scope.SearchFilter, $scope.skip, $scope.take);
        }, function () {
            recordAdded("Something Went Wrong!", "success");
        });

    };
    $scope.DeletePosts = function (id) {
        swal({
            title: 'Are You Sure!',
            text: 'Do you want to Delete it?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes!'
        }).then(function (result) {
            if (result.value) {
                $scope.DeleteBlog(id);
            }
        });
    };
    //================================ Document Ready
    angular.element(document).ready(function () {
        debugger;
        $scope.dateRang.end = new Date();
        $scope.dateRang.str = new Date();
        $scope.dateRang.str.setDate($scope.dateRang.str.getDate() - 30);
        //pagination
        $scope.GetData($scope.SearchFilter, $scope.skip, $scope.take);
        $scope.GetMediaType();

    });
    //Test


}]);

function recordAdded(message, type) {
    if (message === "Saved Successfully!" || message === "Delete Successfully!" || message === "Remove Successfully!") {
        $.notify(" " + message, { type: type, icon: "check", align: "right", verticalAlign: "top" });
    } else {
        $.notify(" " + message, { type: type, icon: "close", align: "right", verticalAlign: "top", background: "#ffbebe", color: "#6a0000" });
    }
}



function imageHandler(image, callback) {
    var data = new FormData();
    data.append('image', image);
    var xhr = new XMLHttpRequest();
    xhr.open('POST', IMGUR_API_URL, true);
    xhr.setRequestHeader('Authorization', 'Client-ID ' + IMGUR_CLIENT_ID);
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) {
            var response = JSON.parse(xhr.responseText);
            if (response.status === 200 && response.success) {
                callback(response.data.link);
            } else {
                var reader = new FileReader();
                reader.onload = function (e) {
                    callback(e.target.result);
                };
                reader.readAsDataURL(image);
            }
        }
    }
    xhr.send(data);
}