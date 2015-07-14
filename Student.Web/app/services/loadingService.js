(function() {
    var module = angular.module("studentApp");

    var loading = function ($rootScope) {
        var pending = 0;
        var complete = 0;

        var log = function (a) {
            pending++;
            console.log("Pending: " + pending);
            $rootScope.$broadcast('pendingRequest', pending.value);

            a.finally(function () {
                complete++;
                console.log("Complete: " + complete);
                $rootScope.$broadcast('completedRequest', complete);
            });

            return a;
        }

        var reset = function() {
            pending = 0;
            complete = 0;
        }

        var getPending = function() {
            return pending;
        }

        var getComplete = function () {
            return complete;
        }

        return {
            Log: log,
            Reset: reset,
            Pending: getPending,
            Complete: getComplete
        }
    };

    module.factory("$loading", loading);
})();