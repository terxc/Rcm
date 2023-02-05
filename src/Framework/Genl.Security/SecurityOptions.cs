namespace Genl.Security;

public class SecurityOptions
{
    public EncryptionOptions Encryption { get; set; } = new();

    public class EncryptionOptions
    {
        public string? Key { get; set; }
    }
}