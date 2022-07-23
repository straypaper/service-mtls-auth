using System.Security.Cryptography.X509Certificates;

namespace CustomerService
{
    public class ClientCertificateValidation : IClientCertificateValidationService
    {
        private readonly IConfiguration _config;

        public ClientCertificateValidation(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public bool ValidateCertificate(X509Certificate2 clientCertificate)
        {
            return clientCertificate.Thumbprint.Equals(_config["ClientCertificateThumbprint"], StringComparison.OrdinalIgnoreCase);
        }
    }
}
