using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Service;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Web.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddTransient(typeof(IObjectGenerator<>), typeof(ObjectGenerator<>));
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddSingleton<ISurveysService, SurveysService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapper(typeof(Service.Mapper));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

    app.Use(async (context, next) =>
    {
        if (!context.Request.Path.Equals("/api/Users/Get"))
        {
            string authHeader = context.Request.Headers["Authorization"];
            if (authHeader != null)
            {
                //Reading the JWT middle part           
                var tokenHandler = new JwtSecurityTokenHandler();

                try
                {
                    tokenHandler.ValidateToken(authHeader, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("ygrcuy3gcryh@$#^%*&^(_+")),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
                    if (jwtToken == null)
                    {
                        throw new HttpResponseException(HttpStatusCode.Unauthorized);
                    }

                    string user = jwtToken.Claims.First(x => x.Type == "User").Value;
                    context.Items["User"]=user;
                    if (user == null)
                    {
                       throw new HttpResponseException(HttpStatusCode.Unauthorized);
                    }
                }
                catch
                {
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }


        }
        
        await next(context);
    });


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();