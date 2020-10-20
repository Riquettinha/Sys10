namespace Sys10.Services.Services
{
    public interface ICryptService
    {
        string Crypt(string password);
        bool IsValid(string typedPassword, string correctPassword);
    }
}
