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
using System.Speech.Synthesis;
using ID3;
using System.Windows.Media.Animation;
using prolog;
using System.Threading;

namespace decision_tree
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
  

    public partial class Page1 : Page
    {
        int colors;
        int envrnomnet;
        colors[] cs = new colors[7];
        int scale;
        int x;
        int y;
        SpeechSynthesizer synthesizer;
        PEngine pe;
        string cm;
        List<string> slist = new List<string>();
        int numberofQ;
        bool whn;
        string pathresulttohow;
        public string commend { get { return cm; } set { cm = value; commandLine.Document.Blocks.Add(new Paragraph((new Run(value)))); commandLine.ScrollToEnd(); } }
        public Page1()
        {
            InitializeComponent();
            pathresulttohow = "";
            whn = false;
            numberofQ = 0;
            slist = MainWindow.t.ConvertSymptomsToPredicate(MainWindow.t.ConvertToPredicate());
            pe = new PEngine();
            synthesizer = new SpeechSynthesizer();
            synthesizer.SelectVoiceByHints(VoiceGender.Female);
            synthesizer.Volume = 100;  
            synthesizer.Rate = 0;
            cs[0] = new colors(true,"#252839","#677077","#f2b632","#b5b5b7");
            cs[1] = new colors(false, "#30231d", "#6a5750", "#b5a379", "#f6f1ed");
            cs[2] = new colors(true, "#262216", "#49412c", "#97743a", "#b0a18e");
            cs[3] = new colors(false, "#16174f", "#963019", "#667467", "#f6f1ed");
            cs[4] = new colors(true, "#1d2120", "#5a5c51", "#ba9077", "#bcd5d1");
            cs[5] = new colors(false, "#173e43", "#3fb0ac", "#fae596", "#dddfd4");
            cs[6] = new colors(false, "#6ed3cf", "#9068be", "#e62739", "#e1e8f0");
            colors = 0;
            envrnomnet = 0;
            scale = 1;
            x = 0;
            y = 0;
            //tb1.Text = ""+MainWindow.t.passvalue;
            repaint();
            //synthesizer.SpeakAsync("hellow");

        }
        public void repaint()
        {
            c1.Children.Clear();
            draw();
        
        }
        private void draw()
        {
            //canvsarea.Children.Clear();
            Ellipse sym;
            Rectangle dis;
            TextBlock tb;
            Line l;
            var bc = new BrushConverter();
            c1.Background = (Brush)bc.ConvertFrom(cs[envrnomnet % 7].back);
            for (int i = 1; i < 150; i++)
            {
                if (MainWindow.t.nodes[i].NodeDef < 3)
                {
                    
                    tb = new TextBlock();
                    tb.Foreground = Brushes.White;
                    tb.Text = MainWindow.t.nodes[i].minis();
                    tb.FontSize = 12;
                    tb.ToolTip= MainWindow.t.nodes[i].fulldetails(); 
                    MainWindow.t.nodes[i].X_pos += x;
                    MainWindow.t.nodes[i].Y_pos += y;
                    Canvas.SetLeft(tb, (MainWindow.t.nodes[i].X_pos+4));
                    Canvas.SetTop(tb, MainWindow.t.nodes[i].Y_pos+7);
                    if (MainWindow.t.nodes[i].NodeDef == 0)
                    {
                        if (i * 2 < 300) {
                            sym = new Ellipse();
                            sym.StrokeThickness = 2;
                            sym.Fill = (Brush)bc.ConvertFrom(cs[envrnomnet % 7].cs[colors % 3]);
                            if (cs[envrnomnet % 7].Isdark) sym.Stroke = Brushes.White;
                            else sym.Stroke = Brushes.Black;
                            sym.Width = 75;
                            sym.Height = 30;
                            Canvas.SetLeft(sym, (MainWindow.t.nodes[i].X_pos));
                            Canvas.SetTop(sym, MainWindow.t.nodes[i].Y_pos);
                            sym.ToolTip = MainWindow.t.nodes[i].fulldetails();
                            l = new Line();
                            if (cs[envrnomnet % 7].Isdark) l.Stroke = Brushes.White;
                            else l.Stroke = Brushes.Black;
                            l.StrokeThickness = 5;
                            l.X1 = (MainWindow.t.nodes[i].X_pos + 15);
                            l.Y1 = (MainWindow.t.nodes[i].Y_pos + 15);
                            if (i * 2 < 300) l.X2 = (MainWindow.t.nodes[i * 2].X_pos + 15);
                            if (i * 2 < 300) l.Y2 = ((MainWindow.t.nodes[i * 2].Y_pos + 15));
                            l.ToolTip = "yes";
                            c1.Children.Add(l);
                            l = new Line();
                            if (cs[envrnomnet % 7].Isdark) l.Stroke = Brushes.White;
                            else l.Stroke = Brushes.Black;
                            l.StrokeThickness = 5;
                            l.X1 = (MainWindow.t.nodes[i].X_pos + 50);
                            l.Y1 = (MainWindow.t.nodes[i].Y_pos + 15);
                            if ((i * 2) + 1 < 300) l.X2 = (MainWindow.t.nodes[(i * 2) + 1].X_pos + 15);
                            if ((i * 2) + 1 < 300) l.Y2 = ((MainWindow.t.nodes[(i * 2) + 1].Y_pos + 15));
                            l.ToolTip = "no";
                            c1.Children.Add(l);
                            c1.Children.Add(sym);
                        }
                        else
                        {
                            MainWindow.t.reset(MainWindow.path + @"\tab.xml", MainWindow.path + @"\tab2.xml");
                            colors = 0;
                            envrnomnet = 0;
                            scale = 1;
                            x = 0;
                            y = 0;
                            repaint();
                            break;
                        }
                    }
                    if (MainWindow.t.nodes[i].NodeDef == 1)
                    {
                        dis = new Rectangle();
                        dis.StrokeThickness = 2;
                        dis.Fill = (Brush)bc.ConvertFrom(cs[envrnomnet % 7].cs[((colors+1) % 3)]);
                        if (cs[envrnomnet%7].Isdark) dis.Stroke = Brushes.White;
                        else dis.Stroke = Brushes.Black;
                        dis.Width = 75;
                        dis.Height = 30;
                        dis.RadiusX = 10;
                        dis.RadiusY = 10;
                        
                        Canvas.SetTop(dis, MainWindow.t.nodes[i].Y_pos);
                        dis.ToolTip = MainWindow.t.nodes[i].fulldetails();
                        if (MainWindow.t.nodes[i / 4].name() == "Rubella")
                        {
                            dis.Width = 30;
                            dis.Height = 75;
                            RotateTransform rotateTransform1 = new RotateTransform(90);
                            tb.RenderTransform = rotateTransform1;
                           // MainWindow.t.nodes[i].X_pos += 30;
                            Canvas.SetLeft(tb, (MainWindow.t.nodes[i].X_pos + 25));
                        }
                        Canvas.SetLeft(dis, (MainWindow.t.nodes[i].X_pos));
                        c1.Children.Add(dis);
                    }
                    if (MainWindow.t.nodes[i].NodeDef == 2)
                    {
                        dis = new Rectangle();
                        dis.StrokeThickness = 2;
                        dis.Fill = (Brush)bc.ConvertFrom(cs[envrnomnet % 7].cs[((colors + 2) % 3)]);
                        if (cs[envrnomnet%7].Isdark) dis.Stroke = Brushes.White;
                        else dis.Stroke = Brushes.Black;
                        dis.Width = 75;
                        dis.Height = 30;
                        dis.RadiusX = 10;
                        dis.RadiusY = 10;
                        Canvas.SetLeft(dis, (MainWindow.t.nodes[i].X_pos));
                        Canvas.SetTop(dis, MainWindow.t.nodes[i].Y_pos);
                        dis.ToolTip = MainWindow.t.nodes[i].fulldetails();
                        l = new Line();
                        if (cs[envrnomnet % 7].Isdark) l.Stroke = Brushes.White;
                        else l.Stroke = Brushes.Black;
                        l.StrokeThickness = 5;
                        l.X1 = (MainWindow.t.nodes[i].X_pos + 30);
                        l.Y1 = (MainWindow.t.nodes[i].Y_pos + 15);
                        if (i * 2 < 300) l.X2 = (MainWindow.t.nodes[i * 2].X_pos+30);
                        if (i * 2 < 300) l.Y2 = ((MainWindow.t.nodes[i * 2].Y_pos+15));
                        l.ToolTip = "from ic";
                        c1.Children.Add(l);
                        c1.Children.Add(dis);
                    }
                    c1.Children.Add(tb);

                }
            }
        }
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.t.d.allnotchanged.sort();
            tb1.Text=MainWindow.t.d.allnotchanged.ToString();
        }

        private void d_Click(object sender, RoutedEventArgs e)
        {
            tb1.Text = MainWindow.t.d.AllDiseases.ToString();
        }

        private void t_Click(object sender, RoutedEventArgs e)
        {
            tb1.Text = MainWindow.t.d.ToString();
        }


        private void a_Click(object sender, RoutedEventArgs e)
        {
            tb1.Text= @"This application applies ID3 algorithms 
for heart diseases data sets . 
In addition it will show them in 
the form of decision tree.
programmed by:
Hamed Mohammadi";
                
        }

      

       

        private void but_Click(object sender, RoutedEventArgs e)
        {
            envrnomnet++;
            repaint();

            
        }

        private void but_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            colors++;
            repaint();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //tb1.Text += "x-";
            x = -10;
            y = 0;
            repaint();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //tb1.Text += "y-";
            y = -10;
            x = 0;
            repaint();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //tb1.Text += "y+";
            y = 10;
            x = 0;
            repaint();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //tb1.Text += "x+";
            x = 10;
            y = 0;
            repaint();
        }

        private void c1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string[] s = MainWindow.t.rulles();
            for (int i = 0; i < 299; i++)
            {
                if (MainWindow.t.nodes[i / 4].name() == "Rubella")
                {
                    if (e.GetPosition(c1).X > MainWindow.t.nodes[i].X_pos && e.GetPosition(c1).X < MainWindow.t.nodes[i].X_pos + 30)
                    {
                        if (e.GetPosition(c1).Y > MainWindow.t.nodes[i].Y_pos && e.GetPosition(c1).Y < MainWindow.t.nodes[i].Y_pos + 75)
                        {
                            tb1.Text = MainWindow.t.nodes[i].fulldetails();
                            if (MainWindow.t.nodes[i].NodeDef == 1 || MainWindow.t.nodes[i].NodeDef == 2) if (MainWindow.t.nodes[i / 2].NodeDef == 0 && MainWindow.t.nodes[i / 4].NodeDef == 0) tb1.Text += ("\r\nRull:\r\n" + s[MainWindow.t.map[i]]);
                            synthesizer.SpeakAsync(MainWindow.t.nodes[i].name());
                            
                        }
                            
                    }
                }
                else
                {
                    if (e.GetPosition(c1).X > MainWindow.t.nodes[i].X_pos && e.GetPosition(c1).X < MainWindow.t.nodes[i].X_pos + 75)
                    {
                        if (e.GetPosition(c1).Y > MainWindow.t.nodes[i].Y_pos && e.GetPosition(c1).Y < MainWindow.t.nodes[i].Y_pos + 30)
                        {
                            tb1.Text = MainWindow.t.nodes[i].fulldetails();
                            if (MainWindow.t.nodes[i].NodeDef == 1 || MainWindow.t.nodes[i].NodeDef == 2 ) if(MainWindow.t.nodes[i/2].NodeDef==0&& MainWindow.t.nodes[i / 4].NodeDef == 0) tb1.Text += ("\r\nRul:\r\n" + s[MainWindow.t.map[i]]);
                            synthesizer.SpeakAsync(MainWindow.t.nodes[i].name());
                        }
                            
                    }
                }
            }
        }

        private void r_Click(object sender, RoutedEventArgs e)
        {
            reset();
            
            
        }
        private void reset()
        {
            MainWindow.t.reset(MainWindow.path + @"\tab.xml", MainWindow.path + @"\tab2.xml");
            colors = 0;
            envrnomnet = 0;
            scale = 1;
            x = 0;
            y = 0;
            //tb1.Text = "" + MainWindow.t.passvalue;
            repaint();
        }

        private void ic_Click(object sender, RoutedEventArgs e)
        {
            tb1.Text = "";
            string[] s = MainWindow.t.IcPredicate().ToArray();
            for (int i = 0; i < s.Length; i++) 
            {
                tb1.Text += (s[i] +".\r\n");
            }
            
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            int i = 1;
            while (i<300)
            {
                MessageBoxResult result = MessageBoxResult.OK;
                if (MainWindow.t.nodes[i].NodeDef == 0)
                {
                    synthesizer.SpeakAsync("Do you have " + MainWindow.t.nodes[i].name() + " ?");
                    result = MessageBox.Show("Do you have " + MainWindow.t.nodes[i].name() + " ?", "Symptom", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes) i = i * 2;
                    if (result == MessageBoxResult.No) i = ((i * 2) + 1);
                }
                else if(MainWindow.t.nodes[i].NodeDef==1)
                {
                    if (MainWindow.t.nodes[i / 4].name() != "Rubella")
                    {
                        synthesizer.SpeakAsync("You may have " + MainWindow.t.nodes[i].name() + " !");
                        MessageBox.Show("You may have " + MainWindow.t.nodes[i].name() + " !", "Disease", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        synthesizer.SpeakAsync("You may have Rubella and in addition " + MainWindow.t.nodes[i].name() + " !");
                        MessageBox.Show("You may Rubella and in addition " + MainWindow.t.nodes[i].name() + " !", "Disease", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    break;
                }
                else if (MainWindow.t.nodes[i].NodeDef == 2)
                {
                    if (MainWindow.t.nodes[i].name() == "Rubella") i = i * 2;
                    else
                    {
                        synthesizer.SpeakAsync("You may have " + MainWindow.t.nodes[i].name() + " and in addition " + MainWindow.t.nodes[i].ic.name() + " !");
                        MessageBox.Show( "You may have " + MainWindow.t.nodes[i].name() + " and in addition "+ MainWindow.t.nodes[i].ic.name()+" !", "Disease", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
                }
                
            }
        }

        private void slideopen_Click(object sender, RoutedEventArgs e)
        {
            slideopen.IsEnabled = false;
            slideopen.Visibility = System.Windows.Visibility.Hidden;
            ThicknessAnimation da = new ThicknessAnimation();
            da.From = new Thickness(-200, 0, 1600, 0);
            da.To = new Thickness(0, 0, 1400, 0);
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            c2.BeginAnimation(Canvas.MarginProperty, da);
            //da.From = new Thickness(0, 0, 0, 0);
            //da.To = new Thickness(200, 0, 0, 0);
            //c1.BeginAnimation(Canvas.MarginProperty, da);
            DoubleAnimation da2 = new DoubleAnimation();
            da2.From = 1;
            da2.To = 0.7;
            da2.Duration = new Duration(TimeSpan.FromSeconds(1));
            c2.BeginAnimation(Canvas.OpacityProperty, da2);
        }

        private void slideclose_Click(object sender, RoutedEventArgs e)
        {
            ThicknessAnimation da = new ThicknessAnimation();
            da.From = new Thickness(0, 0, 1400, 0);
            da.To = new Thickness(-200, 0, 1600, 0);
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            c2.BeginAnimation(Canvas.MarginProperty, da);
            //da.From = new Thickness(200, 0, 0, 0);
            //da.To = new Thickness(0, 0, 0, 0);
            //c1.BeginAnimation(Canvas.MarginProperty, da);
            DoubleAnimation da2 = new DoubleAnimation();
            da2.From = 0.7;
            da2.To = 1;
            da2.Duration = new Duration(TimeSpan.FromSeconds(1));
            c2.BeginAnimation(Canvas.OpacityProperty, da2);
            slideopen.IsEnabled = true;
            slideopen.Visibility = System.Windows.Visibility.Visible;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            commend = "";
            DoubleAnimation da2 = new DoubleAnimation();
            da2.From = 0;
            da2.To = 0.7;
            da2.Duration = new Duration(TimeSpan.FromSeconds(2));
            c3.BeginAnimation(Canvas.OpacityProperty, da2);
            c3.IsEnabled = true;
            c3.Visibility = Visibility.Visible;
            string[] kb = MainWindow.t.ConvertToPredicate();
            foreach (var item in kb)
            {
                commend = ("kbc:\r\n" + item);
                pe.execute("kbc:\r\n" + item );
            }
            kb = MainWindow.t.IcPredicate().ToArray();
            foreach (var item in kb)
            {
                if (item.Contains("Rubella"))
                {
                    commend = ("kbc:\r\n" + item);
                    pe.execute("kbc:\r\n" + item + "\r\n");
                }
                else
                {
                    commend = ("kb:\r\n" + item);
                    pe.execute("kb:\r\n" + item + "\r\n");
                }
            }
            kb = MainWindow.t.IcName().ToArray();
            foreach (var item in kb)
            {
                commend = ("kb:\r\n" + item);
                pe.execute("kb:\r\n" + item);
            }
            KnowledgeBase.Document.TextAlignment = TextAlignment.Left;
            KnowledgeBase.Document.Blocks.Add(new Paragraph(new Bold(new Italic(new Run(pe.kbtostring()) { FontSize = 18 }))));
            c2.Visibility = Visibility.Hidden;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            c3.IsEnabled = false;
            c3.Visibility = Visibility.Hidden;
            c2.Visibility = Visibility.Visible;
            pe.clear();
            WorkingStorege.Document.Blocks.Clear();
            KnowledgeBase.Document.Blocks.Clear();
            yesb.Visibility = Visibility.Hidden;
            nob.Visibility = Visibility.Hidden;
            whyb.Visibility = Visibility.Hidden;
            yesb.IsEnabled = false;
            nob.IsEnabled = false;
            whyb.IsEnabled = false;
            startover.Visibility = Visibility.Hidden;
            startover.IsEnabled = false;
            howb.IsEnabled = false;
            howb.Visibility = Visibility.Hidden;
            whynotb.Visibility = Visibility.Hidden;
            whynotb.IsEnabled = false;
            ques.Text = "";
            numberofQ = 0;
            whn = false;
            whynotbox.Visibility = Visibility.Hidden;
            whynotbox.IsEnabled = false;
            strb.Visibility = Visibility.Visible;
            strb.IsEnabled = true;
            lbl.Visibility = Visibility.Hidden;
            lbl.IsEnabled = false;
        }
        private void ans(int i,bool b)
        {
            if (b)
            {
                pe.execute("assertz(" + slist[i] + ")");
                commend = ("assertz(" + slist[i] + ")");
            }
            else
            {
                pe.execute("assertz(~" + slist[i] + ")");
                commend = ("assertz(~" + slist[i] + ")");
            }

        }
        private void ask(int i)
        {
            string s = "Do you have ";
            int h = slist[i].IndexOf("(");
            s += slist[i].Substring(h + 1,slist[i].Length-(h+2));
            s += " ?";
            synthesizer.SpeakAsync(s);
            ques.Text = s;
        }
        private void strb_Click(object sender, RoutedEventArgs e)
        {
            strb.Visibility = Visibility.Hidden;
            strb.IsEnabled = false;
            yesb.Visibility = Visibility.Visible;
            yesb.IsEnabled = true;
            nob.Visibility = Visibility.Visible;
            nob.IsEnabled = true;
            whyb.Visibility = Visibility.Visible;
            whyb.IsEnabled = true;
            ask(0);
        }

        private void yesb_Click(object sender, RoutedEventArgs e)
        {
            if (whn) whn = false;
            ques.Visibility = Visibility.Visible;
            whybox.Visibility = Visibility.Hidden;
            whybox.IsEnabled = false;
            ans(numberofQ, true);
            WorkingStorege.Document.Blocks.Add(new Paragraph(new Bold(new Italic(new Run(slist[numberofQ]) { FontSize = 25,Foreground= Brushes.Green }))));
            numberofQ++;
            if (numberofQ<slist.Count)
            {
                ask(numberofQ);
            }
            else
            {
                ansermode();
            }
        }
        private void ansermode()
        {
            lbl.Visibility = Visibility.Visible;
            lbl.IsEnabled = true;
            yesb.Visibility = Visibility.Hidden;
            nob.Visibility = Visibility.Hidden;
            whyb.Visibility = Visibility.Hidden;
            yesb.IsEnabled = false;
            nob.IsEnabled = false;
            whyb.IsEnabled = false;
            startover.Visibility = Visibility.Visible;
            startover.IsEnabled = true;
            howb.IsEnabled = true;
            howb.Visibility = Visibility.Visible;
            whynotb.Visibility = Visibility.Visible;
            whynotb.IsEnabled = true;
            whynotbox.Visibility = Visibility.Visible;
            whynotbox.IsEnabled = true;
            commend = "top goal:";
            string result = pe.execute("top goal:");
            pathresulttohow = result;
            int h2 = result.IndexOf(')');
            result = result.Remove(h2);
            int h = result.IndexOf('(');
            result = result.Remove(0, h + 1);
            if (!result.Contains("disease"))
            {
                commend = ("ic(" + result + ")");
                string execute = pe.execute("ic(" + result + ")\r\n");
                commend = execute;
                if (execute.Contains("true"))
                {
                    commend = (result + "(X)");
                    string X = pe.execute(result + "(X)");
                    commend = X;
                    if (!X.Contains("false"))
                    {
                        X = X.Remove(0, 2);
                        string outp = ("you may have " + result + " and in addition " + X + " disease !");
                        synthesizer.SpeakAsync(outp);
                        ques.Text = outp;
                    }
                    else
                    {
                        commend = ("name goal:"+result);
                        X = pe.execute("name goal:" + result);
                        commend = X;
                        X = X.Remove(0, X.IndexOf("(")+1);
                        X = X.Remove(X.IndexOf("("));
                        string outp = ("you may have " + result + " and in addition " + X + " disease !");
                        synthesizer.SpeakAsync(outp);
                        ques.Text = outp;
                    }
                }
                else
                {
                    string outp = ("you may have " + result + " disease !");
                    synthesizer.SpeakAsync(outp);
                    ques.Text = outp;
                }
            }
            else
            {
                string outp = ("you may have " + result + " !");
                synthesizer.SpeakAsync(outp);
                ques.Text = outp;
            }
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            Grid g = (Grid)sender;
            foreach (UIElement item in g.Children)
            {
                if (item.GetType() == typeof(Rectangle))
                {
                    Rectangle el = (Rectangle)item;
                    el.Fill = Brushes.Green;
                    el.StrokeThickness = 2;
                }
            }
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            Grid g = (Grid)sender;
            foreach (UIElement item in g.Children)
            {
                if (item.GetType() == typeof(Rectangle))
                {
                    Rectangle el = (Rectangle)item;
                    el.Fill = Brushes.LightBlue;
                    el.StrokeThickness = 1;
                }
            }
        }

        private void nob_Click(object sender, RoutedEventArgs e)
        {
            if (whn) whn = false;
            ques.Visibility = Visibility.Visible;
            whybox.Visibility = Visibility.Hidden;
            whybox.IsEnabled = false;
            ans(numberofQ, false);
            WorkingStorege.Document.Blocks.Add(new Paragraph(new Bold(new Italic(new Run(slist[numberofQ]) { FontSize = 25, Foreground = Brushes.Red }))));
            numberofQ++;
            if (numberofQ < slist.Count)
            {
                ask(numberofQ);
            }
            else
            {
                ansermode();
            }
        }

        private void Grid_MouseEnter_1(object sender, MouseEventArgs e)
        {
            Grid g = (Grid)sender;
            foreach (UIElement item in g.Children)
            {
                if (item.GetType() == typeof(Rectangle))
                {
                    Rectangle el = (Rectangle)item;
                    el.Fill = Brushes.Red;
                    el.StrokeThickness = 2;
                }
            }
        }

        private void Grid_MouseLeave_1(object sender, MouseEventArgs e)
        {
            Grid g = (Grid)sender;
            foreach (UIElement item in g.Children)
            {
                if (item.GetType() == typeof(Rectangle))
                {
                    Rectangle el = (Rectangle)item;
                    el.Fill = Brushes.LightBlue;
                    el.StrokeThickness = 1;
                }
            }
        }

        private void whyb_Click(object sender, RoutedEventArgs e)
        {
            if (!whn)
            {
                string s = "why:";
                s += slist[numberofQ];
                s = pe.execute(s);
                commend = s;
                ques.Visibility = Visibility.Hidden;
                whybox.Visibility = Visibility.Visible;
                whybox.IsEnabled = true;
                whybox.Document.Blocks.Clear();
                whybox.Document.Blocks.Add(new Paragraph((new Run(s))));
                whn = true;
            }
            else
            {
                ques.Visibility = Visibility.Visible;
                whybox.Visibility = Visibility.Hidden;
                whybox.IsEnabled = false;
                ask(numberofQ);
                whn = false;
            }
        }

        private void startover_Click(object sender, RoutedEventArgs e)
        {
            WorkingStorege.Document.Blocks.Clear();
            pe.reset();
            whynotb.Visibility = Visibility.Hidden;
            whynotb.IsEnabled = false;
            yesb.Visibility = Visibility.Visible;
            nob.Visibility = Visibility.Visible;
            whyb.Visibility = Visibility.Visible;
            yesb.IsEnabled = true;
            nob.IsEnabled = true;
            whyb.IsEnabled = true;
            startover.Visibility = Visibility.Hidden;
            startover.IsEnabled = false;
            howb.IsEnabled = false;
            howb.Visibility = Visibility.Hidden;
            whynotbox.Visibility = Visibility.Hidden;
            whynotbox.IsEnabled = false;
            ques.Text = "";
            numberofQ = 0;
            ask(numberofQ);
            whn = false;
            lbl.Visibility = Visibility.Hidden;
            lbl.IsEnabled = false;
        }

        private void howb_Click(object sender, RoutedEventArgs e)
        {
            commend = ("how:" + pathresulttohow);
            string exresutl = pe.execute("how:" + pathresulttohow);
            WorkingStorege.Document.Blocks.Clear();
            List<string> hold = pe.wstolist();
            foreach (var item in hold)
            {
                if (exresutl.Contains(item))
                {
                    if(item[0]=='~') WorkingStorege.Document.Blocks.Add(new Paragraph(new Bold(new Italic(new Run(item) { FontSize = 25, Foreground = Brushes.Red,Background=Brushes.Gray }))));
                    else WorkingStorege.Document.Blocks.Add(new Paragraph(new Bold(new Italic(new Run(item) { FontSize = 25, Foreground = Brushes.Green, Background = Brushes.Gray }))));
                }
                else
                {
                    if (item[0] == '~') WorkingStorege.Document.Blocks.Add(new Paragraph(new Bold(new Italic(new Run(item) { FontSize = 25, Foreground = Brushes.Red }))));
                    else WorkingStorege.Document.Blocks.Add(new Paragraph(new Bold(new Italic(new Run(item) { FontSize = 25, Foreground = Brushes.Green }))));
                }
            }
        }

        private void whynotb_Click(object sender, RoutedEventArgs e)
        {
            if (!whn)
            {
                whn = true;
                string whynot = whynotbox.Text;
                if (whynot.Length > 0)
                {
                    commend = ("why not:" + whynot);
                    string exresult = pe.execute("why not:" + whynot);
                    ques.IsEnabled = false;
                    ques.Visibility = Visibility.Hidden;
                    whybox.IsEnabled = true;
                    whybox.Visibility = Visibility.Visible;
                    whybox.Document.Blocks.Clear();
                    whybox.Document.Blocks.Add(new Paragraph(new Bold(new Italic(new Run(exresult)))));
                }
            }
            else
            {
                whn = false;
                ques.IsEnabled = true;
                ques.Visibility = Visibility.Visible;
                whybox.IsEnabled = false;
                whybox.Visibility = Visibility.Hidden;
                whybox.Document.Blocks.Clear();
            }
        }

  

        private void newCommand_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            string scommand = newCommand.Text;
            if (scommand.IndexOf('.') != -1)
            {
                commend = scommand;
                commend = pe.execute(scommand);

            }
        }

        private void Grid_MouseEnter_2(object sender, MouseEventArgs e)
        {
            Grid g = (Grid)sender;
            foreach (UIElement item in g.Children)
            {
                if (item.GetType() == typeof(Rectangle))
                {
                    Rectangle el = (Rectangle)item;
                    el.Fill = Brushes.LightGray;
                    el.StrokeThickness = 2;
                }
            }
        }

        private void Grid_MouseLeave_2(object sender, MouseEventArgs e)
        {
            Grid g = (Grid)sender;
            foreach (UIElement item in g.Children)
            {
                if (item.GetType() == typeof(Rectangle))
                {
                    Rectangle el = (Rectangle)item;
                    el.Fill = Brushes.LightBlue;
                    el.StrokeThickness = 1;
                }
            }
        }
    }
}
