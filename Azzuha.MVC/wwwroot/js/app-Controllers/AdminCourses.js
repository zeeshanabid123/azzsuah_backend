angular.module("fitCntr").requires.push('ui.bootstrap');
angular.module("fitCntr").requires.push('ui.bootstrap.datetimepicker');
angular.module("fitCntr").requires.push('ui.select');
angular.module("fitCntr").requires.push('ngQuill');
angular.module("fitCntr").requires.push('ngImgCrop');
angular.module("fitCntr").requires.push('angularUtils.directives.dirPagination');
app.controller("AdminCourses", ["$scope", "$http", "$timeout", "$filter", "scrutinyService", function ($scope, $http, $timeout, $filter, scrutinyService) {
    $scope.Register = "Register Now";
    $scope.RoledrpFlag = false;
    $scope.disable = true;
    $scope.course = {
        Id: "",
        courseName: "",
        courseDetail: "",
        courseImage: "",
        courseDuration: "",

        isEnabled: "",
        courseType: "",
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
        $scope.course = {
            Id: "",
            courseName: "",
            courseDetail: "",
            courseImage: "",
            courseDuration: "",
            isEnabled: "",
            courseType: "",
        };
        $scope.file = {
            Id: 0,
            a: null,
            ImageAlt: "",
            ImageUrl: "",
            Base64Image: ""

        };
        $("#detail").val("");
        quill.container.firstChild.innerHTML = "";
        $scope.message = "Add Course";
        $("#submit").prop('disabled', true);
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
        $scope.file.a = evt.currentTarget.files[0];
        var reader = new FileReader();
        reader.onload = function (evt) {
            $scope.$apply(function ($scope) {
                $scope.myImage = evt.target.result;
                $("#cropper-model").modal('show');
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
        $scope.imageBase64 = $scope.myCroppedImage;
        if ($scope.course.Id === "" || $scope.file.a !== null) {
            var blob = $scope.dataURItoBlob($scope.imageBase64);
            $scope.newimage = new File([blob], 'image.png');
        }

        if ($scope.file.a || $scope.imageBase64) {

            $scope.save($scope.file.a);
        }
    };
    $('#loading').attr('style', 'display: none  !important');
    $('#loadtable').attr('style', 'display: none  !important');


    $scope.save = function (file) {
        $('#loading').attr('style', '');
        $scope.course.courseDetail = $("#detail").val();
        var fd = new FormData();

        fd.append('file', $scope.file.a);
        fd.append('courseName', $scope.course.courseName);
        fd.append('Id', $scope.course.Id);
        fd.append('courseDetail', $scope.course.courseDetail);
        fd.append('base64', $scope.newimage);
        fd.append('courseImage', $scope.course.courseImage);
        fd.append('courseDuration', $scope.course.courseDuration);
        fd.append('courseType', $scope.course.courseType);

        $http({
            url: "/AdminCourses/SaveBlogs/",
            method: "Post",
            data: fd,
            processData: false,
            contentType: false,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (res) {
            if (res.data) {
                $scope.course = {
                    Id: "",
                    courseName: "",
                    courseDetail: "",
                    courseImage: "",
                    courseDuration: "",
                    isEnabled: "",
                    courseType: "",
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
                quill.container.firstChild.innerHTML = "";
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
            url: "/AdminCourses/GetBlogs/",
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
            url: "/AdminCourses/EditBlog/",
            method: "POST",
            data: fd,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (res) {
            $scope.message = "Edit Blog";
            $scope.course.Id = res.data.data.id;
            $scope.course.courseName = res.data.data.courseName;
            $scope.course.courseImage = res.data.data.courseImage;
            $scope.course.courseDuration = res.data.data.courseDuration;
            $scope.course.courseType = res.data.data.courseType;

            $scope.disable = false;
            $scope.course.courseDetail = res.data.data.courseDetail;
            quill.container.firstChild.innerHTML = $scope.course.courseDetail;
            $("#detail").val($scope.course.courseDetail);
            $scope.course.isEnabled = res.data.data.isEnabled;
            $('#loading').attr('style', 'display: none  !important');
            $("#submit").prop('disabled', false);
            $scope.myCroppedImage = res.data.data.courseImage;
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
            url: "/AdminCourses/PostStatus/",
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
            url: "/AdminCourses/DeleteBlog/",
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

var quill = new Quill('.quill-textarea', {
    theme: 'snow',
    modules: {
        imageResize: {
            displaySize: true
        },
        toolbar: [
            ['bold', 'italic', 'underline', 'strike'],        // toggled buttons
            ['blockquote', 'code-block'],

            [{ 'header': 1 }, { 'header': 2 }],               // custom button values
            [{ 'list': 'ordered' }, { 'list': 'bullet' }],
            [{ 'script': 'sub' }, { 'script': 'super' }],      // superscript/subscript
            [{ 'indent': '-1' }, { 'indent': '+1' }],          // outdent/indent
            [{ 'direction': 'rtl' }],                         // text direction

            [{ 'size': ['small', false, 'large', 'huge'] }],  // custom dropdown
            [{ 'header': [1, 2, 3, 4, 5, 6, false] }],

            [{ 'color': [] }, { 'background': [] }],          // dropdown with defaults from theme
            [{ 'font': [] }],
            [{ 'align': [] }],
            ['link', 'image', 'video'],
            ['clean']
        ]

    },
    placeholder: 'Add text in Editor...',
    imageHandler: imageHandler
});
quill.on('text-change', function (delta, oldDelta, source) {

    $('#detail').val(quill.container.firstChild.innerHTML);
});
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