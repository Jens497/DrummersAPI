namespace MusicBackend.Security
{
    public interface IPasswordAuthentication
    {
        string hashingPass(string password, byte[] salt);
        string generateNewPassword(string password);
        bool verifyPassword(string hashedPassword, string passForVerification);
    }
}
