namespace AIMIE.Logging
{
    using System;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Text;

    public class LibLogTraceListener : TraceListener
    {
        public LibLogTraceListener() : this(string.Empty)
        {
        }

        public LibLogTraceListener(string initializeData) : this(GetPropertiesFromInitString(initializeData))
        {
        }

        public LibLogTraceListener(NameValueCollection properties) : base()
        {
            if (properties == null)
            {
                properties = new NameValueCollection();
            }

            this.ApplyProperties(properties);
        }

        public TraceEventType DefaultTraceEventType { get; set; }

        public string LoggerNameFormat { get; set; }
        
        /// <summary>
        /// Writes message to logger provided by <see cref="LogManager.GetLogger(string)"/>.
        /// </summary>
        public override void Write(object o)
        {
            if ((this.Filter == null) || this.Filter.ShouldTrace(null, this.Name, this.DefaultTraceEventType, 0, null, null, o, null))
            {
                this.Log(this.DefaultTraceEventType, null, 0, "{0}", o);
            }
        }

        /// <summary>
        /// Writes message to logger provided by <see cref="LogManager.GetLogger(string)"/>.
        /// </summary>
        public override void Write(object o, string category)
        {
            if ((this.Filter == null) || this.Filter.ShouldTrace(null, this.Name, this.DefaultTraceEventType, 0, null, null, o, null))
            {
                this.Log(this.DefaultTraceEventType, category, 0, "{0}", o);
            }
        }

        /// <summary>
        /// Writes message to logger provided by <see cref="LogManager.GetLogger(string)"/>.
        /// </summary>
        public override void Write(string message)
        {
            if ((this.Filter == null) || this.Filter.ShouldTrace(null, this.Name, this.DefaultTraceEventType, 0, null, null, null, null))
            {
                this.Log(this.DefaultTraceEventType, null, 0, message);
            }
        }

        /// <summary>
        /// Writes message to logger provided by <see cref="LogManager.GetLogger(string)"/>.
        /// </summary>
        public override void Write(string message, string category)
        {
            if ((this.Filter == null) || this.Filter.ShouldTrace(null, this.Name, this.DefaultTraceEventType, 0, null, null, null, null))
            {
                this.Log(this.DefaultTraceEventType, category, 0, message);
            }
        }

        /// <summary>
        /// Writes message to logger provided by <see cref="LogManager.GetLogger(string)"/>.
        /// </summary>
        public override void WriteLine(object o)
        {
            if ((this.Filter == null) || this.Filter.ShouldTrace(null, this.Name, this.DefaultTraceEventType, 0, null, null, o, null))
            {
                this.Log(this.DefaultTraceEventType, null, 0, "{0}", o);
            }
        }

        /// <summary>
        /// Writes message to logger provided by <see cref="LogManager.GetLogger(string)"/>.
        /// </summary>
        public override void WriteLine(object o, string category)
        {
            if ((this.Filter == null) || this.Filter.ShouldTrace(null, this.Name, this.DefaultTraceEventType, 0, null, null, o, null))
            {
                this.Log(this.DefaultTraceEventType, category, 0, "{0}", o);
            }
        }

        /// <summary>
        /// Writes message to logger provided by <see cref="LogManager.GetLogger(string)"/>.
        /// </summary>
        public override void WriteLine(string message)
        {
            if ((this.Filter == null) || this.Filter.ShouldTrace(null, this.Name, this.DefaultTraceEventType, 0, null, null, null, null))
            {
                this.Log(this.DefaultTraceEventType, null, 0, message);
            }
        }

        /// <summary>
        /// Writes message to logger provided by <see cref="LogManager.GetLogger(string)"/>
        /// </summary>
        public override void WriteLine(string message, string category)
        {
            if ((this.Filter == null) || this.Filter.ShouldTrace(null, this.Name, this.DefaultTraceEventType, 0, null, null, null, null))
            {
                this.Log(this.DefaultTraceEventType, category, 0, message);
            }
        }

        /// <summary>
        /// Writes message to logger provided by <see cref="LogManager.GetLogger(string)"/>
        /// </summary>
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
        {
            if ((this.Filter == null) || this.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, null))
            {
                this.Log(eventType, source, id, "Event Id {0}", id);
            }
        }

