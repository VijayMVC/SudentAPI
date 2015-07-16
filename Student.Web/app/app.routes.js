(function () {
    var studentApp = angular.module('studentApp', ['ng', 'ngRoute', 'ui.bootstrap', 'ui.grid', 'ui.grid.autoResize']);

    studentApp.config(['$routeProvider',
        function ($routeProvider) {
            $routeProvider
                .when('/', {
                    templateUrl: 'Views/homeView.html'
                })
                .when('/Students', {
                    templateUrl: 'Views/studentsView.html',
                    controller: function ($scope, data) {
                        var columnDefs = [
                            { displayName: 'First Name', field: 'firstName' },
                            { displayName: 'Last Name', field: 'lastName' },
                        ];

                        $scope.students = data;
                        $scope.sortOrder = 'firstName';
                        $scope.gridOptions =
                        {
                            data: 'students',
                            columnDefs: columnDefs,
                            enableVerticalScrollbar: false,
                            enableHorizontalScrollbar: false,
                            onRegisterApi: function (gridApi) {
                                $scope.gridApi = gridApi;
                                gridApi.grid.gridHeight = $scope.height;
                            }
                        };

                        $scope.height = ((data.length + 1) * 30) + 'px';
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