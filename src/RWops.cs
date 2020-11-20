using System;
using System.Runtime.InteropServices;
using static SDLSharp.NativeMethods;

namespace SDLSharp {
  public unsafe class RWOps : System.Runtime.InteropServices.SafeHandle {
    protected RWOps() : base(IntPtr.Zero, true) {
    }

    public RWOps(IntPtr rw, bool owned) : base(IntPtr.Zero, owned) {
      SetHandle(rw);
    }

    public RWOpsType Type => ptr->type;

    public long Size() => size(ptr);

    public long Seek(long offset, int whence)
      => seek(ptr, offset, whence);

    public long Tell()
      => seek(ptr, 0, 1);


    public int Read<T>(Span<T> dest) where T : struct {
      var buf = MemoryMarshal.AsBytes(dest);
      fixed (byte* b = &MemoryMarshal.GetReference(buf)) {
        var size = buf.Length / dest.Length;
        var ret = (int)read(ptr, b, (UIntPtr)size, (UIntPtr)dest.Length);
        return ret * size;
      }
    }

    public int Write<T>(ReadOnlySpan<T> src) where T : struct {
      var buf = MemoryMarshal.AsBytes(src);
      fixed (byte* b = &MemoryMarshal.GetReference(buf)) {
        var size = buf.Length / src.Length;
        var ret = (int)write(ptr, b, (UIntPtr)size, (UIntPtr)src.Length);
        return ret * size;
      }
    }

    internal SDL_RWops* ptr {
      get {
        if (IsInvalid)
          throw new ObjectDisposedException($"{nameof(RWOps)}<{handle}>");
        return (SDL_RWops*)handle;
      }
    }
    internal SDL_RWopsSize size => Marshal.GetDelegateForFunctionPointer<SDL_RWopsSize>(ptr->size);
    internal SDL_RWopsSeek seek => Marshal.GetDelegateForFunctionPointer<SDL_RWopsSeek>(ptr->seek);
    internal SDL_RWopsRead read => Marshal.GetDelegateForFunctionPointer<SDL_RWopsRead>(ptr->read);
    internal SDL_RWopsWrite write => Marshal.GetDelegateForFunctionPointer<SDL_RWopsWrite>(ptr->write);
    internal SDL_RWopsClose close => Marshal.GetDelegateForFunctionPointer<SDL_RWopsClose>(ptr->close);

    public override bool IsInvalid => handle == IntPtr.Zero;

    override protected bool ReleaseHandle() {
      close(ptr);
      return true;
    }

    public System.IO.Stream AsStream(bool keepOpen = false) {
      return new RWOpsStream(this, !keepOpen);
    }

    public static RWOps FromFile(string file, string mode) {
      var filenameLen = SL(file);
      Span<byte> buf = stackalloc byte[filenameLen + SL(mode)];
      StringToUTF8(file, buf);
      StringToUTF8(mode, buf.Slice(filenameLen));
      
      fixed(byte* p = &MemoryMarshal.GetReference(buf)) {
        return ErrorIfInvalid(SDL_RWFromFile(p, p + filenameLen));
      }
    }

    public static RWOps FromHandle(IntPtr handle, bool autoclose) {
      return ErrorIfInvalid(SDL_RWFromFP(handle, autoclose ? SDL_Bool.True : SDL_Bool.False));
    }

    public static RWOps FromMemory(Memory<byte> buffer) {
      var handle = buffer.Pin();
      var ret = ErrorIfInvalid(SDL_RWFromConstMem((IntPtr)handle.Pointer, buffer.Length));
      ret.memoryHandle = handle;
      return ret;
    }

    public static RWOps FromMemory(ReadOnlyMemory<byte> buffer) {
      var handle = buffer.Pin();
      var ret = ErrorIfInvalid(SDL_RWFromConstMem((IntPtr)handle.Pointer, buffer.Length));
      ret.memoryHandle = handle;
      return ret;
    }
  }

  public class RWOpsFromMemory : RWOps {
    internal System.Buffers.MemoryHandle memoryHandle;

    protected RWOpsFromMemory() : base() {}
    public RWOpsFromMemory(IntPtr rw, bool owned) : base(rw, owned) {}
  }
}
