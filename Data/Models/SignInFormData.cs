namespace Business.Dtos;

public class SignInFormData
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsPersistent { get; set; }
}
