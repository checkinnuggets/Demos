FormCtrl.$inject = ['$scope', '$routeParams', '$location',  'dataSvc', 'dateProcessorSvc'];

function FormCtrl($scope, $routeParams, $location, dataSvc, dateProcessorSvc) {

    $scope.savePlayer = function () {

        dataSvc.savePlayer(
            $scope.currentPlayer.id,
            $scope.currentPlayer.firstName,
            $scope.currentPlayer.surname,
            $scope.currentPlayer.clubName,
            $scope.currentPlayer.dateOfBirth
        ).then(function (response) {
            $location.path('/');
        });
    };

    // Initialisation...
    var init = function () {

        var id = parseInt($routeParams.id);
        if (isNaN(id) || id <= 0) {
            $scope.currentPlayer = new Player();
        }
        else {
            dataSvc.getPlayer(id)
                .then(function (response) {

                    if (response.data == null) {
                        $location.path('/');
                    }

                    $scope.currentPlayer = new Player(
                        response.data.Id,
                        response.data.FirstName,
                        response.data.Surname,
                        response.data.ClubName,
                        dateProcessorSvc.ParseAspNetDateTime(response.data.DateOfBirth)
                    );

                });
        };
    }

    init();
}