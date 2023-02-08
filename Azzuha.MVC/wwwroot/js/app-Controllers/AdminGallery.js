angular.module("fitCntr").requires.push('ui.bootstrap');
angular.module("fitCntr").requires.push('angularUtils.directives.dirPagination');
app.controller("AdminGallery", ["$scope", "$http", "$timeout", "$filter", "scrutinyService", function ($scope, $http, $timeout, $filter, scrutinyService) {
    $scope.Register = "Register Now";
    $scope.RoledrpFlag = false;
    $scope.disable = true;
    $scope.gallery = {
        Id: "",
        name: "",
        isEnabled: "",
    };
    $scope.galleryImage={
        a: null
    }
  

    $scope.ShowModel = function () {
        $("#modal-default").modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        $('#icon12').attr('src', '');
        $scope.gallery = {
            Id: "",
            name: "",
            isEnabled: "",
        };
      
        $scope.message = "Add Gallery";
        $("#submit").prop('disabled', true);
    }
    $scope.dateRang = {
        str: "",
        end: ""
    };

    $scope.dateOptions = {
        date: new Date()
    };
    
    $scope.findSearch = function () {

        $scope.GetData($scope.SearchFilter, $scope.skip, $scope.take);
    }
    $scope.popup1 = {
        opened: false
    };



    $scope.currentPage = 1;
    $scope.pageSize = 10;


    $scope.submit = function () {
        debugger;

            $scope.save();
    };
    $('#loading').attr('style', 'display: none  !important');
    $('#loadtable').attr('style', 'display: none  !important');


    $scope.save = function () {
        debugger;
        $('#loading').attr('style', '');
        var fd = new FormData();
        fd.append('name', $scope.gallery.name);
        fd.append('Id', $scope.gallery.Id);
        fd.append('isEnabled', $scope.gallery.isEnabled);
        for (var i = 0; i != $scope.galleryImage.a.length; i++) {
            fd.append("files", $scope.galleryImage.a[i]);
        }
        $http({
            url: "/AdminGallery/SaveGallery/",
            method: "Post",
            data: fd,
            processData: false,
            contentType: false,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (res) {
            if (res.data) {
                $scope.gallery = {
                    Id: "",
                    name: "",
                    isEnabled: "",
                };
              
                $("#modal-default").modal("hide");
                $("#submit").prop('disabled', true);
                $('#loading').attr('style', 'display: none  !important');
                $scope.galleryForm.$setPristine();
                $scope.galleryForm.$setUntouched();
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

    $scope.galleryslist = [];
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
            url: "/AdminGallery/GetAdminGallery/",
            method: "POST",
            data: fd,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (res) {
            $('#loadtable').attr('style', 'display: none  !important');
            $scope.galleryslist = res.data.data;
            $scope.totalBlog = res.data.data.Count
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
            url: "/AdminGallery/EditGallery/",
            method: "POST",
            data: fd,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (res) {
            $scope.message = "Edit Gallery";
            $scope.gallery.Id = res.data.data.id;
            $scope.gallery.name = res.data.data.name;
            $scope.galleryImage.a = res.data.data.galleryImagesmodel;
          
            $scope.disable = false;
            $scope.gallery.isEnabled = res.data.data.isEnabled;
            $('#loading').attr('style', 'display: none  !important');
            $("#submit").prop('disabled', false);
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
            url: "/AdminGallery/PostStatus/",
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
            url: "/AdminGallery/DeleteGallery/",
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

        $scope.dateRang.end = new Date();
        $scope.dateRang.str = new Date();
        $scope.dateRang.str.setDate($scope.dateRang.str.getDate() - 30);
        //pagination
        $scope.GetData($scope.SearchFilter, $scope.skip, $scope.take);

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


app.directive("imgUpload", function ($http, $compile) {
    return {
        restrict: 'AE',
        scope: {
            url: "@",
            method: "@",
            files: "=model",
        },
        template: '<input class="fileUpload" type="file" multiple />' +
            '<div class="dropzone">' +
            '<p class="msg">Click or Drag and Drop files to upload</p>' +
            '</div>' +
            '<div class="preview clearfix">' +
            '<div class="previewData clearfix" ng-repeat="data in previewData track by $index">' +
            '<img src={{data.src}}></img>' +
            '<div class="previewDetails">' +
            '<div class="detail"><b>Name : </b>{{data.name}}</div>' +
            '<div class="detail"><b>Type : </b>{{data.type}}</div>' +
            '<div class="detail"><b>Size : </b> {{data.size}}</div>' +
            '</div>' +
            '<div class="previewControls">' +
            '<span ng-click="upload(data)" class="circle upload">' +
            '<i class="fa fa-check"></i>' +
            '</span>' +
            '<span ng-click="remove(data)" class="circle remove">' +
            '<i class="fa fa-close"></i>' +
            '</span>' +
            '</div>' +
            '</div>' +
            '</div>'+
          
            '<div class="preview clearfix" ng-if="oldPreview.length>0">' +
            '<div class="previewData clearfix" ng-repeat="data1 in oldPreview track by $index">' +
            '<img src={{data1.galleryImageUrl}}></img>' +
            '<div class="previewControls">' +
            '<span ng-click="removeOld(data1.id)" class="circle remove">' +
            '<i class="fa fa-close"></i>' +
            '</span>' +
            '</div>' +
            '</div>' +
            '</div>'
        ,
        link: function (scope, elem, attrs) {
            var formData = new FormData();
            scope.previewData = [];
            scope.oldPreview = [];
            scope.files = "";

            function previewFile(file) {
                var reader = new FileReader();
                var obj = new FormData().append('file', file);
                reader.onload = function (data) {
                    var src = data.target.result;
                    var size = ((file.size / (1024 * 1024)) > 1) ? (file.size / (1024 * 1024)) + ' mB' : (file.size / 1024) + ' kB';
                    scope.$apply(function () {
                        scope.previewData.push({
                            'name': file.name, 'size': size, 'type': file.type,
                            'src': src, 'data': obj
                        });
                    });
                    console.log(scope.previewData);
                }
                reader.readAsDataURL(file);
            }

            function uploadFile(e, type) {
                debugger;
                e.preventDefault();
                var files = "";
                scope.files = "";
                if (type == "formControl") {
                    files = e.target.files;
                    scope.files = e.target.files;

                } else if (type === "drop") {
                    files = e.originalEvent.dataTransfer.files;
                    scope.files = e.originalEvent.dataTransfer.files;

                }
                for (var i = 0; i < files.length; i++) {
                    var file = files[i];
                    if (file.type.indexOf("image") !== -1) {
                        debugger;
                        previewFile(file);
                    } else {
                        alert(file.name + " is not supported");
                    }
                }
            }
            elem.find('.fileUpload').bind('change', function (e) {
                uploadFile(e, 'formControl');
            });

            elem.find('.dropzone').bind("click", function (e) {
                $compile(elem.find('.fileUpload'))(scope).trigger('click');
            });

            elem.find('.dropzone').bind("dragover", function (e) {
                e.preventDefault();
            });

            elem.find('.dropzone').bind("drop", function (e) {
                uploadFile(e, 'drop');
            });
            scope.upload = function (obj) {
                $http({
                    method: scope.method, url: scope.url, data: obj.data,
                    headers: { 'Content-Type': undefined }, transformRequest: angular.identity
                }).success(function (data) {

                });
            }
            scope.$watch('files', function (value) {
                if (scope.oldPreview.length > 0) {
                    scope.oldPreview = scope.oldPreview;
                }
                else if (value[0].size<=0) {
                    scope.oldPreview = value;
                }
             
            });
            scope.remove = function (data) {
                var index = scope.previewData.indexOf(data);
                scope.previewData.splice(index, 1);
            }
            scope.removeOld = function (data) {
                var fd = new FormData();
                fd.append('id', data);
                $http({
                    url: "/AdminGallery/DeleteGalleryImage/",
                    method: "Post",
                    data: fd,
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                }).success(function (data) {

                    if (data) {
                        var index = scope.oldPreview.indexOf(data);
                        scope.oldPreview.splice(index, 1);
                    }
                });
            }
        }
    }
});