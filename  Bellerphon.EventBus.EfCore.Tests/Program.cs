var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwaggerUI();
app.UseSwagger();

app.MapControllers();

app.Run();