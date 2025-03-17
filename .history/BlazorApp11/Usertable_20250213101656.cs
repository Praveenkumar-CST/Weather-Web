using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;

namespace BlazorApp11
{
    public class UserTable : BaseModel  // ✅ Inherit from BaseModel
    {
        [PrimaryKey("id", false)]  // ✅ Ensure this annotation exists
        public int Id { get; set; }

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("fullname")]
        public string FullName { get; set; } = string.Empty;

        [Column("password")]
        public string Password { get; set; } = string.Empty;
    }
}
