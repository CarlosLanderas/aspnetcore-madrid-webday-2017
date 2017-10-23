using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlainConcepts.WebDay.API.Users.Requests;
using PlainConcepts.WebDay.Infrastructure.Authentication;
using PlainConcepts.WebDay.Infrastructure.Authentication.Policies;
using PlainConcepts.WebDay.Infrastructure.DataContext;
using PlainConcepts.WebDay.Model;

namespace PlainConcepts.WebDay.API.Users
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly WebDayDbContext _dbContext;

        public UsersController(WebDayDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Authorize(Policy = Actions.CreateUser)]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest createRequest, CancellationToken cancellationToken)
        {

            var user = Model.User.Create(createRequest.UserName, createRequest.Name, createRequest.Surname);
            _dbContext.Users.Add(user);

            var userRoles = createRequest.Roles
                .Select(r => UserRole.Create(user.Id, Role.From(r).Id));
            _dbContext.UserRoles.AddRange(userRoles);

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Created(string.Empty, user.Id);
        }
        
        [HttpGet, Route("")]
        [Authorize(Policy = Actions.ListUsers)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok( await _dbContext.Users.Include("_userRoles").ToListAsync(cancellationToken));
        }

        [HttpDelete, Route("{id}")]
        [Authorize(Policy = Actions.RemoveUser)]
        public async Task<IActionResult> Remove([FromRoute] RemoveUserRequest removeRequest, CancellationToken cancellationToken)
        {
            var user = _dbContext.Users.Find(removeRequest.Id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return Ok(user.Id);
            }
            return BadRequest();
        }
    }
}
