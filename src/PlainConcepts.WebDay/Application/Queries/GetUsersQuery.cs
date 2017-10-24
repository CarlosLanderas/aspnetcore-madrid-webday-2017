using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PlainConcepts.WebDay.Application.Queries.Response;

namespace PlainConcepts.WebDay.Application.Queries
{
    public class GetUsersQuery
    {
        private readonly IDbConnection _connection;

        private const string query =
        @"SELECT U.Id, U.Name, U.Surname, U.Username, R.Id, R.Name
        FROM Users U
        LEFT JOIN UserRoles UR on U.Id = UR.UserId
        LEFT JOIN Roles R on UR.UserId = UR.UserId and R.Id = UR.RoleId";

        public GetUsersQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<User>> ExecuteAsync()
        {
            var usersLookup = new Dictionary<int, User>();
            await _connection.QueryAsync<User, Role, User>(query, (user, role) =>
            {
                if (!usersLookup.TryGetValue(user.Id, out User currentUser))
                {
                    usersLookup.Add(user.Id, currentUser = user);
                }
                currentUser.Roles.Add(role);
                return user;
            });

            return usersLookup.Values;
        }
    }
}
