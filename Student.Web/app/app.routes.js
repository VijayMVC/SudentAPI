(function () {
    var studentApp = angular.module('studentApp', ['ng', 'ngRoute', 'ui.bootstrap']);

    studentApp.config(['$routeProvider',
        function ($routeProvider) {
            $routeProvider
                .when('/', {
                    templateUrl: 'Views/studentsView.html',
                    controller: function ($scope, data) {
                        $scope.students = data;
                        $scope.sortOrder = 'firstName';
                    },
                    resolve: {
                        data: function ($http, $q, $loading) {
                            var deferred = $q.defer();
                            $http.get('http://localhost/StudentAPI/api/Students')
                                .success(function (data) {
                                    deferred.resolve(data);
                                });
                            return $loading.Log(deferred.promise);
                        },
                        wait: function ($q, $timeout, $loading) {
                            var deferred = $q.defer();
                            $timeout(function () {
                                deferred.resolve(true);
                            }, 10000);
                            return $loading.Log(deferred.promise);
                        },
                        wait2: function ($q, $timeout, $loading) {
                            var deferred = $q.defer();
                            $timeout(function () {
                                deferred.resolve(true);
                            }, 5000);
                            return $loading.Log(deferred.promise);
                        }
                    }
                });
        }
    ]);
})();