using Deposito.BLL.Interfaces.Repository;
using Deposito.BLL.Interfaces.Services;
using Deposito.BLL.Models.DTO;
using Deposito.BLL.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Deposito.API.Controllers;

public class UsuariosController : Controller
{
    private readonly IUsuarioService  _usuarioService;

    public UsuariosController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    [Route("/usuarios")]
    public List<UsuariosDTO> ListarUsuarios()
    {
        return _usuarioService.ListarUsuarios();
    }

    [HttpGet]
    [Route("/usuarios/{username}")]
    public UsuariosDTO BuscarUsuarioPorId(string username)
    {
        return _usuarioService.BuscarUsuarioPorLogin(username);
    }

    [HttpPost]
    [Route("/usuarios/cadastrar")]
    public string CadastraUsuario(UsuarioCadastroViewModel usuario)
    {
        if (_usuarioService.VerificaSeTemCadastro(usuario) == true)
        {
            return "CPF Já Cadastrado";
        }

        if (_usuarioService.CadastraUsuario(usuario) == true)
        {
            return "usuario Cadastrado";
        }
        else
        {
            return "Erro ao cadastrar";
        }
    }
    
}