using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentApp.Api.Configuration;
using StudentApp.Api.Data;
using StudentApp.Api.Model;
using StudentApp.Api.Types;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region Env settings
var environment = builder.Environment.IsDevelopment() ? ".development" : "";
builder.Configuration.AddJsonFile($"appsettings{environment}.json", optional: true, reloadOnChange: true);
#endregion

#region Builder settings
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
  builder.Configuration.GetConnectionString("DefaultConnection")
));
//builder.Logging.AddJsonConsole();
#endregion

#region DI settings
builder.Services.AddScoped<ISuccess, Success>();
builder.Services.AddScoped<IError, Error>();
builder.Services.AddScoped<ICustomResponse, CustomResponse>();
#endregion

#region Authetication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
	options.RequireHttpsMetadata = false;
	options.SaveToken = true;
	options.TokenValidationParameters = new TokenValidationParameters() {
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidAudience = builder.Configuration["Jwt:Audience"],
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
	};
});
#endregion

//builder.Services.AddScoped<SuccessResponse>();
//builder.Services.AddSingleton<SuccessResponse>();
//builder.Services.AddTransient<SuccessResponse>();

var app = builder.Build();

#region Swagger Config
if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
	//app.UseDeveloperExceptionPage();
}
#endregion

app.UseCors("ApiCorsPolicy");
//app.UseResponseCompression();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.AddGlobalErrorHandler();
app.MapControllers();

app.Run();
