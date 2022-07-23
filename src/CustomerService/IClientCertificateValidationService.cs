using System.Security.Cryptography.X509Certificates;

namespace CustomerService
{
    public interface IClientCertificateValidationService
    {
        bool ValidateCertificate(X509Certificate2 clientCertificate);
    }
}