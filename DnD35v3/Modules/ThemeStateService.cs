using Dapper;
using DnD35v3.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Concurrent;

namespace DnD35v3.Modules
{
    public class ThemeStateService
    {
        public bool IsDarkMode { get; set; }

        public event Action? OnChange;

        public void ToggleTheme()
        {
            IsDarkMode = !IsDarkMode;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }

    public class SaveThemeState
    {
        private string? connectionString;
        private readonly IConfiguration _configuration;
        private readonly UserAccount _userAccount;
        private readonly UserToken _userToken;
        private readonly ConcurrentBag<UserList> _userList;

        public SaveThemeState(IConfiguration configuration, UserAccount userAccount, UserToken userToken)
        {
            _configuration = configuration;
            _userAccount = userAccount;
            _userToken = userToken;
            _userList = new ConcurrentBag<UserList>();
        }

        public async Task Save(string userID, bool isDarkmode)
        {
            connectionString = _configuration.GetConnectionString("dnd35live");

            await using (var db = new MySqlConnection(connectionString))
            {
                try
                {
                    string insertSqlString = @"
            UPDATE user_list SET IsDarkMode = @isDarkmode
            WHERE User_id = @userID;
            ";
                    await db.ExecuteAsync(insertSqlString, new { userID, isDarkmode });

       
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}