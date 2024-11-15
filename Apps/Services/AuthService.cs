using System.Net;
using System.Security.Claims;
using System.Text;
using Apps.Config;
using Apps.Data.Entities;
using Apps.Data.Models;
using Apps.Exceptions;
using Apps.Repositories.Interfaces;
using Apps.Services.Interfaces;
using Apps.Utilities._BCrypt;
using Apps.Utilities._ClientInfo;
using Apps.Utilities._Common;
using Apps.Utilities._Convertion;
using Apps.Utilities._JwtGenerator;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Apps.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepo;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly AppCfg _config;

        public AuthService(
            IAuthRepository authRepo, 
            IHttpContextAccessor httpContextAccessor,
            IOptions<AppCfg> config)
        {
            _authRepo = authRepo;
            // _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _httpContextAccessor = httpContextAccessor;
            _config = config.Value;
        }

        public async Task<User> Login(AuthEntityLoginBody body)
        {
            var user = _Is.ValidEmail(body.Email)
                            ? await _authRepo.FindUserByEmail(body.Email)
                            : await _authRepo.FindUserByUsername(body.Email);

            var labelMsg = _Is.ValidEmail(body.Email)
                                ? "Email"
                                : "Username";

            if (user == null)
            {
                // error karna user tidak ditemukan;
                throw new HttpResponseException($"{labelMsg} belum terdaftar dalam sistem. silahkan registrasi terlebih dahulu", HttpStatusCode.NotFound);
                // return false;
            }

            if (!_BCrypt.Verify(body.Password, user.Password))
            {
                // error karna password tidak sesuai
                throw new HttpResponseException($"{labelMsg} atau password salah");
                // return false;
            }

            return user;
        }

        // public async Task<AuthEntityLoginResponse> GenerateToken(User user)
        public async Task<AuthEntityLoginResponse> GenerateToken(User user)
        {
            var accessTokenTtl = DateTime.UtcNow.AddSeconds(_Int.ToDouble(_config.Jwt.AccessTokenTtl));
            var refreshTokenTtl = DateTime.UtcNow.AddSeconds(_Int.ToDouble(_config.Jwt.RefreshTokenTtl));
            
            Session vSession = new Session {
                UserId = user.Id,
                IpAddress = _IpAddr.Atoi(_ClientInfo.IpAddress(_httpContextAccessor.HttpContext)),
                UserAgent = _ClientInfo.UserAgent(_httpContextAccessor.HttpContext),
                ExpiredAt = refreshTokenTtl
            };
            
            var session = await _authRepo.StoreSession(vSession);

            if (session == null)
            {
                throw new HttpResponseException("Terjadi kesalahan pada sistem. silahkan hubungin admin.", HttpStatusCode.InternalServerError);
            }

            string accessToken = new _JwtGenerator()
                                        .AddSecret(_config.Jwt.Secret)
                                        .AddExpiredIn(accessTokenTtl)
                                        .AddClaim("sid", session.Id)
                                        .AddClaim("typ", "access")
                                        .Generate();

            string refreshToken = new _JwtGenerator()
                                        .AddSecret(_config.Jwt.Secret)
                                        .AddExpiredIn(refreshTokenTtl)
                                        .AddClaim("sid", session.Id)
                                        .AddClaim("typ", "refresh")
                                        .Generate();
            
            return new AuthEntityLoginResponse{
                User = new AuthEntityUserProp {
                    Name = user.Name,
                    Email = user.Email
                },
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<AuthEntityRefreshTokenResponse> RefreshToken(string rToken)
        {
            ClaimsPrincipal? claimsPrincipal;
            
            var IsValid = new _JwtGenerator()
                                .AddValidateParamters(new TokenValidationParameters{
                                    /* ValidateIssuerSigningKey = true,
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Jwt.Secret)),
                                    ValidateIssuer = false,
                                    ValidateAudience = false,
                                    ValidateLifetime = false,
                                    RequireExpirationTime = false,
                                    ClockSkew = TimeSpan.Zero */

                                    ValidateIssuerSigningKey = true,
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Jwt.Secret)),
                                    ValidateIssuer = false,
                                    ValidateAudience = false,
                                    ClockSkew = TimeSpan.Zero
                                })
                                .Validate(rToken, out claimsPrincipal);

            if (!IsValid)
            {
                throw new HttpResponseException("Token tidak valid.", HttpStatusCode.BadRequest);
            }

            var type = claimsPrincipal!.FindFirst(x => x.Type == "typ")?.Value;
            
            var sessId = claimsPrincipal!.FindFirst(x => x.Type == "sid")?.Value;

            if (type != "refresh" && sessId == null)
            {
                throw new HttpResponseException("Token tidak valid.", HttpStatusCode.BadRequest);
            }

            var session = await _authRepo.FindSessionById(Ulid.Parse(sessId));

            if (session == null)
            {
                throw new HttpResponseException("Token tidak valid.", HttpStatusCode.BadRequest);
            }

            var accessTokenTtl = DateTime.UtcNow.AddSeconds(_Int.ToDouble(_config.Jwt.AccessTokenTtl));
            
            string accessToken = new _JwtGenerator()
                                        .AddSecret(_config.Jwt.Secret)
                                        .AddExpiredIn(accessTokenTtl)
                                        .AddClaim("sid", session.Id)
                                        .AddClaim("typ", "access")
                                        .Generate();
                                
            return new AuthEntityRefreshTokenResponse{
                AccessToken = accessToken
            };
        }
    }
}