
app.factory("GetService", ["$http", function ($http) {

    return {
        loadForwardedCases: function (data, str, end, url, profileTypeid) {
            if (str instanceof Date) {
                str = str.toDateString();
            }
            if (end instanceof Date) {
                end = end.toDateString();
            }
            var xsrf = $.param({ page: data, str: str, endd: end, profileTypeId: profileTypeid });
            return $http({
                url: url,
                method: "POST",
                data: xsrf,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).then(function (res) {
                return res.data;
            });
        },
        getUnsubsribedList: function (data, str, end, url) {
            if (str instanceof Date) {
                str = str.toDateString();
            }
            if (end instanceof Date) {
                end = end.toDateString();
            }
            var xsrf = $.param({ model: data, str: str, endd: end });
            return $http({
                url: url,
                method: "POST",
                data: xsrf,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).then(function (res) {
                return res.data;
            });
        },
        getFeedbackList: function (data, str, end, url, isBlogSuggestion) {
            if (str instanceof Date) {
                str = str.toDateString();
            }
            if (end instanceof Date) {
                end = end.toDateString();
            }
            var xsrf = $.param({ model: data, str: str, endd: end, isBlogSuggestion: isBlogSuggestion });
            return $http({
                url: url,
                method: "POST",
                data: xsrf,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).then(function (res) {
                return res.data;
            });
        },
        getVouchers: function (data, str, end, url) {
            if (str instanceof Date) {
                str = str.toDateString();
            }
            if (end instanceof Date) {
                end = end.toDateString();
            }
            var xsrf = $.param({ model: data, str: str, endd: end });
            return $http({
                url: url,
                method: "POST",
                data: xsrf,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).then(function (res) {
                return res.data;
            });
        },

        changeStatusVoucher: function (id, isActive) {
            var xsrf = $.param({ id: id, isActive: isActive });
            return $http({
                url: "/Voucher/ControlActivation",
                method: "POST",
                data: xsrf,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).then(function (res) {
                return res.data;
            });
        },
        createVoucher: function (data) {
            if (data.Expiry instanceof Date) {
                data.Expiry = data.Expiry.toDateString();
            }

            var xsrf = $.param({ request: data });
            return $http({
                url: "/Voucher/Create",
                method: "POST",
                data: xsrf,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).then(function (res) {
                return res.data;
            });
        },
        isCodeExist: function (data) {
            var xsrf = $.param({ code: data });
            return $http({
                url: "/Voucher/IsCodeExist",
                method: "POST",
                data: xsrf,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).then(function (res) {
                return res.data;
            });
        },
        getClientMonthlyReports: function (data, str, end, url) {
            if (str instanceof Date) {
                str = str.toDateString();
            }
            if (end instanceof Date) {
                end = end.toDateString();
            }
            var xsrf = $.param({ model: data, str: str, endd: end });
            return $http({
                url: url,
                method: "POST",
                data: xsrf,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).then(function (res) {
                return res.data;
            });
        },
    }
}]);