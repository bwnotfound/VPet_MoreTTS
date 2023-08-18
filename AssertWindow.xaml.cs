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
using System.Windows.Shapes;

namespace VPet.Plugin.MoreTTS
{
    /// <summary>
    /// AssertWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AssertWindow : Window
    {
        TTSMain tts;
        public AssertWindow(string assertText, TTSMain tts)
        {
            InitializeComponent();
            TextBox.Text = assertText;
            this.tts = tts;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            tts.assertWindow = null;
        }
    }
}
