using System.Reflection.Metadata;
using Deposito.BLL.Interfaces.Repository;
using Deposito.BLL.Models.DTO;
using Dapper;
using Deposito.BLL.Models.ViewModels;
using Deposito.DAL.Context;
using Microsoft.Win32.SafeHandles;

namespace Deposito.DAL.Repository;

public class UsuarioRepository:IUsuarioRepository
{
    private readonly DBContext _context;

    public UsuarioRepository(DBContext context)
    {
        _context = context;
    }


    public List<UsuariosDTO> ListarUsuarios()
    {
        var retorno  = new List<UsuariosDTO>();
        var query = @" select 
	                    u.id,
                        u.username,
                        u.email,
                        u.nome_completo,
                        u.cpf,
                        u.data_nascimento,
                        u.ativo,
                        u.data_criacao,
                        u.data_atualizacao
                    from deposito.usuario u ";

        try
        {
            using (var conn = _context.CreateConnection())
            {
                retorno = (List<UsuariosDTO>)conn.Query<UsuariosDTO>(query);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return retorno;
    }

    public UsuariosDTO BuscarUsuarioPorLogin(string login)
    {
        var retorno = new UsuariosDTO();

        var query = @$"select 
	                    u.id,
                      u.username,
                      u.email,
                      u.nome_completo,
                      u.cpf,
                      u.data_nascimento,
                      u.ativo,
                      u.data_criacao,
                      u.data_atualizacao
                    from deposito.usuario u
                    where u.username = '{login}' ";

        try
        {
            using (var conn = _context.CreateConnection())
            {
                retorno = conn.Query<UsuariosDTO>(query).FirstOrDefault();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        
        return retorno;
    }

    public bool CadastraUsuario(UsuarioCadastroViewModel usuario)
    {
        var cadastro = new UsuariosDTO()
        {
            Nome_Completo = usuario.NomeCompleto,
            Senha_Hash = usuario.Senha,
            Cpf = usuario.Cpf,
            Username = usuario.UserName,
            Data_Nascimento = usuario.DataNascimento
        };
        var insert = @$"INSERT INTO deposito.usuario (username, email, senha_hash, nome_completo, cpf, data_nascimento) 
                        VALUES
                        ('{usuario.UserName}', '{usuario.Email}', '{usuario.Senha}', '{usuario.NomeCompleto}', '{usuario.Cpf}', '{usuario.DataNascimento}')";

        try
        {
            using (var conn = _context.CreateConnection())
            {
                conn.Execute(insert);
                return true;
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool VerificaSeTemCadastro(UsuarioCadastroViewModel usuario)
    {
        int retorno = 0;
        var query = @$" select count(1) from deposito.usuario u where u.cpf= '{usuario.Cpf}' ";

        try
        {
            using (var conn = _context.CreateConnection())
            {
                retorno = conn.Query<int>(query).FirstOrDefault();
            }

            if (retorno == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}