using System;
using System.Runtime.InteropServices;

namespace SDLSharp {
  static unsafe partial class NativeMethods {
    [DllImport("SDL2")]
    public static extern /*const*/byte* SDL_GetError();

    [DllImport("SDL2")]
    public static extern int SDL_SetError(/*const*/ char* fmt, __arglist);

    [DllImport("SDL2")]
    public static extern void SDL_ClearError();

    public static Exception GetError() {
      var err = SDL_GetError();
      if (err == null || err[0] == 0)
        return null;
      return new Exception(UTF8ToString(err));
    }

    public static UInt32 ErrorIfZero(UInt32 val) {
      if (val == 0)
        throw GetError();
      return val;
    }

    public static UInt32 ErrorIfNonZero(UInt32 val) {
      if (val != 0)
        throw GetError();
      return val;
    }

    public static int ErrorIfNegative(int val) {
      if (val < 0)
        throw GetError();
      return val;
    }

    public static T ErrorIfNull<T>(T val) {
      if (val == null)
        throw GetError();
      return val;
    }

    public static IntPtr ErrorIfNull(IntPtr val) {
      if (val == IntPtr.Zero)
        throw GetError();
      return val;
    }

    public static T ErrorIfInvalid<T>(T val) where T : SafeHandle {
      if (val.IsInvalid)
        throw GetError();
      return val;
    }

    public static SDL_Bool ErrorIfFalse(SDL_Bool val) {
      if (val != SDL_Bool.True)
        throw GetError();
      return val;
    }

  }
}
