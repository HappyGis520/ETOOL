
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System;

/// <summary>
/// 图片处理类
/// 1、生成缩略图片或按照比例改变图片的大小和画质
/// 2、将生成的缩略图放到指定的目录下
/// </summary>
public class ImageClass
{
    public Image ResourceImage;
    private int ImageWidth;
    private int ImageHeight;

    public string ErrMessage;

    /// <summary>
    /// 类的构造函数
    /// </summary>
    /// <param name="ImageFileName">图片文件的全路径名称</param>
    public ImageClass(string ImageFileName)
    {
        ResourceImage = Image.FromFile(ImageFileName);
        ErrMessage = "";
    }

    public bool ThumbnailCallback()
    {
        return false;
    }

    /// <summary>
    /// 生成缩略图重载方法1，返回缩略图的Image对象
    /// </summary>
    /// <param name="Width">缩略图的宽度</param>
    /// <param name="Height">缩略图的高度</param>
    /// <returns>缩略图的Image对象</returns>
    public Image GetReducedImage(int Width, int Height)
    {
        try
        {
            Image ReducedImage;

            Image.GetThumbnailImageAbort callb = new Image.GetThumbnailImageAbort(ThumbnailCallback);

            ReducedImage = ResourceImage.GetThumbnailImage(Width, Height, callb, IntPtr.Zero);

            return ReducedImage;
        }
        catch (Exception e)
        {
            ErrMessage = e.Message;
            return null;
        }
    }

    /// <summary>
    /// 生成缩略图重载方法2，将缩略图文件保存到指定的路径
    /// </summary>
    /// <param name="Width">缩略图的宽度</param>
    /// <param name="Height">缩略图的高度</param>
    /// <param name="targetFilePath">缩略图保存的全文件名，(带路径)，参数格式：D:\Images\filename.jpg</param>
    /// <returns>成功返回true，否则返回false</returns>
    public bool GetReducedImage(int Width, int Height, string targetFilePath)
    {
        try
        {
            Image ReducedImage;

            Image.GetThumbnailImageAbort callb = new Image.GetThumbnailImageAbort(ThumbnailCallback);

            ReducedImage = ResourceImage.GetThumbnailImage(Width, Height, callb, IntPtr.Zero);
            ReducedImage.Save(@targetFilePath, ImageFormat.Jpeg);

            ReducedImage.Dispose();

            return true;
        }
        catch (Exception e)
        {
            ErrMessage = e.Message;
            return false;
        }
    }

    /// <summary>
    /// 生成缩略图重载方法3，返回缩略图的Image对象
    /// </summary>
    /// <param name="Percent">缩略图的宽度百分比 如：需要百分之80，就填0.8</param>  
    /// <returns>缩略图的Image对象</returns>
    public Image GetReducedImage(double Percent)
    {
        try
        {
            Image ReducedImage;

            Image.GetThumbnailImageAbort callb = new Image.GetThumbnailImageAbort(ThumbnailCallback);

            ImageWidth = Convert.ToInt32(ResourceImage.Width * Percent);
            ImageHeight = Convert.ToInt32(ResourceImage.Width * Percent);

            ReducedImage = ResourceImage.GetThumbnailImage(ImageWidth, ImageHeight, callb, IntPtr.Zero);

            return ReducedImage;
        }
        catch (Exception e)
        {
            ErrMessage = e.Message;
            return null;
        }
    }

    /// <summary>
    /// 生成缩略图重载方法4，返回缩略图的Image对象
    /// </summary>
    /// <param name="Percent">缩略图的宽度百分比 如：需要百分之80，就填0.8</param>  
    /// <param name="targetFilePath">缩略图保存的全文件名，(带路径)，参数格式：D:\Images\filename.jpg</param>
    /// <returns>成功返回true,否则返回false</returns>
    public bool GetReducedImage(double Percent, string targetFilePath)
    {
        try
        {
            Image ReducedImage;

            Image.GetThumbnailImageAbort callb = new Image.GetThumbnailImageAbort(ThumbnailCallback);

            ImageWidth = Convert.ToInt32(ResourceImage.Width * Percent);
            ImageHeight = Convert.ToInt32(ResourceImage.Width * Percent);

            ReducedImage = ResourceImage.GetThumbnailImage(ImageWidth, ImageHeight, callb, IntPtr.Zero);

            ReducedImage.Save(@targetFilePath, ImageFormat.Jpeg);

            ReducedImage.Dispose();

            return true;
        }
        catch (Exception e)
        {
            ErrMessage = e.Message;
            return false;
        }
    }




    /**//// <summary> 
        /// 生成缩略图 
        /// </summary> 
        /// <param name="originalImagePath">源图路径（物理路径）</param> 
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param> 
        /// <param name="width">缩略图宽度</param> 
        /// <param name="height">缩略图高度</param> 
        /// <param name="mode">生成缩略图的方式</param>     
    public static string MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
    {
        Image originalImage = Image.FromFile(originalImagePath);

        int towidth = width;
        int toheight = height;

        int x = 0;
        int y = 0;
        int ow = originalImage.Width;
        int oh = originalImage.Height;

        if (ow<towidth)
        {
            return originalImagePath;
        }

        switch (mode)
        {
            case "HW"://指定高宽缩放（可能变形）                 
                break;
            case "W"://指定宽，高按比例                     
                toheight = originalImage.Height * width / originalImage.Width;
                break;
            case "H"://指定高，宽按比例 
                towidth = originalImage.Width * height / originalImage.Height;
                break;
            case "Cut"://指定高宽裁减（不变形）                 
                if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                {
                    oh = originalImage.Height;
                    ow = originalImage.Height * towidth / toheight;
                    y = 0;
                    x = (originalImage.Width - ow) / 2;
                }
                else
                {
                    ow = originalImage.Width;
                    oh = originalImage.Width * height / towidth;
                    x = 0;
                    y = (originalImage.Height - oh) / 2;
                }
                break;
            default:
                break;
        }

        //新建一个bmp图片 
        Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

        //新建一个画板 
        Graphics g = System.Drawing.Graphics.FromImage(bitmap);

        //设置高质量插值法 
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

        //设置高质量,低速度呈现平滑程度 
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

        //清空画布并以透明背景色填充 
        g.Clear(Color.Transparent);

        //在指定位置并且按指定大小绘制原图片的指定部分 
        g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
            new Rectangle(x, y, ow, oh),
            GraphicsUnit.Pixel);

        try
        {
            //以jpg格式保存缩略图 
            bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        catch (System.Exception e)
        {
            throw e;
        }
        finally
        {
            originalImage.Dispose();
            bitmap.Dispose();
            g.Dispose();
        }

        return thumbnailPath;
    }

}
