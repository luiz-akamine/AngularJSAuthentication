﻿'use strict';
app.controller('signupController', ['$scope', '$location', '$timeout', 'authService', function ($scope, $location, $timeout, authService) {

    //Variáveis para controle de mensagem de retorno do cadastro para usuário
    $scope.savedSuccessfully = false;
    $scope.message = "";

    //objeto JSON que será enviado para a API que realiza o registro/cadastro dos novos usuários
    $scope.registration = {
        userName: "",
        password: "",
        confirmPassword: ""
    };

    //Função que tenta realizar o registro do novo usuário, chamando o serviço authService que realiza o registro chamando a WEB API no server
    $scope.signUp = function () {

        //Realizando chamanda da WEB API de registro pelo serviço authService
        authService.saveRegistration($scope.registration).then(function (response) {

            //Cadastro foi OK!
            $scope.savedSuccessfully = true;
            $scope.message = "Usuário cadastrado com sucesso! Redirecionando para página inicial em 2 segundos...";
            //Redirecionando para página com delay de 2 segundos
            startTimer();
        },
         //Ocorreu erro no cadastro
         function (response) {
             var errors = [];
             for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             $scope.message = "Falha no registro do usuário: " + errors.join(' ');
         });
    };

    //Método que redireciona para outra página
    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            //Chama outra página pelo $location
            $location.path('/login');
        }, 2000);
    }

}]);