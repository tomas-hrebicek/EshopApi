using Asp.Versioning;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Api.DTOs;
using Sample.Api.Interfaces;
using Sample.Application.DTOs;
using Sample.Application.Interfaces;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Api.Controllers.v1
{
    #region UserController

    [ApiVersion("1.0")]
    public class UserController : ApiController
    {
        private readonly IUsersService _users;
        private readonly ITokenService _token;
        private readonly ISecurityService _security;

        public UserController(IUsersService users, ISecurityService security, ITokenService token)
        {
            _users = users;
            _security = security;
            _token = token;
        }

        /// <summary>
        /// Retrieves an users list page by page settings.
        /// </summary>
        /// <param name="pageSetting">Page settings</param>
        /// <returns>an users list page</returns>
        [Authorize(Role.Administrator)]
        [HttpGet("list")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<UserDTO>))]
        public async Task<IActionResult> ListPagination([FromRoute] PaginationSettingsDTO pageSetting)
        {
            return Ok(await _users.ListAsync(pageSetting));
        }

        /// <summary>
        /// Retrieves a specific product by unique id.
        /// </summary>
        /// <param name="id">User unique identificator</param>
        /// <returns>an user</returns>
        /// <response code="200">User found</response>
        /// <response code="404">User not found</response>
        [Authorize]
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _users.GetAsync(id);
            return product is null ? NotFound() : Ok(product);
        }

        /// <summary>
        /// Creates a new account.
        /// </summary>
        /// <param name="accountData">new account data</param>
        /// <returns>new user</returns>
        /// <response code="200">new account has been created</response>
        [HttpPost("create")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        public async Task<IActionResult> CreateAccount([FromBody]CreateAccountDTO accountData)
        {
            var user = await _security.CreateAccount(accountData);
            return Ok(user);
        }

        /// <summary>
        /// Authenticate user.
        /// </summary>
        /// <param name="authenticationData">authentication data</param>
        /// <returns>new user</returns>
        /// <response code="200">account has been authenticated successfully</response>
        /// <response code="401">account has been authenticated successfully</response>
        [HttpPost("authenticate")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationResultDTO))]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateAccountDTO authenticationData)
        {
            var user = await _security.Authenticate(authenticationData.Username, authenticationData.Password);
            
            if (user is null)
            {
                return Unauthorized();
            }
            else
            {
                var token = _token.CreateToken(user);
                
                var result = new AuthenticationResultDTO() 
                { 
                    Token = token.Data,
                    Expiration = token.Expiration
                };

                return Ok(result);
            }
        }
    }

    #endregion
}