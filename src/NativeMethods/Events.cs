using System;
using System.Runtime.InteropServices;

namespace SDLSharp {
  static unsafe partial class NativeMethods {
    [DllImport("SDL2")]
    public static extern void SDL_AddEventWatch(SDL_EventFilter filter, void* userdata);

    [DllImport("SDL2")]
    public static extern void SDL_DelEventWatch(SDL_EventFilter filter, void* userdata);

    [DllImport("SDL2")]
    public static extern byte SDL_EventState(uint type, int state);

    [DllImport("SDL2")]
    public static extern void SDL_FilterEvents(SDL_EventFilter filter, void* userdata);

    [DllImport("SDL2")]
    public static extern void SDL_FlushEvent(uint type);

    [DllImport("SDL2")]
    public static extern void SDL_FlushEvents(uint minType, uint maxType);

    [DllImport("SDL2")]
    public static extern SDL_Bool SDL_GetEventFilter(out SDL_EventFilter filter, out void* userdata);

    [DllImport("SDL2")]
    public static extern byte SDL_GetEventState(uint type);

    [DllImport("SDL2")]
    public static extern int SDL_GetNumTouchDevices();

    [DllImport("SDL2")]
    public static extern int SDL_GetNumTouchFingers(long touchID);

    [DllImport("SDL2")]
    public static extern long SDL_GetTouchDevice(int index);

    [DllImport("SDL2")]
    public static extern SDL_Finger* SDL_GetTouchFinger(long touchID, int index);

    [DllImport("SDL2")]
    public static extern SDL_Bool SDL_HasEvent(uint type);

    [DllImport("SDL2")]
    public static extern SDL_Bool SDL_HasEvents(uint minType, uint maxType);

    //[DllImport("SDL2")]
    //public static extern int SDL_LoadDollarTemplates(SDL_TouchID touchId, SDL_RWOps* src)

    [DllImport("SDL2")]
    public static extern int SDL_PeepEvents(
        Event* events,
        int numevents,
        SDL_eventaction action,
        uint minType,
        uint maxType
    );

    [DllImport("SDL2")]
    public static extern int SDL_PollEvent(out Event @event);

    [DllImport("SDL2")]
    public static extern void SDL_PumpEvents();

    [DllImport("SDL2")]
    public static extern int SDL_PushEvent(Event* @event);

    [DllImport("SDL2")]
    public static extern SDL_Bool SDL_QuitRequested();

    [DllImport("SDL2")]
    public static extern int SDL_RecordGesture(long touchId);

    [DllImport("SDL2")]
    public static extern uint SDL_RegisterEvents(int numevents);

    //[DllImport("SDL2")]
    //public static extern int SDL_SaveAllDollarTemplates(SDL_RWops* dst);
    
    //[DllImport("SDL2")]
    //public static extern int SDL_SaveDollarTemplate(SDL_GestureID gestureId, SDL_RWops* dst);
    
    [DllImport("SDL2")]
    public static extern void SDL_SetEventFilter(SDL_EventFilter filter, void* userdata);

    [DllImport("SDL2")]
    public static extern int SDL_WaitEvent(out Event @event);

    [DllImport("SDL2")]
    public static extern int SDL_WaitEventTimeout(out Event @event, int timeout);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SDL_EventFilter(void* userdata, Event* @event);

    public enum SDL_eventaction {
      Add,
      Peek,
      Get,
    }

    public struct SDL_Finger {
      public long id;
      public float x, y, pressure;
    }
  }

  public enum EventType : uint {
    FirstEvent = 0,

    Quit = 0x100,

    AppTerminating,
    AppLowMemory,
    AppWillEnterBackground,
    AppDidEnterBackground,
    AppWillEnterForeground,
    AppDidEnterForeground,

    WindowEvent = 0x200,
    SysWMEvent,

    KeyDown = 0x300,
    KeyUp,
    TextEditing,
    TextInput,
    KeymapChanged,

    MouseMotion = 0x400,
    MouseButtonDown,
    MouseButtonUp,
    MouseWheel,

    JoyAxisMotion = 0x600,
    JoyBallMotion,
    JoyHatMotion,
    JoyButtonDown,
    JoyButtonUp,
    JoyDeviceAdded,
    JoyDeviceRemoved,

    ControllerAxisMotion = 0x650,
    ControllerButtonDown,
    ControllerButtonUp,
    ControllerDeviceAded,
    ControllerDeviceRemoved,
    ControllerDeviceRemapped,

    FingerDown = 0x700,
    FingerUp,
    FingerMotion,

    DollarGesture = 0x800,
    DollarRecord,
    MultiGesture,

    ClipboardUpdate = 0x900,

    DropFile = 0x1000,
    DropText,
    DropBegin,
    DropComplete,
    
    AudioDeviceAdded = 0x1100,
    AudioDeviceRemoved,

    RenderTargetsReset = 0x2000,
    RenderDeviceReset,

    UserEvent = 0x8000,

    LastEvent = 0xFFFF,
  }

