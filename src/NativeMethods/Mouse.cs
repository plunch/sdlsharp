using System;
using System.Runtime.InteropServices;

namespace SDLSharp {
  static unsafe partial class NativeMethods
  {
    [DllImport("SDL2")]
    public static extern int SDL_CaptureMouse(SDL_Bool enabled);

    [DllImport("SDL2")]
    public static extern SDL_CursorPtr SDL_CreateColorCursor(
        SDL_SurfacePtr surface, int hot_x, int hot_y);

    [DllImport("SDL2")]
    public static extern SDL_CursorPtr SDL_CreateCursor(
        /*const*/ byte* data,
        /*const*/ byte* mask,
        int w, int h,
        int hot_x, int hot_y);

    [DllImport("SDL2")]
    public static extern SDL_CursorPtr SDL_CreateSystemCursor(SystemCursor id);

    [DllImport("SDL2")]
    public static extern void SDL_FreeCursor(IntPtr cursor);

    [DllImport("SDL2")]
    public static extern IntPtr SDL_GetCursor();

    [DllImport("SDL2")]
    public static extern IntPtr SDL_GetDefaultCursor();

    [DllImport("SDL2")]
    public static extern uint SDL_GetGlobalMouseState(out int x, out int y);

    [DllImport("SDL2")]
    public static extern IntPtr SDL_GetMouseFocus();

    [DllImport("SDL2")]
    public static extern uint SDL_GetMouseState(out int x, out int y);

    [DllImport("SDL2")]
    public static extern SDL_Bool SDL_GetRelativeMouseMode();

    [DllImport("SDL2")]
    public static extern uint SDL_GetRelativeMouseMode(out int x, out int y);

    [DllImport("SDL2")]
    public static extern void SDL_SetCursor(SDL_CursorPtr cursor);

    [DllImport("SDL2")]
    public static extern void SDL_SetCursor(IntPtr cursor);

    [DllImport("SDL2")]
    public static extern int SDL_SetRelativeMouseMode(SDL_Bool enabled);

    [DllImport("SDL2")]
    public static extern int SDL_ShowCursor(int toggle);

    [DllImport("SDL2")]
    public static extern int SDL_WarpMouseGlobal(int x, int y);

    [DllImport("SDL2")]
    public static extern int SDL_WarpMouseInWindow(SDL_WindowPtr window, int x, int y);
  }

  class SDL_CursorPtr : Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid {
    private SDL_CursorPtr() : base(true) {
    }

    internal SDL_CursorPtr(IntPtr ptr) : base(false) {
      SetHandle(ptr);
    }

    override protected bool ReleaseHandle() {
      NativeMethods.SDL_FreeCursor(this.handle);
      return true;
    }
  }

  public enum SystemCursor {
    Arrow,
    IBeam,
    Wait,
    Crosshair,
    WaitArrow,
    SizeSWSE,
    SizeNESW,
    SizeWE,
    SizeNS,
    SizeAll,
    No,
    Hand,
  }


}
