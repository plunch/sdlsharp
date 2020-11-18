using System;
using System.Text;
using static SDLSharp.NativeMethods;
using System.Runtime.InteropServices;

namespace SDLSharp {
  public class PixelFormat : IDisposable {
    SDL_PixelFormatPtr format;

    public PixelFormat(PixelDataFormat dataFormat) : this((uint)dataFormat) {}
    public PixelFormat(uint dataFormat) {
      this.format = ErrorIfInvalid(SDL_AllocFormat(dataFormat));
    }

    public PixelFormat(PixelFormatMask mask): this(mask.DataFormat) {}

    internal PixelFormat(SDL_PixelFormatPtr ptr) {
      this.format = ptr;
    }

    public unsafe uint DataFormat {
      get {
        var ptr = (SDL_PixelFormat*)format.DangerousGetHandle();
        return ptr->format;
      }
    }

    public unsafe Palette Palette {
      get {
        var ptr = (SDL_PixelFormat*)format.DangerousGetHandle();
        return new Palette(new SDL_PalettePtr((IntPtr)ptr->palette));
      }
      set {
        ErrorIfNegative(SDL_SetPixelFormatPalette(format, value.palette));
      }
    }

    public unsafe byte BitsPerPixel {
      get {
        var ptr = (SDL_PixelFormat*)format.DangerousGetHandle();
        return ptr->BitsPerPixel;
      }
    }

    public unsafe byte BytesPerPixel {
      get {
        var ptr = (SDL_PixelFormat*)format.DangerousGetHandle();
        return ptr->BytesPerPixel;
      }
    }

    public unsafe uint RMask {
      get {
        var ptr = (SDL_PixelFormat*)format.DangerousGetHandle();
        return ptr->Rmask;
      }
    }

    public unsafe uint GMask {
      get {
        var ptr = (SDL_PixelFormat*)format.DangerousGetHandle();
        return ptr->Gmask;
      }
    }

    public unsafe uint BMask {
      get {
        var ptr = (SDL_PixelFormat*)format.DangerousGetHandle();
        return ptr->Bmask;
      }
    }

    public unsafe uint AMask {
      get {
        var ptr = (SDL_PixelFormat*)format.DangerousGetHandle();
        return ptr->Amask;
      }
    }

    public void SetPalette(Palette p) {
      ErrorIfNegative(SDL_SetPixelFormatPalette(format, p.palette));
    }


    public uint Encode(byte r, byte g, byte b) {
      return SDL_MapRGB(format, r, g, b);
    }

    public uint Encode(byte r, byte g, byte b, byte a) {
      return SDL_MapRGBA(format, r, g, b, a);
    }

    public uint Encode(Color clr) {
      return SDL_MapRGBA(format, clr.r, clr.g, clr.b, clr.a);
    }

    public Color Decode(uint pixel) {
      byte r, g, b, a;
      SDL_GetRGBA(pixel, format, out r, out g, out b, out a);
      return new Color(r, g, b, a);
    }

    public void Dispose() {
      format.Dispose();
    }

    public static unsafe string? GetName(uint dataFormat) {
      return UTF8ToString(SDL_GetPixelFormatName(dataFormat));
    }
    public static unsafe string? GetName(PixelDataFormat dataFormat) {
      return GetName((uint)dataFormat);
    }

    public static unsafe void Convert(
      int width, int height,
      uint srcFormat,
      ReadOnlySpan<byte> src,
      int srcPitch,
      uint dstFormat,
      Span<Byte> dst,
      int dstPitch
    ) {
      fixed (byte* srcp = &MemoryMarshal.GetReference(src))
      fixed (byte* dstp = &MemoryMarshal.GetReference(dst))
        ErrorIfNegative(SDL_ConvertPixels(width, height, srcFormat, srcp, srcPitch, dstFormat, dstp, dstPitch));
    }
  }
}
