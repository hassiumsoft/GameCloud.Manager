﻿using System.ComponentModel.Composition;
using NLog;

namespace GameCloud.UCenter.Web.Common.Logger
{
    /// <summary>
    /// Provide a log adapter for NLog.
    /// </summary>
    [Export("Trace.NLog", typeof(ILoggerAdapter))]
    public class NLoggerAdapter : ILoggerAdapter
    {
        private readonly NLog.Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Trace error message.
        /// </summary>
        /// <param name="message">Indicating the message.</param>
        public void TraceError(string message)
        {
            this.logger.Error(message);
        }

        /// <summary>
        /// Trace information message.
        /// </summary>
        /// <param name="message">Indicating the message.</param>
        public void TraceInformation(string message)
        {
            this.logger.Info(message);
        }

        /// <summary>
        /// Trace warning message.
        /// </summary>
        /// <param name="message">Indicating the message.</param>
        public void TraceWarning(string message)
        {
            this.logger.Warn(message);
        }
    }
}
