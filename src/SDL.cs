using System;
using static SDLSharp.NativeMethods;
using System.Runtime.InteropServices;

namespace SDLSharp {
  public static class SDL {
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
    public struct IgnoredEvents {
      public bool this[EventType type] {
        get {
          return SDL_EventState((uint)type, -1) == 0;
        }
        set {
          SDL_EventState((uint)type, value ? 0 : 1);
        }
      }
    }

    public static unsafe bool PushEvent(in Event ev) {
      fixed(Event* ptr = &ev)
        return ErrorIfNegative(SDL_PushEvent(ptr)) != 0;
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
  }

  public enum EventState {
    Query = -1,
    Disable = 0,
    Enable = 1,
  }
}
