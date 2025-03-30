using Deposito.BLL.Models.DTO;
using Deposito.BLL.Models.ViewModels;

namespace Deposito.BLL.Interfaces.Services;

public interface IUsuarioService
{
    public List<UsuariosDTO> ListarUsuarios();
    public UsuariosDTO BuscarUsuarioPorLogin(string login);
    public bool CadastraUsuario(UsuarioCadastroViewModel usuario);
    public bool VerificaSeTemCadastro(UsuarioCadastroViewModel usuario);
}