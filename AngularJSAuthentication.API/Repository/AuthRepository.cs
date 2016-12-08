using AngularJSAuthentication.API.Entities;
using AngularJSAuthentication.API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AngularJSAuthentication.API
{
    public class AuthRepository : IDisposable
    {
        private AuthContext _ctx;

        //Classe UserManager que gerencia a autenticação, registro, hash de password, etc referente ao usuário
        private UserManager<IdentityUser> _userManager;

        //Construtor criando o contexto e o UserManager
        public AuthRepository()
        {
            _ctx = new AuthContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
        }

        //Método que cadastra usuário
        //você usa a palavra-chave async na declaração de uma função que dependa da palavra-chave await
        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            // 1) await é um "comando" para o código ficar esperando pela conclusão de uma tarefa até o fim, para aí sim,
            //continuar a execução normal permitindo que outras execuções possam acontecer concomitantemente
            // 2) await só pode ser usado em um método declarado com o modificador async
            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        //Método que busca usuário
        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        //Liberação das conexões e gerenciador da autenticação dos usuários
        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
        }


        /*********************************************************
                     IMPLEMENTAÇÃO DO REFRESH TOKEN 
        *********************************************************/

        //Método que busca usuário
        public Client FindClient(string clientId)
        {
            var client = _ctx.Clients.Find(clientId);

            return client;
        }

        //Método que adiciona RefreshToken
        public async Task<bool> AddRefreshToken(RefreshToken token)
        {
            //"Chave" do RefreshToken será usuario (subject) + client (clientid)
            var existingToken = _ctx.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

            if (existingToken != null)
            {
                //removendo caso já exista
                var result = await RemoveRefreshToken(existingToken);
            }

            //Adicionando
            _ctx.RefreshTokens.Add(token);

            return await _ctx.SaveChangesAsync() > 0;
        }

        //Método que remove registro do Refresh Token pelo Id
        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                _ctx.RefreshTokens.Remove(refreshToken);
                return await _ctx.SaveChangesAsync() > 0;
            }

            return false;
        }

        //Método que remove registro do Refresh Token pelo próprio objeto
        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _ctx.RefreshTokens.Remove(refreshToken);
            return await _ctx.SaveChangesAsync() > 0;
        }

        //Método que busca RefreshToken pelo Id
        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        //Retorna lista dos RefreshTokens cadastrados
        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _ctx.RefreshTokens.ToList();
        }


        /*********************************************************
                     IMPLEMENTAÇÃO DO LOGIN EXTERNO
        *********************************************************/

        public async Task<IdentityUser> FindAsync(UserLoginInfo loginInfo)
        {
            IdentityUser user = await _userManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(IdentityUser user)
        {
            var result = await _userManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }
    }
}