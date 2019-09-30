using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Windows.Interop;
using System.IO;
using WpfAnimatedGif;
using ID3;
namespace decision_tree
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public static tree t;
        public static string path = "";
        static string CD = Environment.CurrentDirectory;
        public string gifpath = CD + @"\ge.gif";
        public MainWindow()
        {
            InitializeComponent();
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(CD+@"\ic.ico", UriKind.Relative);
            src.EndInit();
            this.Icon = src;
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(gifpath);
            image.EndInit();
            ImageBehavior.SetAnimatedSource(img, image);
            path = CD;

        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            te1.Text = CD;
            path = CD;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            g.Children.Clear();
            t = new tree(path+@"\tab.xml",path+@"\tab2.xml");
            this.Content = new Page1();
        }
    }
}
