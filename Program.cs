using BooksList.Interfaces;
using BooksList.Services;

var builder = WebApplication.CreateBuilder(args);
var XmlFilePath = builder.Configuration["XmlFilePath"];
if (string.IsNullOrEmpty(XmlFilePath))
{
    throw new Exception("XmlFilePath is not configured in appsettings.");
}
// Add services to the container.
builder.Services.AddSingleton<IBookService>(new BookService(XmlFilePath));

//  CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()  
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AllowAllOrigins");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
