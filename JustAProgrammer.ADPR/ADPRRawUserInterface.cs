using System;
using System.Management.Automation.Host;

namespace JustAProgrammer.ADPR
{
    /// <summary>
    /// A sample implementation of the <seealso cref="PSHostRawUserInterface"/> for console
    /// applications. Members of this class that easily map to the .NET 
    /// console class are implemented. More complex methods are not 
    /// implemented and throw a NotImplementedException exception.
    /// </summary>
    /// <remarks>Based on the msdn page <seealso cref="http://msdn.microsoft.com/en-us/library/windows/desktop/ee706570(v=vs.85).aspx">Writing a Windows PowerShell Host Application</seealso></remarks>
    internal class ADPRRawUserInterface : PSHostRawUserInterface
    {
        /// <summary>
        /// Gets or sets the background color of the displayed text.
        /// This maps to the corresponding Console.Background property.
        /// </summary>
        public override ConsoleColor BackgroundColor
        {
            get { return Console.BackgroundColor; }
            set { Console.BackgroundColor = value; }
        }

        /// <summary>
        /// Gets or sets the size of the host buffer. In this example the 
        /// buffer size is adapted from the Console buffer size members.
        /// </summary>
        public override Size BufferSize
        {
            get { return new Size(Console.BufferWidth, Console.BufferHeight); }
            set { Console.SetBufferSize(value.Width, value.Height); }
        }

        /// <summary>
        /// Gets or sets the size of the displayed cursor. In this example 
        /// the cursor size is taken directly from the Console.CursorSize 
        /// property.
        /// </summary>
        public override int CursorSize
        {
            get { return Console.CursorSize; }
            set { Console.CursorSize = value; }
        }

        /// <summary>
        /// Gets or sets the foreground color of the displayed text.
        /// This maps to the corresponding Console.ForgroundColor property.
        /// </summary>
        public override ConsoleColor ForegroundColor
        {
            get { return Console.ForegroundColor; }
            set { Console.ForegroundColor = value; }
        }

        /// <summary>
        /// Gets a value indicating whether the user has pressed a key. This maps   
        /// to the corresponding Console.KeyAvailable property.
        /// </summary>
        public override bool KeyAvailable
        {
            get { return Console.KeyAvailable; }
        }

        /// <summary>
        /// Gets the dimensions of the largest window that could be 
        /// rendered in the current display, if the buffer was at the least 
        /// that large. This example uses the Console.LargestWindowWidth and 
        /// Console.LargestWindowHeight properties to determine the returned 
        /// value of this property.
        /// </summary>
        public override Size MaxPhysicalWindowSize
        {
            get { return new Size(Console.LargestWindowWidth, Console.LargestWindowHeight); }
        }

        /// <summary>
        /// Gets the dimentions of the largest window size that can be 
        /// displayed. This example uses the Console.LargestWindowWidth and 
        /// console.LargestWindowHeight properties to determine the returned 
        /// value of this property.
        /// </summary>
        public override Size MaxWindowSize
        {
            get { return new Size(Console.LargestWindowWidth, Console.LargestWindowHeight); }
        }

        /// <summary>
        /// Gets or sets the position of the displayed window. This example 
        /// uses the Console window position APIs to determine the returned 
        /// value of this property.
        /// </summary>
        public override Coordinates WindowPosition
        {
            get { return new Coordinates(Console.WindowLeft, Console.WindowTop); }
            set { Console.SetWindowPosition(value.X, value.Y); }
        }

        /// <summary>
        /// Gets or sets the size of the displayed window. This example 
        /// uses the corresponding Console window size APIs to determine the  
        /// returned value of this property.
        /// </summary>
        public override Size WindowSize
        {
            get { return new Size(Console.WindowWidth, Console.WindowHeight); }
            set { Console.SetWindowSize(value.Width, value.Height); }
        }

        /// <summary>
        /// Gets or sets the title of the displayed window. The example 
        /// maps the Console.Title property to the value of this property.
        /// </summary>
        public override string WindowTitle
        {
            get { return Console.Title; }
            set { Console.Title = value; }
        }

        /// <summary>
        /// This API resets the input buffer. In this example this 
        /// functionality is not needed so the method returns nothing.
        /// </summary>
        public override void FlushInputBuffer()
        {
            Console.In.ReadToEnd();
        }

        /// <summary>
        /// This API returns a rectangular region of the screen buffer. In 
        /// this example this functionality is not needed so the method throws 
        /// a NotImplementException exception.
        /// </summary>
        /// <param name="rectangle">Defines the size of the rectangle.</param>
        /// <returns>Throws a NotImplementedException exception.</returns>
        public override BufferCell[,] GetBufferContents(Rectangle rectangle)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        /// <summary>
        /// This API reads a pressed, released, or pressed and released keystroke 
        /// from the keyboard device, blocking processing until a keystroke is 
        /// typed that matches the specified keystroke options.
        /// </summary>
        /// <param name="options">Options, such as IncludeKeyDown,  used when 
        /// reading the keyboard.</param>
        /// <returns>Throws a NotImplementedException exception.</returns>
        public override KeyInfo ReadKey(ReadKeyOptions options)
        {
            throw new NotImplementedException("TODO: Verify my ReadKey code works");
            var keyInfo = Console.ReadKey((options & ReadKeyOptions.NoEcho) == ReadKeyOptions.NoEcho);
            ControlKeyStates ctrlKeyState;
            switch (keyInfo.Modifiers)
            {
                case ConsoleModifiers.Control:
                    ctrlKeyState = ControlKeyStates.LeftCtrlPressed;
                    break;
                case ConsoleModifiers.Shift:
                    ctrlKeyState = ControlKeyStates.ShiftPressed;
                    break;
                case ConsoleModifiers.Alt:
                    ctrlKeyState = ControlKeyStates.LeftAltPressed;
                    break;
                case ConsoleModifiers.Control | ConsoleModifiers.Alt:
                    ctrlKeyState = ControlKeyStates.LeftCtrlPressed | ControlKeyStates.LeftAltPressed;
                    break;
                case ConsoleModifiers.Control | ConsoleModifiers.Shift:
                    ctrlKeyState = ControlKeyStates.LeftCtrlPressed | ControlKeyStates.ShiftPressed;
                    break;
                case ConsoleModifiers.Alt | ConsoleModifiers.Shift:
                    ctrlKeyState = ControlKeyStates.LeftAltPressed | ControlKeyStates.ShiftPressed;
                    break;
                default:
                    ctrlKeyState = 0;
                    break;
            }
            return new KeyInfo
                       {
                           Character = keyInfo.KeyChar,
                           ControlKeyState = ctrlKeyState,
                           KeyDown = false,
                           //TODO: Verify this is correct
                           VirtualKeyCode = (int) keyInfo.Key
                       };
        }

        public override Coordinates CursorPosition
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void ScrollBufferContents(Rectangle source, Coordinates destination, Rectangle clip, BufferCell fill)
        {
            throw new NotImplementedException();
        }

        public override void SetBufferContents(Rectangle rectangle, BufferCell fill)
        {
            throw new NotImplementedException();
        }

        public override void SetBufferContents(Coordinates origin, BufferCell[,] contents)
        {
            throw new NotImplementedException();
        }
    }
}