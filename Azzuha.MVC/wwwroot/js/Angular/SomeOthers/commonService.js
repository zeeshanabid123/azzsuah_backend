app.factory("commonService", ["$http", function ($http) {
    return {
        convertToDate: function (dateStr) {
            if (!dateStr) {
                return null;
            }
            if (dateStr instanceof Date) {
                return new Date(dateStr.getFullYear(), dateStr.getMonth(), dateStr.getDate(),
                            dateStr.getHours(),
                            dateStr.getMinutes(),
                            dateStr.getSeconds(),
                            dateStr.getMilliseconds()
                            );
            }

            var milli = dateStr.replace(/\/Date\((-?\d+)\)\//, '$1');
            var date = new Date(parseInt(milli));
            return date;
        },
        
    }
}
]);