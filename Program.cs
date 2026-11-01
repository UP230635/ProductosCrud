using MYBACK.Endpoints;
using MYBACK.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors( options =>
{
   options.AddPolicy("AllowAll", policy =>
   {
       policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
   }); 
});

builder.Services.AddSingleton<Conexion>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.MapProductoEndpoints();

app.Run();
