using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BusinessManagement.BusinessLayer.Utilities.Security.Encryption;

public class SecurityKeyHelper
{
    public static SecurityKey CreateSecurityKey(string securityKeys)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKeys));
    }
}
