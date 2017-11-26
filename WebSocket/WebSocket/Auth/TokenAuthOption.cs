using Microsoft.IdentityModel.Tokens;
using System;

namespace WebSocketApi.Auth
{
    public class TokenAuthOption
    {
        public static string Audience { get; } = "Audience";
        public static string Issuer { get; } = "Issuer";
        public static RsaSecurityKey Key { get; } = new RsaSecurityKey(RSAKeyHelper.GenerateKey());
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);

        public static TimeSpan ExpiresSpan { get; } = TimeSpan.FromMinutes(60);
        public static string TokenType { get; } = "Bearer";
    }
}

