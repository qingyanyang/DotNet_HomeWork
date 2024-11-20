using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Net_6_Assignment.Config;
using System.Text;

namespace Net_6_Assignment.Init
{
    public static class JwtInitExtension
    {
        public static void AddJWTEXT(this IServiceCollection services, JWTConfig jWTConfig)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jWTConfig.Issuer,
                        ValidateIssuer = true,
                        ValidAudience = jWTConfig.Audience,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jWTConfig.SecrectKey))
                    };
                });
        }
    }
}
