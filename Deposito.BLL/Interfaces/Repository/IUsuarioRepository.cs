using Deposito.BLL.Models.DTO;
using Deposito.BLL.Models.ViewModels;

namespace Deposito.BLL.Interfaces.Repository;

public interface IUsuarioRepository
{
    public List<UsuariosDTO> ListarUsuarios();
    public UsuariosDTO BuscarUsuarioPorLogin(string login);
    public bool CadastraUsuario(UsuarioCadastroViewModel usuario);
    public bool VerificaSeTemCadastro(UsuarioCadastroViewModel usuario);
    public bool ValidarUsuario(string username, string password);
    
}