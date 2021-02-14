angular.module("projectApp")
    .controller("editCompanyCtrl", ($scope, $routeParams, $uibModal, $log, $document, apiUrl, customUrl, ApiService) => {
        $scope.companyEditMsg = "";
        
        var id = $routeParams.id;
        $scope.companyToEdit = {};
        $scope.newCompanyProduct = {};
        $scope.newCompanyService = {};
        ApiService.get(customUrl + "Companies/" + id)
            .then(r => {
                $scope.companyToEdit = r.data;
                console.log(r.data)
                $scope.companyToEdit.Eshtablished = $scope.companyToEdit.Eshtablished.substring(0, 10);
            }, err => {
                console.log(err);
            });
        $scope.editDone = (frm) => {
            ApiService.put(apiUrl + "Companies/" + $scope.companyToEdit.CompanyId, $scope.companyToEdit, null)
                .then(r => {
                    $scope.$emit("companyUpdated", $scope.companyToEdit);
                    $scope.companyEditMsg = "Data updated successfuly."
                }, err => {
                    console.log(err);
                });
        }
        $scope.deleteCompanyService = (index) => {
            $scope.companyToEdit.Services.splice(index, 1);
            
        }
        $scope.deleteCompanyProduct = (index) => {
            $scope.companyToEdit.Products.splice(index, 1);
        }
        $scope.openAddCompanyProductDialog = function () {
            console.log($scope.companyToEdit.CompanyId)
            $("#addCompanyProductModal").modal('show');
        }
        $scope.addProductToCompany = (frm) => {
            $scope.newCompanyProduct.CompanyId = $scope.companyToEdit.CompanyId;
            $scope.companyToEdit.Products.push($scope.newCompanyProduct);
            frm.$setPristine();
            $scope.newCompanyProduct = {};
            $("#addCompanyProductModal").modal('hide');
        }
        $scope.openAddCompanyServiceDialog = function () {
            console.log($scope.companyToEdit.CompanyId)
            $("#addCompanyServiceModal").modal('show');
        }
        $scope.addServiceToCompany = (frm) => {
            $scope.newCompanyService.CompanyId = $scope.companyToEdit.CompanyId;
            $scope.companyToEdit.Services.push($scope.newCompanyService);
            frm.$setPristine();
            $scope.newCompanyService = {};
            $("#addCompanyServiceModal").modal('hide');
        }
    });