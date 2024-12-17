namespace API_Web.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }

        public string Surname { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Patronymic { get; set; } = null!;

        public string? Login { get; set; }

        public string? Password { get; set; }
    }
}
