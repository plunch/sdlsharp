using System;
using static SDLSharp.NativeMethods;
using System.Runtime.InteropServices;

namespace SDLSharp
{
  public class Surface : IDisposable {
    internal readonly SDL_SurfacePtr surface;

    internal Surface(SDL_SurfacePtr handle) {
      this.surface = handle;
    }

    public unsafe int Width {
      get {
        var s = (SDL_Surface*)surface.DangerousGetHandle();
        return s->w;
      }
    }

    public unsafe int Height {
      get {
        var s = (SDL_Surface*)surface.DangerousGetHandle();
        return s->h;
      }
    }

    public unsafe PixelFormat Format {
      get {
        var s = (SDL_Surface*)surface.DangerousGetHandle();
        return new PixelFormat(new SDL_PixelFormatPtr((IntPtr)s->format));
      }
    }

    public unsafe int Pitch {
      get {
        var s = (SDL_Surface*)surface.DangerousGetHandle();
        return s->pitch;
      }
    }

    public unsafe Rect Clip {
      get {
        Rect result;
        SDL_GetClipRect(surface, out result);
        return result;
      }
      set {
        SDL_SetClipRect(surface, &value);
      }
    }

    public BlendMode BlendMode {
      get {
        BlendMode mode;
        ErrorIfNegative(SDL_GetSurfaceBlendMode(surface, out mode));
        return mode;
      }
      set {
        ErrorIfNegative(SDL_SetSurfaceBlendMode(surface, value));
      }
    }

    public unsafe uint? ColorKey {
      get {
        uint key;
        var res = SDL_GetColorKey(surface, out key);
        if (res == -1)
          return null;
        ErrorIfNegative(res);
        return key;
      }
      set {
        if (value == null)
          SDL_SetColorKey(surface, 0, 0);
        else
          SDL_SetColorKey(surface, 1, value.Value);
      }
    }

    public byte AlphaMod {
      get {
        byte alpha;
        ErrorIfNegative(SDL_GetSurfaceAlphaMod(surface, out alpha));
        return alpha;
      }
      set {
        ErrorIfNegative(SDL_SetSurfaceAlphaMod(surface, value));
      }
    }

    public Color ColorMod {
      get {
        byte r, g, b;
        ErrorIfNegative(SDL_GetSurfaceColorMod(surface, out r, out g, out b));
        return new Color(r, g, b);
      }
      set {
        ErrorIfNegative(SDL_SetSurfaceColorMod(surface, value.r, value.g, value.b));
      }
    }

    public static unsafe void Blit(
      Surface src,
      Surface dst
    ) {
      ErrorIfNegative(SDL_BlitSurface(src.surface, null, dst.surface, null));
    }

    public static unsafe void Blit(
      Surface src,
      in Rect srcRect, 
      Surface dst
    ) {
      fixed(Rect* srcrect = &srcRect)
        ErrorIfNegative(SDL_BlitSurface(src.surface, srcrect, dst.surface, null));
    }

    public static unsafe void Blit(
      Surface src,
      Surface dst,
      in Rect dstRect
    ) {
      fixed(Rect* dstrect = &dstRect)
        ErrorIfNegative(SDL_BlitSurface(src.surface, null, dst.surface, dstrect));
    }

    public static unsafe void Blit(
      Surface src,
      in Rect srcRect, 
      Surface dst,
      in Rect dstRect
    ) {
      fixed(Rect* srcrect = &srcRect)
      fixed(Rect* dstrect = &dstRect)
        ErrorIfNegative(SDL_BlitSurface(src.surface, srcrect, dst.surface, dstrect));
    }

    public static unsafe void BlitScaled(
      Surface src,
      Surface dst
    ) {
      ErrorIfNegative(SDL_BlitScaled(src.surface, null, dst.surface, null));
    }

    public static unsafe void BlitScaled(
      Surface src,
      in Rect srcRect, 
      Surface dst
    ) {
      fixed(Rect* srcrect = &srcRect)
        ErrorIfNegative(SDL_BlitScaled(src.surface, srcrect, dst.surface, null));
    }

    public static unsafe void BlitScaled(
      Surface src,
      Surface dst,
      in Rect dstRect
    ) {
      fixed(Rect* dstrect = &dstRect)
        ErrorIfNegative(SDL_BlitScaled(src.surface, null, dst.surface, dstrect));
    }

    public static unsafe void BlitScaled(
      Surface src,
      in Rect srcRect, 
      Surface dst,
      in Rect dstRect
    ) {
      fixed(Rect* srcrect = &srcRect)
      fixed(Rect* dstrect = &dstRect)
        ErrorIfNegative(SDL_BlitScaled(src.surface, srcrect, dst.surface, dstrect));
    }

    public static unsafe void LowerBlit(
      Surface src,
      Surface dst
    ) {
      ErrorIfNegative(SDL_LowerBlit(src.surface, null, dst.surface, null));
    }

    public static unsafe void LowerBlit(
      Surface src,
      in Rect srcRect, 
      Surface dst
    ) {
      fixed(Rect* srcrect = &srcRect)
        ErrorIfNegative(SDL_LowerBlit(src.surface, srcrect, dst.surface, null));
    }

    public static unsafe void LowerBlit(
      Surface src,
      Surface dst,
      in Rect dstRect
    ) {
      fixed(Rect* dstrect = &dstRect)
        ErrorIfNegative(SDL_LowerBlit(src.surface, null, dst.surface, dstrect));
    }

