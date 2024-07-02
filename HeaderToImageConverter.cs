using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using WpfTreeView;

namespace WPFTreeView
{

    ///<summary>
    /// Converts a full path to a specific image type of a drive or a folder
    ///</summary>
    ///
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var image = "Images/file.png";

            //If the name is blank, then it will be a drive
            switch ((DirectoryItemType)value)
            {
                case DirectoryItemType.Drive:
                    image = "Images/harddrive.png";
                    break;

                case DirectoryItemType.Folder:
                    image = "Images/folder.png";
                    break;

                case DirectoryItemType.OpenFolder:
                    image = "Images/open-folder.png";
                    break;

                case DirectoryItemType.File:
                    break;
            }


            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
