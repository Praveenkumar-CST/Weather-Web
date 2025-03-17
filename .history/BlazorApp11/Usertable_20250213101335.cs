using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

public class UserTable : BaseModel
{
    [Column("id")] // Ensure this column exists in Supabase
    public int Id { get; set; }

    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Column("fullname")]
    public string FullName { get; set; } = string.Empty;
}
