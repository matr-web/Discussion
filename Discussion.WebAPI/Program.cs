using Discussion.WebAPI.ProgramExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterCors(builder);

builder.Services.RegisterDatabase(builder);

builder.Services.RegisterServices();

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();

builder.Services.RegisterSwagger();

builder.Services.RegisterAuthentication(builder);

var app = builder.Build();

app.UseCors("FrontEndClient");

app.ConfigureSwagger();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
