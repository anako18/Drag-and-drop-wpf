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

namespace dragdrop_wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Canvas[] canvases;

        int pp;

        int current_panel;

        public MainWindow()
        {
            InitializeComponent();
            canvases = new Canvas[] {panel1, panel2, panel3, panel4 };
            rb1.Tag = 0;
            rb2.Tag = 1;
            rb3.Tag = 2;
            rb4.Tag = 3;
            panel1.Tag = 0;
            panel2.Tag = 1;
            panel3.Tag = 2;
            panel4.Tag = 3;
            current_panel = 0;
            rb1.Click += new RoutedEventHandler(rb1_Checked);
            rb2.Click += new RoutedEventHandler(rb1_Checked);
            rb3.Click += new RoutedEventHandler(rb1_Checked);
            rb4.Click += new RoutedEventHandler(rb1_Checked);
        }

        //Colorbox в меню
        private void MenuItem_Click_color(object sender, RoutedEventArgs e)
        {
            colorDialog1.ShowDialog();
            foreach (Label l in canvases[current_panel].Children)
            {
                l.Background = new SolidColorBrush(WFColorToWPFColor(colorDialog1.Color));
            }

        }

        //Exit в меню
        private void MenuItem_Click_exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //Переключение радиокнопок
        private void rb1_Checked(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 4; i++)
            foreach (Label l in canvases[i].Children)
            {
                l.IsEnabled = false;
            }

            current_panel = Convert.ToInt32((sender as RadioButton).Tag);

            foreach (Control c in canvases[current_panel].Children)
            {
                c.IsEnabled = true;
            }

        }

        private void label1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Label s = sender as Label;
            (s.Parent as Canvas).Children.Remove(s);
            DragDrop.DoDragDrop(s, s, DragDropEffects.All);
            Canvas sed = (s.Parent as Canvas);
        }

        private void panel1_Drop(object sender, DragEventArgs e)
        {
           
            Label src = e.Data.GetData(typeof(Label)) as Label;
            Point p = e.GetPosition(canvases[pp]);
            (canvases[pp] as Canvas).Children.Add(src);

            Canvas.SetLeft(src, p.X - src.ActualWidth / 2);
            Canvas.SetTop(src, p.Y - src.ActualHeight / 2);
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
            pp = (int)(sender as Canvas).Tag;
        }

        //======ColorDialog

        System.Windows.Forms.ColorDialog colorDialog1 = new System.Windows.Forms.ColorDialog();

        Color WFColorToWPFColor(System.Drawing.Color c)
        {
            return Color.FromArgb(c.A, c.R, c.G, c.B);
        }

        System.Drawing.Color WPFColorToWFColor(Color c)
        {
            return System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B);
        }

    }




}
