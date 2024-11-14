using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Apps.Utilities._JwtGenerator
{
    public sealed class _JwtGenerator
    {
        // private readonly JwtSecurityTokenHandler _handler;

        private byte[]? _secret;

        private ClaimsIdentity _claims = new ClaimsIdentity();

        private DateTime _expiredIn;

        public _JwtGenerator AddSecret(string secret)
        {
            _secret = Encoding.UTF8.GetBytes(secret);

            return this;
        }

        public _JwtGenerator AddClaim(string key, string value)
        {
            _claims.AddClaim(new Claim(key, value));
            return this;
        }

        public _JwtGenerator AddExpiredIn(DateTime expiredIn)
        {
            _expiredIn = expiredIn;
            return this;
        }

        public string Generate()
        {
            var handler = new JwtSecurityTokenHandler();
            
            var credentials = new SigningCredentials(
                                new SymmetricSecurityKey(_secret),
                                SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = _expiredIn,
                Subject = _claims
            };

            var token = handler.CreateToken(tokenDescriptor);
            
            return handler.WriteToken(token);
        }

        public bool Validate(string token, out ClaimsPrincipal? claimsPrincipal)
        {
            claimsPrincipal = null;

            try
            {
                var handler = new JwtSecurityTokenHandler();

                // Set token validation parameters
                var validationParameters = new TokenValidationParameters
                {
                    // ValidateIssuerSigningKey = true,
                    // IssuerSigningKey = new SymmetricSecurityKey(_secret),
                    // ValidateIssuer = true,
                    // ValidIssuer = _validIssuer,
                    // ValidateAudience = true,
                    // ValidAudience = _validAudience,
                    // ValidateLifetime = true,
                    // ClockSkew = TimeSpan.Zero // opsional, atur sesuai kebutuhan
                    
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_secret),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                // Validasi token
                claimsPrincipal = handler.ValidateToken(token, validationParameters, out _);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}