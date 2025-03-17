public class UserTable : Supabase.Postgrest.Models.BaseModel
{
    [PrimaryKey("id", false)] // Add this if your table has an 'id' primary key
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}
