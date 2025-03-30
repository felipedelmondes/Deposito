namespace Deposito.BLL.Models.DTO;

public class UsuariosDTO
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Senha_Hash { get; set; }
    public string Nome_Completo { get; set; }
    public string Cpf { get; set; }
    public string Data_Nascimento { get; set; }
    public bool Ativo { get; set; }
    public string Data_Criacao { get; set; }
    public string Data_atualizacao { get; set; }
}