using Dapper;
using DnD35v3.Data;
using MySqlConnector;
using System.Collections.Concurrent;

namespace DnD35v3.Modules
{
    public class GroupHandler
    {
        private readonly IConfiguration _configuration;
        private readonly ConcurrentBag<DungeonGroups> _dungeonGroups;
        private readonly ConcurrentBag<DungeonTracker> _dungeonTracker;

        public event Action OnGroupUpdated;

        public GroupHandler(IConfiguration configuration)
        {
            _configuration = configuration;
            _dungeonGroups = new ConcurrentBag<DungeonGroups>();
            _dungeonTracker = new ConcurrentBag<DungeonTracker>();
        }

        public IEnumerable<DungeonGroups> DungeonGroups => _dungeonGroups;
        public IEnumerable<DungeonTracker> DungeonTracker => _dungeonTracker;

        public async Task FetchGroups()
        {
            Console.WriteLine("Running: FetchGroups()");
            string connectionString = _configuration.GetConnectionString("dnd35live");

            await using (var db = new MySqlConnection(connectionString))
            {
                try
                {
                    string sqlString = "SELECT * FROM dungeon_groups";
                    var groups = (await db.QueryAsync<DungeonGroups>(sqlString)).ToList();

                    _dungeonGroups.Clear();
                    foreach (var group in groups)
                    {
                        _dungeonGroups.Add(group);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            await FetchTracker();
        }

        public async Task FetchTracker()
        {
            string connectionString = _configuration.GetConnectionString("dnd35live");

            await using (var db = new MySqlConnection(connectionString))
            {
                try
                {
                    string sqlString = "SELECT * FROM user_dungeon_tracker";
                    var tracker = (await db.QueryAsync<DungeonTracker>(sqlString)).ToList();

                    foreach (var track in tracker)
                    {
                        _dungeonTracker.Add(track);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public async Task CreateGroup(string groupName, string? description, int groupOwnerId)
        {
            string connectionString = _configuration.GetConnectionString("dnd35live");

            var newGroup = new DungeonGroups
            {
                Group_name = groupName,
                Description = description,
                Group_owner = groupOwnerId,
            };

            string insertSql = @"INSERT INTO dungeon_groups (Group_owner, Group_name, Description)
                         VALUES (@Group_owner, @Group_name, @Description)";

            await using (var db = new MySqlConnection(connectionString))
            {
                try
                {
                    await db.ExecuteAsync(insertSql, newGroup);

                    _dungeonGroups.Add(newGroup);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            await FetchGroups();
            OnGroupUpdated?.Invoke();
        }

        public async Task JoinGroup(int groupID, int userID)
        {
            string connectionString = _configuration.GetConnectionString("dnd35live");

            var newTrack = new DungeonTracker
            {
                Dungeon_ID = groupID,
                User_ID = userID,
            };

            string insertSql = @"INSERT INTO user_dungeon_tracker (Dungeon_ID, User_ID)
                         VALUES (@Dungeon_ID, @User_ID)";

            await using (var db = new MySqlConnection(connectionString))
            {
                try
                {
                    await db.ExecuteAsync(insertSql, newTrack);

                    _dungeonTracker.Add(newTrack);
                    OnGroupUpdated?.Invoke();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}