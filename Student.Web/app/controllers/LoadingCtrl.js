(function () {
    var studentApp = angular.module('studentApp');

    studentApp.controller('loadingCtrl', function ($scope, $rootScope, $location, $modal, $loading) {
        //$rootScope.$on("$routeChangeError", function (event, current, previous, rejection) {
        //    alert("ROUTE CHANGE ERROR: " + rejection);
        //    $scope.active = "";
        //});
        var modal = $modal;

        $rootScope.$on("$routeChangeStart", function (event, next, current) {
            $scope.dynamic = 0;
            $scope.max = 1;

            modal = $modal.open({
                animation: true,
                templateUrl: 'loadingModal.html',
                controller: 'loadingCtrl',
                size: 'sm',
                backdrop: 'static',
                resolve: {
                    items: function () {
                        return $scope.items;
                    }
                }
            });
        });

        $rootScope.$on("$routeChangeSuccess", function (event, current, previous) {
            $scope.newLocation = $location.path();
            if (modal.close) {
                modal.close();
                $loading.Reset();
            }
        });

        $rootScope.$on('pendingRequest', function (event, current, previous) {
            $scope.dynamic = $loading.Complete();
            $scope.max = $loading.Pending();
        });

        $rootScope.$on('completedRequest', function (event, current, previous) {
            $scope.dynamic = $loading.Complete();
            $scope.max = $loading.Pending();
        });
    });
})();