//Enumeradores referente ao "Tipo de Aplicação" para implementação do Refresh Token
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJSAuthentication.API.Models
{
    public enum ApplicationTypes
    {
        //Websites por exemplo
        JavaScript = 0,
        //Aplicações denominadas "confidenciais", como por exemplo dispositivos móveis, celulares, tablets, computador pessoal, etc
        NativeConfidential = 1
    };
}