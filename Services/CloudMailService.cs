namespace AppTurismoAPI.Services
{
    public class CloudMailService : IEmailService
    {
        private readonly string _mailtTo = string.Empty;
        private readonly string _mailFrom = string.Empty;

        public CloudMailService(IConfiguration configuration)
        {
            _mailtTo = configuration["mailSettings:mailToAddress"];
            _mailFrom = configuration["mailSettings:mailFromAddress"];
        }
        public void EnviarEmail(string email, string mensagem)
        {
            Console.WriteLine($"Email de {_mailFrom} para {_mailtTo}, " + $"com {nameof(CloudMailService)}.");
            Console.WriteLine($"Conteúdo: {mensagem}");
            Console.WriteLine($" Destinatário: {email}");
        }
    }
}
