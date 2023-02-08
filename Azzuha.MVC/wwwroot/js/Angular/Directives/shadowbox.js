app.directive('shadowbox', function () {
        return {
            // here you can style the link/thumbnail/etc.
            template: '<a ng-click="openShadowbox()">{{imageName}}</a>',
            scope: {
                imageName: '@name',
                imageUrl: '@url'
            },
            link: function (scope, element, attrs) {

                // the function that is called from the template:
                scope.openShadowbox = function () {

                    // see Shadowbox documentation on what to write here.
                    // Example from the documentation:

                    Shadowbox.open({
                        content: '<div id="welcome-msg">Welcome to my website!</div>',
                        player: "html",
                        title: "Welcome",
                        height: 350,
                        width: 350
                    });
                };

            }
        };
    });