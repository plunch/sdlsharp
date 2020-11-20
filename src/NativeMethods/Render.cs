using System;
using System.Runtime.InteropServices;

namespace SDLSharp {
  static unsafe partial class NativeMethods {
    [DllImport("SDL2")]
    public static extern BlendMode SDL_ComposeCustomBlendMode(
      int srcColorFactor,
      int dstColorFactor,
      int colorOperation,
      int srcAlphaFactor,
      int dstAlphaFactor,
      int alphaOperation
    );

    [DllImport("SDL2")]
    public static extern SDL_RendererPtr SDL_CreateRenderer(
      SDL_WindowPtr window,
      int index,
      RendererFlags flags
    );

    [DllImport("SDL2")]
    public static extern SDL_RendererPtr SDL_CreateSoftwareRenderer(
      Surface surface
    );

    [DllImport("SDL2")]
    public static extern SDL_TexturePtr SDL_CreateTexture(
      SDL_RendererPtr renderer,
      UInt32 format,
      TextureAccess access,
      int w,
      int h
    );

    [DllImport("SDL2")]
    public static extern SDL_TexturePtr SDL_CreateTextureFromSurface(
      SDL_RendererPtr renderer,
      Surface surface
    );

    [DllImport("SDL2")]
    public static extern void SDL_DestroyRenderer(
      IntPtr renderer
    );

    [DllImport("SDL2")]
    public static extern void SDL_DestroyTexture(
      IntPtr texture
    );

    [DllImport("SDL2")]
    public static extern int SDL_GetNumRenderDrivers();

    [DllImport("SDL2")]
    public static extern int SDL_GetRenderDrawBlendMode(
      SDL_RendererPtr renderer,
      out BlendMode blendMode
    );

    [DllImport("SDL2")]
    public static extern int SDL_GetRenderDrawColor(
      SDL_RendererPtr renderer,
      out byte r,
      out byte g,
      out byte b,
      out byte a
    );

    [DllImport("SDL2")]
    public static extern int SDL_GetRenderDriverInfo(
      int index,
      out SDL_RendererInfo info
    );

    [DllImport("SDL2")]
    public static extern IntPtr SDL_GetRenderTarget(SDL_RendererPtr renderer);

    [DllImport("SDL2")]
    public static extern int SDL_GetRendererInfo(
      SDL_RendererPtr renderer,
    out SDL_RendererInfo info
  );

  [DllImport("SDL2")]
  public static extern int SDL_GetRendererOutputSize(
    SDL_RendererPtr renderer,
    out int w,
    out int h
  );

  [DllImport("SDL2")]
  public static extern int SDL_GetTextureAlphaMod(
    SDL_TexturePtr texture,
    out byte alpha
  );

  [DllImport("SDL2")]
  public static extern int SDL_GetTextureBlendMode(
    SDL_TexturePtr texture,
    out BlendMode blendMode
  );

  [DllImport("SDL2")]
  public static extern int SDL_GetTextureColorMod(
    SDL_TexturePtr texture,
    out byte r,
    out byte g,
    out byte b
  );

  [DllImport("SDL2")]
  public static extern int SDL_LockTexture(
    SDL_TexturePtr texture,
    /*const*/ Rect* rect,
    void** pixels,
    out int pitch
  );

  [DllImport("SDL2")]
  public static extern int SDL_QueryTexture(
    SDL_TexturePtr texture,
    UInt32* format,
    TextureAccess* access,
    int* w,
    int* h
  );

  [DllImport("SDL2")]
  public static extern int SDL_RenderClear(
    SDL_RendererPtr renderer
  );

  [DllImport("SDL2")]
  public static extern int SDL_RenderCopy(
    SDL_RendererPtr renderer,
    SDL_TexturePtr texture,
    /*const*/ Rect* srcrect,
    /*const*/ Rect* dstrect
  );

  [DllImport("SDL2")]
  public static extern int SDL_RenderCopyEx(
    SDL_RendererPtr renderer,
    SDL_TexturePtr texture,
    /*const*/ Rect* srcrect,
    /*const*/ Rect* dstrect,
    double angle,
    /*const*/ Point* center,
    RendererFlip flip
  );

  [DllImport("SDL2")]
  public static extern int SDL_RenderDrawLine(
    SDL_RendererPtr renderer,
    int x1,
    int y1,
    int x2,
    int y2
  );

  [DllImport("SDL2")]
  public static extern int SDL_RenderDrawLines(
    SDL_RendererPtr renderer,
    /*const*/ Point* points,
    int count
  );

  [DllImport("SDL2")]
  public static extern int SDL_RenderDrawPoint(
    SDL_RendererPtr renderer,
    int x,
    int y
  );

  [DllImport("SDL2")]
  public static extern int SDL_RenderDrawPoints(
    SDL_RendererPtr renderer,
    /*const*/ Point* points,
    int count
  );

  [DllImport("SDL2")]
  public static extern int SDL_RenderDrawRect(
    SDL_RendererPtr renderer,
    /*const*/ Rect* rect
  );

  [DllImport("SDL2")]
  public static extern int SDL_RenderDrawRects(
    SDL_RendererPtr renderer,
    /*const*/ Rect* rects,
    int count
  );

  [DllImport("SDL2")]
  public static extern int SDL_RenderFillRect(
    SDL_RendererPtr renderer,
    /*const*/ Rect* rect
  );

  [DllImport("SDL2")]
  public static extern int SDL_RenderFillRects(
    SDL_RendererPtr renderer,
    /*const*/ Rect* rects,
    int count
  );

  [DllImport("SDL2")]
  public static extern void SDL_RenderGetClipRect(
    SDL_RendererPtr renderer,
    out Rect rect
  );

