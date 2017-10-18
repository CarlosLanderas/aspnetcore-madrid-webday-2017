using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlainConcepts.WebDay.API.Users.Requests;
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

        [HttpPost, Route("")]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest createRequest)
        {

            var user = Model.User.Create(createRequest.Name, createRequest.Surname);
            _dbContext.Users.Add(user);

            var userRoles = createRequest.Roles
                .Select(r => UserRole.Create(user.Id, Role.From(r).Id));
            _dbContext.UserRoles.AddRange(userRoles);

            await _dbContext.SaveChangesAsync();
            return Created("api/users", user.Id);
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> GetAll()
        {
            var users = _dbContext.Users.Include(u => u.Roles)
                .Select(u => new
                {
                    u.Name,
                    u.Surname,
                    Roles = u.Roles.Select(r => Role.From(r.RoleId))
                });

            return Ok(users);
        }
    }
}