  [StructLayout(LayoutKind.Explicit)]
  public unsafe struct Event {
    [FieldOffset(0)]
    public EventType type;
    [FieldOffset(0)]
    public CommonEvent common;
    [FieldOffset(0)]
    public WindowEvent window;
    [FieldOffset(0)]
    public KeyboardEvent keyboard;
    [FieldOffset(0)]
    public TextEditingEvent edit;
    [FieldOffset(0)]
    public TextInputEvent text;
    [FieldOffset(0)]
    public MouseMotionEvent motion;
    [FieldOffset(0)]
    public MouseButtonEvent button;
    [FieldOffset(0)]
    public MouseWheelEvent wheel;
    [FieldOffset(0)]
    public JoyAxisEvent jaxis;
    [FieldOffset(0)]
    public JoyBallEvent jball;
    [FieldOffset(0)]
    public JoyDeviceEvent jdevice;
    [FieldOffset(0)]
    public ControllerAxisEvent caxis;
    [FieldOffset(0)]
    public ControllerButtonEvent cbutton;
    [FieldOffset(0)]
    public ControllerDeviceEvent cdevice;
    [FieldOffset(0)]
    public AudioDeviceEvent addevice;
    [FieldOffset(0)]
    public QuitEvent quit;
    [FieldOffset(0)]
    public UserEvent user;
    [FieldOffset(0)]
    public SysWMEvent syswm;
    [FieldOffset(0)]
    public TouchFingerEvent tfinger;
    [FieldOffset(0)]
    public MultiGestureEvent mgesture;
    [FieldOffset(0)]
    public DollarGestureEvent dgesture;
    [FieldOffset(0)]
    public DropEvent drop;
    [FieldOffset(0)]
    fixed byte padding[56];
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct CommonEvent {
    public EventType type;
    public uint timestamp;
  }

  public enum WindowEventType : byte {
    None,
    Shown,
    Hidden,
    Exposed,
    Moved,
    Resized,
    SizeChanged,
    Minimized,
    Maximized,
    Restored,
    Enter,
    Leave,
    FocusGained,
    FocusLost,
    Close,
    TakeFocus,
    HitTest,
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct WindowEvent {
    public EventType type;
    public uint timestamp;
    public uint windowID;
    public WindowEventType @event; 
    byte padding1, padding2, padding3;
    int data1, data2;
  }
  
  [StructLayout(LayoutKind.Sequential)]
  public struct KeyboardEvent {
    public EventType type;
    public uint timestamp;
    public uint windowID;
    public byte state;
    public byte repeat;
    byte padding2, padding3;
    Keysym keysym;
  }

  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct TextEditingEvent {
    public EventType type;
    public uint timestamp;
    public uint windowID;
    public fixed byte text[32];
    public int start, length;
  }

  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct TextInputEvent {
    public EventType type;
    public uint timestamp;
    public uint windowID;
    public fixed byte text[32];
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct MouseMotionEvent {
    public EventType type;
    public uint timestamp;
    public uint windowID;
    public uint which;
    public uint state;
    public int x, y;
    public int xrel, yrel;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct MouseButtonEvent {
    public EventType type;
    public uint timestamp;
    public uint windowID;
    public uint which;
    public byte button, state, clicks;
    byte padding1;
    public int x, y;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct MouseWheelEvent {
    public EventType type;
    public uint timestamp;
    public uint windowID;
    public uint which;
    public uint state;
    public int x, y;
    public uint direction;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct JoyAxisEvent {
    public EventType type;
    public uint timestamp;
    public int which;
    public byte axis;
    byte padding1, padding2, padding3;
    public short value;
    ushort padding4;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct JoyBallEvent {
    public EventType type;
    public uint timestamp;
    public int which;
    public byte ball;
    byte padding1, padding2, padding3;
    public short xrel, yrel;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct JoyHatEvent {
    public EventType type;
    public uint timestamp;
    public int which;
    public byte hat, value;
    byte padding1, padding2;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct JoyButtonEvent {
    public EventType type;
    public uint timestamp;
    public int which;
    public byte button, state;
    byte padding1, padding2;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct JoyDeviceEvent {
    public EventType type;
    public uint timestamp;
    public int which;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct ControllerAxisEvent {
    public EventType type;
    public uint timestamp;
    public int which;
    public byte axis;
    byte padding1, padding2, padding3;
    public short value;
    ushort padding4;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct ControllerButtonEvent {
    public EventType type;
    public uint timestamp;
    public int which;
    public byte button, state;
    byte padding1, padding2;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct ControllerDeviceEvent {
    public EventType type;
    public uint timestamp;
    public int which;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct AudioDeviceEvent {
    public EventType type;
    public uint timestamp;
    public uint which;
    public byte iscapture;
    byte padding1, padding2, padding3;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct TouchFingerEvent {
    public EventType type;
    public uint timestamp;
    public long touchId;
    public long fingerId;
    public float x, y, dx, dy, pressure;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct MultiGestureEvent {
    public EventType type;
    public uint timestamp;
    public long touchId;
    public float dTheta, dDist, x, y;
    public ushort numFingers;
    ushort padding;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct DollarGestureEvent {
    public EventType type;
    public uint timestamp;
    public long touchId;
    public long gestureId;
    public uint numFingers;
    public float error, x, y;
  }

  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct DropEvent {
    public EventType type;
    public uint timestamp;
    byte* _file;
    public string file => NativeMethods.UTF8ToString(this._file);
    public uint windowId;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct QuitEvent {
    public EventType type;
    public uint timestamp;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct OSEvent {
    public EventType type;
    public uint timestamp;
  }
  
  [StructLayout(LayoutKind.Sequential)]
  public struct UserEvent {
    public EventType type;
    public uint timestamp;
    public uint windowID;
    public int code;
    public IntPtr data1, data2;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct SysWMEvent {
    public EventType type;
    public uint timestamp;
    public IntPtr msg;
  }
}
