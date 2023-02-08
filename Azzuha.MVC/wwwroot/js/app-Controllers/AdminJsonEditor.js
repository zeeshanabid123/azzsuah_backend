angular.module("fitCntr").requires.push('ui.bootstrap');
angular.module("fitCntr").requires.push('json-tree');

app.controller("AdminJsonEditor", ["$scope", "$http", "$timeout", "$filter", function ($scope, $http, $timeout, $filter) {
    $scope.Register = "Register Now";
    $scope.RoledrpFlag = false;
    $scope.disable = true;

    $scope.jsonObject = {
        Id: "",
        text: "",
        isEnabled: "",
    }
    $scope.stringFyJson = "";
    $scope.jsonData = defaultData();

    $scope.refresh = function () {
        $scope.jsonData = defaultData();
        $scope.nodeOptions.refresh();
    }

    function defaultData() {
        return {
            degressProgram: [{
                topHeading: "",
                topSubheading: "",
                secondTopHeading: "",
                secondTopSubHeading: "",
                subjects: [{
                    year: "",
                    studysubjects: ["", "", ""]
                },
                   {
                        year: "",
                        studysubjects: ["", "", ""]
                    },
                        {
                            year: "",
                            studysubjects: ["", "", ""]
                        },
                            {
                                year: "",
                                studysubjects: ["", "", ""]
                            },  {
                                year: "",
                                studysubjects: ["", "", ""]
                            },
                                 {
                                    year: "",
                                    studysubjects: ["", "", ""]
                                },  {
                                    year: "",
                                    studysubjects: ["", "", ""]
                                }
                ]
            },
                {
                    topHeading: "",
                    topSubheading: "",
                    secondTopHeading: "",
                    secondTopSubHeading: "",
                    subjects: [{
                        year: "",
                        studysubjects: ["", "", ""]
                    },
                        {
                            year: "",
                            studysubjects: ["", "", ""]
                        }
                    ]
                },
            ]
        };
    };
    $scope.submit = function () {
        debugger;
        $scope.save();
    };


    $scope.save = function () {
        debugger;
        $('#loading').attr('style', '');
        $scope.stringFyJson = JSON.stringify($scope.jsonData)
        var fd = new FormData();
        fd.append('DataHeadingJson', $scope.stringFyJson);
        fd.append('Id', $scope.jsonObject.Id);
        fd.append('isEnabled', true);
        $http({
            url: "/JsonEditor/SaveBlogs/",
            method: "Post",
            data: fd,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (res) {
            if (res.data) {
            

                $("#detail").val("");
                $("#submit").prop('disabled', true);
                $('#loading').attr('style', 'display: none  !important');
               
                $scope.blogForm.$setPristine();
                $scope.blogForm.$setUntouched();
                recordAdded("Saved Successfully!", "success");
                $scope.GetEditData("F5D3C21D-5311-4458-B979-FCA52F2B61D8");
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
            url: "/JsonEditor/EditJsonData/",
            method: "POST",
            data: fd,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (res) {
            debugger;
            $scope.jsonObject.Id = res.data.data.id;
            $scope.jsonObject.text = res.data.data.dataHeadingJson;
            $scope.jsonData = JSON.parse($scope.jsonObject.text);
            $scope.jsonObject.isEnabled = res.data.data.isEnabled;

            $('#loading').attr('style', 'display: none  !important');
            $("#submit").prop('disabled', false);
        }, function () {
            recordAdded("Something Went Wrong!", "success");
        });
    };

    //================================ Document Ready
    angular.element(document).ready(function () {
        $scope.GetEditData("F5D3C21D-5311-4458-B979-FCA52F2B61D8");
    })
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