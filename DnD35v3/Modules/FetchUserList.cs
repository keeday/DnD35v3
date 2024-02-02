using Dapper;
using DnD35v3.Data;
using MySqlConnector;
using System.Collections.Concurrent;

namespace DnD35v3.Modules
{
    public class FetchUserList
    {
        private string? connectionString;

        private readonly IConfiguration _configuration;
        private readonly ConcurrentBag<UserList> _userList;

        public FetchUserList(IConfiguration configuration)
        {
            _configuration = configuration;
            _userList = new ConcurrentBag<UserList>();
        }

        public IEnumerable<UserList> Users => _userList;

        public async Task FetchUsers()
        {
            Console.WriteLine("Running: FetchUserList()");
            string connectionString = _configuration.GetConnectionString("dnd35live");

            await using (var db = new MySqlConnection(connectionString))
            {
                try
                {
                    string sqlString = "SELECT ID, Email, Account_name, Picture, Created_at, Last_login FROM user_list";

                    var users = (await db.QueryAsync<UserList>(sqlString)).ToList();

                    _userList.Clear();
                    foreach (var user in users)
                    {
                        _userList.Add(user);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}