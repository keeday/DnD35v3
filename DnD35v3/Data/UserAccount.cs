namespace DnD35v3.Data
{
    public class UserAccount
    {
        public int ID { get; set; }
        public string? User_id { get; set; }
        public string? Email { get; set; }
        public string? Account_name { get; set; }
        public string Picture { get; set; } = string.Empty;
        public DateTime? Created_at { get; set; }
        public DateTime? Last_login { get; set; }
        public DateTime? Account_name_edited { get; set; }
        public bool New_user { get; set; }
        public bool IsDarkMode { get; set; }
    }

    public class UserToken
    {
        public string Token { get; set; }
    }

    public class UserList
    {
        public int ID { get; set; }
        public string? Email { get; set; }
        public string Account_name { get; set; } = string.Empty;
        public string Picture { get; set; } = string.Empty;
        public DateTime? Created_at { get; set; }
        public DateTime? Last_login { get; set; }
    }
}