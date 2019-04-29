using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LdCms.Common.Extension
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Extensions;

    /// <summary>
    /// 扩展 HttpRequest
    /// </summary>
    public static class HttpRequestExtensions
    {
        public static string GetHttpMethod(this HttpRequest request)
        {
            try
            {
                return request.Method.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static string GetHttpHeaderValue(this HttpRequest request, string name)
        {
            try
            {
                return request.Headers[name].ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 获取当前URL 绝对路径
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetAbsoluteUri(this HttpRequest request)
        {
            try
            {
                return new StringBuilder()
                    .Append(request.Scheme)
                    .Append("://")
                    .Append(request.Host)
                    .Append(request.PathBase)
                    .Append(request.Path)
                    .Append(request.QueryString)
                    .ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static string GetHttpWebRoot(this HttpRequest request)
        {
            try
            {
                return new StringBuilder()
                    .Append(request.Scheme)
                    .Append("://")
                    .Append(request.Host)
                    .Append(request.PathBase)
                    .ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 获取QueryString
        /// </summary>
        /// <param name="request"></param>
        /// <param name="name">节点名称</param>
        /// <returns></returns>
        public static string GetQueryString(this HttpRequest request, string name)
        {
            try
            {
                var obj = request.Query[name].FirstOrDefault();
                if (string.IsNullOrEmpty(obj))
                    return "";
                else
                    return obj.ToString().Trim();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 获取FormValue
        /// </summary>
        /// <param name="request"></param>
        /// <param name="name">表单名称</param>
        /// <returns></returns>
        public static string GetFormValue(this HttpRequest request, string name)
        {
            try
            {
                var obj = request.Form[name].FirstOrDefault();
                if (string.IsNullOrEmpty(obj))
                    return "";
                else
                    return obj.ToString().Trim();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static string GetFormValueArr(this HttpRequest request, string name)
        {
            try
            {
                var obj = request.Form[name];
                if (string.IsNullOrEmpty(obj))
                    return "";
                else
                    return obj.ToString().Trim();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 获取传入流
        /// </summary>
        /// <returns></returns>
        public static string GetInputStream(this HttpRequest request)
        {
            try
            {
                System.IO.Stream stream = request.Body;
                bool isContentLength = request.ContentLength.HasValue;
                if (isContentLength)
                {
                    byte[] buffer = new byte[request.ContentLength.Value];
                    stream.Read(buffer, 0, buffer.Length);
                    string content = Encoding.UTF8.GetString(buffer);
                    return content;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
