using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Tasks.Middlewares
{
    public class TokenRefreshMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<TokenRefreshMiddleware> _logger;
        public TokenRefreshMiddleware(RequestDelegate next, ILogger<TokenRefreshMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        //public async Task Invoke(HttpContext context)
        //{
        //    if (context.Request.Headers.ContainsKey("Authorization"))
        //    {
        //        string token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        //        var Issure = MyConfig["JWTParams:Issure"];
        //        var Audience = MyConfig["JWTParams:Audience"];

        //        var validationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuer = true,
        //            ValidateAudience = true,
        //            ValidateLifetime = true,
        //            ValidateIssuerSigningKey = true,
        //            ValidIssuer = Issure,
        //            ValidAudience = Audience,
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("ygrcuy3gcryh@$#^%*&^(_+")),
        //        };

        //        try
        //        {
        //            var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var securityToken);

        //            if (securityToken.ValidTo > DateTime.UtcNow)
        //            {
        //                // Get the user id from the claims
        //                var userIdClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
        //                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
        //                {
        //                    // Reset the expiration time to 30 minutes from now
        //                    var newToken = GenerateNewToken(userId);
        //                    context.Response.Headers.Add("Authorization", "Bearer " + newToken);
        //                }
        //            }
        //        }
        //        catch (SecurityTokenException)
        //        {
        //            // Invalid token, do nothing
        //        }
        //    }

        //    await _next.Invoke(context);
        //}

        public int ValidateToken(string token)
        {
            _logger.LogInformation(token);
            if (token == null)
                return -1;

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
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return -1;
            }

        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var userId = ValidateToken(token);
                _logger.LogInformation(userId.ToString());
                if (userId != -1)
                {
                    JwtSecurityToken jwtSecurityToken;

                    jwtSecurityToken = new JwtSecurityToken(token);
                    if (jwtSecurityToken.ValidTo > DateTime.UtcNow)
                    {
                        var newToken = GenerateNewToken(userId);
                        context.Response.Headers.Add("Authorization", "Bearer " + newToken);
                    }
                }
            }
            await _next(context);
        }
        //private string GenerateNewToken(int userId)
        //{
        //    var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        //    var Issure = MyConfig["JWTParams:Issure"];
        //    var Audience = MyConfig["JWTParams:Audience"];
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ygrcuy3gcryh@$#^%*&^(_+"));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        //    string jsonString = userId.ToString();
        //    var claims = new List<Claim>
        //     { 
        //        new Claim(JwtRegisteredClaimNames.Sub, jsonString) };
        //    var token = new JwtSecurityToken(
        //        issuer: Issure,
        //        audience: Audience,
        //        claims: claims,
        //        expires: DateTime.UtcNow.AddMinutes(1),
        //        signingCredentials: credentials
        //    );
        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
        public string GenerateNewToken(int userId)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("ygrcuy3gcryh@$#^%*&^(_+");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userId.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            _logger.LogInformation(tokenHandler.WriteToken(token));
            return tokenHandler.WriteToken(token);
        }
        // Extension method used to add the middleware to the HTTP request pipeline.

    }
    public static class CacheMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenRefreshMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenRefreshMiddleware>();
        }
    }
}