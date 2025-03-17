using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace BlazorApp11.Pages.Signup // Ensure correct namespace
{
    public class UserTable : BaseModel // ✅ Inherit from BaseModel
    {
        [PrimaryKey("id", false)] // ✅ Ensure 'id' is marked as primary key
        public int Id { get; set; }

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("fullname")]
        public string FullName { get; set; } = string.Empty;

        [Column("password")]
        public string Password { get; set; } = string.Empty;
    }
}
