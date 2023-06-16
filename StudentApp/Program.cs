using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentApp.Configuration;
using StudentApp.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment.IsDevelopment() ? ".development" : "";
builder.Configuration.AddJsonFile($"appsettings{environment}.json", optional: true, reloadOnChange: true);


#region 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
  builder.Configuration.GetConnectionString("DefaultConnection")
));
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

var app = builder.Build();

#region Swagger Config
if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}
#endregion

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.AddGlobalErrorHandler();
app.Run();
