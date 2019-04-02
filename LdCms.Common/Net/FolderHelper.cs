using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdCms.Common.Net
{
    public static class FolderHelper
    {
        public static bool IsFolder(string folder)
        {
            try
            {
                string root = AppDomain.CurrentDomain.BaseDirectory;
                if (Directory.Exists(string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, folder)))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 根据传入路径创建文件夹
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public static string CreateFolder(string folderName)
        {
            try
            {
                string strFolderName = string.Empty;
                string rootFolder = AppDomain.CurrentDomain.BaseDirectory;
                string[] arrFolderName = folderName.Split('/');
                for (var i = 0; i < arrFolderName.Length; i++)
                {
                    string name = arrFolderName[i];
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        if (i == 0)
                            strFolderName = name;
                        else
                            strFolderName = string.Format("{0}/{1}", strFolderName, name);
                        string folderPath = string.Format("{0}{1}", rootFolder, strFolderName);
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            System.IO.Directory.CreateDirectory(folderPath);
                        }
                    }
                }
                return folderName;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }





    }
}
