using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using static SDLSharp.NativeMethods;

namespace SDLSharp
{
  public class JoystickAxes : IReadOnlyList<int> {
    readonly Joystick j;

    internal JoystickAxes(Joystick j) {
      this.j = j;
    }

    public int Count => ErrorIfNegative(SDL_JoystickNumAxes(j));

    public int this[int index] {
      get {
        int v = SDL_JoystickGetAxis(j, index);
        if (v == 0) {
          var err = GetError();
          if (err != null) throw err;
        }
        return v;
      }
    }
    
    IEnumerable<int> Enumerate() {
      int c = Count;
      for (int i = 0; i < c; ++i)
        yield return this[i];
    }

    public IEnumerator<int> GetEnumerator() {
      return this.Enumerate().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }
  }
}
