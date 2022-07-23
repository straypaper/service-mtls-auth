using CustomerService;

using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IClientCertificateValidationService, ClientCertificateValidation>();

builder.Services
    .AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
    .AddCertificate(options =>
    {
        options.AllowedCertificateTypes = CertificateTypes.Chained;
        options.Events = new CertificateAuthenticationEvents
        {
            OnAuthenticationFailed = context =>
            {
                context.Fail("Client certificate authentication failed");
                return Task.CompletedTask;
            },
            OnCertificateValidated = context =>
            {
                var validationService = context.HttpContext.RequestServices.GetRequiredService<IClientCertificateValidationService>();

                if (validationService.ValidateCertificate(context.ClientCertificate))
                    context.Success();
                else
                    context.Fail("Client certificate authentication failed after validation");

                return Task.CompletedTask;
            }
        };
    });

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.ConfigureHttpsDefaults(opt => opt.ClientCertificateMode = ClientCertificateMode.AllowCertificate);
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
