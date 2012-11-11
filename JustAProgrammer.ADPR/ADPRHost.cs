using System;
using System.Globalization;
using System.Management.Automation.Host;
using System.Threading;

namespace JustAProgrammer.ADPR
{
    /// <summary>
    /// <seealso cref="PSHost"/> implementation for AppDomainPowerShellRunner.
    /// </summary>
    /// <remarks>Based on the msdn page <seealso cref="http://msdn.microsoft.com/en-us/library/windows/desktop/ee706570(v=vs.85).aspx">Writing a Windows PowerShell Host Application</seealso></remarks>
    public class ADPRHost : PSHost
    {
        /// <summary>
        /// The identifier of this PSHost implementation.
        /// </summary>
        private Guid instanceId = Guid.NewGuid();

        /// <summary>
        /// A reference to the implementation of the PSHostUserInterface
        /// class for this application.
        /// </summary>
        private ADPRUserInterface ui = new ADPRUserInterface();

        /// <summary>
        /// Gets the culture information to use.
        /// </summary>
        public override CultureInfo CurrentCulture
        {
            get { return Thread.CurrentThread.CurrentCulture; }
        }

        /// <summary>
        /// Gets the UI culture information to use. This implementation 
        /// returns a snapshot of the UI culture information of the thread 
        /// that created this object.
        /// </summary>
        public override CultureInfo CurrentUICulture
        {
            get { return Thread.CurrentThread.CurrentUICulture; }
        }

        /// <summary>
        /// Gets an identifier for this host. This implementation always 
        /// returns the GUID allocated at instantiation time.
        /// </summary>
        public override Guid InstanceId
        {
            get { return this.instanceId; }
        }

        /// <summary>
        /// Gets a string that contains the name of this host implementation. 
        /// Keep in mind that this string may be used by script writers to
        /// identify when your host is being used.
        /// </summary>
        public override string Name
        {
            get { return "AppDomainPowerShellRunner"; }
        }

        /// <summary>State about the <seealso cref="ADPRHost"/></summary>
        internal ADPRState State { get; private set; }

        /// <summary>
        /// Gets an instance of the implementation of the <seealso cref="PSHostUserInterface"/>
        /// class for this application. This instance is allocated once at startup time
        /// and returned every time thereafter.
        /// </summary>
        public override PSHostUserInterface UI
        {
            get { return this.ui; }
        }

        /// <summary>
        /// Gets the version object for this application. This is the assembly version.
        /// </summary>
        public override Version Version
        {
            get { return GetType().Assembly.GetName().Version; }
        }

        public ADPRHost(ADPRState state)
        {
            State = state;
        }

        /// <summary>
        /// This API Instructs the host to interrupt the currently running 
        /// pipeline and start a new nested input loop. In this example this 
        /// functionality is not needed so the method throws a 
        /// NotImplementedException exception.
        /// </summary>
        public override void EnterNestedPrompt()
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        /// <summary>
        /// This API instructs the host to exit the currently running input loop. 
        /// In this example this functionality is not needed so the method 
        /// throws a NotImplementedException exception.
        /// </summary>
        public override void ExitNestedPrompt()
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        /// <summary>
        /// This API is called before an external application process is 
        /// started. Typically it is used to save state so that the parent  
        /// can restore state that has been modified by a child process (after 
        /// the child exits). In this example this functionality is not  
        /// needed so the method returns nothing.
        /// </summary>
        public override void NotifyBeginApplication()
        {
            return;
        }

        /// <summary>
        /// This API is called after an external application process finishes.
        /// Typically it is used to restore state that a child process has
        /// altered. In this example, this functionality is not needed so  
        /// the method returns nothing.
        /// </summary>
        public override void NotifyEndApplication()
        {
            return;
        }

        /// <summary>
        /// Indicate to the host application that exit has
        /// been requested. Pass the exit code that the host
        /// application should use when exiting the process.
        /// </summary>
        /// <param name="exitCode">The exit code that the 
        /// host application should use.</param>
        public override void SetShouldExit(int exitCode)
        {
            State.ShouldExit = true;
            State.ExitCode = exitCode;
        }
    }
}