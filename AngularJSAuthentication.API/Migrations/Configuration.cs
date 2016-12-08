namespace AngularJSAuthentication.API.Migrations
{
    using AngularJSAuthentication.API.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AngularJSAuthentication.API.AuthContext>
    {
        public Configuration()
        {
            //Foi necessário colocar como true para que criasse as tabelas padrões do ASPNET Identity
            //Porém depois voltei para false
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AngularJSAuthentication.API.AuthContext context)
        {
            //  This method will be called after migrating to the latest version.

            //Inserindo "Clients" na nossa tabela de cotnrole de acesso a nossa API
            if (context.Clients.Count() > 0)
            {
                return;
            }
            context.Clients.AddRange(BuildClientsList());
            context.SaveChanges();
        }

        //Cria "Clients" fictícios de nosso exemplo que irão acessar as APIs protegidas
        private static List<Client> BuildClientsList()
        {
            List<Client> ClientsList = new List<Client> 
            {
                //Client exemplo 1: acessado via website
                new Client
                { Id = "ngAuthApp", 
                    Secret= Helper.GetHash("teste@1"), 
                    Name="WebSite SPA AngularJS", 
                    ApplicationType =  Models.ApplicationTypes.JavaScript, 
                    Active = true, 
                    RefreshTokenLifeTime = 72000, //tempo de vida em minutos do RefreshToken
                    //AllowedOrigin = "http://angularjsauthweb.azurewebsites.net"
                    AllowedOrigin = "https://angularjsauthweb.azurewebsites.net"
                    //AllowedOrigin = "http://localhost:1000"
                },
                //Cliente exemplo 2: alguma aplicação desktop
                new Client
                { Id = "consoleApp", 
                    Secret=Helper.GetHash("teste@2"), 
                    Name="Console Application", 
                    ApplicationType =Models.ApplicationTypes.NativeConfidential, 
                    Active = true, 
                    RefreshTokenLifeTime = 14400, //tempo de vida em minutos do RefreshToken
                    AllowedOrigin = "*"
                }
            };

            return ClientsList;
        }
    }
}
