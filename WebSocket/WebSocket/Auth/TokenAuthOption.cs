using Microsoft.IdentityModel.Tokens;
using System;

namespace Masterslavl.Auth
{
    public class TokenAuthOption
    {
        public static string Audience { get; } = "http://localhost:9955/";
        public static string Issuer { get; } = "http://localhost:9955/";
        public static RsaSecurityKey Key { get; } = new RsaSecurityKey(RSAKeyHelper.GenerateKey());
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);

        public static TimeSpan ExpiresSpan { get; } = TimeSpan.FromMinutes(60);
        public static string TokenType { get; } = "Bearer";
    }
}

