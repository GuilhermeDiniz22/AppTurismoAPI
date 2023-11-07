namespace AppTurismoAPI.Services
{
    public interface IEmailService
    {
        void EnviarEmail(string email, string mensagem);
    }
}