namespace DnD35v3.Data
{
    public class DungeonGroups
    {
        public int ID { get; set; }
        public int Group_owner { get; set; }
        public int Member_count { get; set; }
        public string Group_name { get; set; } = string.Empty;
        public string? Picture { get; set; }
        public string? Description { get; set; }
        public DateTime Created_date { get; set; }
        public bool Is_private { get; set; }
    }

    public class DungeonTracker
    {
        public int User_ID { get; set; }
        public int Dungeon_ID { get; set; }

        public bool Is_accepted { get; set; }
        public DateTime Created_date { get; set; }
    }
}