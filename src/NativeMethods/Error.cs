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

    public static Exception? GetError() {
      var err = SDL_GetError();
      if (err == null || err[0] == 0)
        return null;
      return new Exception(UTF8ToString(err));
    }

    public static Exception GetError2() {
      return GetError() ?? new Exception("An error was expected, but there was none!");
    }

    public static void Throw() {
      throw GetError2();
    }

    public static UInt32 ErrorIfZero(UInt32 val) {
      if (val == 0)
        Throw();
      return val;
    }

    public static int ErrorIfZero(int val) {
      if (val == 0)
        Throw();
      return val;
    }
    public static UInt32 ErrorIfNonZero(UInt32 val) {
      if (val != 0)
        Throw();
      return val;
    }

    public static int ErrorIfNegative(int val) {
      if (val < 0)
        Throw();
      return val;
    }

    public static T ErrorIfNull<T>(T val) {
      if (val == null)
        Throw();
      return val;
    }

    public static IntPtr ErrorIfNull(IntPtr val) {
      if (val == IntPtr.Zero)
        Throw();
      return val;
    }

    public static T ErrorIfInvalid<T>(T val) where T : SafeHandle {
      if (val.IsInvalid)
        Throw();
      return val;
    }

    public static SDL_Bool ErrorIfFalse(SDL_Bool val) {
      if (val != SDL_Bool.True)
        Throw();
      return val;
    }

  }
}
