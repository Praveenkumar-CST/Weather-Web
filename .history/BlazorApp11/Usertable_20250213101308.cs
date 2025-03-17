using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

public class UserTable : BaseModel
{
    [Column("id", IsPrimaryKey = true)] // If your table has an 'id' column as the primary key
    public int Id { get; set; }

    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Column("fullname")]
    public string FullName { get; set; } = string.Empty;
}
