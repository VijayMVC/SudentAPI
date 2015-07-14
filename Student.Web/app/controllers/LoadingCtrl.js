(function () {
    var studentApp = angular.module('studentApp');

    studentApp.controller('loadingCtrl', function ($scope, $rootScope, $location, $modal, $loading) {
        //$rootScope.$on("$routeChangeError", function (event, current, previous, rejection) {
        //    alert("ROUTE CHANGE ERROR: " + rejection);
        //    $scope.active = "";
        //});
        var modal = null;
        //$rootScope.$on("$routeChangeStart", function (event, next, current) {
        //    $scope.dynamic = 0;
        //    $scope.max = 1;

        //    modal = $modal.open({
        //        animation: true,
        //        templateUrl: 'loadingModal.html',
        //        controller: 'loadingCtrl',
        //        size: 'sm',
        //        backdrop: 'static',
        //        resolve: {
        //            items: function () {
        //                return $scope.items;
        //            }
        //        }
        //    });
        //});

        //$rootScope.$on("$routeChangeSuccess", function (event, current, previous) {
        //    $scope.newLocation = $location.path();
        //    if (modal.close) {
        //        modal.close();
        //        $loading.Reset();
        //    }
        //});

        $rootScope.$on('pendingRequest', function (event, current, previous) {
            var pending = $loading.Pending();
            var complete = $loading.Complete();

            if (pending > 0 && modal == null) {
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
            }

            $scope.dynamic = complete;
            $scope.max = pending;
        });

        $rootScope.$on('completedRequest', function (event, current, previous) {
            var pending = $loading.Pending();
            var complete = $loading.Complete();

            if (pending == complete) {
                $scope.newLocation = $location.path();
                if (modal != null && modal.close) {
                    modal.close();
                    $loading.Reset();
                }
            }

            $scope.dynamic = complete;
            $scope.max = pending;
        });
    });
})();