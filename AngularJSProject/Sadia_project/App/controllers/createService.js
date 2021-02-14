angular.module("projectApp")
    .controller("createServiceCtrl", ($scope, $routeParams, $location, apiUrl, ApiService) => {

        $scope.newService = null;

        $scope.newServiceMsg = "";
        $scope.insertService = (frm) => {
            ApiService.post(apiUrl + "Services", $scope.newService, null)
                .then(r => {
                    console.log(r);
                    $scope.$emit('serviceInserted', r.data);
                    $scope.newServiceMsg = "Data inserted";
                    // $location.path("/companies");
                    frm.$setPristine();
                }, err => {
                    console.log(err);
                });
        }
    });