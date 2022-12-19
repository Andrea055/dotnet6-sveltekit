using AspNetCore.Proxy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    builder.Services.AddProxies();
    app.RunProxy(proxy => proxy
        .UseHttp((context, args) =>
        {
            if (context.Request.Path.StartsWithSegments("/"))
                return "http://localhost:5173";

            return "http://localhost:5173";
        })
        .UseWs((context, args) => "ws://127.0.0.1:5173"));
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseStaticFiles();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
