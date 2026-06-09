namespace GMS_Backend.Models;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Cpf { get; set; }
    public string? Cep { get; set; }
    public string Password { get; set; }

    public ICollection<Product> Favorites { get; set; }
    public ICollection<Product> Chart { get; set; }
}