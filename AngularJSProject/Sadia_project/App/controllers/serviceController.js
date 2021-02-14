angular.module("projectApp")
    .controller("serviceController", ($scope, $routeParams, $location, apiUrl, ApiService) => {
        var id = $routeParams.id;
        $scope.currentService = null;
        $scope.newService = null;
        $scope.updateServiceMsg = "";
        if (id == null) {
            $scope.errorMsg = "Service id not available.";
            $location.path("/companies");
        }
        ApiService.get(apiUrl + "Services/" + id, null)
            .then(r => {
                $scope.currentService = r.data;
                console.log(r.data);
            }, err => {
                console.log(err);
            });
        $scope.updateService = (frm) => {
            ApiService.put(apiUrl + "Services/" + id, $scope.currentService, null)
                .then(r => {
                    console.log(r);
                    $scope.$emit('serviceUpdated', $scope.currentService);
                    $scope.updateServiceMsg = "Data updated."
                   // $location.path("/companies");
                }, err => {
                    console.log(err);
                });
           
        }
        $scope.insertService = () => {
            ApiService.post(apiUrl + "Services/", $scope.newService, null)
                .then(r => {
                    console.log(r);
                    $scope.$emit('serviceInserted', r.data);

                    // $location.path("/companies");
                }, err => {
                    console.log(err);
                });
        }
    });