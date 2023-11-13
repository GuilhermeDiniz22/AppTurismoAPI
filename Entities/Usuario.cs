namespace AppTurismoAPI.Entities
{
    public class Usuario
    {
        internal static readonly object Claims;

        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public byte[] SenhaHash { get; set; } = new byte[0];

        public byte[] SenhaSalt { get; set; } = new byte[0];
    }
}
