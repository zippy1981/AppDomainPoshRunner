using System;
using System.Globalization;

using log4net.Core;
using log4net.Repository;
using log4net.Util;

namespace JustAProgrammer.ADPR.Log4Net
{
    internal sealed class ADPRLog : LogImpl, IADPRLog
    {

        /// <summary>
        /// The fully qualified name of this declaring type not the type of any subclass.
        /// </summary>
        private readonly static Type ThisDeclaringType = typeof(LogImpl);

        private Level m_levelVerbose;

        /// <summary>
        /// Construct a new wrapper for the logger.
        /// </summary>
        /// <param name="logger">The logger to wrap.</param>
        /// <remarks>
        /// <para>
        /// The specified logger to wrap.
        /// </para>
        /// </remarks>
        public ADPRLog(ILogger logger) : base(logger) { }

        /// <summary>
        /// Checks if this logger is enabled for the <c>INFO</c> level.
        /// </summary>
        /// <value>
        /// <c>true</c> if this logger is enabled for <c>INFO</c> events,
        /// <c>false</c> otherwise.
        /// </value>
        /// <remarks>
        /// <para>
        /// See <see cref="LogImpl.IsDebugEnabled"/> for more information and examples 
        /// of using this method.
        /// </para>
        /// </remarks>
        /// <seealso cref="LogImpl.IsDebugEnabled"/>
        public bool IsVerboseEnabled
        {
            get { return Logger.IsEnabledFor(m_levelVerbose); }
        }

        /// <summary>
        /// Virtual method called when the configuration of the repository changes
        /// </summary>
        /// <param name="repository">the repository holding the levels</param>
        /// <remarks>
        /// <para>
        /// Virtual method called when the configuration of the repository changes
        /// </para>
        /// </remarks>
        protected override void ReloadLevels(ILoggerRepository repository)
        {
            LevelMap levelMap = repository.LevelMap;

            m_levelVerbose = levelMap.LookupWithDefault(Level.Verbose);
            base.ReloadLevels(repository);
        }


        /// <summary>
        /// Logs a message object with the <c>VERBOSE</c> level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <remarks>
        /// <para>
        /// This method first checks if this logger is <c>VERBOSE</c>
        /// enabled by comparing the level of this logger with the 
        /// <c>VERBOSE</c> level. If this logger is
        /// <c>VERBOSE</c> enabled, then it converts the message object
        /// (passed as parameter) to a string by invoking the appropriate
        /// <see cref="log4net.ObjectRenderer.IObjectRenderer"/>. It then 
        /// proceeds to call all the registered appenders in this logger 
        /// and also higher in the hierarchy depending on the value of 
        /// the additivity flag.
        /// </para>
        /// <para>
        /// <b>WARNING</b> Note that passing an <see cref="Exception"/> 
        /// to this method will print the name of the <see cref="Exception"/> 
        /// but no stack trace. To print a stack trace use the 
        /// <see cref="M:Verbose(object,Exception)"/> form instead.
        /// </para>
        /// </remarks>
        public void Verbose(object message)
        {
            Logger.Log(ThisDeclaringType, m_levelVerbose, message, null);
        }

        /// <summary>
        /// Logs a message object with the <c>VERBOSE</c> level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <remarks>
        /// <para>
        /// Logs a message object with the <c>VERBOSE</c> level including
        /// the stack trace of the <see cref="Exception"/> <paramref name="exception"/> 
        /// passed as a parameter.
        /// </para>
        /// <para>
        /// See the <see cref="M:Verbose(object)"/> form for more detailed information.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:Verbose(object)"/>
        public void Verbose(object message, Exception exception)
        {
            Logger.Log(ThisDeclaringType, m_levelVerbose, message, exception);
        }

        /// <summary>
        /// Logs a formatted message string with the <c>VERBOSE</c> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <see cref="M:String.Format(IFormatProvider, string, object[])"/> method. See
        /// <c>String.Format</c> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// The string is formatted using the <see cref="CultureInfo.InvariantCulture"/>
        /// format provider. To specify a localized provider use the
        /// <see cref="M:VerboseFormat(IFormatProvider,string,object[])"/> method.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="M:Verbose(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        public void VerboseFormat(string format, object arg0)
        {
            if (IsVerboseEnabled)
            {
                Logger.Log(ThisDeclaringType, m_levelVerbose, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0 }), null);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <c>VERBOSE</c> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <see cref="M:String.Format(IFormatProvider, string, object[])"/> method. See
        /// <c>String.Format</c> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// The string is formatted using the <see cref="CultureInfo.InvariantCulture"/>
        /// format provider. To specify a localized provider use the
        /// <see cref="M:VerboseFormat(IFormatProvider,string,object[])"/> method.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="M:Verbose(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        public void VerboseFormat(string format, params object[] args)
        {
            if (IsVerboseEnabled)
            {
                Logger.Log(ThisDeclaringType, m_levelVerbose, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <c>VERBOSE</c> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <param name="arg1">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <see cref="M:String.Format(IFormatProvider, string, object[])"/> method. See
        /// <c>String.Format</c> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// The string is formatted using the <see cref="CultureInfo.InvariantCulture"/>
        /// format provider. To specify a localized provider use the
        /// <see cref="M:VerboseFormat(IFormatProvider,string,object[])"/> method.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="M:Verbose(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        public void VerboseFormat(string format, object arg0, object arg1)
        {
            if (IsVerboseEnabled)
            {
                Logger.Log(ThisDeclaringType, m_levelVerbose, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1 }), null);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <c>VERBOSE</c> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <param name="arg1">An Object to format</param>
        /// <param name="arg2">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <see cref="M:String.Format(IFormatProvider, string, object[])"/> method. See
        /// <c>String.Format</c> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// The string is formatted using the <see cref="CultureInfo.InvariantCulture"/>
        /// format provider. To specify a localized provider use the
        /// <see cref="M:VerboseFormat(IFormatProvider,string,object[])"/> method.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="M:Verbose(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        public void VerboseFormat(string format, object arg0, object arg1, object arg2)
        {
            if (IsVerboseEnabled)
            {
                Logger.Log(ThisDeclaringType, m_levelVerbose, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1, arg2 }), null);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <c>VERBOSE</c> level.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <see cref="M:String.Format(IFormatProvider, string, object[])"/> method. See
        /// <c>String.Format</c> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="M:Verbose(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        public void VerboseFormat(IFormatProvider provider, string format, params object[] args)
        {
            if (IsVerboseEnabled)
            {
                Logger.Log(ThisDeclaringType, m_levelVerbose, new SystemStringFormat(provider, format, args), null);
            }
        }
    }
}