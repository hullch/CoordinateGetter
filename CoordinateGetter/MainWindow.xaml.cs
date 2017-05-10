using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

/*
     
     */

namespace CoordinateGetter {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged {
        #region ViewModelProperty: TheImage
        private Image _theImage;
        public Image TheImage {
            get {
                return _theImage;
            }

            set {
                _theImage = value;
                OnPropertyChanged("TheImage");
            }
        }
        #endregion

        StringBuilder csv;

        public MainWindow() {
            csv = new StringBuilder();
            InitializeComponent();
            DataContext = this;

            this.PreviewKeyDown += new KeyEventHandler(MainWindow_PreviewKeyDown);

            TheImage = new Image();
            //Fill in filepath here
            TheImage.Source = new BitmapImage(new Uri(@"filepath of source file"));
            TheImage.Stretch = Stretch.None;
            TheImage.HorizontalAlignment = HorizontalAlignment.Left;

        }

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e) {
            if(e.Key == Key.Space) {
                //Fill in filepath here
                File.WriteAllText(@"filepath of output file", csv.ToString());
            }
        }

        #region INotifiedProperty Block
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        private void ContentControl_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            Point clickPoint = e.GetPosition(TheImage);
            //Console.WriteLine("X: " + clickPoint.X + ", Y: " + clickPoint.Y);
            var x = clickPoint.X.ToString();
            var y = clickPoint.Y.ToString();
            var newLine = string.Format("{0},{1}", x, y);
            csv.AppendLine(newLine);
        }

        

    }
}
