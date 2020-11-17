using System.Runtime.InteropServices;

namespace SDLSharp {
  static unsafe partial class NativeMethods {
    [DllImport("SDL2")]
    public static extern /*const*/char* SDL_GetRevision();

    [DllImport("SDL2")]
    public static extern int SDL_GetRevisionNumber();

    [DllImport("SDL2")]
    public static extern void SDL_GetVersion(SDL_Version* ver);

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_Version {
      byte major;
      byte minor;
      byte patch;
    }
  }
}
