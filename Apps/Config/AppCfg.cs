namespace Apps.Config
{
    public class AppCfg
    {
        public DatabaseCfg Database { get; set; } = null!;

        public MailerCfg Mailer { get; set; } = null!;

        public JwtCfg Jwt { get; set; } = null!;
    }
}