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

namespace SpellChecker
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
    private void buttonOpenFile_Click(object sender, RoutedEventArgs e)
    {
      Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
      dialog.ShowDialog();
      FileUtils fileUtils = new FileUtils(dialog.FileName);
      if (fileUtils.OpenFile())
      {
        Tuple<string, string> text_dict = fileUtils.ParseFile();
        txtBoxText.Text = text_dict.Item1;
        txtBoxDict.Text = text_dict.Item2;
      }
      else
        MessageBox.Show("Invalid file!");

    }

    private void buttonStart_Click(object sender, RoutedEventArgs e)
    {
      var spellingCorrector = new SpellingCorrector(txtBoxText.Text.Trim(), txtBoxDict.Text);
      txtBoxCorrected.Text = spellingCorrector.SpellCheker();
    }
  }
}
