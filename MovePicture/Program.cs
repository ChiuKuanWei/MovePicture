using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovePicture
{
    internal class Program
    {
        public static List<FileInfo> fileList = new List<FileInfo>();
        public static List<DirectoryInfo> dirList = new List<DirectoryInfo>();
        static void Main(string[] args)
        {
            Boolean bMove = false;  //是否搬移資料夾
            string sTargetPath = @"C:\C# Project\MovePicture\BackUp\" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            string[] sPath = { "C:", "D:", "E:", "F:", "G:", "H:", "I:", "J:", "K:", "L:", "M:", "N:", "O:", "P:", "Q:", "R:", "S:", "T:", "U:", "V:", "W:", "X:", "Y:", "Z:" };
            
            for (int j = 0; j < sPath.Length; j++)
            {
                if (Directory.Exists(sPath[j] + "\\2000W"))
                {
                    string sDirectory = sPath[j] + "\\2000W";
                    foreach (var di_file in Directory.GetDirectories(sDirectory))
                    {
                        if (Directory.GetFiles(di_file).Length != 0)
                        {
                            if (!bMove)
                            {
                                CopyDireToDire(sPath[j] + "\\2000W", sTargetPath);
                                bMove = true;
                            }

                            //刪除檔案
                            DirectoryInfo file = new DirectoryInfo(di_file);
                            FileInfo[] files = file.GetFiles();
                            foreach (FileInfo fi in files)
                            {
                                fi.Delete();
                            }
                        }
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// 複製資料夾內所有檔案到另外一個資料夾
        /// </summary>
        /// <param name="sourceDir">來源目錄</param>
        /// <param name="destDir">目的地目錄</param>
        private static void CopyDireToDire(string sourceDir, string destDir)
        {
            DirectoryInfo sourceDireInfo = new DirectoryInfo(sourceDir);
            //List<FileInfo> fileList = new List<FileInfo>();
            GetFileList(sourceDireInfo);
            //List<DirectoryInfo> dirList = new List<DirectoryInfo>();
            GetDirList(sourceDireInfo);
            foreach (DirectoryInfo dir in dirList)
            {
                string m = dir.FullName;
                string n = m.Replace(sourceDir, destDir);
                if (!Directory.Exists(n))
                {
                    Directory.CreateDirectory(n);
                }
            }
            foreach (FileInfo fileInfo in fileList)
            {
                string m = fileInfo.FullName;
                string n = m.Replace(sourceDir, destDir);
                File.Copy(m, n, true);
            }
        }
        private static void GetFileList(DirectoryInfo dir)
        {
            fileList.AddRange(dir.GetFiles());
            foreach (DirectoryInfo directory in dir.GetDirectories()) 
            {
                GetFileList(directory);
            } 
        }
        private static void GetDirList(DirectoryInfo dir)
        {
            dirList.AddRange(dir.GetDirectories());
            foreach (DirectoryInfo directory in dir.GetDirectories()) 
            {
                GetDirList(directory);
            } 
        }
    }
}
