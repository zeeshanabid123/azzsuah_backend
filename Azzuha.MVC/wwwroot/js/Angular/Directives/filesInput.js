app.directive("filesInput", ["$http", function ($http) {
    return {
        require: "ngModel",
        link: function postLink(scope, elem, attrs, ngModel) {
            elem.on("change", function (e) {
                if (elem[0].files.length > 0 && attrs.multiple) {
                    ngModel.$setViewValue(elem[0].files);
                    return;
                }
                var file = elem[0].files[0];
                ngModel.$setViewValue(file);

                //var reader = new FileReader();
                //if (document.getElementById(attrs.filesImageId)) {
                //    reader.onload = function () {
                //        var dataUrl = reader.result;
                //        var output = document.getElementById(attrs.filesImageId);
                //        if (output) {
                //            output.src = dataUrl;
                //        }
                //    };
                //}
                //reader.readAsDataURL(file);
                var myScope = scope;
                if (!(attrs.saveonchange && attrs.saveonchangeurl)) return;
                var fd = new FormData();
                fd.append('file', file);
                if (attrs.saveonchangemodel) {
                    var result = JSON.parse(attrs.saveonchangemodel);
                    result.Id = attrs.lstid;
                    fd.append('model', JSON.stringify(result));
                }
                $http({
                    url: attrs.saveonchangeurl,
                    method: "POST",
                    data: fd,
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                }).then(function (res) {
                    recordAdded("Saved Successfully!", "success");
                }, function () {
                    recordAdded("Something Went Wrong!", "success");
                });
            });
        }
    }
}]);