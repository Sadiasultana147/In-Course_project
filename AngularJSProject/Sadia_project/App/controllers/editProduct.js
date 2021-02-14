angular.module("projectApp")
    .controller("editProduct", ($scope, $routeParams, $location, apiUrl, ApiService) => {
       
        var id = $routeParams.id;
        $scope.currentProduct = null;
       
        $scope.updateProductMsg = "";
        if (id == null) {
            $scope.errorMsg = "Service id not available.";
            $location.path("/companies");
        }
        ApiService.get(apiUrl + "Products/" + id, null)
            .then(r => {
                $scope.currentProduct = r.data;
                console.log(r.data);
            }, err => {
                console.log(err);
            });
        $scope.updateProduct = (frm) => {
            ApiService.put(apiUrl + "Products/" + id, $scope.currentProduct, null)
                .then(r => {
                    console.log(r);
                    $scope.$emit('productUpdated', $scope.currentProduct);
                    $scope.updateProductMsg = "Data updated."
                    // $location.path("/companies");
                }, err => {
                    console.log(err);
                });

        }
        
    });