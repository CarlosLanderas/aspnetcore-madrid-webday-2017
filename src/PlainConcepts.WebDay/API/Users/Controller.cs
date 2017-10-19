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
    public class Controller : ControllerBase
    {
        private readonly WebDayDbContext _dbContext;

        public Controller(WebDayDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Authorize(Policy = Actions.CreateUser)]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest createRequest)
        {

            var user = Model.User.Create(createRequest.UserName, createRequest.Name, createRequest.Surname);
            _dbContext.Users.Add(user);

            var userRoles = createRequest.Roles
                .Select(r => UserRole.Create(user.Id, Role.From(r).Id));
            _dbContext.UserRoles.AddRange(userRoles);

            await _dbContext.SaveChangesAsync();
            return Created($"api/user/{user.Id}", user.Id);
        }

        [HttpGet, Route("")]
        [Authorize(Policy = Actions.ListUsers)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok( await _dbContext.Users.Include("_userRoles").ToListAsync(cancellationToken));
        }
    }
}
