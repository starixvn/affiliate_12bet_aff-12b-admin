using Fanex.Data;
using Fanex.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Fanex.Affiliate.DataService.Importer.Core.Utilities
{
    public static class LogHelper
    {
        private const int LimitedLevelOfInnerException = 5;

        public static void Error(string msg)
        {
            LogService.Error(msg);
        }

        public static void Error(string msg, string category)
        {
            LogService.Error(msg, category);
        }

        public static void Error(string msg, Exception exception)
        {
            Error(exception, msg);
        }

        public static void Error(string msg, string category, Exception exception)
        {
            Error(exception, msg, category);
        }

        public static void Error(Exception exception, string msg = null, string category = null)
        {
            var content = new StringBuilder().AppendLine($"Error Message: {msg ?? exception?.Message}");

            AddDatabaseInfo(content, exception);
            AddExceptionInfo(content, exception, LimitedLevelOfInnerException);

            LogService.Error(content.ToString(), category, exception);
        }

        public static void Info(string msg)
        {
            LogService.Info(msg);
        }

        public static void Info(string msg, string category)
        {
            LogService.Info(msg, category);
        }

        public static void Info(string format, params object[] args)
        {
            LogService.Info(format, args);
        }

        public static void Info(string format, string category, params object[] args)
        {
            LogService.Info(format, category, args);
        }

        public static void Warn(string format, params object[] args)
        {
            LogService.Warn(format, args);
        }
        public static void Debug(string format, params object[] args)
        {
            LogService.Debug(format, args);
        }

        public static void Critical(string format, params object[] args)
        {
            LogService.Critical(format, args);
        }

        private static void AddDatabaseInfo(StringBuilder builder, Exception exception)
        {
            builder.AppendLine();
            builder.AppendLine("DATABASE INFO");

            var dbInfo = GetDbExceptionInfo(exception);

            foreach (var item in dbInfo)
            {
                builder.AppendLine($"{item.Key}: {item.Value}");
            }
        }

        private static IDictionary<string, object> GetDbExceptionInfo(Exception exception)
        {
            var dbInfo = new Dictionary<string, object>();
            BindSqlExceptionInfo(exception, dbInfo);
            BindFanexDataExceptionInfo(exception, dbInfo);
            return dbInfo;
        }

        private static void BindFanexDataExceptionInfo(Exception exception, IDictionary<string, object> dbInfo)
        {
            if (exception is ObjectDbException fanexDataException)
            {
                BindSqlExceptionInfo(exception.InnerException, dbInfo);

                var connString = new SqlConnectionStringBuilder(fanexDataException.ConnectionString);

                dbInfo["Server"] = connString.DataSource;
                dbInfo["DbName"] = fanexDataException.DbName;
                dbInfo["SpName"] = fanexDataException.SpName;
                dbInfo["UserId"] = connString.UserID;
                dbInfo["Parameters"] = GetParameters(fanexDataException.Parameters);
                dbInfo["CommandTimeout"] = fanexDataException.CommandTimeout;
            }
        }

        private static void BindSqlExceptionInfo(Exception exception, IDictionary<string, object> dbInfo)
        {
            if (exception is SqlException sqlException)
            {
                dbInfo["Server"] = sqlException.Server;
                dbInfo["Procedure"] = sqlException.Procedure;
                dbInfo["Line"] = sqlException.LineNumber;
            }
        }

        private static string GetParameters(IDictionary<string, object> parameters)
            => parameters?.Count > 0
               ? parameters.Select(parameter => $"{parameter.Key}={parameter.Value}")
                        .Aggregate((firstPair, secondPair) => $"{firstPair};{secondPair}")
               : string.Empty;

        private static void AddExceptionInfo(StringBuilder builder, Exception exception, int limitedInnerException)
        {
            builder.AppendLine();
            builder.AppendLine($"EXCEPTION INFO LEVEL: {LimitedLevelOfInnerException - limitedInnerException}");
            builder.AppendLine($"Message: {exception.Message}");
            builder.AppendLine($"Source: {exception.Source}");
            builder.AppendLine($"TargetSite: {exception.TargetSite}");
            builder.AppendLine($"StackTrace: {exception.StackTrace}");

            if (exception.InnerException != null && limitedInnerException > 0)
            {
                AddExceptionInfo(builder, exception.InnerException, --limitedInnerException);
            }

            if(exception is SocketException)
            {
                var socketEx = exception as SocketException;
                builder.AppendLine();
                builder.AppendLine($"SOCKET EXCEPTION INFO");
                builder.AppendLine($"Sockets Error Codes: {socketEx.SocketErrorCode}");
                builder.AppendLine($"Native Error Codes: {socketEx.NativeErrorCode}");
                builder.AppendLine($"Error Codes: {socketEx.ErrorCode}");
            }
        }
    }
}