ListingCtrl.$inject = ['$scope', 'dataSvc', 'dateProcessorSvc'];

function ListingCtrl($scope, dataSvc, dateProcessorSvc) {

    $scope.refreshTable = function () {
        dataSvc.listPlayers()
            .then(function (response) {

                $scope.playerList = [];

                response.data.forEach(function (item) {
                    var player = new Player(
                        item.Id,
                        item.FirstName,
                        item.Surname,
                        item.ClubName,
                        dateProcessorSvc.ParseAspNetDateTime(item.DateOfBirth)
                    );

                    $scope.playerList.push(player);
                });

            });
    };

    // TODO (GG): confirm first
    $scope.removeRow = function (id) {
        dataSvc.deletePlayer(id)
            .then(function (response) {
                $scope.refreshTable();
            });
    };

    // Initialisation...
    var init = function() {
        $scope.refreshTable();
    };

    init();
}