using VueApp2.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("train", (string codice) =>
{
    try {  return Newtonsoft.Json.JsonConvert.SerializeObject(GestioneTreno.RicercaTreno(codice)); } catch {  return null; }

});
app.MapGet("autoCompleteStation", (string? stazione) =>
{
    try { return Newtonsoft.Json.JsonConvert.SerializeObject(GestioneTreno.AutoCompletamentoStazione(stazione)); } catch { return null; }

});



app.MapFallbackToFile("/index.html");

app.Run();

