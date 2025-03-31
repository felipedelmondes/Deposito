using System.Security.Claims;
using Deposito.BLL.Interfaces.Repository;
using Deposito.BLL.Interfaces.Services;
using Deposito.BLL.Models.DTO;
using Deposito.BLL.Models.ViewModels;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Deposito.API.Controllers;

public class UsuariosController : Controller
{
    private readonly IUsuarioService  _usuarioService;
    private readonly ITokenService _tokenService;

    public UsuariosController(IUsuarioService usuarioService, ITokenService _tokenService)
    {
        _usuarioService = usuarioService;
        _tokenService = _tokenService;
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

    [HttpPost]
    [Route("/usuarios/login")]
    public IActionResult Login(LoginDTO request)
    {
        var validaUsuario = _tokenService.ValidarUsuario(request.Username, request.Senha);

        if (!validaUsuario)
        {
            return Unauthorized();
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, request.Username),
            new Claim(ClaimTypes.Role, "User") // Adicione roles conforme necessário
        };
        
        var token = _tokenService.GenerateToken(claims);
        
        return Ok(new { Token = token });

    }
    
}