angular.module("bookApp")
    .factory("categorySvc", ($http) => {
        return {
            getCategoriesWithBook: () => {
                return $http({
                    method: "GET",
                    url: "/CategoryData/CategoriesWithBook",
                    headers: {
                        'Content-Type': "application/json"
                    }
                });
            }
        }
    });