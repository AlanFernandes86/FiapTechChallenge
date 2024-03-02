namespace TechChallenge.Domain.Entities;

public class Client
{
    public long Cpf { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public Client(int cpf, string name, string email)
    {
        Cpf = cpf;
        Name = name;
        Email = email;
    }

    public Client() 
    { 
        Cpf = 0;
        Name = string.Empty;
        Email = string.Empty;
    }
}
