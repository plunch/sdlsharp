using System;
using System.Collections.Generic;
using static SDLSharp.NativeMethods;
using System.Runtime.InteropServices;

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

    public static bool PollEvent(out Event ev) {
      return SDL_PollEvent(out ev) != 0;
    }

    public static void WaitEvent(out Event ev) {
      ErrorIfZero(SDL_WaitEvent(out ev));
    }

    public static bool WaitEvent(out Event ev, int timeout) {
      if (SDL_WaitEventTimeout(out ev, timeout) != 0) {
        return true;
      } else {
        var error = GetError();
        if (error != null)
          throw error;
        return false;
      }
    }

    public static void PumpEvents() {
      SDL_PumpEvents();
    }

    public static unsafe int PeepEvents(
      Span<Event> events,
      EventType minType = EventType.FirstEvent,
      EventType maxType = EventType.LastEvent,
      bool remove = false
    ) {
      fixed(Event* ptr = &MemoryMarshal.GetReference(events))
        return ErrorIfNegative(SDL_PeepEvents(ptr, events.Length, remove ? SDL_eventaction.Peek : SDL_eventaction.Get, (uint)minType, (uint)maxType));
    }

    public static bool HasEvent(EventType type)
      => SDL_HasEvent((uint)type) == SDL_Bool.True;
    public static bool HasEvents(EventType start, EventType end)
      => SDL_HasEvents((uint)start, (uint)end) == SDL_Bool.True;

    public static bool QuitRequested()
      => SDL_QuitRequested() == SDL_Bool.True;

    public static IgnoredEvents IgnoreEvent => new IgnoredEvents();
    public class IgnoredEvents {
      public bool this[EventType type] {
        get {
          return SDL_EventState((uint)type, -1) == 0;
        }
        set {
          SDL_EventState((uint)type, value ? 0 : 1);
        }
      }
    }

    public static bool PushEvent(in Event ev) {
      return ErrorIfNegative(SDL_PushEvent(ev)) != 0;
    }

    public static unsafe int PushEvents(
      Span<Event> events,
      EventType minType = EventType.FirstEvent,
      EventType maxType = EventType.LastEvent
    ) {
      fixed(Event* ptr = &MemoryMarshal.GetReference(events))
        return ErrorIfNegative(SDL_PeepEvents(ptr, events.Length, SDL_eventaction.Add, (uint)minType, (uint)maxType));
    }

    public static uint RegisterEvents(int count) {
      return SDL_RegisterEvents(count);
    }

    public delegate void EventWatch(ref Event ev);
    struct WatchReg {
      public SDL_EventFilter del;
      public IntPtr fp;
      public EventWatch func;

      public WatchReg(EventWatch func) {
        this.func = func;
        del = (IntPtr ud, ref Event v) => {
          try {
            func(ref v);
          } catch {
          }
        };
        fp = Marshal.GetFunctionPointerForDelegate(del);
      }
    }
    private static List<WatchReg> watches = new List<WatchReg>();

    public static event EventWatch OnEvent {
      add {
        var wr = new WatchReg(value);
        SDL_AddEventWatch(wr.fp, IntPtr.Zero);
        lock(watches)
          watches.Add(wr);
      }
      remove {
        WatchReg w;
        lock(watches) {
          int i = watches.FindIndex(x => x.func == value);
          if (i >= 0) {
            w = watches[i];
            watches.RemoveAt(i);
          } else {
            return;
          }
        }

        SDL_DelEventWatch(w.fp, IntPtr.Zero);
      }
    }

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
      SDL.IgnoreEvent[EventType.DropFile] = true;
      SDL.IgnoreEvent[EventType.DropText] = true;
      SDL.IgnoreEvent[EventType.DropBegin] = true;
      SDL.IgnoreEvent[EventType.DropComplete] = true;
    }

  }
}
