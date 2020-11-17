using System.Runtime.InteropServices;

namespace SDLSharp {
  static unsafe partial class NativeMethods {
    [DllImport("SDL2")]
    public static extern void SDL_LogSetAllPriority(LogPriority level);
  }

  public enum LogPriority {
    Verbose = 1,
    Debug = 2,
    Info = 3,
    Warn = 4,
    Error = 5,
    Critical = 6,
  }
}
