angular
    .module('shortlistApp.services', [])
    .factory('dataSvc',
    [
        '$http', function($http) {

            function DataSvc() {

                this.listPlayers = function() {
                    return $http({
                        method: 'GET',
                        url: url_Shortlist_ListPlayers,
                        cache: false
                    });
                };

                this.getPlayer = function (id) {

                    return $http({
                        method: 'GET',
                        url: url_Shortlist_GetPlayer + '/' + id
                    });
                };

                this.deletePlayer = function(id) {
                    return $http({
                        method: 'POST',
                        url: url_Shortlist_DeletePlayer,
                        data: { id: id }
                    });
                };

                this.savePlayer = function(id, firstName, surname, clubName, dateOfBirth) {

                    return $http({
                        method: 'POST',
                        url: url_Shortlist_SavePlayer,
                        data: {
                            Id: id,
                            FirstName: firstName,
                            Surname: surname,
                            ClubName: clubName,
                            DateOfBirth: dateOfBirth
                        }
                    });
                };
            }

            return new DataSvc();
        }
    ]);