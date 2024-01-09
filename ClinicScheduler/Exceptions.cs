using System;
using System.Runtime.Serialization;
using System.Data.SqlClient;
using System.Data.Common;
using System.Text;

namespace SiMed.Clinic
{
    [Serializable]
    public class InvalidActivationKeyException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public InvalidActivationKeyException()
        {
        }

        public InvalidActivationKeyException(string message) : base(message)
        {
        }

        public InvalidActivationKeyException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidActivationKeyException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class ValidateDataException : Exception
    {
        public ValidateDataException() { }
        public ValidateDataException(string message) : base(message) { }
        public ValidateDataException(string message, Exception inner) : base(message, inner) { }
        protected ValidateDataException(
          SerializationInfo info,
          StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class DatabaseInteractionException : Exception
    {
        public static DatabaseInteractionException Create(DbException ex)
        {
            var exception = ex as SqlException;
            return exception != null 
                ? new SqlDatabaseInteractionException(exception) 
                : new DatabaseInteractionException(ex);
        }

        public DatabaseInteractionException()
        { }

        protected DatabaseInteractionException(DbException ex)
            : this("", ex) { }
        public DatabaseInteractionException(string message) : base(message) { }
        public DatabaseInteractionException(string message, Exception inner) 
            : base(message, inner) { }
        protected DatabaseInteractionException(
          SerializationInfo info,
          StreamingContext context)
            : base(info, context) { }

        public bool IsAuthenticatedError { get; protected set; }
        public bool IsConnectionError { get; protected set; }
        public bool IsConstraintError { get; protected set; }
    }

    [Serializable]
    public class SqlDatabaseInteractionException : DatabaseInteractionException
    {
        private string msg;

        public SqlDatabaseInteractionException()
        {
            msg = String.Empty;
        }

        public SqlDatabaseInteractionException(SqlException ex)
            :base(ex)
        {
            ProcessException(ex);
        }
        public SqlDatabaseInteractionException(string message) : base(message) { }
        public SqlDatabaseInteractionException(string message, Exception inner) 
            : base(message, inner) { }
        protected SqlDatabaseInteractionException(
          SerializationInfo info,
          StreamingContext context)
            : base(info, context) { }

        public override string Message
        {
            get
            {
                return msg;
            }
        }

        private string ProcessError(int number, string message)
        {
            string tmsg;

            switch (number)
            {
                case 17142: //SQL Server paused
                    tmsg = ("SQL Server отказывается принимать входящие соединения");
                    IsConnectionError = true;

                    break;
                case 18456: //Login failed
                    tmsg = ("Не удалось войти с указанной учетной записью");
                    IsConnectionError = true;
                    IsAuthenticatedError = true;
                    break;
                case -1:
                    tmsg = ("Не удалось подключиться к SQL Server" + Environment.NewLine
                        + message);
                    IsConnectionError = true;
                    break;
                case 2627:
                        tmsg = (message);
                    IsConstraintError = true;
                    break;
                case 3621:
                    tmsg = "Выполнение операции прервано." + Environment.NewLine + message;
                    break;
                case 547:
                    IsConstraintError = true;
                    tmsg = "Нарушено ограничение целостности БД." + Environment.NewLine + message;
                    break;
                default:
                    tmsg = "Выполнение операции прервано." + Environment.NewLine + message;
                    break;
            }

            return tmsg;
        }

        private void ProcessException(SqlException exception)
        {
            var messageBuilder = new StringBuilder();

            foreach (SqlError error in exception.Errors)
            {
                int number = error.Number;
                string tmsg = error.Message;

                if (error.Number == 50000)
                {
                    try
                    {
                        var tokens = error.Message.Split(new[] { ' ' }, 2);
                        number = Convert.ToInt32(tokens[0]);
                        tmsg = tokens[1];
                    }
                    catch (Exception)
                    {
                        
                    }
                }

                messageBuilder.AppendLine(ProcessError(number, tmsg));
            }

            msg = messageBuilder.ToString();
        }
    }

    [Serializable]
    public class LoadDataException : Exception
    {
        public LoadDataException() { }
        public LoadDataException(string message) : base(message) { }
        public LoadDataException(string message, Exception inner) : base(message, inner) { }
        protected LoadDataException(
          SerializationInfo info,
          StreamingContext context)
            : base(info, context) { }
    }
}