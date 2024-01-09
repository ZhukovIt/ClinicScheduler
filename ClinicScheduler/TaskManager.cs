using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClinicScheduler
{
    public class TaskManager
    {
        public static string GetTaskData(string taskName)
        {
            string res = "";
            try
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();

                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C SCHTASKS /Query /XML /TN SiMed_Задача_" + taskName;
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.CreateNoWindow = true;
                startInfo.StandardOutputEncoding = System.Text.Encoding.GetEncoding(866);

                process.StartInfo = startInfo;
                process.Start();
                res = process.StandardOutput.ReadToEnd();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Невозможно получить данные задачи " + taskName +Environment.NewLine+ "Ошибка: "+ex.Message);
            }

            return res;
        }
        public static bool DeleteTask(string taskName)
        {
            try
            {
                string command = "/C SCHTASKS /Delete /TN \"SiMed_Задача_" + taskName + "\" /F";

                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = command;
                p.StartInfo.Verb = "runas";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.Start();

                p.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Невозможно удалить задачу" + taskName + Environment.NewLine + "Ошибка: " + ex.Message);
                return false;
            }
            return true;
        }

        public static bool  AddTask(string taskName, string command)
        {
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = command;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.WaitForExit();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Невозможно добавить задачу" + taskName + Environment.NewLine + "Ошибка: " + ex.Message);
                return false;
            }
        }

    }
}
