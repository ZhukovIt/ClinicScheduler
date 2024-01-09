using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ClinicScheduler
{
    public static class ZipArchiveHelper
    {
        public static void GenerateZipArchive(string _FolderSource, string _FileFullNameResult, string _Password)
        {
            if (File.Exists(_FileFullNameResult))
                File.Delete(_FileFullNameResult);

            using (ZipFile zip = new ZipFile())
            {
                zip.UseUnicodeAsNecessary = true;
                zip.ProvisionalAlternateEncoding = Encoding.GetEncoding("cp866");
                //zip.AlternateEncoding = Encoding.GetEncoding("cp866");

                if (!String.IsNullOrEmpty(_Password))
                {
                    zip.Encryption = EncryptionAlgorithm.PkzipWeak;
                    zip.Password = _Password;
                }
                zip.AddDirectory(_FolderSource, null);
                zip.Save(_FileFullNameResult);
            }
        }
    }
}
