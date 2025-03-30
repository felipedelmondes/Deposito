using System;
using System.Security.Cryptography;
using System.Text;

namespace Deposito.BLL.CryptoService;

public static class CriptoSenhas
{
    private const int SaltSize = 16; // 128 bits 
    private const int HashSize = 32; // 256 bits
    private const int Iterations = 10000; // Número de iterações

    public static string HashPassword(string password)
    {
        // Criar um salt aleatório
        using (var rng = RandomNumberGenerator.Create())
        {
            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);

            // Criar o hash
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);

                // Combinar salt e hash
                byte[] hashBytes = new byte[SaltSize + HashSize];
                Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

                // Converter para string base64
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
    
    public static bool VerifyPassword(string password, string hashedPassword)
    {
        try
        {
            // Converter de base64 para bytes
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);

            // Extrair o salt
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Criar o hash da senha fornecida
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);

                // Comparar os hashes
                for (int i = 0; i < HashSize; i++)
                {
                    if (hashBytes[i + SaltSize] != hash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        catch
        {
            return false; // Se houver erro na conversão, considerar inválido
        }
    }
    
}