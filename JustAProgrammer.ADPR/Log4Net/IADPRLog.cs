using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Core;

namespace JustAProgrammer.ADPR.Log4Net
{
    interface IADPRLog :ILog
    {

        /// <summary>
        /// Checks if this logger is enabled for the <see cref="Level.Verbose"/> level.
        /// </summary>
        /// <value>
        /// <c>true</c> if this logger is enabled for <see cref="Level.Verbose"/> events, <c>false</c> otherwise.
        /// </value>
        /// <remarks>
        /// For more information see <see cref="ILog.IsDebugEnabled"/>.
        /// </remarks>
        /// <seealso cref="M:Verbose(object)"/>
        /// <seealso cref="M:VerboseFormat(IFormatProvider, string, object[])"/>
        /// <seealso cref="ILog.IsDebugEnabled"/>
        bool IsVerboseEnabled { get; }

        /// <overloads>Log a message object with the <see cref="Level.Verbose"/> level.</overloads>
        /// <summary>
        /// Logs a message object with the <see cref="Level.Verbose"/> level.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method first checks if this logger is <c>VERBOSE</c>
        /// enabled by comparing the level of this logger with the 
        /// <see cref="Level.Verbose"/> level. If this logger is
        /// <c>VERBOSE</c> enabled, then it converts the message object
        /// (passed as parameter) to a string by invoking the appropriate
        /// <see cref="log4net.ObjectRenderer.IObjectRenderer"/>. It then 
        /// proceeds to call all the registered appenders in this logger 
        /// and also higher in the hierarchy depending on the value of the 
        /// additivity flag.
        /// </para>
        /// <para><b>WARNING</b> Note that passing an <see cref="Exception"/> 
        /// to this method will print the name of the <see cref="Exception"/> 
        /// but no stack trace. To print a stack trace use the 
        /// <see cref="M:Verbose(object,Exception)"/> form instead.
        /// </para>
        /// </remarks>
        /// <param name="message">The message object to log.</param>
        /// <seealso cref="M:Verbose(object,Exception)"/>
        /// <seealso cref="IsVerboseEnabled"/>
        void Verbose(object message);

        /// <summary>
        /// Logs a message object with the <c>VERBOSE</c> level including
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <remarks>
        /// <para>
        /// See the <see cref="M:Verbose(object)"/> form for more detailed information.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:Verbose(object)"/>
        /// <seealso cref="IsVerboseEnabled"/>
        void Verbose(object message, Exception exception);

        /// <overloads>Log a formatted message string with the <see cref="Level.Verbose"/> level.</overloads>
        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Verbose"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="M:Verbose(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:Verbose(object,Exception)"/>
        /// <seealso cref="IsVerboseEnabled"/>
        void VerboseFormat(string format, params object[] args);

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Verbose"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="M:Verbose(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:Verbose(object)"/>
        /// <seealso cref="IsVerboseEnabled"/>
        void VerboseFormat(string format, object arg0);

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Verbose"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <param name="arg1">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="M:Verbose(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:Verbose(object)"/>
        /// <seealso cref="IsVerboseEnabled"/>
        void VerboseFormat(string format, object arg0, object arg1);

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Verbose"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <param name="arg1">An Object to format</param>
        /// <param name="arg2">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="M:Verbose(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:Verbose(object)"/>
        /// <seealso cref="IsVerboseEnabled"/>
        void VerboseFormat(string format, object arg0, object arg1, object arg2);

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Verbose"/> level.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="M:Verbose(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:Verbose(object,Exception)"/>
        /// <seealso cref="IsVerboseEnabled"/>
        void VerboseFormat(IFormatProvider provider, string format, params object[] args);
    }
}
