namespace BlazorApp11.Pages
{
    public class SignupModel // ✅ Renamed from Signup to SignupModel
    {
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
