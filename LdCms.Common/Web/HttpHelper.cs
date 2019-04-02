using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace LdCms.Common.Web
{
    using Newtonsoft.Json;
    /// <summary>
    /// HTTP操作类
    /// </summary>
    public class HttpHelper
    {
        /*
         * http 操作类
         * 目前主要功能如下：
         * 
         * 1、获取传入数据流
         * 2、GET、POST请求获取数据流
         * 3、POST请求传送数据
         * 4、POST上传文件服务器
         * 5、GET下载远程文件
         * 
         * 使用方法：Common.HttpHelper.GetToUrl(url,codeType)
         * 
         * 
         */


        /// <summary>
        /// 创建HttpWeb操作类
        /// </summary>
        private static HttpWebResponseOperationUtility HttpUtility = new HttpWebResponseOperationUtility();
        /// <summary>
        /// 请收传入流
        /// </summary>
        /// <param name="codeType">流编码</param>
        /// <returns></returns>
        public static string GetInputStream(string codeType = "utf-8")
        {
            return HttpUtility.RequestInputStream(codeType);
        }
        /// <summary>
        /// GET获取网址数据
        /// </summary>
        /// <param name="url">请求网址</param>
        /// <param name="codeType">编码 默认UTF-8</param>
        /// <returns></returns>
        public static string GetToUrl(string url, string codeType = "utf-8")
        {
            return HttpUtility.GetToUrl(url, codeType);
        }
        /// <summary>
        /// POST获取网址数据
        /// </summary>
        /// <param name="url">请求网址</param>
        /// <returns></returns>
        public static string PostToUrl(string url)
        {
            return PostToUrl(url, "", HttpRequestContentType.UrlEncoded);
        }
        /// <summary>
        /// POST获取网址数据
        /// </summary>
        /// <param name="url">请求网址</param>
        /// <param name="postData">发送内容</param>
        /// <returns></returns>
        public static string PostToUrl(string url, string postData)
        {
            return PostToUrl(url, postData, HttpRequestContentType.UrlEncoded);
        }
        /// <summary>
        /// POST获取网址数据
        /// </summary>
        /// <param name="url">请求网址</param>
        /// <param name="postData">发送内容</param>
        /// <param name="cert">X509证书文件</param>
        /// <returns></returns>
        public static string PostToUrl(string url, string postData, X509Certificate2 cert)
        {
            try
            {
                System.Net.HttpWebRequest webrequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
                webrequest.ClientCertificates.Add(cert);
                byte[] bs = Encoding.UTF8.GetBytes(postData);
                webrequest.Method = "POST";
                webrequest.ContentType = "application/x-www-form-urlencoded";
                webrequest.ContentLength = bs.Length;
                using (Stream reqStream = webrequest.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                    reqStream.Close();
                }
                System.Net.HttpWebResponse webreponse = (System.Net.HttpWebResponse)webrequest.GetResponse();
                Stream stream = webreponse.GetResponseStream();
                string resp = string.Empty;
                using (StreamReader reader = new StreamReader(stream))
                {
                    resp = reader.ReadToEnd();
                }
                return resp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// POST获取网址数据
        /// </summary>
        /// <param name="url">请求网址</param>
        /// <param name="postData">发送内容</param>
        /// <param name="contentType">Content-Type 类型</param>
        /// <param name="codeType">编码 默认UTF-8</param>
        /// <returns></returns>
        public static string PostToUrl(string url, string postData, HttpRequestContentType contentType, string codeType = "utf-8")
        {
            HttpUtility.ContentType = contentType;
            return HttpUtility.PostToUrl(url, postData, codeType);
        }
        /// <summary>
        /// POST文件、内容指定请求网址
        /// </summary>
        /// <param name="url">请求网址</param>
        /// <param name="filePath">上传的文件 全路径 比如：c:\12.jpg</param>
        /// <param name="postData">POST 内容字符串</param>
        /// <param name="name">文本域的名称 比如：name="file"，那么 file</param>
        /// <param name="codeType">编码</param>
        /// <returns></returns>
        public static string PostFileToUrl(string url, string filePath, string postData = "", string name = "file", string codeType = "utf-8")
        {
            return HttpUtility.PostFileToUrl(url, filePath, postData, name, codeType);
        }
        /// <summary>
        /// 下载文件到本地 返回 JSON
        /// </summary>
        /// <param name="url">下载文件地址</param>
        /// <param name="savePath">文件存放地址，包含文件名 相对路径 /download/</param>
        /// <returns>
        /// 用法：Common.HttpUtility.GetFileToDownload("http://www.ldcms.net/001.png", "/content/147258.png");
        /// </returns>
        public static string GetFileToDownload(string url, string savePath)
        {
            return HttpUtility.GetFileToDownload(url, savePath);
        }
        /// <summary>
        /// 取得HTML中所有图片的 URL
        /// </summary>
        /// <param name="htmlText">html代码内容</param>
        /// <returns></returns>
        public static string[] GetHtmlImageUrlList(string htmlText)
        {
            try
            {
                // 定义正则表达式用来匹配 img 标签            
                System.Text.RegularExpressions.Regex regImg = new System.Text.RegularExpressions.Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
                // 搜索匹配的字符串
                MatchCollection matches = regImg.Matches(htmlText);
                int i = 0;
                string[] urlLists = new string[matches.Count];
                // 取得匹配项列表 
                foreach (Match match in matches)
                {
                    urlLists[i++] = match.Groups["imgUrl"].Value;
                }
                return urlLists;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 取得HTML中所有A链接的 URL
        /// </summary>
        /// <param name="htmlText">html代码内容</param>
        /// <returns></returns>
        public static string[] GetHtmlHrefUrlList(string htmlText)
        {
            try
            {
                Regex reg = new Regex(@"(?is)<a[^>]*?href=(['""\s]?)(?<href>[^'""\s]*)\1[^>]*?>");
                MatchCollection match = reg.Matches(htmlText);
                int i = 0;
                string[] urlLists = new string[match.Count];
                foreach (Match m in match)
                {
                    urlLists[i++] = m.Groups["href"].Value;
                }
                return urlLists;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
    /// <summary>
    /// HttpWebResponse 操作类 新版
    /// </summary>
    public class HttpWebResponseOperationUtility
    {

        /// <summary>
        /// 获取POST过来的数据流
        /// </summary>
        /// <param name="CodeType">页面编码</param>
        /// <returns></returns>
        public string RequestInputStream(string codeType = "UTF-8")
        {
            Stream stream = Extension.HttpContext.Current.Request.Body;
            bool isContentLength = Extension.HttpContext.Current.Request.ContentLength.HasValue;
            if (isContentLength)
            {
                byte[] buffer = new byte[Extension.HttpContext.Current.Request.ContentLength.Value];
                stream.Read(buffer, 0, buffer.Length);
                string content = Encoding.UTF8.GetString(buffer);
                //StreamReader reader = new StreamReader(stream, Encoding.GetEncoding(codeType));
                //return reader.ReadToEnd();
                return content;
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// GET获取网址数据
        /// </summary>
        /// <param name="url">请求网址</param>
        /// <param name="codeType">编码 默认UTF-8</param>
        /// <returns></returns>
        public string GetToUrl(string url, string codeType = "UTF-8")
        {
            Method = HttpRequestMethod.GET;
            ContentType = HttpRequestContentType.Html;
            System.Net.HttpWebResponse hwrs = HttpWebResponse(url);
            //string content_type = hwrs.ContentType.ToLower();
            System.IO.Stream get_stream = hwrs.GetResponseStream();
            return StreamToStr(get_stream, codeType);
        }
        /// <summary>
        /// POST获取网址数据
        /// </summary>
        /// <param name="url">请求网址</param>
        /// <param name="postData">POST数据</param>
        /// <param name="codeType">编码 默认UTF-8</param>
        /// <returns></returns>
        public string PostToUrl(string url, string postData, string codeType = "UTF-8")
        {
            Method = HttpRequestMethod.POST;
            switch (ContentType.ToString().ToUpper())
            {
                case "URLENCODED":
                    ContentType = HttpRequestContentType.UrlEncoded;
                    break;
                case "JSON":
                    ContentType = HttpRequestContentType.Json;
                    break;
                case "DATA":
                    ContentType = HttpRequestContentType.Data;
                    break;
                case "XML":
                    ContentType = HttpRequestContentType.Xml;
                    break;
                case "HTML":
                    ContentType = HttpRequestContentType.Html;
                    break;
                default:
                    ContentType = HttpRequestContentType.UrlEncoded;
                    break;
            }
            System.Net.HttpWebResponse hwrs = HttpWebResponse(url, postData);
            //string content_type = hwrs.ContentType.ToLower();
            System.IO.Stream get_stream = hwrs.GetResponseStream();
            return StreamToStr(get_stream, codeType);
        }
        /// <summary>
        /// POST文件、内容指定请求网址
        /// </summary>
        /// <param name="url">请求网址</param>
        /// <param name="filePath">上传的文件路径  比如：c:\12.jpg</param>
        /// <param name="postData">POST 内容字符串</param>
        /// <param name="name">文本域的名称    比如：name="file"，那么 file</param>
        /// <param name="codeType">编码</param>
        /// <returns></returns>
        public string PostFileToUrl(string url, string filePath, string postData = "", string name = "file", string codeType = "utf-8")
        {
            Method = HttpRequestMethod.POST;
            ContentType = HttpRequestContentType.Data;
            System.Net.HttpWebResponse hwrs = HttpWebResponse(url, postData, name, filePath);
            //string content_type = hwrs.ContentType.ToLower();
            System.IO.Stream get_stream = hwrs.GetResponseStream();
            return StreamToStr(get_stream, codeType);
        }


        /// <summary>
        /// 下载文件到本地 返回 JSON
        /// </summary>
        /// <param name="url">下载文件地址</param>
        /// <param name="savePath">文件存放地址，包含文件名 相对路径 /download/</param>
        /// <returns>
        /// 用法：Common.HttpUtility.GetFileToDownload("http://www.ldcms.net/001.png", "/content/147258.png");
        /// </returns>
        public string GetFileToDownload(string url, string savePath)
        {
            //指定虚拟路径
            string strSavePath = string.Format("{0}/{0}", AppDomain.CurrentDomain.BaseDirectory, savePath);
            if (!IsSavePath(savePath))
                return JsonConvert.SerializeObject(new { errcode = -1, errmsg = "输入保存路径错误！" });
            try
            {
                Method = HttpRequestMethod.GET;
                ContentType = HttpRequestContentType.Html;
                System.Net.HttpWebResponse hwrs = HttpWebResponse(url);
                System.IO.Stream inputStream = hwrs.GetResponseStream();

                string tempFile = CreateTempFilePath(savePath);   //不需要指定虚拟路径 System.Web.HttpContext.Current.Server.MapPath
                FileStream fs = new FileStream(tempFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                byte[] bArr = new byte[1024];
                int size = inputStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    fs.Write(bArr, 0, size);
                    size = inputStream.Read(bArr, 0, (int)bArr.Length);
                }
                fs.Close();
                inputStream.Close();
                //如果存在删除再创建
                if (Net.FileHelper.IsFile(strSavePath))
                    Net.FileHelper.DeleteFile(strSavePath);
                System.IO.File.Move(tempFile, strSavePath);
                return JsonConvert.SerializeObject(new { file_path = savePath });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        #region 辅助方法

        /// <summary>
        /// 设置HttpRequest请求超时时间默认值19600毫秒
        /// </summary>
        private int timeout = 19600;
        /// <summary>
        /// 设置HttpRequest请求超时时间
        /// </summary>
        public int Timeout
        {
            get
            {
                return timeout;
            }
            set
            {
                timeout = value;
            }
        }
        /// <summary>
        /// 设置HttpRequestMethod 请求动作默认值为 GET
        /// </summary>
        private HttpRequestMethod method = HttpRequestMethod.GET;
        /// <summary>
        /// 设置HttpRequestMethod 请求动作 
        /// </summary>
        public HttpRequestMethod Method 
        {
            get 
            { 
                return method;
            }
            set 
            {
                method = value; 
            }
        }
        /// <summary>
        /// 设置HttpRequest Content-Type 请求内容类型默认text/html
        /// </summary>
        private HttpRequestContentType contentType = HttpRequestContentType.Html;
        /// <summary>
        /// 设置HttpRequest Content-Type 请求内容类型
        /// </summary>
        public HttpRequestContentType ContentType
        {
            get
            {
                return contentType;
            }
            set
            {
                contentType = value;
            }
        }
        /// <summary>
        /// 设置HttpRequest请求编码 默认utf-8
        /// </summary>
        private string codeType = "utf-8";
        /// <summary>
        /// 设置HttpRequest请求编码
        /// </summary>
        public string CodeType
        {
            get
            {
                return codeType;
            }
            set
            {
                codeType = value;
            }
        }

        /// <summary>
        /// 检查路径字符串正确性
        /// </summary>
        /// <param name="savePath"></param>
        /// <returns></returns>
        private bool IsSavePath(string savePath)
        {
            bool result = false;
            if (string.IsNullOrEmpty(savePath) && savePath.Length <= 2)
            {
                result = false;
            }
            else
            {
                string lStr = Utility.Utility.Left(savePath, 1);
                string fileName = System.IO.Path.GetFileName(savePath);
                if (lStr == "/" && !string.IsNullOrEmpty(fileName))
                {
                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// 创建HttpWebResponse请求对象
        /// </summary>
        /// <param name="url">请求网址</param>
        /// <param name="method">请求动作</param>
        /// <param name="postData">POST数据</param>
        /// <returns></returns>
        private System.Net.HttpWebResponse HttpWebResponse(string url, string postData = "", string name = "", string filePath = "")
        {
            try
            {
                System.Net.HttpWebRequest hwr = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                hwr.Timeout = Timeout;
                hwr.Method = Method.ToString();

                if (Method.ToString().ToUpper() == "POST")
                {
                    if (ContentType.ToString().ToUpper() == "DATA")
                    {
                        //POST 文件
                        string boundary = DateTime.Now.Ticks.ToString("X");                                              // 随机分隔线
                        byte[] item_boundary_bytes = Encoding.UTF8.GetBytes(string.Format("\r\n--{0}\r\n", boundary));   // 开始boundary
                        byte[] post_header_bytes = Encoding.UTF8.GetBytes(StringBuilderHeader(name, filePath));          // 头部boundary
                        byte[] file_bytes = FileBytes(filePath);                                                         // 文件byte
                        byte[] post_data = StrToByte(postData);                                                          // 内容byte
                        byte[] end_boundary_bytes = Encoding.UTF8.GetBytes(string.Format("\r\n--{0}--\r\n", boundary));  // 结束boundary

                        hwr.ContentType = string.Format("multipart/form-data;charset=utf-8;boundary={0}", boundary);
                        System.IO.Stream post_stream = hwr.GetRequestStream();
                        post_stream.Write(item_boundary_bytes, 0, item_boundary_bytes.Length);
                        post_stream.Write(post_header_bytes, 0, post_header_bytes.Length);
                        post_stream.Write(file_bytes, 0, file_bytes.Length);
                        post_stream.Write(post_data, 0, post_data.Length);
                        post_stream.Write(end_boundary_bytes, 0, end_boundary_bytes.Length);
                        post_stream.Close();
                    }
                    else
                    {
                        //POST 字符串
                        byte[] post_data = StrToByte(postData);
                        hwr.ContentType = GetContentType(ContentType);
                        hwr.ContentLength = post_data.Length;
                        System.IO.Stream newStream = hwr.GetRequestStream();
                        newStream.Write(post_data, 0, post_data.Length); //设置POST
                        newStream.Close();
                    }
                }
                System.Net.HttpWebResponse hwrs = (System.Net.HttpWebResponse)hwr.GetResponse();
                return hwrs;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 请求头部信息
        /// </summary>
        /// <param name="name">文本域的名称    比如：name="file"，那么 file</param>
        /// <param name="filePath">上传的文件路径  比如：c:\12.jpg</param>
        /// <returns></returns>
        private string StringBuilderHeader(string name, string filePath)
        {
            //请求头部信息 
            int pos = filePath.LastIndexOf("\\");
            string file_name = filePath.Substring(pos + 1);
            System.Text.StringBuilder sb_header = new System.Text.StringBuilder(string.Format("Content-Disposition:form-data;name=\"{0}\";filename=\"{1}\"\r\nContent-Type:application/octet-stream\r\n\r\n", name, file_name));
            return sb_header.ToString();     
        }
        /// <summary>
        /// 发送文件 byte
        /// </summary>
        /// <param name="filePath">上传的文件路径  比如：c:\12.jpg</param>
        /// <returns></returns>
        private byte[] FileBytes(string filePath)
        {
            System.IO.FileStream fs = new System.IO.FileStream(filePath, FileMode.Open, FileAccess.Read);
            byte[] bArr = new byte[fs.Length];
            fs.Read(bArr, 0, bArr.Length);
            fs.Close();
            return bArr;
        }
        /// <summary>
        /// 字符串转二进制
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="codeType">编码</param>
        /// <returns></returns>
        private byte[] StrToByte(string str, string codeType = "UTF-8")
        {
            Encoding encoding = Encoding.GetEncoding(codeType);
            return encoding.GetBytes(str);
        }
        /// <summary>
        /// 文本流转字符输出
        /// </summary>
        /// <param name="instream">输入流</param>
        /// <param name="codeType">输出字符编码</param>
        /// <returns></returns>
        private string StreamToStr(System.IO.Stream instream, string codeType = "UTF-8")
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(instream, Encoding.GetEncoding(codeType));
            return sr.ReadToEnd();
        }
        /// <summary>
        /// 创建临时文件夹、文件
        /// </summary>
        /// <param name="savePath">文件存放地址，包含文件名 相对路径</param>
        /// <returns></returns>
        private string CreateTempFilePath(string savePath)
        {
            //指定虚拟路径
            string save_path = string.Format("{0}/{0}", AppDomain.CurrentDomain.BaseDirectory, savePath);
            string file_path = System.IO.Path.GetDirectoryName(save_path);            //获取路径文件夹
            string file_name = System.IO.Path.GetFileName(save_path);                 //获取文件名
            string temp_path = string.Format("{0}\\temp", file_path);                 //临时文件夹
            string temp_file = string.Format("{0}\\{1}.temp", temp_path, file_name);  //临时文件
            //判断文件夹是否存在，不存在创建临时文件目录
            if (!System.IO.Directory.Exists(temp_path))
                System.IO.Directory.CreateDirectory(temp_path);
            //判断文件是否存在，存在则删除
            if (System.IO.File.Exists(temp_file))
                System.IO.File.Delete(temp_file);
            return temp_file;
        }
        /// <summary>
        /// 生成文件名
        /// </summary>
        /// <param name="fileName">原来文件名</param>
        /// <returns></returns>
        private string CreateSaveFileName(string fileName)
        {
            string guid_str = Guid.NewGuid().ToString();
            string file_name = System.IO.Path.GetFileName(fileName);
            return string.Format("{0}_{1}", guid_str, file_name);
        }
        /// <summary>
        /// 获取请求头部
        /// </summary>
        /// <param name="contentType">HttpRequestContentType.html</param>
        /// <returns></returns>
        private string GetContentType(HttpRequestContentType contentType)
        {
            string result = string.Empty;
            Type type = typeof(HttpRequestContentType);
            foreach (System.Reflection.MemberInfo mInfo in type.GetMembers())
            {
                foreach (Attribute attr in Attribute.GetCustomAttributes(mInfo))
                {
                    if (attr.GetType() == typeof(System.ComponentModel.DescriptionAttribute))
                    {
                        if (mInfo.Name == contentType.ToString())
                        {
                            result = ((System.ComponentModel.DescriptionAttribute)attr).Description;
                        }
                    }
                }
            }
            return result;
        }
        #endregion
    }
    /// <summary>
    /// HTTP REQUEST 请求动作
    /// </summary>
    public enum HttpRequestMethod
    {
        /// <summary>
        /// http请求动作 GET
        /// </summary>
        GET,
        /// <summary>
        /// http请求动作 POST
        /// </summary>
        POST,
        /// <summary>
        /// http请求动作 DELETE
        /// </summary>
        DELETE,
        /// <summary>
        /// http请求动作 PUT
        /// </summary>
        PUT
    }
    /// <summary>
    /// HttpRequest Content-Type 请求内容类型
    /// </summary>
    public enum HttpRequestContentType
    {
        /// <summary>
        /// POST内容 url编码类型,默认POST数据类型 application/x-www-form-urlencoded
        /// </summary>
        [System.ComponentModel.Description("application/x-www-form-urlencoded")]
        UrlEncoded,
        /// <summary>
        /// POST文件 上传文件类型  multipart/form-data
        /// </summary>
        [System.ComponentModel.Description("multipart/form-data")]
        Data,
        /// <summary>
        /// JSON 格式 application/json
        /// </summary>
        [System.ComponentModel.Description("application/json")]
        Json,
        /// <summary>
        /// XML格式 text/xml
        /// </summary>
        [System.ComponentModel.Description("text/xml")]
        Xml,
        /// <summary>
        /// html格式 text/html
        /// </summary>
        [System.ComponentModel.Description("text/html")]
        Html
    }

}
