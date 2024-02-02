using Dapper;
using DnD35v3.Data;
using MySqlConnector;
using System.Collections.Concurrent;

namespace DnD35v3.Modules
{
    public class FetchUserData
    {
        private string? connectionString;

        private readonly IConfiguration _configuration;
        private readonly UserAccount _userAccount;
        private readonly UserToken _userToken;
        private readonly ConcurrentBag<UserList> _userList;

        public FetchUserData(IConfiguration configuration, UserAccount userAccount, UserToken userToken)
        {
            _configuration = configuration;
            _userAccount = userAccount;
            _userToken = userToken;
            _userList = new ConcurrentBag<UserList>();
        }

        public async Task FetchUser(string userID)
        {
            connectionString = _configuration.GetConnectionString("dnd35live");

            await using (var db = new MySqlConnection(connectionString))
            {
                try
                {
                    string sqlString = @"
                SELECT
                id,
                user_id,
                email,
                account_name,
                picture,
                created_at,
                last_login,
                Account_name_edited,
                new_user,
                IsDarkMode
                FROM user_list
                WHERE
                user_id = @userID
            ";

                    var user = db.QueryFirstOrDefault<UserAccount>(sqlString, new { userID });

                    if (user != null)
                    {
                        _userAccount.ID = user.ID;
                        _userAccount.User_id = user.User_id;
                        _userAccount.Email = user.Email;
                        _userAccount.Account_name = user.Account_name;
                        _userAccount.Picture = user.Picture;
                        _userAccount.Created_at = user.Created_at;
                        _userAccount.Account_name_edited = user.Account_name_edited;
                        _userAccount.Last_login = user.Last_login;
                        _userAccount.New_user = user.New_user;
                        _userAccount.IsDarkMode = user.IsDarkMode;
                    }
                    _userToken.Token = userID;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public async Task CreateUser(string userID, string userName, string email)
        {
            connectionString = _configuration.GetConnectionString("dnd35live");

            await using (var db = new MySqlConnection(connectionString))
            {
                try
                {
                    string insertSqlString = @"
            INSERT INTO user_list(user_id, account_name, email)
            VALUES (@userID, @userName, @email);
            ";
                    await db.ExecuteAsync(insertSqlString, new { userID, userName, email });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public async Task UpdateUsername(string userID, string newUserName)
        {
            connectionString = _configuration.GetConnectionString("dnd35live");

            await using (var db = new MySqlConnection(connectionString))
            {
                try
                {
                    string insertSqlString = @"
            UPDATE user_list SET Account_name = @newUserName WHERE User_id = @userID";
                    await db.ExecuteAsync(insertSqlString, new { userID, newUserName });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public async Task UpdateUserEmail(string userID, string? newEmail)
        {
            connectionString = _configuration.GetConnectionString("dnd35live");

            await using (var db = new MySqlConnection(connectionString))
            {
                try
                {
                    string insertSqlString = @"
            UPDATE user_list SET Email = @newEmail WHERE User_id = @userID";
                    await db.ExecuteAsync(insertSqlString, new { userID, newEmail });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}