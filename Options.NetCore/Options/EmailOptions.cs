namespace Options.NetCore.Options
{
    public class EmailOptions
    {
        public const string Email = "Email";
        public const string AdminEmail = "AdminEmail";

        public string Subject { get; set; }

        public string Recepient { get; set; }
    }
}
