//Serviço com métodos relacionados a nossa entidade fictícia de exemplo "Orders"

'use strict';
app.factory('ordersService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    //Server onde está hospedado as WEB APIs
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    //Variável para acessarmos este serviço
    var ordersServiceFactory = {};

    //Método que retorna lista fictícia de "Orders"
    var _getOrders = function () {

        //Chamando WEB API no server que retorna a lista de "Orders"
        return $http.get(serviceBase + 'api/orders').then(function (results) {
            return results;
        });
    };

    //Definindo métodos desta factory a serem chamadas por outros js
    ordersServiceFactory.getOrders = _getOrders;

    return ordersServiceFactory;
}]);