  [DllImport("SDL2")]
  public static extern SDL_Bool SDL_RenderGetIntegerScale(
    SDL_RendererPtr renderer
  );

  [DllImport("SDL2")]
  public static extern void SDL_RenderGetLogicalSize(
    SDL_RendererPtr renderer,
    out int w,
    out int h
  );

  [DllImport("SDL2")]
  public static extern void SDL_RenderGetScale(
    SDL_RendererPtr renderer,
    out float scaleX,
    out float scaleY
  );

  [DllImport("SDL2")]
  public static extern void SDL_RenderGetViewport(
    SDL_RendererPtr renderer,
    out Rect rect
  );

  [DllImport("SDL2")]
  public static extern SDL_Bool SDL_RenderIsClipEnabled(
    SDL_RendererPtr renderer
  );

  [DllImport("SDL2")]
  public static extern void SDL_RenderPresent(
    SDL_RendererPtr renderer
  );

  [DllImport("SDL2")]
  public static extern int SDL_RenderReadPixels(
    SDL_RendererPtr renderer,
    /*const*/ Rect* rect,
    UInt32 format,
    void* pixels,
    int pitch
  );
  
  [DllImport("SDL2")]
  public static extern int SDL_RenderSetClipRect(
    SDL_RendererPtr renderer,
    /*const*/ Rect* rect
  );

  [DllImport("SDL2")]
  public static extern int SDL_RenderSetIntegerScale(
    SDL_RendererPtr renderer,
    SDL_Bool enable
  );

  [DllImport("SDL2")]
  public static extern int SDL_RenderSetLogicalSize(
    SDL_RendererPtr renderer,
    int w,
    int h
  );

  [DllImport("SDL2")]
  public static extern int SDL_RenderSetScale(
    SDL_RendererPtr renderer,
    float scaleX,
    float scaleY
  );

  [DllImport("SDL2")]
  public static extern int SDL_RenderSetViewport(
    SDL_RendererPtr renderer,
    /*const*/ Rect* rect
  );

  [DllImport("SDL2")]
  public static extern SDL_Bool SDL_RenderTargetSupported(
    SDL_RendererPtr renderer
  );

  [DllImport("SDL2")]
  public static extern int SDL_SetRenderDrawBlendMode(
    SDL_RendererPtr renderer,
    BlendMode blendMode
  );

  [DllImport("SDL2")]
  public static extern int SDL_SetRenderDrawColor(
    SDL_RendererPtr renderer,
    byte r,
    byte g,
    byte b,
    byte a
  );

  [DllImport("SDL2")]
  public static extern int SDL_SetRenderTarget(
    SDL_RendererPtr renderer,
    SDL_TexturePtr texture
  );

  [DllImport("SDL2")]
  public static extern int SDL_SetRenderTarget(
    SDL_RendererPtr renderer,
    IntPtr texture
  );

  [DllImport("SDL2")]
  public static extern int SDL_SetTextureAlphaMod(
    SDL_TexturePtr texture,
    byte alpha
  );

  [DllImport("SDL2")]
  public static extern int SDL_SetTextureBlendMode(
    SDL_TexturePtr texture,
    BlendMode blendMode
  );

  [DllImport("SDL2")]
  public static extern int SDL_SetTextureColorMod(
    SDL_TexturePtr texture,
    byte r,
    byte g,
    byte b
  );

  [DllImport("SDL2")]
  public static extern void SDL_UnlockTexture(
    SDL_TexturePtr texture
  );

  [DllImport("SDL2")]
  public static extern void SDL_UpdateTexture(
    SDL_TexturePtr texture,
    /*const*/ Rect* rect,
    /*const*/ void* pixels,
    int pitch
  );

  [DllImport("SDL2")]
  public static extern void SDL_UpdateYUVTexture(
    SDL_TexturePtr texture,
    /*const*/ Rect* rect,
    /*const*/ byte* Yplane,
    int Ypitch,
    /*const*/ byte* Uplane,
    int Upitch,
    /*const*/ byte* Vplane,
    int Vpitch
  );

  [StructLayout(LayoutKind.Sequential)]
  public struct SDL_RendererInfo {
      public /*const char*/ byte* name;
      public RendererFlags flags;
      public UInt32 num_texture_formats;
      public fixed uint texture_formats[16];
      public int max_texture_width, max_texture_height;
    }
  }

  class SDL_RendererPtr : Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid {
    private SDL_RendererPtr() : base(true) {
    }

    internal SDL_RendererPtr(IntPtr ptr) : base(false) {
      SetHandle(ptr);
    }

    override protected bool ReleaseHandle() {
      NativeMethods.SDL_DestroyRenderer(this.handle);
      return true;
    }
  }

  class SDL_TexturePtr : Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid {
    private SDL_TexturePtr() : base(true) {
    }

    internal SDL_TexturePtr(IntPtr ptr) : base(false) {
      SetHandle(ptr);
    }

    override protected bool ReleaseHandle() {
      NativeMethods.SDL_DestroyTexture(this.handle);
      return true;
    }
  }

  [Flags]
  public enum RendererFlags : uint {
    Software = 1,
    Accelerated = 2,
    PresentVSync = 4,
    TargetTexture = 8,
  }

  public enum RendererFlip {
    None = 0,
    Horizontal = 1,
    Vertical = 2,
  }

  public enum TextureAccess {
    Static,
    Streaming,
    Target,
  }

  public enum ScaleMode {
    Nearest,
    Linear,
    Best,
  }

  public enum BlendMode {
    None = 0,
    Blend = 1,
    Add = 2,
    Mod = 4,
  }

}
