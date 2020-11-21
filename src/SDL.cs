using System;
using System.Collections.Generic;
using static SDLSharp.NativeMethods;
using System.Runtime.InteropServices;
using System.Runtime.ExceptionServices;

namespace SDLSharp {
  public static class SDL {
    public static void Init(InitFlags flags = InitFlags.Nothing) {
      bool disableDrop = ShouldDisableDropAfterInit(flags);
      ErrorIfNegative(SDL_Init(flags));
      if (disableDrop) DisableDropEvents();
    }

    public static void InitSubSystem(InitFlags flags) {
      bool disableDrop = ShouldDisableDropAfterInit(flags);
      ErrorIfNegative(SDL_InitSubSystem(flags));
      if (disableDrop) DisableDropEvents();
    }

    public static void Quit() {
      SDL_Quit();
    }

    public static void QuitSubSystem(InitFlags flags) {
      SDL_QuitSubSystem(flags);
    }

    public static bool QuitRequested()
      => SDL_QuitRequested() == SDL_Bool.True;

    static internal bool ShouldDisableDropAfterInit(InitFlags flags) {
      var initsEvent
        = InitFlags.Video
        | InitFlags.Joystick
        | InitFlags.GameController
        | InitFlags.Events
      ;

      if (SDL_WasInit(InitFlags.Events) != 0)
        return false;

      if ((flags & initsEvent) != InitFlags.Nothing)
        return false;

      return true;
    }

    static internal void DisableDropEvents() {
      Events.Ignore[EventType.DropFile] = true;
      Events.Ignore[EventType.DropText] = true;
      Events.Ignore[EventType.DropBegin] = true;
      Events.Ignore[EventType.DropComplete] = true;
    }
  }
}