    public static unsafe void LowerBlit(
      Surface src,
      in Rect srcRect, 
      Surface dst,
      in Rect dstRect
    ) {
      fixed(Rect* srcrect = &srcRect)
      fixed(Rect* dstrect = &dstRect)
        ErrorIfNegative(SDL_LowerBlit(src.surface, srcrect, dst.surface, dstrect));
    }

    public static unsafe void LowerBlitScaled(
      Surface src,
      Surface dst
    ) {
      ErrorIfNegative(SDL_LowerBlitScaled(src.surface, null, dst.surface, null));
    }

    public static unsafe void LowerBlitScaled(
      Surface src,
      in Rect srcRect, 
      Surface dst
    ) {
      fixed(Rect* srcrect = &srcRect)
        ErrorIfNegative(SDL_LowerBlitScaled(src.surface, srcrect, dst.surface, null));
    }

    public static unsafe void LowerBlitScaled(
      Surface src,
      Surface dst,
      in Rect dstRect
    ) {
      fixed(Rect* dstrect = &dstRect)
        ErrorIfNegative(SDL_LowerBlitScaled(src.surface, null, dst.surface, dstrect));
    }

    public static unsafe void LowerBlitScaled(
      Surface src,
      in Rect srcRect, 
      Surface dst,
      in Rect dstRect
    ) {
      fixed(Rect* srcrect = &srcRect)
      fixed(Rect* dstrect = &dstRect)
        ErrorIfNegative(SDL_LowerBlitScaled(src.surface, srcrect, dst.surface, dstrect));
    }

    public unsafe void Fill(uint color) {
      ErrorIfNegative(SDL_FillRect(surface, null, color));
    }

    public unsafe void Fill(in Rect rect, uint color) {
      fixed(Rect* rp = &rect)
        ErrorIfNegative(SDL_FillRect(surface, rp, color));
    }

    public unsafe void Fill(ReadOnlySpan<Rect> rects, uint color) {
      fixed(Rect* rp = &MemoryMarshal.GetReference(rects))
        ErrorIfNegative(SDL_FillRects(surface, rp, rects.Length, color));
    }

    public Surface Convert(uint pixelFormat) {
      return new Surface(ErrorIfInvalid(SDL_ConvertSurfaceFormat(surface, pixelFormat, 0)));
    }

    public bool MustLock() {
      return SDL_MUSTLOCK(surface) == SDL_Bool.True;
    }

    public void SetRLE(bool enabled) {
      ErrorIfNegative(SDL_SetSurfaceRLE(surface, enabled ? 1 : 0));
    }

    public void SetPalette(Palette p) {
      ErrorIfNegative(SDL_SetSurfacePalette(surface, p.palette));
    }

    /*
    public unsafe void Save(string file) {
      Span<byte> utf8 = stackalloc byte[SL(file)];
      StringToUTF8(file, utf8);
      fixed (byte* ptr = &MemoryMarshal.GetReference(utf8))
        ErrorIfNegative(SDL_SaveBMP(surface, ptr));
    }*/

    public static Surface Create(
        int width,
        int height,
        int depth,
        uint rmask,
        uint gmask,
        uint bmask,
        uint amask) {
      return new Surface(ErrorIfInvalid(SDL_CreateRGBSurface(0, width, height, depth, rmask, gmask, bmask, amask)));
    }

    public static Surface Create(
        int width,
        int height,
        int depth,
        uint format) {
      return new Surface(ErrorIfInvalid(SDL_CreateRGBSurfaceWithFormat(0, width, height, depth, format)));
    }

    /*
    public static unsafe Surface From(string file) {
      Span<byte> utf8 = stackalloc byte[SL(file)];
      StringToUTF8(file, utf8);
      fixed (byte* ptr = &MemoryMarshal.GetReference(utf8))
        return new Surface(ErrorIfInvalid(SDL_LoadBMP(ptr)));
    }*/

    public static unsafe Surface From(
        ReadOnlySpan<byte> data,
        int width,
        int height,
        in PixelFormatMask mask,
        int pitch){
      fixed (byte* ptr = &MemoryMarshal.GetReference(data))
        return new Surface(ErrorIfInvalid(SDL_CreateRGBSurfaceFrom(ptr, width, height, mask.BitsPerPixel, pitch, mask.R, mask.G, mask.B, mask.A)));
    }

    public static unsafe Surface From(
        ReadOnlySpan<byte> data,
        int width,
        int height,
        int depth,
        int pitch,
        uint format) {
      fixed (byte* ptr = &MemoryMarshal.GetReference(data))
        return new Surface(ErrorIfInvalid(SDL_CreateRGBSurfaceWithFormatFrom(ptr, width, height, depth, pitch, format)));
    }

    public void Dispose() {
      surface.Dispose();
    }

    public SurfacePixels Pixels => new SurfacePixels(this);

    public class SurfacePixels : IDisposable {
      readonly Surface surface;
      readonly bool wasLocked;
      bool disposed;

      internal SurfacePixels(Surface surface) {
        this.wasLocked = SDL_MUSTLOCK(surface.surface) ==  SDL_Bool.True;
        this.surface = surface;
        if (wasLocked)
          ErrorIfNegative(SDL_LockSurface(surface.surface));
      }

      public unsafe Span<byte> Get() {
        var len = surface.Format.BytesPerPixel * surface.Width * surface.Height; 
        var s = (SDL_Surface*)surface.surface.DangerousGetHandle();
        return new Span<byte>(s->pixels, len);
      }

      public void Dispose() {
        if (wasLocked && !disposed)
          SDL_UnlockSurface(surface.surface);
        disposed = true;
      }
    }
  }
}
