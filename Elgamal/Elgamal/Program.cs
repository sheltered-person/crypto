using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Elgamal
{
    class Program
    {
        #region Key 1
        private static EGSAEncoder.PublicKey key1 = new EGSAEncoder.PublicKey(
            BigInteger.Parse("016dcd6cb81fd719af184b89b80353f7b60258e3852ba4550f4eefda2ea09d5754508964f7fd98ed4bc645855d0c4ca1cf", System.Globalization.NumberStyles.AllowHexSpecifier),
            BigInteger.Parse("0b6e6b65c0feb8cd78c25c4dc01a9fbdb012c71c295d22a87a777ed17504eabaa2844b27bfecc76a5e322c2ae862650e7", System.Globalization.NumberStyles.AllowHexSpecifier),
            BigInteger.Parse("092451fb23631c40e0bbb940a0cdb6473a9868983c186a82b526d45aeaaab02ebe799450f52ff4c6f6e092abf21270ad0", System.Globalization.NumberStyles.AllowHexSpecifier),
            BigInteger.Parse("063df9bb3c7705b68829f7fcf1ab3552099535e218875914b3c499f680dd4348dcad1d1e12f66c45632e328ad535eaa90", System.Globalization.NumberStyles.AllowHexSpecifier));

        private static BigInteger message1 = BigInteger.Parse("0b1db6da293a334c2f0dba5ae5ed587e1dd2159e69cef7b83138f62b58ac2a49e4ab104f4d511d1aa188782a90177cf69", System.Globalization.NumberStyles.AllowHexSpecifier);

        private static EGSAEncoder.Signature signature1 = new EGSAEncoder.Signature(
            BigInteger.Parse("0a7f173233ceb8264da812711604cfd46918bab411fc88df28fc1cf852a9bbeea8767b276adee584d6520b06ac8dc5007", System.Globalization.NumberStyles.AllowHexSpecifier),
            BigInteger.Parse("06cbe6dd9041f927776efa274f5ff067481963bda5ca34b0cf44b58e01d6e6d7f227b9e51876c18eb88c44a3d872c663c", System.Globalization.NumberStyles.AllowHexSpecifier));
        #endregion

        #region Key 2
        private static EGSAEncoder.PublicKey key2 = new EGSAEncoder.PublicKey(
            BigInteger.Parse("03c2950e9775a508124b295585480d7a8a1dacc38496a070debbe980516ab189bd6fa71779acf0e3e27864585a01f95bb89e178aff0ebf391245b85043a7fc34bfd20c28686cb7f4aaa90db71249efc10907b402ec98258f0ccf6bbaa3bbc50afa8d5f0aa5c86e425a3815efe4493aa59299e80ae4fb6fe7d3d26417873171dde0883dbea983015d18a04801272dbabc4ed2b9decc857336c19e0f68212de8474fe525e5d", System.Globalization.NumberStyles.AllowHexSpecifier),
            BigInteger.Parse("0694b5234fbf354d3ffe23794a3eae152c044594875a72ada7", System.Globalization.NumberStyles.AllowHexSpecifier),
            BigInteger.Parse("032881f9d06a05be3af5c830f394dad95cca273da5da1942395e7a4926dd87bd3b6051c8aca511935959ed867caa916172e7588e046c773335ed53166f37c1330e70006433ecfcc7bb08d65992aac8f4406eb716b1d9cfddfcb9ce6b032e9f3d8549b36a41a006dd30d6d828faed07dd0473f4c99bc1becd890a886a328855339ad8baf24d2853d3529c10fae61cc7cd6ceaad64a4f7da9e971da4ad0de707fc02138648a", System.Globalization.NumberStyles.AllowHexSpecifier),
            BigInteger.Parse("02dcfefa22d138d384b509a2db0f159b083937b1addc28ccae505a041fb1582d1839e2cccd6f870c62e40cca2b0724b6485a33d1ac956b08d28118ed04c60a79bcb0486ce77db5e4b9c00525e2e12561a1a144b1cd0b35727ca7e2a47888ec3dea646a3c2332cdc7ea8bef79c998da6fb4a86496172fad801227bded5ff42204c7d6395a94c14da63ab9dc58faea0e827eab4b95d03cc1f1f91f4f5c65e27b8c515882d9", System.Globalization.NumberStyles.AllowHexSpecifier));

        private static BigInteger message2 = BigInteger.Parse("0501611f4841b793219fcba4479e85c90ea21844bfca80d84f", System.Globalization.NumberStyles.AllowHexSpecifier);

        private static EGSAEncoder.Signature signature2 = new EGSAEncoder.Signature(
            BigInteger.Parse("020122d5d9434cf5f6469e86ae17b26aa3ea7c4d0105f508a4ef0e35f21f640e520ba1cd4cc6d3b88543abb7cea48613cabae3694ea63c4277985d9b7f8919907fd526f378dee3282f6e9346ce3b37fe974f5fb3252a94b68aa00b1432ec57917da974022500f1b73eb8f7554b18ddf81e3ca70c72180b51faf49059808d091af01b46c17f27153c17f1461c32f05f6b3df32f780cb1bb5d8faf3e86f2e4fb16dfbdd4877", System.Globalization.NumberStyles.AllowHexSpecifier),
            BigInteger.Parse("050b8d66819962004d746dcd270caf4490f8e0d10606ea8e72", System.Globalization.NumberStyles.AllowHexSpecifier));
        #endregion

        #region Key 3
        private static EGSAEncoder.PublicKey key3 = new EGSAEncoder.PublicKey(
            BigInteger.Parse("030a71e69766c48cf20d7050e1d375e900c89bf9893862fdb385c0db8f403b75e8beb046b9516db686d049d397d71b685985631d2ef6b418e21dfbec34320d487be7240aa865bbcd0f1ad143854daf9214b7f30ca27bc2456e4efb8faa759bd202c9eb336085ace8e873d00b22c6e729f16a16f1c245fc93ba0cf0700e6c0309524eb5647c86b2a7a8619223d10ebb1999bb4a7e226cc847ba3eae492286ab13263af57a3", System.Globalization.NumberStyles.AllowHexSpecifier),
            BigInteger.Parse("04acb6eefcda2d87ee306ee0714f503c0a60d96cd0391a74d7", System.Globalization.NumberStyles.AllowHexSpecifier),
            BigInteger.Parse("01c522f7af809609115bb21fd38c9af658f821bed3e9f6bcd8a40f62358e6b8f88e087dc283e4616caf814741531757a6b78462eb52b3ebeba814c46f6ebf9342014395c880416b8d99ca888c48a9f8d742f77924c259336938568e4aa42e53312175ecd5f0308f24f16bd9d7ecb770443752c4e9b3dc38ca5e61e56e65fd20de215f7ed7003c93275d30de18bcb6dc2b357bbba442b66c015c5755d3ab71fb20f289f85", System.Globalization.NumberStyles.AllowHexSpecifier),
            BigInteger.Parse("0347bdd4da017714446183d21b6820cd91ba65cf48370dad32a0cc22910d511cd9af9559ee481beafed148215d909736feb83cf304cec389fa583932bb95ff3de82d641c8423489621d4d0a04f4541a0d378f3b7f22ede8a3f40157abad3128213fb04793681e319228641b52f1e6a8eafefac10b4c00dfd29f3ec28ceba1501db9f98a9ab523de3c43a436f16c9dcaca2d4dfa90de63d9303d2586a8394d0f432b62432", System.Globalization.NumberStyles.AllowHexSpecifier));

        private static BigInteger message3 = BigInteger.Parse("02eefe2a4137098839be21615cb9af275bbd62a3ae2cd0d38f", System.Globalization.NumberStyles.AllowHexSpecifier);

        private static EGSAEncoder.Signature signature3 = new EGSAEncoder.Signature(
            BigInteger.Parse("01d345c56e51474148a20750c501b03be9200c47aec45541cdb4d8d12db495b85e67d097aa8833740f0333c3a3c759e9b9c38a8293c9850a40aac51cdc4bf97ad94371e2d5cc34899fd5f6fe3c5444ecc6f34dc9940297cade50c60484262cc36bab4c49d78ba6e24dfeb1e4cc254857861972b55997178f4ea678e7fe1bd01435a68442d7980fba8f2412bc78fdd13dd139d33c4da62440472f0f23299a30aed94990c9b", System.Globalization.NumberStyles.AllowHexSpecifier),
            BigInteger.Parse("0486a0d995289cffa7cd75da28256cf7bf00b0a7656dbcb42e", System.Globalization.NumberStyles.AllowHexSpecifier));
        #endregion

        #region Sign Generation
        private static BigInteger p = BigInteger.Parse("03fa07e7275cc401c20a251b0865d5ae95ae736efadca87f48bfa78e7305842b2233c6bf19acab907e5c55314304d58a923b2be2579066dca3209b5a158020b284256489807530bb37c3658b2750b0ba3db6c841470e983030dd5ed3705b2ed2f45dc8fa3f27dfd442c5bd0315caa48637784cfbe8e2540abd6f24c5fc319887d6ec420cba965296ddc3a01586666d3fad1afb7663cd8ba771a7dea3b8727c36322fff221", System.Globalization.NumberStyles.AllowHexSpecifier);
        private static BigInteger q = BigInteger.Parse("07abc6b40ae2b16bf2c05150d3b233df53d9727ac218d03f11", System.Globalization.NumberStyles.AllowHexSpecifier);
        private static BigInteger g = BigInteger.Parse("0212dbadff56d80386cc5a221d03e225284cc11fb0448069c26cdbf49f83b0573c70551b8cb386ff30ccb5f2f7cd1ac32a564fe8c150968949383412369d2eb0c01e0a4703bf9f1d5dcbad160fb32a163e925b708474354afca8b1e87ceaebd8eac50900e37dadb374cbef5fc9c6b8902a2616a8c78c8f189fb5341f23d5bacab58576d72b6ab0c1f7cffd3770565e173e758cb10b35f55bec2723916131fe8b106beeb3c", System.Globalization.NumberStyles.AllowHexSpecifier);
        private static BigInteger m = BigInteger.Parse("06e4c496191653f9b63b54b188639a99d7afb7e43ef97ea333", System.Globalization.NumberStyles.AllowHexSpecifier);
        private static BigInteger x = BigInteger.Parse("03295a0b69bda4a41c385c7b59618a783891f171ae5b365a7a", System.Globalization.NumberStyles.AllowHexSpecifier);
        private static BigInteger k = BigInteger.Parse("022970dc8ce1915ab3dece911e4108ff76b77502ae6ce71856", System.Globalization.NumberStyles.AllowHexSpecifier);
        private static BigInteger s = BigInteger.Parse("0242cb5690e22d87ad296a67e807a6b4f336df1a84786d19d3", System.Globalization.NumberStyles.AllowHexSpecifier);
        #endregion

        static void Main(string[] args)
        {
            EGSAEncoder encoder = new EGSAEncoder(p, q, g, x);
            EGSAEncoder.Signature sign = encoder.SignatureMessage(m, k);

            sign.s = s;

            Console.WriteLine("R:  " + sign.r.ToString("x"));
            Console.WriteLine("S:  " + sign.s.ToString("x"));

            Console.WriteLine(encoder.VerifySignature(m, sign, encoder.Key));

            //EGSAEncoder.PublicKey key = key3;
            //EGSAEncoder.Signature sign = signature3;

            //EGSAEncoder encoder = new EGSAEncoder(key);
            //bool verify = encoder.VerifySignature(message3, sign, key);

            //Console.WriteLine("Result: " + verify);
            Console.ReadKey();
        }
    }
}
