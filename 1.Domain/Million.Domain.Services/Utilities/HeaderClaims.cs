using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Common.Utils.Utils.Interface
{
    [ExcludeFromCodeCoverage]
    public static class HeaderClaims
    {
        /// <summary>
        /// Method to get value claim from JwtToken
        /// </summary>
        /// <param name="authorization"> Request.Headers["Authorization"] </param>
        /// <param name="claim"></param>
        /// <returns></returns>
        public static string GetClaimValue(string token, string claim)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            string authHeader = token.Replace("Bearer ", "").Replace("bearer ", "");
            JwtSecurityToken tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;

            Claim claimData = tokenS.Claims.FirstOrDefault(cl => cl.Type.ToUpper() == claim.ToUpper());

            return claimData != null ? claimData.Value : string.Empty;
        }
    }
}