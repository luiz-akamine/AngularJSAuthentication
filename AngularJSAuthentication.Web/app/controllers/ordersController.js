'use strict';
app.controller('ordersController', ['$scope', 'ordersService', function ($scope, ordersService) {

    $scope.orders = [];

    //Adquirindo lista de Ordens pelo serviço que chama a API
    ordersService.getOrders().then(function (results) {

        $scope.orders = results.data;

    }, function (error) {
        alert(error.data.message);
    });

}]);