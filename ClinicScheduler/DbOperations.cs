using System;
using System.Data;
using System.Data.SqlClient;

namespace SiMed.Clinic
{
    //---------------------------------------------
    public static class DbHelpers
    {
        //---------------------------------------------
        public static void ProcessAction(Action action)
        {
            try
            {
                if (action != null)
                    action();
            }
            catch (System.Data.Common.DbException exeption)
            {
                throw DatabaseInteractionException.Create(exeption);
            }
        }
        //---------------------------------------------
        public static void ProcessAction<TArg>(Action<TArg> action, TArg arg)
        {
            try
            {
                if (action != null)
                    action(arg);
            }
            catch (System.Data.Common.DbException execption)
            {
                throw DatabaseInteractionException.Create(execption);
            }
        }
        //---------------------------------------------
        public static TResult ProcessFunction<TResult>(Func<TResult> action)
        {
            try
            {
                if (action != null)
                    return action();
                else
                    throw new ArgumentNullException("action");
            }
            catch (System.Data.Common.DbException execption)
            {
                throw DatabaseInteractionException.Create(execption);
            }
        }
        //---------------------------------------------
        public static TResult ProcessFunction<TArg, TResult>(Func<TArg, TResult> action, TArg arg)
        {
            try
            {
                if (action != null)
                    return action(arg);
                else
                    throw new ArgumentNullException("action");
            }
            catch (System.Data.Common.DbException execption)
            {
                throw DatabaseInteractionException.Create(execption);
            }
        }
        //---------------------------------------------
        public static TResult ProcessFunction<TArg1, TArg2, TResult>(Func<TArg1, TArg2, TResult> action, TArg1 arg1, TArg2 arg2)
        {
            try
            {
                if (action != null)
                    return action(arg1, arg2);
                else
                    throw new ArgumentNullException("action");
            }
            catch (System.Data.Common.DbException execption)
            {
                throw DatabaseInteractionException.Create(execption);
            }
        }
        //---------------------------------------------
        public static TResult ProcessFunction<TArg1, TArg2, TArg3, TResult>(Func<TArg1, TArg2, TArg3, TResult> action, TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            try
            {
                if (action != null)
                    return action(arg1, arg2, arg3);
                else
                    throw new ArgumentNullException("action");
            }
            catch (System.Data.Common.DbException execption)
            {
                throw DatabaseInteractionException.Create(execption);
            }
        }
        //---------------------------------------------
    }
}