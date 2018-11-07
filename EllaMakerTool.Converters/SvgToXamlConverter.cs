using Svg2Xaml;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;

namespace EllaMakerTool.WPF.Converter
{
    public class SvgToXamlConverter: IValueConverter
    {
        /// <summary>
        /// 将SVG矢量图转换为可用资源
        /// 用法<Image Source="{Binding ConverterParameter=pc_Button_search.svg, Converter={StaticResource SvgToXamlConverter}, Mode=OneWay}" />
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DrawingImage svg_image;
            string file_name = AppDomain.CurrentDomain.BaseDirectory + @"\Resources\" +parameter.ToString();
            using (FileStream file_stream = new FileStream(file_name, FileMode.Open, FileAccess.Read))
                svg_image = SvgReader.Load(file_stream, new SvgReaderOptions(false));
            return svg_image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public DrawingImage SvgToPic(string extension)
        {
            try
            {
                //图片类
                if (extension.Contains("bmp") || extension.Contains("jpg") || extension.Contains("png") || extension.Contains("jpeg") || extension.Contains("ico")|| extension.Contains("gif"))
                {
                    return (DrawingImage)Convert("", null, "Icon_Type_jpg.svg",
                        System.Globalization.CultureInfo.CurrentCulture);
                }
                //音频类
                if (extension.Contains("wav") || extension.Contains("ogg") || extension.Contains("mp3") || extension.Contains("ape") || extension.Contains("cda") || extension.Contains("au") || extension.Contains("midi") || extension.Contains("mac") || extension.Contains("aac") || extension.Contains("aac") || extension.Contains("m4a"))
                {
                    return (DrawingImage)Convert("", null, "Icon_Type_mp3.svg",
                        System.Globalization.CultureInfo.CurrentCulture);
                }
                //视频类
                if (extension.Contains("rmvb") || extension.Contains("wmv") || extension.Contains("asf") || extension.Contains("avi") || extension.Contains("3gp") || extension.Contains("mpg") || extension.Contains("mkv") || extension.Contains("mp4") || extension.Contains("mpeg2") || extension.Contains("mpeg4"))
                {
                    return (DrawingImage)Convert("", null, "Icon_Type_avi.svg",
                        System.Globalization.CultureInfo.CurrentCulture);
                }
                //压缩类
                if (extension.Contains("7z") || extension.Contains("zip") || extension.Contains("rar"))
                {
                    return (DrawingImage)Convert("", null, "Icon_Type_zip.svg",
                        System.Globalization.CultureInfo.CurrentCulture);
                }
                //word类
                if (extension.Contains("doc") || extension.Contains("docx") || extension.Contains("wps"))
                {
                    return (DrawingImage)Convert("", null, "Icon_Type_doc.svg",
                        System.Globalization.CultureInfo.CurrentCulture);
                }
                //ppt类
                if (extension.Contains("ppt") || extension.Contains("dps"))
                {
                    return (DrawingImage)Convert("", null, "Icon_Type_ppt.svg",
                        System.Globalization.CultureInfo.CurrentCulture);
                }
                //excel类
                if (extension.Contains("xls") || extension.Contains("et"))
                {
                    return (DrawingImage)Convert("", null, "Icon_Type_xls.svg",
                        System.Globalization.CultureInfo.CurrentCulture);
                }
                //PDF
                if (extension.Contains("pdf"))
                {
                    return (DrawingImage)Convert("", null, "Icon_Type_pdf.svg",
                        System.Globalization.CultureInfo.CurrentCulture);
                }
                //exe
                if (extension.Contains("exe"))
                {
                    return (DrawingImage)Convert("", null, "Icon_Type_exe.svg",
                        System.Globalization.CultureInfo.CurrentCulture);
                }
                if (extension.Contains("folder"))
                {
                    return (DrawingImage)Convert("", null, "Icon_Type_folder.svg",
                        System.Globalization.CultureInfo.CurrentCulture);
                }
                return (DrawingImage)Convert("", null, "Icon_Type_other.svg",
                    System.Globalization.CultureInfo.CurrentCulture);
            }
            catch
            {
                return (DrawingImage)Convert("", null, "Icon_Type_other.svg",
                    System.Globalization.CultureInfo.CurrentCulture);
            }
        }
    }
}
