using AutoMapper;
using Repository;
using Service;
using System.Security.Claims;
using System.Text;

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
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapper(typeof(Service.Mapper));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//, RequestDelegate next
void HandleGetRequest(IApplicationBuilder app)
{
    app.Use(async (context, next) =>
    {
        string authHeader = context.Request.Headers["Authorization"];

        if (authHeader != null)
        {
            //Reading the JWT middle part           
            int startPoint = authHeader.IndexOf(".") + 1;
            int endPoint = authHeader.LastIndexOf(".");

            var tokenString = authHeader
                .Substring(startPoint, endPoint - startPoint).Split(".");
            var token = tokenString[0].ToString() + "==";

            var credentialString = Encoding.UTF8
                .GetString(Convert.FromBase64String(token));

            // Splitting the data from Jwt
            var credentials = credentialString.Split(new char[] { ':', ',' });

            // Trim this Username and UserRole.
            var userRule = credentials[5].Replace("\"", "");
            var userName = credentials[3].Replace("\"", "");

            // Identity Principal
            var claims = new[]
            {
               new Claim("name", userName),
               new Claim(ClaimTypes.Role, userRule),
           };
            var identity = new ClaimsIdentity(claims, "basic");
            context.User = new ClaimsPrincipal(identity);
        }
        //Pass to the next middleware
        await next(context);
    });

};

app.MapWhen(context => !context.Request.Path.Equals("/api/Users/Get"), HandleGetRequest);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();






