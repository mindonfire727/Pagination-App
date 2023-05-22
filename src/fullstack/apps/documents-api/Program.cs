using Fullstack.DocumentsApi.Features.Documents.Data;
using Fullstack.DocumentsApi.Features.Documents.Repositories;
using Fullstack.DocumentsApi.Features.Documents.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DocumentDbContext>(options => options.UseInMemoryDatabase("FullStack"));
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(
    policy =>
    {
      policy.WithOrigins("http://localhost:4200")
      .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
SeedInMemoryDatabase(app);

if (app.Environment.IsDevelopment())
{
  //app.UseExceptionHandler("/Error-development");
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseExceptionHandler("/Error");
app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void SeedInMemoryDatabase(IHost host)
{
  using var scope = host.Services.CreateScope();
  using var context = scope.ServiceProvider.GetService<DocumentDbContext>();

  if (context is null)
    return;

  context.Database.EnsureCreated();
}
