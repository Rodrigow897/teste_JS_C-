var builder = WebApplication.CreateBuilder(args);

// Registra o serviço CORS
builder.Services.AddCors();

var app = builder.Build();

// Configura o middleware CORS
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.MapPost("/imc", (IMCRequest req) =>
{
    double imc = req.Peso / (req.Altura * req.Altura);
    string classificacao = imc switch
    {
        < 18.5 => "Abaixo do peso",
        < 25 => "Peso normal",
        < 30 => "Sobrepeso",
        < 35 => "Obesidade Grau 1",
        < 40 => "Obesidade Grau 2",
        _ => "Obesidade Grau 3"
    };

    return Results.Json(new { imc, classificacao });
});

app.Run();

record IMCRequest(double Peso, double Altura);
