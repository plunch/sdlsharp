using System;
using System.Text;
using System.Drawing;
using static SDLSharp.NativeMethods;
using System.Runtime.InteropServices;

namespace SDLSharp
{
  public class Joystick : SafeHandle {
    public static JoystickDevices Devices { get; } = new JoystickDevices();

    public JoystickHats Hats { get; }
    public JoystickAxes Axes { get; }
    public JoystickBalls Balls { get; }
    public JoystickButtons Buttons { get; }

    protected Joystick() : base(IntPtr.Zero, true) {
      Hats = new JoystickHats(this);
      Axes = new JoystickAxes(this);
      Balls = new JoystickBalls(this);
      Buttons = new JoystickButtons(this);
    }

    public Joystick(IntPtr h, bool owned) : base(IntPtr.Zero, owned) {
      SetHandle(h);
      Hats = new JoystickHats(this);
      Axes = new JoystickAxes(this);
      Balls = new JoystickBalls(this);
      Buttons = new JoystickButtons(this);
    }

    public Joystick(int id) :this() {
      var joy = ErrorIfInvalid(SDL_JoystickFromInstanceID(id));
      SetHandle(joy.handle);
      joy.SetHandle(IntPtr.Zero);
    }

    internal static byte[] ZeroJoystickGUID = new byte[16];

    public int ID => ErrorIfNegative(SDL_JoystickInstanceID(this));
    public unsafe string Name => ErrorIfNull(UTF8ToString(SDL_JoystickName(this))) ?? "";
    public unsafe Guid Guid {
      get {
        var g = SDL_JoystickGetGUID(this);
        var b = new Span<byte>(g.data, 16);
        if (b.SequenceEqual(ZeroJoystickGUID))
          throw GetError2();
        return new Guid(b);
      }
    }
    public JoystickPowerLevel PowerLevel {
      get {
        var p = SDL_JoystickCurrentPowerLevel(this);
        if (p == JoystickPowerLevel.Unknown) {
          var err = GetError();
          if (err != null) throw err;
        }
        return p;
      }
    }
    public bool IsAttached {
      get {
        var b = SDL_JoystickGetAttached(this) == SDL_Bool.True;
        if (!b) {
          var err = GetError();
          if (err != null) throw err;
        }
        return b;
      }
    }

    public static void Update() {
      SDL_JoystickUpdate();
    }

    public static bool EventsEnabled {
      get {
        return ErrorIfNegative(SDL_JoystickEventState(-1)) == 1;
      }
      set {
        ErrorIfNegative(SDL_JoystickEventState(value ? 0 : 1));
      }
    }

    public override bool IsInvalid => handle == IntPtr.Zero;

    override protected bool ReleaseHandle() {
      NativeMethods.SDL_JoystickClose(this.handle);
      return true;
    }
  }
}
