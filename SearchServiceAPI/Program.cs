using FastEndpoints;
using SearchServiceAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();

// Add search Services  
builder.Services.AddSearchServices();

// Add MongoDb
builder.Services.AddMongoDatabase(builder.Configuration); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerDocument();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi(options => options.Path = "swagger");
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger", "My API V1");
        options.RoutePrefix = "docs";
    });
}

// app.UseHttpsRedirection();
app.UseFastEndpoints();

app.Run();