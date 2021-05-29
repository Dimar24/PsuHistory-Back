using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PsuHistory.API.Host.Options

{
    public class AuthOptions
    {
        public string Issuer { get; set; } = ""; // издатель токена
        public string Audience { get; set; } = ""; // потребитель токена
        public string Secret { get; set; } = ""; // ключ для шифрации
        public int TokenLifetime { get; set; } = 3600; // время жизни токена - 1 минута
        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }
    }
}
