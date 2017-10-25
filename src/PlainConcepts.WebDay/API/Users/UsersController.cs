using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlainConcepts.WebDay.Application.Queries;
using PlainConcepts.WebDay.API.Users.Requests;
using PlainConcepts.WebDay.Infrastructure.Authentication;
using PlainConcepts.WebDay.Infrastructure.DataContext;
using PlainConcepts.WebDay.Infrastructure.Repositories;
using PlainConcepts.WebDay.Model;

namespace PlainConcepts.WebDay.API.Users
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly GetUsersQuery _getUsersQuery;
        public UsersController(IUserRepository userRepository, GetUsersQuery getUsersQuery)
        {
            _userRepository = userRepository;
            _getUsersQuery = getUsersQuery;
        }

        [HttpGet, Route("")]
        [Authorize(Policy = Actions.ListUsers)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _getUsersQuery.ExecuteAsync();
            return Ok(users);
        }

        [HttpPost]
        [Authorize(Policy = Actions.CreateUser)]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest createRequest, CancellationToken cancellationToken)
        {
            var user = Model.User.Create(createRequest.UserName, createRequest.Name, createRequest.Surname);

            foreach (var role in createRequest.Roles.Select(Role.From))
            {
                user.AddRole(role.Id);
            }

            await _userRepository.AddAsync(user, cancellationToken);
            return Created(string.Empty, user.Id);
        }

        [HttpDelete, Route("{id}")]
        [Authorize(Policy = Actions.RemoveUser)]
        public async Task<IActionResult> Remove([FromRoute] RemoveUserRequest removeRequest, CancellationToken cancellationToken)
        {
            await _userRepository.RemoveAsync(removeRequest.Id, cancellationToken);
            return Ok();
        }
    }
}
