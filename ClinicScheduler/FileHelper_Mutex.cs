using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ClinicScheduler
{
    public static class FileHelper_Mutex
    {
        public static bool GetFileContent(string _FileFullName, string _MutexId, bool _ErrorIfNotExists, out string _FileContent, out string _ErrorMessage)
        {
            _FileContent = null;
            _ErrorMessage = null;
            try
            {
                using (var mutex = new Mutex(false, _MutexId))
                {
                    bool mutex_wait_result = false;
                    try
                    {
                        mutex_wait_result = mutex.WaitOne(TimeSpan.FromSeconds(5));
                        if (!mutex_wait_result)
                            throw new Exception("Метод FileHelper_Mutex.GetFileContent. Не удалось получить Mutex для доступа к файлу настроек в течение 5 секунд");

                        if (!File.Exists(_FileFullName))
                        {
                            if (_ErrorIfNotExists)
                                throw new Exception($"Метод FileHelper_Mutex.GetFileContent. Файл '{ _FileFullName }' не существует");

                            return true;
                        }

                        _FileContent = File.ReadAllText(_FileFullName);
                    }
                    finally
                    {
                        if (mutex_wait_result)
                            mutex.ReleaseMutex();
                    }
                }

                return true;
            }
            catch (Exception exc)
            {
                _ErrorMessage = exc.Message;
                return false;
            }
        }

        public static bool MoveFile(string _SourceFileName, string _DestinationFileName, string _MutexId, out string _ErrorMessage)
        {
            _ErrorMessage = null;
            try
            {
                using (var mutex = new Mutex(false, _MutexId))
                {
                    bool mutex_wait_result = false;
                    try
                    {
                        mutex_wait_result = mutex.WaitOne(TimeSpan.FromSeconds(5));
                        if (!mutex_wait_result)
                            throw new Exception("Метод FileHelper_Mutex.GetFileContent. Не удалось получить Mutex для доступа к файлу настроек в течение 5 секунд");

                        File.Move(_SourceFileName, _DestinationFileName);
                    }
                    finally
                    {
                        if (mutex_wait_result)
                            mutex.ReleaseMutex();
                    }
                }

                return true;
            }
            catch (Exception exc)
            {
                _ErrorMessage = exc.Message;
                return false;
            }
        }
    }
}
