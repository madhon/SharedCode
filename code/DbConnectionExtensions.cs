using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace System
{
    [DebuggerStepThrough]
    public static class DbConnectionExtensions
    {
        public static bool StateIsWithin(this IDbConnection connection, params ConnectionState[] states)
        {
            return (connection != null &&
                    (states != null && states.Length > 0) &&
                    (states.Any(x => (connection.State & x) == x)));
        }

        public static bool IsInState(this IDbConnection connection, ConnectionState state)
        {
            return (connection != null &&
                    (connection.State & state) == state);
        }

        public static void OpenIfNot(this IDbConnection connection)
        {
            if (!connection.IsInState(ConnectionState.Open))
                connection.Open();
        }

        public static void CloseIfOpen(this IDbConnection connection)
        {
            connection.SafeClose(false);
        }

        public static void CloseIfOpenAndDispose(this IDbConnection connection)
        {
            connection.SafeClose(true);
        }

        public static void SafeClose(this IDbConnection toClose, bool dispose)
        {
            if (toClose == null)
            {
                return;
            }
            if (toClose.State != ConnectionState.Closed)
            {
                toClose.Close();
            }
            if (dispose)
            {
                toClose.Dispose();
            }
        }

        public static int ExecuteNonQuery(this IDbConnection connection, string text, params object[] parameters)
        {
            using (var command = connection.CreateCommand())
            {
                PrepareCommand(command, text, parameters);
                return command.ExecuteNonQuery();
            }
        }

        public static T ExecuteScalar<T>(this IDbConnection connection, string text, params object[] parameters)
        {
            using (var command = connection.CreateCommand())
            {
                PrepareCommand(command, text, parameters);
                return (T)Convert.ChangeType(command.ExecuteScalar(), typeof(T));
            }
        }

        public static IDataReader ExecuteReader(this IDbConnection connection, string text, params object[] parameters)
        {
            using (var command = connection.CreateCommand())
            {
                PrepareCommand(command, text, parameters);
                return command.ExecuteReader();
            }
        }

        private static void PrepareCommand(IDbCommand command, string text, object[] parameters)
        {
            if (parameters.Any())
            {
                var parameterNames = new List<string>();
                for (int i = 0; i < parameters.Length; i++)
                {
                    parameterNames.Add("@p" + i);

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@p" + i;
                    parameter.Value = parameters[i];
                    command.Parameters.Add(parameter);
                }
                text = string.Format(text, parameterNames.ToArray());
            }
            command.CommandText = text;
        }


    }
}
