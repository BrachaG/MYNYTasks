using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Tasks.Middlewares
{
        public class TokenRefreshMiddleware
        {
            private readonly RequestDelegate _next;

            public TokenRefreshMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public async Task Invoke(HttpContext context)
            {
                if (context.Request.Headers.ContainsKey("Authorization"))
                {
                    string token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                    var Issure = MyConfig["JWTParams:Issure"];
                    var Audience = MyConfig["JWTParams:Audience"];

                    var validationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Issure,
                        ValidAudience = Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("ygrcuy3gcryh@$#^%*&^(_+")),
                    };

                    try
                    {
                        var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var securityToken);

                        if (securityToken.ValidTo > DateTime.UtcNow)
                        {
                            // Reset the expiration time to 30 minutes from now
                            var newToken = GenerateNewToken(claimsPrincipal.Identity.Name);
                            context.Response.Headers.Add("Authorization", "Bearer " + newToken);
                        }
                    }
                    catch (SecurityTokenException)
                    {
                        // Invalid token, do nothing
                    }
                }

                await _next.Invoke(context);
            }

            private string GenerateNewToken(string username)
            {
                var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                var Issuer = MyConfig["JWTParams:Issuer"];
                var Audience = MyConfig["JWTParams:Audience"];
                var Key = MyConfig["JWTParams:Key"];

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim(ClaimTypes.Name, username)
            }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    Issuer = Issuer,
                    Audience = Audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key)), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
          }
}
