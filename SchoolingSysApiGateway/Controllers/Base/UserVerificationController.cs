using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SchoolingSysApiGateway.Dto;
using SchoolingSysApiGateway.ExtensionMethods;
using static SchoolingSysApiGateway.Dto.ResponseMessageExtensions;

namespace SchoolingSysApiGateway.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserVerificationController : BaseController
    {
        private readonly FirebaseAuth _firebaseAuth;
        public UserVerificationController(IConfiguration configuration, IMapper mapper) : base(configuration, mapper)
        {
            _firebaseAuth = FirebaseAuth.DefaultInstance;

        }

        [HttpGet]
            [Route("RefreshToken")]
            public async Task<JsonResult> RefreshToken()
            {
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(this._configuration["JWT:Secret"]);

                    try
                    {
                        var tokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["JWT:Secret"])),
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidIssuer = this._configuration["JWT:ValidIssuer"],
                            ValidAudience = this._configuration["JWT:ValidAudience"],
                            ClockSkew = TimeSpan.FromMinutes(5) // Default clock skew
                        };
                        var newToken = GetToken();
                        var _expiry = _configuration["JWT:Expiry"].ToInt() * 60;

                        return new JsonResult(new
                        {
                            status = ApiResponseStatus.Success.ToInt(),
                            msg = ResponseMessages.Success.GetMessage(),
                            data = new { token = newToken },
                            expiry = _expiry
                        });
                    }
                    catch
                    {
                        throw;

                    }
                }
                catch
                {
                    throw;
                }
            }
        
        private string GetToken()
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: this._configuration["JWT:ValidIssuer"],
                audience: this._configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(_configuration["JWT:Expiry"].ToInt()),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            var returnToken = new JwtSecurityTokenHandler().WriteToken(token);
            return returnToken;
        }
    }
}
