namespace NeoSmart.ClassLibraries.Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateTokenJwt(string username);
    }
}
