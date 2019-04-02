using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdCms.Common.Net
{

    /// <summary>
    /// 
    /// </summary>
    public static class FileHelper
    {

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="file">文件全路径 如：E:\Upload\Zip\300001\20181025\123.jpg</param>
        /// <returns></returns>
        public static bool IsFile(string file)
        {
            try
            {
                string root = string.Empty;
                if (file.IndexOf(":") < 0)
                    root = AppDomain.CurrentDomain.BaseDirectory;
                if (File.Exists(string.Format("{0}{1}", root, file)))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void DeleteFile(string file)
        {
            try
            {
                if (IsFile(file))
                    File.Delete(file);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void Move(string sourceFileName,string destFileName)
        {
            try
            {
                File.Move(sourceFileName, destFileName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void Copy(string sourceFileName, string destFileName)
        {
            try
            {
                File.Copy(sourceFileName, destFileName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sourceFileName">来源文件</param>
        /// <param name="destFileName">目标文件</param>
        /// <param name="overwrite">覆盖已存在的同名文件 true/false</param>
        public static void Copy(string sourceFileName, string destFileName, bool overwrite)
        {
            try
            {
                File.Copy(sourceFileName, destFileName, overwrite);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 文件转流
        /// </summary>
        /// <param name="fileName">文件名 全路径</param>
        /// <returns></returns>
        public static Stream FileToStream(string fileName)
        {
            try
            {
                // 打开文件 
                FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                // 读取文件的 byte[] 
                byte[] bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, bytes.Length);
                fileStream.Close();
                // 把 byte[] 转换成 Stream 
                Stream stream = new MemoryStream(bytes);
                return stream;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static byte[] StreamToBytes(Stream fileStream)
        {
            try
            {
                Stream stream = fileStream;
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                //设置当前流的位置为流的开始
                stream.Seek(0, SeekOrigin.Begin);
                //将bytes转换为流
                return bytes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="fileName">文件名 全路径</param>
        /// <returns></returns>
        public static string Open(string fileName)
        {
            try
            {
                using (FileStream fsRead = new FileStream(fileName, FileMode.Open))
                {
                    int fsLen = (int)fsRead.Length;
                    byte[] heByte = new byte[fsLen];
                    int r = fsRead.Read(heByte, 0, heByte.Length);
                    string myStr = System.Text.Encoding.UTF8.GetString(heByte);
                    return myStr;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static bool Write(string fileName, Stream fileStream)
        {
            try
            {
                using (FileStream targetStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    int bufferLen = 1024 * 1024;
                    byte[] buffer = new byte[bufferLen];
                    int count = 0;
                    while ((count = fileStream.Read(buffer, 0, bufferLen)) > 0)
                    {
                        targetStream.Write(buffer, 0, count);
                    }
                    targetStream.Flush();
                    targetStream.Close();
                    fileStream.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}
