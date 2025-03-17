using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace BlazorApp11 // Make sure this matches your project namespace
{
    public class UserTable : BaseModel // Ensure it inherits from BaseModel
    {
        [Column("id")] // Ensure the column names match your Supabase table
        public int Id { get; set; }

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("fullname")]
        public string FullName { get; set; } = string.Empty;

        [Column("password")]
        public string Password { get; set; } = string.Empty;
    }
}
