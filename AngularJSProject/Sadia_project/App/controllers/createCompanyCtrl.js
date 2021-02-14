angular.module("projectApp")
    .controller("createCompanyCtrl", ($scope, apiUrl, ApiService) => {
        /////////
        /// locals
        $scope.companyInsertMsg = "";
        $scope.activeTab = 0;
        $scope.popup1 = {
            opened: false,
        };
        $scope.open1 = function () {
            $scope.popup1.opened = true;
        };
        /////////////
        ///Model Related
        $scope.newCompany = {};
        $scope.newProduct = {};
        $scope.products = [];
        $scope.newService = {};
        $scope.services = [];
        $scope.companyDone = () => {
            $scope.activeTab = 1;
        };
        $scope.productDone = function (frm) {
            //console.log($scope.newProduct);

            $scope.products.push($scope.newProduct);

            $scope.newProduct = {};
            frm.$setPristine();

            console.log($scope.products);
        };
        $scope.productDel = (index) => {
            $scope.products.splice(index, 1);
        };
        $scope.productPre = () => {
            $scope.activeTab = 0;
        };
        $scope.productNext = () => {
            $scope.activeTab = 2;
        };
        $scope.servicePre = () => {
            $scope.activeTab = 1;
        };
        $scope.serviceNext = (frms) => {
            $scope.newCompany.Products = $scope.products;
            $scope.newCompany.Services = $scope.services;
            ApiService.post(apiUrl + "Companies", $scope.newCompany, null)
                .then(r => {
                    $scope.companyInsertMsg = "Data inserted successfully."
                  $scope.$emit('companyInserted', r.data);
                    $scope.newCompany = {};
                    $scope.newProduct = {};
                    $scope.products = [];
                    $scope.newService = {};
                    $scope.services = [];
                    $scope.activeTab = 0;
                    frms[0].$setPristine();
                    frms[1].$setPristine();
                    frms[2].$setPristine();
                    console.log(frms);
                }, err => {
                    console.log(r);
                });
        };
        $scope.serviceDel = (index) => {
            $scope.services.splice(index, 1);
        }
        $scope.serviceDone = function (frm) {
            //console.log($scope.newProduct);

            $scope.services.push($scope.newService);

            $scope.newService = {};
            frm.$setPristine();

            console.log($scope.services);
        };
    });