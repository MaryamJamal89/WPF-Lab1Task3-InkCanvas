using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Forms;
using System.IO;
using RadioButton = System.Windows.Controls.RadioButton;

namespace lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void brushColor(object sender, RoutedEventArgs e)
        {
            switch ((sender as RadioButton).Content.ToString())
            {
                case "Red":
                    InkCanvas.DefaultDrawingAttributes.Color = Colors.Red;
                    break;
                case "Blue":
                    InkCanvas.DefaultDrawingAttributes.Color = Colors.Blue;
                    break;
                case "Green":
                    InkCanvas.DefaultDrawingAttributes.Color = Colors.Green;
                    break;
            }
        }

        private void inkCanvasMode(object sender, RoutedEventArgs e)
        {
            switch ((sender as RadioButton).Content.ToString())
            {
                case "Ink":
                    InkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case "Select":
                    InkCanvas.EditingMode = InkCanvasEditingMode.Select;
                    break;
                case "EraseByPoint":
                    InkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
                    break;
                case "EraseByStroke":
                    InkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                    break;
            }
        }

        private void brushShape(object sender, RoutedEventArgs e)
        {
            switch ((sender as RadioButton).Content.ToString())
            {
                case "Ellipse":
                    InkCanvas.DefaultDrawingAttributes.StylusTip = StylusTip.Ellipse;
                    break;
                case "Rectangle":
                    InkCanvas.DefaultDrawingAttributes.StylusTip = StylusTip.Rectangle;
                    break;
            }
        }

        private void brushSize(object sender, RoutedEventArgs e)
        {
            switch ((sender as RadioButton).Content.ToString())
            {
                case "Small":
                    InkCanvas.DefaultDrawingAttributes.Width = 10;
                    InkCanvas.DefaultDrawingAttributes.Height = 10;
                    break;
                case "Medium":
                    InkCanvas.DefaultDrawingAttributes.Width = 20;
                    InkCanvas.DefaultDrawingAttributes.Height = 20;
                    break;
                case "Larg":
                    InkCanvas.DefaultDrawingAttributes.Width = 30;
                    InkCanvas.DefaultDrawingAttributes.Height = 30;
                    break;
            }
        }

        private void brnNew(object sender, RoutedEventArgs e)
        {
            string message = $"Save the changes?";
            string title = "New Paint";
            MessageBoxButton buttons = MessageBoxButton.YesNoCancel;
            DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, (MessageBoxButtons)buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                btnSave(sender, e);
            }
            else if (result == System.Windows.Forms.DialogResult.No)
            {
                InkCanvas.Strokes.Clear();
            }
            else
            {
                //cancel
            }
        }

        private void btnSave(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "InkCanvas Format|*.sandy";
            saveFile.Title = "Save InkConvas File";
            saveFile.ShowDialog();
            if (saveFile.FileName == "") return;
            FileStream fs = File.Open(saveFile.FileName, FileMode.OpenOrCreate);
            InkCanvas.Strokes.Save(fs);
            fs.Close();
        }

        private void btnLoad(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "InkCanvas Format(.sandy)|*.sandy";
            openFile.Title = "Load InkConvas File";
            openFile.DefaultExt = "sandy";
            openFile.ShowDialog();
            if (openFile.FileName == "") return;
            FileStream fs = File.Open(openFile.FileName, FileMode.Open);
            StrokeCollection sc = new StrokeCollection(fs);
            InkCanvas.Strokes = sc;
            fs.Close();
        }

        private void btnCopy(object sender, RoutedEventArgs e)
        {
            if (this.InkCanvas.GetSelectedStrokes().Count > 0)
                this.InkCanvas.CopySelection();
        }

        private void btnCut(object sender, RoutedEventArgs e)
        {
            if (this.InkCanvas.GetSelectedStrokes().Count > 0)
                this.InkCanvas.CutSelection();
        }

        private void btnPaste(object sender, RoutedEventArgs e)
        {
            if (this.InkCanvas.CanPaste())
                this.InkCanvas.Paste();
        }

        private void ColorSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Color color = Color.FromRgb((byte)Red.Value, (byte)Green.Value, (byte)Blue.Value);
            InkCanvas.DefaultDrawingAttributes.Color = color;
            prev.Background = new SolidColorBrush(color);
        }
    }
}
