using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Tasks.Middlewares
{
    public class TokenRefreshMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<TokenRefreshMiddleware> _logger;
        readonly string _issure;
        readonly string _audience;
        IConfiguration _configuration;
        public TokenRefreshMiddleware(RequestDelegate next, ILogger<TokenRefreshMiddleware> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _issure = _configuration["JWTParams:Issure"];
            _audience = _configuration["JWTParams:Audience"];
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var Details = ValidateToken(token);
                var userId = Details.Item1;
                var PermissionLevelId = Details.Item2;

                _logger.LogInformation("userId after decrypt", userId.ToString());
                if (userId != -1 && PermissionLevelId != -1)
                {
                    JwtSecurityToken jwtSecurityToken;
                    jwtSecurityToken = new JwtSecurityToken(token);
                    var newToken = GenerateNewToken(userId, PermissionLevelId);
                    _logger.LogInformation(newToken);
                    context.Response.Headers.Add("Authorization", "Bearer " + newToken);
                    context.Items["userId"] = userId;
                    context.Items["PermissionLevelId"] = PermissionLevelId;
                }
            }
            await _next(context);
        }
        public Tuple<int, int> ValidateToken(string token)
        {
            _logger.LogInformation(token, "token before validation");
            if (token == null)
                return Tuple.Create(-1, -1);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("ygrcuy3gcryh@$#^%*&^(_+");
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.NameId).Value);
                var Status = int.Parse(jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value);


                // return user id from JWT token if validation successful
                return Tuple.Create(userId, Status);
            }
            catch
            {
                // return null if validation fails
                return Tuple.Create(-1, -1);
            }
        }
        public string GenerateNewToken(int userId, int PermissionLevelId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ygrcuy3gcryh@$#^%*&^(_+"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            string IdJsonString = userId.ToString();
            string PermissionLevelIdJsonString = PermissionLevelId.ToString();

            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new List<Claim>
            {    new Claim(JwtRegisteredClaimNames.NameId, IdJsonString) ,
             new Claim(JwtRegisteredClaimNames.Sub, PermissionLevelIdJsonString)  };
            var token = new JwtSecurityToken(
                issuer: _issure,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );
            _logger.LogInformation(tokenHandler.WriteToken(token));
            return tokenHandler.WriteToken(token);
        }
    }

    public static class CacheMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenRefreshMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenRefreshMiddleware>();
        }
    }
}