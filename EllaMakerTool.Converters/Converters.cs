
using EllaMakerTool.Core;
using System.Windows.Controls;

namespace EllaMakerTool.WPF.Converter
{
    /// <summary>
    /// 常用转换器的静态引用
    /// 使用实例：Converter={x:Static local:XConverter.TrueToFalseConverter}
    /// </summary>
    public sealed class XConverter
    {
        public static BooleanToVisibilityConverter BooleanToVisibilityConverter
        {
            get { return Singleton<BooleanToVisibilityConverter>.Instance; }
        }

        public static BarEditButtonVisConverter BarEditButtonVisConverter
        {
            get { return Singleton<BarEditButtonVisConverter>.Instance; }
        }

        public static BarDeleteButtonVisConverter BarDeleteButtonVisConverter
        {
            get { return Singleton<BarDeleteButtonVisConverter>.Instance; }
        }

        public static NumChangeConverter NumChangeConverter
        {
            get { return Singleton<NumChangeConverter>.Instance; }
        }

        public static TrueToFalseConverter TrueToFalseConverter
        {
            get { return Singleton<TrueToFalseConverter>.Instance; }
        }

        public static ThicknessToDoubleConverter ThicknessToDoubleConverter
        {
            get { return Singleton<ThicknessToDoubleConverter>.Instance; }
        }
        public static BackgroundToForegroundConverter BackgroundToForegroundConverter
        {
            get { return Singleton<BackgroundToForegroundConverter>.Instance; }
        }
        public static TreeViewMarginConverter TreeViewMarginConverter
        {
            get { return Singleton<TreeViewMarginConverter>.Instance; }
        }

        public static SvgToXamlConverter SvgToXamlConverter
        {
            get { return Singleton<SvgToXamlConverter>.Instance; }
        }

        public static EnumDescriptionConverter EnumDescriptionConverter
        { get { return Singleton<EnumDescriptionConverter>.Instance; } }

        public static StoreBarStrConverter StoreBarStrConverter
        { get { return Singleton<StoreBarStrConverter>.Instance; } }

        public static CollaspedToVisConverter CollaspedToVisConverter
        { get { return Singleton<CollaspedToVisConverter>.Instance; } }

        public static ProgressImageConverter ProgressImageConverter
        { get { return Singleton<ProgressImageConverter>.Instance; } }

        public static DocmentItemsToIrVisConvert DocmentItemsToIrVisConvert
        { get { return Singleton<DocmentItemsToIrVisConvert>.Instance; } }

        public static StrAppendConverter StrAppendConverter
        { get { return Singleton<StrAppendConverter>.Instance; } }
        
        public static SizeToStringConverter SizeToStringConverter
        { get { return Singleton<SizeToStringConverter>.Instance; } }

        public static DataTimeTostringConverter DataTimeTostringConverter
        { get { return Singleton<DataTimeTostringConverter>.Instance; } }

        public static SyncButtonVisConverter SyncButtonVisConverter
        { get { return Singleton<SyncButtonVisConverter>.Instance; } }

        public static FilenameToTypeIconConverter FilenameToTypeIconConverter
        { get { return Singleton<FilenameToTypeIconConverter>.Instance; } }

        public static StrAppendBeforeConverter StrAppendBeforeConverter
        { get { return Singleton<StrAppendBeforeConverter>.Instance; } }

        public static DocTypeVisConverter DocTypeVisConverter
        { get { return Singleton<DocTypeVisConverter>.Instance; } }

        public static SizeToProgressStringConverter SizeToProgressStringConverter
        { get { return Singleton<SizeToProgressStringConverter>.Instance; } }

        public static DpOrPsTypeVisConverter DpOrPsTypeVisConverter
        {
            get { return Singleton<DpOrPsTypeVisConverter>.Instance; }
        }

        public static GetNetImageConverter GetNetImageConverter
        {
            get { return Singleton<GetNetImageConverter>.Instance; }
        }

        public static EnumDocStatusTypeToVisibilityConverter EnumDocStatusTypeToVisibilityConverter
        { get { return Singleton<EnumDocStatusTypeToVisibilityConverter>.Instance; } }

        public static SyncSeriseButtonVisConverter SyncSeriseButtonVisConverter
        { get { return Singleton<SyncSeriseButtonVisConverter>.Instance; } }

        public static BoolToVisHideConverter BoolToVisHideConverter
        {
            get { return Singleton<BoolToVisHideConverter>.Instance; }
        }

        public static DeleteButtonVisConverter DeleteButtonVisConverter
        {
            get { return Singleton<DeleteButtonVisConverter>.Instance; }
        }

        public static ChangeStatusEnableConverter ChangeStatusEnableConverter
        {
            get { return Singleton<ChangeStatusEnableConverter>.Instance; }
        }

        public static GobackEnableVisConverter GobackEnableVisConverter
        {
            get { return Singleton<GobackEnableVisConverter>.Instance; }
        }

        public static GofrontEnableVisConverter GofrontEnableVisConverter
        {
            get { return Singleton<GofrontEnableVisConverter>.Instance; }
        }
    }
}
