using Deposito.BLL.Interfaces.Repository;
using Deposito.BLL.Interfaces.Services;
using Deposito.BLL.Models.DTO;
using Deposito.BLL.Models.ViewModels;
using Deposito.BLL.CryptoService;

namespace Deposito.BLL.Services;

public class UsuariosService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuariosService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public List<UsuariosDTO> ListarUsuarios()
    {
        return _usuarioRepository.ListarUsuarios();
    }

    public UsuariosDTO BuscarUsuarioPorLogin(string login)
    {
        return  _usuarioRepository.BuscarUsuarioPorLogin(login);
    }

    public bool CadastraUsuario(UsuarioCadastroViewModel usuario)
    {
        usuario.NomeCompleto = usuario.NomeCompleto.ToUpper();
        usuario.Email = usuario.Email.ToUpper();
        usuario.UserName = usuario.UserName.ToUpper();
        usuario.Senha = CriptoSenhas.HashPassword(usuario.Senha);
        return _usuarioRepository.CadastraUsuario(usuario);
    }

    public bool VerificaSeTemCadastro(UsuarioCadastroViewModel usuario)
    {
        return _usuarioRepository.VerificaSeTemCadastro(usuario);
    }
}