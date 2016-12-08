//Classe para implementação do "Refresh Token"
//Esta classe nomeada como "Client", refere-se aos clientes que irão consumir as APIs do lado do servidor. Ex: Browsers, Celulares, Desktops, etc
using AngularJSAuthentication.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngularJSAuthentication.API.Entities
{
    public class Client
    {
        [Key]
        public string Id { get; set; } //id client
        [Required]
        public string Secret { get; set; }  
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } //Nome client
        public ApplicationTypes ApplicationType { get; set; } //ver classe Enum
        public bool Active { get; set; } //0 = client desativado não podendo mais requisitar APIs | 1 = client ativo
        public int RefreshTokenLifeTime { get; set; } //tempo de vida do token
        [MaxLength(100)]
        public string AllowedOrigin { get; set; } //configuração do CORs (de onde será aceito ser chamada as APIs)
    }
}