        /// <summary>
        /// Writes message to logger provided by <see cref="LogManager.GetLogger(string)"/>
        /// </summary>
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            if ((this.Filter == null) || this.Filter.ShouldTrace(eventCache, source, eventType, id, message, null, null, null))
            {
                this.Log(eventType, source, id, message);
            }
        }

        /// <summary>
        /// Writes message to logger provided by <see cref="LogManager.GetLogger(string)"/>
        /// </summary>
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message, params object[] args)
        {
            if ((this.Filter == null) || this.Filter.ShouldTrace(eventCache, source, eventType, id, message, args, null, null))
            {
                this.Log(eventType, source, id, message, args);
            }
        }

        /// <summary>
        /// Writes message to logger provided by <see cref="LogManager.GetLogger(string)"/>
        /// </summary>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        {
            if ((this.Filter == null) || this.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, data))
            {
                string fmt = this.GetFormat((object[])data);
                this.Log(eventType, source, id, fmt, data);
            }
        }

        /// <summary>
        /// Writes message to logger provided by <see cref="LogManager.GetLogger(string)"/>
        /// </summary>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if ((this.Filter == null) || this.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
            {
                string fmt = this.GetFormat((object)data);
                this.Log(eventType, source, id, fmt, data);
            }
        }

        protected virtual void Log(TraceEventType eventType, string source, int id, string format, params object[] args)
        {
            source = this.LoggerNameFormat.Replace("{listenerName}", this.Name).Replace("{sourceName}", string.Empty + source);
            ILog log = LogProvider.GetLogger(source);
            LogLevel logLevel = this.MapLogLevel(eventType);

            switch (logLevel)
            {
                case LogLevel.Trace:
                    log.TraceFormat(format, args);
                    break;
                case LogLevel.Debug:
                    log.DebugFormat(format, args);
                    break;
                case LogLevel.Info:
                    log.InfoFormat(format, args);
                    break;
                case LogLevel.Warn:
                    log.WarnFormat(format, args);
                    break;
                case LogLevel.Error:
                    log.ErrorFormat(format, args);
                    break;
                case LogLevel.Fatal:
                    log.FatalFormat(format, args);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("eventType", eventType, "invalid TraceEventType value");
            }
        }

        private static NameValueCollection GetPropertiesFromInitString(string initializeData)
        {
            NameValueCollection props = new NameValueCollection();

            if (initializeData == null)
            {
                return props;
            }

            string[] parts = initializeData.Split(';');
            foreach (string s in parts)
            {
                string part = s.Trim();
                if (part.Length == 0)
                {
                    continue;
                }

                int indexEquals = part.IndexOf('=');
                if (indexEquals > -1)
                {
                    string name = part.Substring(0, indexEquals).Trim();
                    string value = (indexEquals < part.Length - 1) ? part.Substring(indexEquals + 1) : string.Empty;
                    props[name] = value.Trim();
                }
                else
                {
                    props[part.Trim()] = null;
                }
            }

            return props;
        }

        private void ApplyProperties(NameValueCollection props)
        {
            if (props["defaultTraceEventType"] != null)
            {
                this.DefaultTraceEventType = (TraceEventType)Enum.Parse(typeof(TraceEventType), props["defaultTraceEventType"], true);
            }
            else
            {
                this.DefaultTraceEventType = TraceEventType.Verbose;
            }

            if (props["name"] != null)
            {
                this.Name = props["name"];
            }
            else
            {
                this.Name = "Diagnostics";
            }

            if (props["loggerNameFormat"] != null)
            {
                this.LoggerNameFormat = props["loggerNameFormat"];
            }
            else
            {
                this.LoggerNameFormat = "{listenerName}.{sourceName}";
            }
        }

        private string GetFormat(params object[] data)
        {
            if (data == null || data.Length == 0)
            {
                return null;
            }

            StringBuilder fmt = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                fmt.Append('{').Append(i).Append('}');
                if (i < data.Length - 1)
                {
                    fmt.Append(',');
                }
            }

            return fmt.ToString();
        }

        private LogLevel MapLogLevel(TraceEventType eventType)
        {
            switch (eventType)
            {
                case TraceEventType.Start:
                case TraceEventType.Stop:
                case TraceEventType.Suspend:
                case TraceEventType.Resume:
                case TraceEventType.Transfer:
                    return LogLevel.Trace;
                case TraceEventType.Verbose:
                    return LogLevel.Debug;
                case TraceEventType.Information:
                    return LogLevel.Info;
                case TraceEventType.Warning:
                    return LogLevel.Warn;
                case TraceEventType.Error:
                    return LogLevel.Error;
                case TraceEventType.Critical:
                    return LogLevel.Fatal;
                default:
                    return LogLevel.Trace;
            }
        }
    }
}
