using Asp.Versioning;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Api.DTOs;
using Sample.Api.Interfaces;
using Sample.Application;
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
        /// <response code="200">User list loaded</response>
        /// <response code="500">Internal server error</response>
        [Authorize(Role.Administrator)]
        [HttpGet("list")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<UserDTO>))]
        [ProducesResponseType<ApiError>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListPagination([FromRoute] PaginationSettingsDTO pageSetting)
        {
            var result = await _users.ListAsync(pageSetting);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return UnexpectedError(result.Error);
            }
        }

        /// <summary>
        /// Retrieves a specific product by unique id.
        /// </summary>
        /// <param name="id">User unique identificator</param>
        /// <returns>an user</returns>
        /// <response code="200">User found</response>
        /// <response code="404">User not found</response>
        /// <response code="500">Internal server error</response>
        [Authorize]
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiError>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _users.GetAsync(id);
            
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else if (result.Error is NotFoundError)
            {
                return NotFound(result.Error);
            }
            else
            {
                return UnexpectedError(result.Error);
            }
        }

        /// <summary>
        /// Creates a new account.
        /// </summary>
        /// <param name="accountData">new account data</param>
        /// <returns>new user</returns>
        /// <response code="200">new account has been created</response>
        /// <response code="409">account with this username already exists</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("create")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType<UserDTO>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiError>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<ApiError>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAccount([FromBody]CreateAccountDTO accountData)
        {
            var result = await _security.CreateAccount(accountData);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else if (result.Error is AlreadyExistsError)
            {
                return Conflict(result.Error);
            }
            else
            {
                return UnexpectedError(result.Error);
            }
        }

        /// <summary>
        /// Authenticate user.
        /// </summary>
        /// <param name="authenticationData">authentication data</param>
        /// <returns>new user</returns>
        /// <response code="200">account has been authenticated successfully</response>
        /// <response code="401">account authentication failed</response>
        [HttpPost("authenticate")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationResultDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateAccountDTO authenticationData)
        {
            var result = await _security.Authenticate(authenticationData.Username, authenticationData.Password);

            if (result.IsSuccess)
            {
                var token = _token.CreateToken(result.Data);

                return Ok(new AuthenticationResultDTO()
                {
                    Token = token.Data,
                    Expiration = token.Expiration
                });
            }
            else
            {
                return Unauthorized();
            }
        }
    }

    #endregion
}