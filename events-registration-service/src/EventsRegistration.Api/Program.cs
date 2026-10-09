using EventsRegistration.Infrastructure;
using EventsRegistration.Application.Events.CreateEvent;
using Microsoft.EntityFrameworkCore;
using EventsRegistration.Domain;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<EventsRegistrationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EventsRegistration")));

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();
builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<EventsRegistrationDbContext>());


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateEventCommand).Assembly));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
