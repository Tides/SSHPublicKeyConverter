using SSHPublicKeyConverter;
using System.Text;
using System.Xml;

Console.Write("Input SSH Public Key: ");
var key = Console.ReadLine();

if (string.IsNullOrEmpty(key))
{
    Console.WriteLine("Invalid Key");
    return;
}

var split = key.Split();

if (split.Length == 1)
{
    Console.WriteLine("Invalid Key");
    return;
}

var base64Key = Convert.FromBase64String(split[1]);

using var simpleStream = new SimpleStream(base64Key);

var stringLength = simpleStream.ReadInt();

var bytes = new byte[stringLength];
simpleStream.ReadExactly(bytes, 0, stringLength);

var algo = Encoding.UTF8.GetString(bytes);

Console.WriteLine("Algorithm: {0}", algo);

var nextLength = simpleStream.ReadInt();

var exponent = new byte[nextLength];
simpleStream.ReadExactly(exponent, 0, exponent.Length);

nextLength = simpleStream.ReadInt();

var modulus = new byte[nextLength];
simpleStream.ReadExactly(modulus, 0, modulus.Length);

var sb = new StringBuilder();
using (var writer = XmlWriter.Create(sb, new()
{
    Indent = true,
    Async = true,
}))
{
    await writer.WriteStartElementAsync(null, "RSAKeyValue", null);

    await writer.WriteStartElementAsync(null, "Modulus", null);
    await writer.WriteStringAsync(Convert.ToBase64String(modulus));
    await writer.WriteEndElementAsync();

    await writer.WriteStartElementAsync(null, "Exponent", null);
    await writer.WriteStringAsync(Convert.ToBase64String(exponent));
    await writer.WriteEndElementAsync();

    await writer.WriteEndElementAsync();
}

Console.WriteLine(sb.ToString());