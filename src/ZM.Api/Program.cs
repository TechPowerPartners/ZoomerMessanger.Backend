using ZM.Api.Hubs;
using ZM.Application;
using ZM.Common.Hubs;
using ZM.Infrastructure;
using ZM.Infrastructure.Persistence;
using ZM.Infrastructure.RoutePrefix;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opts =>
{
	opts.Conventions.Add(new RoutePrefixConvention());
});

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(builder =>
		{
			builder.AllowAnyHeader()
				.AllowAnyMethod()
				.SetIsOriginAllowed((host) => true)
				.AllowCredentials();
		});
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddSingleton(TimeProvider.System);

builder.Services.AddExceptionHandler<ExceptionHandler>();

var app = builder.Build();
app.UseExceptionHandler(opt => { });

Seeder.Seed(app.Services);

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<P2PChatHub>(HubsRoutes.P2PChatsHubs);

app.MapControllers();

app.Run();
