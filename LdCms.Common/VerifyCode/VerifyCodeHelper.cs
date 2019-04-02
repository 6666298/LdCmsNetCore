using System;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.IO;

namespace LdCms.Common.VerifyCode
{
    /// <summary>
    /// 生成验证码
    ///
    /// 作者：小草 
    /// 功能：
    ///     1、生成随机验证码输出图片byte[]
    /// 
    /// 使用说明：
    ///     控制器中使用:
    ///     var imageByte = VerifyCodeHelper.Create();
    ///     return File(imageByte, @"image/png");
    /// 
    /// </summary>
    public static class VerifyCodeHelper
    {
        /// <summary>
        /// 创建随机码图片
        /// </summary>
        /// <returns></returns>
        public static byte[] Create()
        {
            try
            {
                string randomCode = CreateRandomCode(5);
                return Create(randomCode, Version.V2);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 创建随机码图片
        /// </summary>
        /// <param name="randomCode"></param>
        /// <returns></returns>
        public static byte[] Create(string randomCode)
        {
            try
            {
                return Create(randomCode, Version.V2);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 创建随机码图片
        /// </summary>
        /// <param name="code"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static byte[] Create(string code, Version v)
        {
            try
            {
                int versionNumber = (int)v;
                string randomCode = string.IsNullOrWhiteSpace(code) ? CreateRandomCode(5) : code;
                if (versionNumber == (int)Version.V1)
                    return CreateImageV1(randomCode);
                else
                    return CreateImageV2(randomCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 生成随机码
        /// </summary>
        /// <param name="length">随机码个数</param>
        /// <returns></returns>
        private static string CreateRandomCode(int length)
        {
            int rand;
            char code;
            string randomcode = String.Empty;

            //生成一定长度的验证码
            System.Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                rand = random.Next();

                if (rand % 3 == 0)
                {
                    code = (char)('A' + (char)(rand % 26));
                }
                else
                {
                    code = (char)('0' + (char)(rand % 10));
                }

                randomcode += code.ToString();
            }
            return randomcode;
        }
        /// <summary>
        /// 创建随机码图片
        /// </summary>
        /// <param name="chkCode"></param>
        /// <returns></returns>
        private static byte[] CreateImageV1(string randomCode)
        {
            try
            {
                int codeW = 80;
                int codeH = 30;
                int fontSize = 16;
                Random rnd = new Random();
                //颜色列表，用于验证码、噪线、噪点 
                Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
                //字体列表，用于验证码 
                string[] font = { "Times New Roman" };
                //验证码的字符集，去掉了一些容易混淆的字符 

                //写入Session、验证码加密
                //WebHelper.WriteSession("session_verifycode", Md5Helper.MD5(chkCode.ToLower(), 16));
                //创建画布
                Bitmap bmp = new Bitmap(codeW, codeH);
                Graphics g = Graphics.FromImage(bmp);
                g.Clear(Color.White);
                //画噪线 
                for (int i = 0; i < 1; i++)
                {
                    int x1 = rnd.Next(codeW);
                    int y1 = rnd.Next(codeH);
                    int x2 = rnd.Next(codeW);
                    int y2 = rnd.Next(codeH);
                    Color clr = color[rnd.Next(color.Length)];
                    g.DrawLine(new Pen(clr), x1, y1, x2, y2);
                }
                //画验证码字符串 
                for (int i = 0; i < randomCode.Length; i++)
                {
                    string fnt = font[rnd.Next(font.Length)];
                    Font ft = new Font(fnt, fontSize);
                    Color clr = color[rnd.Next(color.Length)];
                    g.DrawString(randomCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 18, (float)0);
                }
                //将验证码图片写入内存流，并将其以 "image/Png" 格式输出 
                MemoryStream ms = new MemoryStream();

                bmp.Save(ms, ImageFormat.Png);
                g.Dispose();
                bmp.Dispose();
                return ms.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 创建随机码图片
        /// </summary>
        /// <param name="randomcode">随机码</param>
        private static byte[] CreateImageV2(string randomCode)
        {
            try
            {
                int randAngle = 45; //随机转动角度
                int mapwidth = (int)(randomCode.Length * 16);
                Bitmap map = new Bitmap(mapwidth, 41);//创建图片背景
                Graphics graph = Graphics.FromImage(map);
                graph.Clear(Color.AliceBlue);//清除画面，填充背景
                graph.DrawRectangle(new Pen(Color.Black, 0), 0, 0, map.Width - 1, map.Height - 1);//画一个边框
                //graph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;//模式

                Random rand = new Random();
                
                //背景噪点生成
                Pen blackPen = new Pen(Color.LightGray, 0);
                for (int i = 0; i < 50; i++)
                {
                    int x = rand.Next(0, map.Width);
                    int y = rand.Next(0, map.Height);
                    graph.DrawRectangle(blackPen, x, y, 1, 1);
                }
                //验证码旋转，防止机器识别
                char[] chars = randomCode.ToCharArray();//拆散字符串成单字符数组

                //文字距中
                StringFormat format = new StringFormat(StringFormatFlags.NoClip);
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                //定义颜色
                Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
                //定义字体
                string[] font = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };
                int cindex = rand.Next(7);

                for (int i = 0; i < chars.Length; i++)
                {
                    int findex = rand.Next(5);

                    Font f = new Font(font[findex], 16, FontStyle.Bold);//字体样式(参数2为字体大小)
                    Brush b = new SolidBrush(c[cindex]);

                    Point dot = new Point(14, 22);
                    //graph.DrawString(dot.X.ToString(),fontstyle,new SolidBrush(Color.Black),10,150);//测试X坐标显示间距的
                    float angle = rand.Next(-randAngle, randAngle);//转动的度数

                    graph.TranslateTransform(dot.X, dot.Y);//移动光标到指定位置
                    graph.RotateTransform(angle);
                    graph.DrawString(chars[i].ToString(), f, b, 1, 1, format);
                    //graph.DrawString(chars[i].ToString(),fontstyle,new SolidBrush(Color.Blue),1,1,format);
                    graph.RotateTransform(-angle);//转回去
                    graph.TranslateTransform(-2, -dot.Y);//移动光标到指定位置，每个字符紧凑显示，避免被软件识别
                }
                //生成图片
                MemoryStream ms = new MemoryStream();
                map.Save(ms, ImageFormat.Png);
                graph.Dispose();
                map.Dispose();
                return ms.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 版本枚举
        /// </summary>
        public enum Version
        {
            /// <summary>
            /// 版本V1
            /// </summary>
            V1 = 1,
            /// <summary>
            /// 版本V2
            /// </summary>
            V2 = 2
        }

    }
}
