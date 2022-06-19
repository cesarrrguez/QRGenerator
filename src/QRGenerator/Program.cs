using QRCoder;

var policy = "MyPolicy";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policy, build =>
    {
        build
            .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseCors(policy);

app.MapGet("/qr", (string text) =>
{
    var qrGenerator = new QRCodeGenerator();
    var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
    var qrCodeAsBitmap = new BitmapByteQRCode(qrCodeData);
    var bitmap = qrCodeAsBitmap.GetGraphic(20);

    using var ms = new MemoryStream();
    ms.Write(bitmap);
    byte[] byteImage = ms.ToArray();
    return Convert.ToBase64String(byteImage);
});

app.Run();
