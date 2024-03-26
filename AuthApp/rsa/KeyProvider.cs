using System.Security.Cryptography;

namespace AuthApp.rsa
{
    public static class KeyProvider
    {
        public static RSA GetPublicKey()
        {
            var f = File.ReadAllText("rsa/public_key.pem");

            var rsa = RSA.Create();

            rsa.ImportFromPem(f);

            return rsa;
        }

        public static RSA GetPrivateKey()
        {
            var f = File.ReadAllText("rsa/private_key.pem");

            var rsa = RSA.Create();

            rsa.ImportFromPem(f);

            return rsa;
        }
    }
}
