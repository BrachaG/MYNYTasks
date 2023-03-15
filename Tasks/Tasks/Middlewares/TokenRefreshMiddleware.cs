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
                        // Get the user id from the claims
                        var userIdClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
                        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                        {
                            // Reset the expiration time to 30 minutes from now
                            var newToken = GenerateNewToken(userId);
                            context.Response.Headers.Add("Authorization", "Bearer " + newToken);
                        }
                    }
                }
                catch (SecurityTokenException)
                {
                    // Invalid token, do nothing
                }
            }

            await _next.Invoke(context);
        }

        private string GenerateNewToken(int userId)
        {
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var Issure = MyConfig["JWTParams:Issure"];
            var Audience = MyConfig["JWTParams:Audience"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ygrcuy3gcryh@$#^%*&^(_+"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            string jsonString = userId.ToString();
            var claims = new List<Claim>
             { 
                new Claim(JwtRegisteredClaimNames.Sub, jsonString) };
            var token = new JwtSecurityToken(
                issuer: Issure,
                audience: Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}