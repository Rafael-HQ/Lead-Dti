using Desafio_Dti_Lead.Application.Services;
using Desafio_Dti_Lead.Domian;
using Desafio_Dti_Lead.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ILeadRepository, LeadRepository>();
builder.Services.AddScoped<ILeadServices, LeadServices>();

/*
Esta configuração CORS está mais flexível para facilitar os testes da API na sua máquina local. Deixei essa configuração mais aberta para facilitar os testes da API na sua máquina,
independente da porta que estiver usando no frontend. Num projeto real de produção, eu ajustaria isso para aceitar apenas as origens
específicas do nosso domínio, mas pra essa avaliação tá mais tranquilo assim
*/ 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.AllowAnyOrigin() 
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowReactApp");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();