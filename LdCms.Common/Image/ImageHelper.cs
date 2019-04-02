using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdCms.Common.Image
{
    /// <summary>
    /// LdCms 图片帮助操作类
    /// </summary>
    public class ImageHelper
    {
        public enum ImageType
        {
            Bmp = 0,
            Jpg = 1,
            Gif = 2,
            Png = 3
        }
        /// <summary>  
        /// LdCms base64编码的文本转为图片  
        /// </summary>  
        /// <param name="imagePath">图片保存绝对物理路径(存到服务器上)</param>  
        /// <param name="base64String">图片字符串</param>  
        /// <param name="type">图片格式类型</param>
        /// <returns></returns>
        public static string Base64StringToImage(string imagePath, string base64String, ImageType type)
        {
            try
            {
                string imageCode = GetImageCode(base64String);
                byte[] arr = Convert.FromBase64String(imageCode);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(arr);
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ms);

                System.Drawing.Imaging.ImageFormat imageType = System.Drawing.Imaging.ImageFormat.Jpeg;
                if ((int)type == 0)
                    imageType = System.Drawing.Imaging.ImageFormat.Bmp;
                if ((int)type == 1)
                    imageType = System.Drawing.Imaging.ImageFormat.Jpeg;
                if ((int)type == 2)
                    imageType = System.Drawing.Imaging.ImageFormat.Gif;
                if ((int)type == 3)
                    imageType = System.Drawing.Imaging.ImageFormat.Png;
                bmp.Save(imagePath, imageType);
                ms.Close();
                return imagePath;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static string Base64StringToImage(string imagePath, string base64String)
        {
            ImageType imageType = GetImageType(base64String);
            return Base64StringToImage(imagePath, base64String, imageType);
        }
        public static string ImageToBase64String(string imagePath)
        {
            try
            {
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(imagePath);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static string GetImageCode(string base64String)
        {
            try
            {
                return base64String.Contains(",") ? base64String.Split(',')[1] : base64String;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static string GetImageExtension(string base64String)
        {
            try
            {
                if (base64String.Contains(";"))
                {
                    string[] arrData = base64String.Split(";");
                    string[] arrType = arrData[0].Split(":");
                    if (arrType.Length == 2)
                    {
                        string imageType = arrType[1];
                        switch (imageType.ToLower())
                        {
                            case "image/bmp":
                                return "bmp";
                            case "image/jpg":
                                return "jpg";
                            case "image/gif":
                                return "gif";
                            case "image/png":
                                return "png";
                            default:
                                return "jpg";
                        }
                    }
                }
                return "jpg";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private static ImageType GetImageType(string base64String)
        {
            try
            {
                if (base64String.Contains(";"))
                {
                    string[] arrData = base64String.Split(";");
                    string[] arrType = arrData[0].Split(":");
                    if (arrType.Length == 2)
                    {
                        string imageType = arrType[1];
                        switch (imageType.ToLower())
                        {
                            case "image/bmp":
                                return ImageType.Bmp;
                            case "image/jpg":
                                return ImageType.Jpg;
                            case "image/gif":
                                return ImageType.Gif;
                            case "image/png":
                                return ImageType.Png;
                            default:
                                return ImageType.Jpg;
                        }
                    }
                }
                return ImageType.Jpg;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
