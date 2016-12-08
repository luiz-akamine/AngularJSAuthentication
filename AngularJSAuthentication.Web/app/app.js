var app = angular.module('AngularAuthApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

//Configurações das rotas da aplicação
app.config(function ($routeProvider, $httpProvider) {

    $httpProvider.defaults.headers.common = {};
    $httpProvider.defaults.headers.post = {};
    $httpProvider.defaults.headers.put = {};
    $httpProvider.defaults.headers.patch = {};
    $httpProvider.defaults.headers.delete = {};
    $httpProvider.defaults.headers.post['Content-Type'] = 'application/json';

    //Esta view pode ser acessada anonimamente
    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });

    //Esta view pode ser acessada anonimamente
    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    //Esta view pode ser acessada anonimamente
    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/views/signup.html"
    });

    //Esta view necessita de autenticação!
    $routeProvider.when("/orders", {
        controller: "ordersController",
        templateUrl: "/app/views/orders.html"
    });

    //Refresh Token
    $routeProvider.when("/refresh", {
        controller: "refreshController",
        templateUrl: "/app/views/refresh.html"
    });

    //Lista de Refresh Tokens
    $routeProvider.when("/tokens", {
        controller: "tokensManagerController",
        templateUrl: "/app/views/tokens.html"
    });

    //View de associação de conta do sistema com login externo
    $routeProvider.when("/associate", {
        controller: "associateController",
        templateUrl: "/app/views/associate.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });    
});

//Constante que define na aplicação inteira a URI da API e o "Id" desta aplicação
app.constant('ngAuthSettings', {
    //apiServiceBaseUri: 'http://localhost:1001/',
    //apiServiceBaseUri: 'http://localhost:56953/',
    //apiServiceBaseUri: 'http://angularjsauthapi.azurewebsites.net/',
    apiServiceBaseUri: 'https://angularjsauthapi.azurewebsites.net/',
    clientId: 'ngAuthApp'
});

//Setando serviço que realizará a interceptação das requisições http
app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

//Chamando rotina inicial da aplicação
app.run(['authService', function (authService) {
    authService.fillAuthData();

}]);