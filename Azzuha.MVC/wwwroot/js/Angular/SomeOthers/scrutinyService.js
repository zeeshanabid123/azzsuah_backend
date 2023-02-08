app.factory("scrutinyService", ["$http", function ($http) {
    return {
        loadForwardedCases: function (data, str, end, url) {
            if (str instanceof Date) {
                str = str.toDateString();
            }
            if (end instanceof Date) {
                end = end.toDateString();
            }
            var url = url;
            return $http({
                url: url,
                method: "POST",
                data: { page: data, str: str, endd: end }
            }).then(function (res) {
                return res.data;
            });
        },
        loadForwardedCases2: function (data, str, end, url, flg) {
            if (str instanceof Date) {
                str = str.toDateString();
            }
            if (end instanceof Date) {
                end = end.toDateString();
            }
            var url = url;
            return $http({
                url: url,
                method: "POST",
                data: { page: data, str: str, endd: end, flag: flg }
            }).then(function (res) {
                return res.data;
            });
        }
    };
}]);