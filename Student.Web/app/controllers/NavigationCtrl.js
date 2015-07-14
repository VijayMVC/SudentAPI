(function() {
    var studentApp = angular.module('studentApp');

    studentApp.controller('navigationCtrl', function($scope, $location) {
        $scope.isActive = function (path)
        {
            if (path === $location.path()) {
                return "active";
            }
            return "";
        }
    });
})();