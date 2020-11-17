using System;
using System.Runtime.InteropServices;

namespace SDLSharp {
  static unsafe partial class NativeMethods {
    [DllImport("SDL2")]
    public static extern int SDL_BlitScaled(
      SDL_SurfacePtr src,
      /* const*/ Rect* srcrect,
      SDL_SurfacePtr dst,
      Rect* dstrect
    );

    [DllImport("SDL2")]
    public static extern int SDL_BlitSurface(
      SDL_SurfacePtr src,
      /* const*/ Rect* srcrect,
      SDL_SurfacePtr dst,
      Rect* dstrect
    );

    [DllImport("SDL2")]
    public static extern int SDL_ConvertPixels(
      int width,
      int height,
      UInt32 src_format,
      /*const*/ void* src,
      int src_pitch,
      UInt32 dst_format,
      void* dst,
      int dst_pitch
    );

    [DllImport("SDL2")]
    public static extern SDL_SurfacePtr SDL_ConvertSurface(
      SDL_SurfacePtr src,
      /*const*/ SDL_PixelFormat* fmt,
      UInt32 flags
    );

    [DllImport("SDL2")]
    public static extern SDL_SurfacePtr SDL_ConvertSurfaceFormat(
      SDL_SurfacePtr src,
      UInt32 pixel_format,
      UInt32 flags
    );

    [DllImport("SDL2")]
    public static extern SDL_SurfacePtr SDL_CreateRGBSurface(
      UInt32 flags,
      int width,
      int height,
      int depth,
      UInt32 Rmask,
      UInt32 Gmask,
      UInt32 Bmask,
      UInt32 Amask
    );

    [DllImport("SDL2")]
    public static extern SDL_SurfacePtr SDL_CreateRGBSurfaceFrom(
      void* pixels,
      int width,
      int height,
      int depth,
      int pitch,
      UInt32 Rmask,
      UInt32 Gmask,
      UInt32 Bmask,
      UInt32 Amask
    );

    [DllImport("SDL2")]
    public static extern SDL_SurfacePtr SDL_CreateRGBSurfaceWithFormat(
      UInt32 flags,
      int width,
      int height,
      int depth,
      UInt32 format
    );

    [DllImport("SDL2")]
    public static extern SDL_SurfacePtr SDL_CreateRGBSurfaceWithFormatFrom(
      void* pixels,
      int width,
      int height,
      int depth,
      int pitch,
      UInt32 format
    );

    [DllImport("SDL2")]
    public static extern int SDL_FillRect(
      SDL_SurfacePtr dst,
      /*const*/ Rect* rect,
      UInt32 color
    );

    [DllImport("SDL2")]
    public static extern int SDL_FillRects(
      SDL_SurfacePtr dst,
      /*const*/ Rect* rects,
      int count,
      UInt32 color
    );

    [DllImport("SDL2")]
    public static extern void SDL_FreeSurface(IntPtr surface);

    [DllImport("SDL2")]
    public static extern void SDL_GetClipRect(
      SDL_SurfacePtr surface,
      out Rect rect
    );

    [DllImport("SDL2")]
    public static extern int SDL_GetColorKey(
      SDL_SurfacePtr surface,
      out UInt32 key
    );

    [DllImport("SDL2")]
    public static extern int SDL_GetSurfaceAlphaMod(
      SDL_SurfacePtr surface,
      out byte alpha
    );

    [DllImport("SDL2")]
    public static extern int SDL_GetSurfaceBlendMode(
      SDL_SurfacePtr surface,
      out BlendMode blendMode
    );

    [DllImport("SDL2")]
    public static extern int SDL_GetSurfaceColorMod(
      SDL_SurfacePtr surface,
      out byte r,
      out byte g,
      out byte b
    );

    /*
    [DllImport("SDL2")]
    public static extern SDL_SurfacePtr SDL_LoadBMP_RW(
      SDL_RWops* src,
      int freesrc
    );*/

    [DllImport("SDL2")]
    public static extern int SDL_LockSurface(SDL_SurfacePtr surface);

    [DllImport("SDL2")]
    public static extern int SDL_LowerBlit(
      SDL_SurfacePtr surface,
      Rect* srcrect,
      SDL_SurfacePtr dst,
      Rect* dstrect
    );

    [DllImport("SDL2")]
    public static extern int SDL_LowerBlitScaled(
      SDL_SurfacePtr surface,
      Rect* srcrect,
      SDL_SurfacePtr dst,
      Rect* dstrect
    );

    [DllImport("SDL2")]
    public static extern SDL_Bool SDL_MUSTLOCK(
      SDL_SurfacePtr surface
    );

    /*
    [DllImport("SDL2")]
    public static extern int SDL_SaveBMP_RW(
      SDL_SurfacePtr surface,
      SDL_RWops* dst,
      int freedst
    );*/

    [DllImport("SDL2")]
    public static extern SDL_Bool SDL_SetClipRect(
      SDL_SurfacePtr surface,
      /*const*/ Rect* rect
    );

    [DllImport("SDL2")]
    public static extern int SDL_SetColorKey(
      SDL_SurfacePtr surface,
      int flag,
      UInt32 key
    );

    [DllImport("SDL2")]
    public static extern int SDL_SetSurfaceAlphaMod(
      SDL_SurfacePtr surface,
      byte alpha
    );

    [DllImport("SDL2")]
    public static extern int SDL_SetSurfaceBlendMode(
      SDL_SurfacePtr surface,
      BlendMode blendMode
    );

    [DllImport("SDL2")]
    public static extern int SDL_SetSurfaceColorMod(
      SDL_SurfacePtr surface,
      byte r,
      byte g,
      byte b
    );

    [DllImport("SDL2")]
    public static extern int SDL_SetSurfacePalette(
      SDL_SurfacePtr surface,
      SDL_PalettePtr palette
    );

    [DllImport("SDL2")]
    public static extern int SDL_SetSurfaceRLE(
      SDL_SurfacePtr surface,
      int flag
    );

    [DllImport("SDL2")]
    public static extern void SDL_UnlockSurface(SDL_SurfacePtr surface);

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_Surface {
      public SDL_PixelFormat* format;
      public int w, h;
      public int pitch;
      public void* pixels;
      public void* userdata;
      private int locked;
      private void* lock_data;
      public Rect clip_rect;
      private void* SDL_BlitMap;
      public int refcount;
    }
  }

  class SDL_SurfacePtr : Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid {
    private SDL_SurfacePtr() : base(true) {
    }

    override protected bool ReleaseHandle() {
      NativeMethods.SDL_FreeSurface(this.handle);
      return true;
    }
  }
}
