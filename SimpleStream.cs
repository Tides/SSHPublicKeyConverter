namespace SSHPublicKeyConverter;
public sealed partial class SimpleStream(byte[] data) : Stream
{
    private bool disposed;

    public Stream BaseStream { get; set; } = new MemoryStream(data);

    public override bool CanRead => BaseStream.CanRead;

    public override bool CanSeek => BaseStream.CanSeek;

    public override bool CanWrite => BaseStream.CanWrite;

    public override long Length => BaseStream.Length;

    public override long Position
    {
        get => BaseStream.Position;
        set => BaseStream.Position = value;
    }

    public override void Flush() => this.BaseStream.Flush();

    public override int Read(byte[] buffer, int offset, int count) => 
        this.BaseStream.Read(buffer, offset, count);

    public override long Seek(long offset, SeekOrigin origin) => this.BaseStream.Seek(offset, origin);

    public override void SetLength(long value) => this.BaseStream.SetLength(value);

    public override void Write(byte[] buffer, int offset, int count) => this.BaseStream.Write(buffer, offset, count);
    protected override void Dispose(bool disposing)
    {
        if (this.disposed)
            return;

        if (disposing)
        {
            this.BaseStream.Dispose();
        }

        this.disposed = true;
    }
}
