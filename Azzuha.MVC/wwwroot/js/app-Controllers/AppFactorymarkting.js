app.factory("marktingservice", ["$http", function ($http) {

    return {
        loadForwardedCases: function (data, str, end, url, profileTypeid, pId, isused) {
            if (str instanceof Date) {
                str = str.toDateString();
            }
            if (end instanceof Date) {
                end = end.toDateString();
            }
            var xsrf = $.param({ page: data, str: str, endd: end, profileTypeId: profileTypeid, pId: pId,Isused:isused });
            return $http({
                url: url,
                method: "POST",
                data: xsrf,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).then(function (res) {
                return res.data;
            });
        },


        loadForwardedCases1: function (data, str, end, url, status) {
            if (str instanceof Date) {
                str = str.toDateString();
            }
            if (end instanceof Date) {
                end = end.toDateString();
            }
            var xsrf = $.param({ page: data, str: str, endd: end, status: status });
            return $http({
                url: url,
                method: "POST",
                data: xsrf,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).then(function (res) {
                return res.data;
            });
        }
    }
}]);