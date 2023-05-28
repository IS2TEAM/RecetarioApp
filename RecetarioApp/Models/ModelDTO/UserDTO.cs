namespace RecetarioApp.Models.ModelDTO
{
    public class UserDTO
    {
        public int IdUser { get; set; }
        public string Username { get; set; } = null!;

        public string EmailUser { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
