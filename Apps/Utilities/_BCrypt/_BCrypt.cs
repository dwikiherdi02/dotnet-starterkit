namespace Apps.Utilities._BCrypt
{
    // Learn more about How to Secure Passwords with BCrypt.NET at https://code-maze.com/dotnet-secure-passwords-bcrypt/
    public sealed class _BCrypt
    {
        public static string Hash(string inpuyKey)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(inpuyKey, BCrypt.Net.HashType.SHA512);
        }

        public static bool Verify(string text, string hash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(text, hash, BCrypt.Net.HashType.SHA512);
        }
    }
}