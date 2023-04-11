using System;
using System.Windows;
using System.Drawing; // Color, Bitmap, Graphics
using System.Windows.Forms; // Screen height and width
using MessageBox = System.Windows.MessageBox; // Use message box of wpf

namespace PixelFindBot
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void OnButtonSearchPixelClick(object sender, RoutedEventArgs e)
        {
            string inputHexColorCode = TextBoxHexColor.Text;
            MessageBox.Show(inputHexColorCode);
        }
        private bool SearchPixel(string hexCode)
        {
            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            return false;
        }
    }
}
