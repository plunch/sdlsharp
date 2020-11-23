using System;
using System.Runtime.InteropServices;

namespace SDLSharp {
  static unsafe partial class NativeMethods {
    [DllImport("SDL2")]
    public static extern /*const*/byte* SDL_GetError();

    [DllImport("SDL2")]
    public static extern int SDL_SetError(/*const char*/ byte* fmt, __arglist);

    [DllImport("SDL2")]
    public static extern void SDL_ClearError();

    public static void SetError(Exception ex) {
      SetError(ex.ToString());
    }

    public static unsafe void SetError(string error) {
      const string fmt = "%s";
      Span<byte> buf = stackalloc byte[SL(fmt) + SL(error)];
      fixed(byte* ptr = buf)
        SDL_SetError(ptr, __arglist(ptr + SL(fmt)));
    }

    public static SDLException? GetError() {
      var err = SDL_GetError();
      if (err == null || err[0] == 0)
        return null;
      return new SDLException(UTF8ToString(err)??"");
    }

    public static SDLException GetError2() {
      return GetError() ?? new SDLException("An error was expected, but there was none!");
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
