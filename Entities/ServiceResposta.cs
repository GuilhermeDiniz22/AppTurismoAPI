namespace AppTurismoAPI.Entities
{
    public class ServiceResposta<T>
    {
        public T? Dados { get; set; }

        public bool Sucesso { get; set; } = true;

        public string Mensagem { get; set; } = string.Empty;
    }
}
