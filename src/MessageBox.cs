using System;
using System.Text;
using System.Drawing;
using static SDLSharp.NativeMethods;
using System.Runtime.InteropServices;

namespace SDLSharp
{
  public static class MessageBox
  {
    public static void ShowError(string title, string message, Window? parent = null)
      => Show(MessageBoxFlags.Error, title, message, parent);

    public static void ShowWarning(string title, string message, Window? parent = null)
      => Show(MessageBoxFlags.Error, title, message, parent);

    public static void ShowInformation(string title, string message, Window? parent = null)
      => Show(MessageBoxFlags.Error, title, message, parent);

    private static unsafe void Show(MessageBoxFlags flags, string title, string message, Window? parent)
    {
      Span<byte> tbuf = stackalloc byte[SL(title)];
      Span<byte> msgbuf = stackalloc byte[SL(message)];
      StringToUTF8(title, tbuf);
      StringToUTF8(message, msgbuf);
      fixed (byte* t = &MemoryMarshal.GetReference(tbuf))
      fixed (byte* msg = &MemoryMarshal.GetReference(msgbuf)) {
        if (parent == null)
          ErrorIfNegative(SDL_ShowSimpleMessageBox((uint)flags, t, msg, IntPtr.Zero));
        else
          ErrorIfNegative(SDL_ShowSimpleMessageBox((uint)flags, t, msg, parent.handle));
      }
    }
  }
}
