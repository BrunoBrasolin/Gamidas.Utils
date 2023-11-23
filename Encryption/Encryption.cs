using System.Text;

namespace Gamidas.Utils.Encryption;

public class Encryption : IEncryption
{
    public string EncryptData(string value)
    {
        byte[] bytes = Encoding.ASCII.GetBytes(value);

        string converted = Convert.ToBase64String(bytes);

        return converted;
    }
    public bool CompareEncryptedData(string encryptedData, string value)
    {
        string encryptedValue = EncryptData(value);

        return encryptedValue == encryptedData;
    }
}
