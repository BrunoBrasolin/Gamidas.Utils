namespace Gamidas.Utils.Encryption;

public interface IEncryption
{
    string EncryptData(string value);

    bool CompareEncryptedData(string encryptedData, string value);

}
