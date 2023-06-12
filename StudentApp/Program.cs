using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentApp.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
  builder.Configuration.GetConnectionString("DefaultConnection")
  ));

// Authetication
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

//// Validation
//builder.Services.AddScoped<IValidator<User>, UserValidator>();
//builder.Services.AddScoped<IValidator<UserDto>, UserDtoValidator>();
//builder.Services.AddScoped<IValidator<StudentTeacherDto>, StudentTeacherDtoValidator>();

//Using Helperfunction
//builder.Services.AddScoped<HelperFunctions>();


//builder.Services.AddAuthorization(options => {
//	options.AddPolicy("Student", policy =>
//					  policy.RequireClaim("RoleId", "1"));
//});

//builder.Services.AddAuthorization(options => {
//	options.AddPolicy("Teacher", policy =>
//					  policy.RequireClaim("RoleId", "2"));
//});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
