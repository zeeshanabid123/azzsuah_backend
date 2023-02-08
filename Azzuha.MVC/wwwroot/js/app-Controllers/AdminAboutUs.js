angular.module("fitCntr").requires.push('ui.bootstrap');
angular.module("fitCntr").requires.push('summernote');
app.controller("AdminAboutUs", ["$scope", "$http", "$timeout", "$filter", "scrutinyService", function ($scope, $http, $timeout, $filter, scrutinyService) {
    $scope.Register = "Register Now";
    $scope.RoledrpFlag = false;
    $scope.disable = true;
    $scope.aboutUs = {
        Id: "",
        text: "",
        isEnabled: "",
    };
    $scope.text = "<h3>This is an Air-mode editable area.</h3>";
    $scope.submit = function () {
       
            $scope.save();
    };

    $scope.save = function () {
        debugger;
        $('#loading').attr('style', '');
        $scope.aboutUs.text = $scope.text;
        var fd = new FormData();
        fd.append('Text', $scope.aboutUs.text);
        fd.append('Id', $scope.aboutUs.Id);
        fd.append('isEnabled', $scope.aboutUs.isEnabled);
        $http({
            url: "/AboutUs/SaveAboutUs/",
            method: "Post",
            data: fd,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (res) {
            if (res.data) {
                $scope.aboutUs = {
                    Id: "",
                    text: "",
                    isEnabled: "",
                };
               
                $("#detail").val("");
                $("#submit").prop('disabled', true);
                $('#loading').attr('style', 'display: none  !important');
        /*        quill.container.firstChild.innerHTML = "";*/
                $scope.blogForm.$setPristine();
                $scope.blogForm.$setUntouched();
                recordAdded("Saved Successfully!", "success");
                $scope.GetEditData("920682BC-CD2C-4FD4-8142-ED86B0C43B28");
            } else {
                recordAdded(res.data.error, "success");
            }
        }, function (res) {
            recordAdded("Something Went Wrong!", "success");
        });
    }


    $scope.GetEditData = function (i) {
        $('#loading').attr('style', '');

        var fd = new FormData();
        fd.append('id', i);
        $http({
            url: "/AboutUs/EditAboutUs/",
            method: "POST",
            data: fd,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (res) {
            $scope.message = "Edit Blog";
            $scope.aboutUs.Id = res.data.data.id;
            $scope.disable = false;

            $scope.aboutUs.text = res.data.data.text;
            $scope.text = $scope.aboutUs.text;
          /*  quill.container.firstChild.innerHTML = $scope.aboutUs.text;*/
            $("#detail").val($scope.aboutUs.text);
            $scope.aboutUs.isEnabled = res.data.data.isEnabled;
            $('#loading').attr('style', 'display: none  !important');
            $("#submit").prop('disabled', false);
        }, function () {
            recordAdded("Something Went Wrong!", "success");
        });
    };


   
    //================================ Document Ready
    angular.element(document).ready(function () {
        $scope.GetEditData("920682BC-CD2C-4FD4-8142-ED86B0C43B28");
    });
    //Test


}]);

app.directive("summerNote", function () {
    return {

        link: function (scope, el, attr) {

            el.summernote({
                height: 300,        // set editor height
                minHeight: null,    // set minimum height of editor
                maxHeight: null,    // set maximum height of editor
                focus: false        // set focus to editable area after initializing summernote
            });

            var str = "asd asd asd a s akfjhkjasdhkj hskdjfh ashdfjkash jkasdh fjashdf lkjhasdj hfjkashdflkja hsdjkfa sld haskjd fhlakshd fkjsahldkf haskjd";
            el.summernote("code", str);

            el.summernote("justifyCenter");
        }
    };
});
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