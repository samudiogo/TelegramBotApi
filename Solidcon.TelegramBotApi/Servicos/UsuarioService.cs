using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Solidcon.TelegramBotApi.Helpers;
using Solidcon.TelegramBotApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Solidcon.TelegramBotApi.Servicos
{
    public interface IUsuarioService
    {
        Usuario Autentica(string nome, string senha);
    }
    public class UsuarioService : IUsuarioService
    {
        private readonly AppSettings _appSettings;

        public UsuarioService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }


        public Usuario Autentica(string nome, string senha)
        {
            var usuarioIgual = nome == "solidcon" && senha == "nocdilos";
            if (usuarioIgual == false) return null;

            var usuario = new Usuario { Nome = nome, Senha = string.Empty };

            //gerando token

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, nome)
                }),
                Expires = DateTime.MaxValue,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            usuario.Token = tokenHandler.WriteToken(token);

            return usuario;

        }
    }
}