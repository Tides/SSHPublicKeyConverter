using System.Buffers.Binary;

namespace SSHPublicKeyConverter;
public partial class SimpleStream
{
    public int ReadInt()
    {
        Span<byte> buffer = stackalloc byte[4];
        this.ReadExactly(buffer);
        return BinaryPrimitives.ReadInt32BigEndian(buffer);
    }
}
