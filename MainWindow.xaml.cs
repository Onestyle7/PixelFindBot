using System;
using System.Windows;
using System.Drawing; // Color, Bitmap, Graphics
using System.Windows.Forms; // Screen height and width
using MessageBox = System.Windows.MessageBox; // Use message box of wpf
using System.Runtime.InteropServices; // User32.dll (dll import)


namespace PixelFindBot
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const UInt32 MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const UInt32 MOUSEEVENTF_LEFTUP = 0x0004;

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, uint dwExtraInf);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Click()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
        private void DoubleClickAtPosition(int posX, int posY)
        {
            SetCursorPos(posX, posY);
            Click();
            System.Threading.Thread.Sleep(250);
            Click();
        }
        private void OnButtonSearchPixelClick(object sender, RoutedEventArgs e)
        {
            string inputHexColorCode = TextBoxHexColor.Text;
            SearchPixel(inputHexColorCode);
        }
        private bool SearchPixel(string hexCode)
        {
            // Utworzenie pustej bitmapy z wielkością aktualnego ekranu
            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            //Utowrzenie nowego objektu graficznego, który jest przechwytywany z ekranu
            Graphics graphics = Graphics.FromImage(bitmap as Image);
            //Moment SS -> Treść SS leci do objektu graficznego^
            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            // Przetłumaczenie koloru np. #ffffff na kolor objektu
            Color desiredPixelColor = ColorTranslator.FromHtml(hexCode);

            for(int x = 0; x<SystemInformation.VirtualScreen.Width; x++)
            {
                for(int y = 0; y<SystemInformation.VirtualScreen.Height; y++)
                {
                    // Pobranie aktualnego koloru pixela
                    Color currentPixelColor = bitmap.GetPixel(x, y);
                    throw new ArgumentOutOfRangeException("Taki kolor nie istnieje na ekranie");
                    if (desiredPixelColor == currentPixelColor)
                    {
                        MessageBox.Show(String.Format("Found Pixel at {0}, {1} - Now set mouse cursor", x,y));

                        DoubleClickAtPosition(x, y);
                        return true;
                    }
                }
            }
    
            return false;
        }
    }
}
