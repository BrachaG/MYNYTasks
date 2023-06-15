using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using Repository;
using Service;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Tasks.Middlewares;
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseNLog();
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
    builder.Services.AddTransient(typeof(IObjectGenerator<>), typeof(ObjectGenerator<>));
    builder.Services.AddScoped<IUsersService, UsersService>();
    builder.Services.AddScoped<ISurveysService, SurveysService>();
    builder.Services.AddScoped<ITaskService, TaskService>();
    builder.Services.AddScoped<ITargetsService, TargetsService>();
    builder.Services.AddScoped<ISettingsService, SettingsService>();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var Issure = MyConfig["JWTParams:Issure"];
        var Audience = MyConfig["JWTParams:Audience"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Issure,
            ValidAudience = Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("ygrcuy3gcryh@$#^%*&^(_+")),
            ClockSkew = TimeSpan.Zero
        };

    });
    var app = builder.Build();
    //builder.Services.AddEndpointsApiExplorer();
    app.UseTokenRefreshMiddleware();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}

catch (Exception ex)
{
    throw;
}
finally
{
    // Ensure to shout downon the NLog ( Disposing )
    NLog.LogManager.Shutdown();
}






