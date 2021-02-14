angular.module("projectApp")
    .controller("createProductCtrl", ($scope, $routeParams, $location, apiUrl, ApiService) => {

        $scope.newProduct = null;
        $scope.newProductMsg = "";

        $scope.insertProduct = (frm) => {
            ApiService.post(apiUrl + "Products", $scope.newProduct, null)
                .then(r => {
                    console.log(r);
                    $scope.$emit('productInserted', r.data);
                    $scope.newProduct = {};
                    // $location.path("/companies");
                    $scope.newProductMsg = "Data inserted";
                    frm.$setPristine();
                }, err => {
                    console.log(err);
                });
        }
    });