using System.Windows;

namespace EllaMakerTool.WPF.Controls.UserControls
{
    public class CoverMessageBox
    {
        public static MessageBoxResult Show(Window owner,string tipStr="",string mainStr = "", string okBtnMsg = "替换目标中的文件", string cancelBtnMsg = "保存成新的文件",bool isOkBtnVis=true,bool isCancelVis=true)
        {
            var messageBox = new CoverMessageBoxControl();
            if (owner != null)
            {
                messageBox.Owner = owner;
                messageBox.Icon = owner.Icon;
            }

            messageBox.TipStr = tipStr;
            messageBox.MainStr = mainStr;
            messageBox.cancelBtn.Content = cancelBtnMsg;
            messageBox.okBtn.Content = okBtnMsg;
            messageBox.okBtn.Visibility = isOkBtnVis ? Visibility.Visible : Visibility.Collapsed;
            messageBox.cancelBtn.Visibility = isCancelVis ? Visibility.Visible : Visibility.Collapsed;

            var dialogResult = messageBox.ShowDialog();
            switch (dialogResult)
            {
                case null:
                    return MessageBoxResult.None;
                case true:
                    return MessageBoxResult.OK;
                default:
                    return MessageBoxResult.Cancel;
            }
        }

    }
}
