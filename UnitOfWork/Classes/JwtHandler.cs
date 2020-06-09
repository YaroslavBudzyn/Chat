using ChatTest.ApplicationSettings;
using ChatTest.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ChatTest.UnitOfWork.Classes
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtOptions _jwtOptions;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly JwtHeader _jwtHeader;

        // Constructor
        public JwtHandler(IOptions<JwtOptions> options)
        {
            //Check maybe use appsettings.Development.json
            _jwtOptions = options.Value;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            _jwtHeader = new JwtHeader(signingCredentials);
        }

        public Entities.Token Generate(Entities.User user, string role)
        {
            var expires = DateTime.Now.AddMinutes(_jwtOptions.ExpiryMinutes).ToUniversalTime();
            var centuryBegin = new DateTime(1970, 1, 1);
            var exp = (long)new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalSeconds;
            var iat = (long)new TimeSpan(DateTime.Now.Ticks - centuryBegin.Ticks).TotalSeconds;

            var payload = new JwtPayload
            {
                {"sub", user.Name},
                {"iss", _jwtOptions.Issuer},
                {"aud", _jwtOptions.Audience},
                {"iat", iat},
                {"exp", exp},
                {"email", user.Email},
                {"role", role}
            };

            var jwt = new JwtSecurityToken(_jwtHeader, payload);
            var token = _jwtSecurityTokenHandler.WriteToken(jwt);

            // Refresh Token Generate
            var refresh =
                (long)new TimeSpan(expires.AddMinutes(_jwtOptions.RefreshExpiryMinutes).Ticks - centuryBegin.Ticks)
                    .TotalSeconds;
            var refreshPayload = new JwtPayload
            {
                {"sub", user.Email},
                {"iss", _jwtOptions.Issuer},
                {"iat", iat},
                {"exp", refresh},
                {"email", user.Email},
                {"role", role}
            };

            var refreshJwt = new JwtSecurityToken(_jwtHeader, refreshPayload);
            var refreshToken = _jwtSecurityTokenHandler.WriteToken(refreshJwt);

            // Return token
            return new Entities.Token
            {
                Code = token,
                ExpiredTime = expires,
                UserId = user.Id,
                User = user,
                CreatedAt = DateTime.Now,
                RefreshToken = refreshToken
            };
        }
    }
}
