﻿using LinePutScript.Converter;
using System;
using System.Collections.Generic;
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
using VPet_Simulator.Core;

namespace VPet.Plugin.MoreTTS
{
    /// <summary>
    /// winSetting.xaml 的交互逻辑
    /// </summary>
    public partial class winSetting : Window
    {
        TTSMain tts;
        public winSetting(TTSMain tts)
        {
            InitializeComponent();
            this.tts = tts;
            SwitchOn.IsChecked = tts.Set.Enable;
            VolumeSilder.Value = tts.MW.Main.PlayVoiceVolume * 100;
            Speakers.Text = tts.Set.Speaker;
            Language.Text = tts.Set.Language;
            NoiseScaleSlider.Value = tts.Set.NoiseScale;
            NoiseScaleWSlider.Value = tts.Set.NoiseScale;
            LengthScaleSilder.Value = tts.Set.LengthScale;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            tts.winSetting = null;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (tts.Set.Enable != SwitchOn.IsChecked.Value)
            {
                if (SwitchOn.IsChecked.Value)
                    tts.MW.Main.OnSay += tts.Main_OnSay;
                else
                    tts.MW.Main.OnSay -= tts.Main_OnSay;
                tts.Set.Enable = SwitchOn.IsChecked.Value;
            }
            tts.Set.Speaker = Speakers.Text;
            tts.Set.NoiseScale = NoiseScaleSlider.Value;
            tts.Set.NoiseScale = NoiseScaleWSlider.Value;
            tts.Set.LengthScale = LengthScaleSilder.Value;
            tts.Set.Language = Language.Text;
            tts.MW.Main.PlayVoiceVolume = VolumeSilder.Value / 100;
            tts.MW.Set.Remove("MoreTTS");
            tts.MW.Set.Add(LPSConvert.SerializeObject(tts.Set, "MoreTTS"));
            Close();
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            //tts.MW.Main.PlayVoiceVolume = VolumeSilder.Value / 100;
            //tts.MW.Main.PlayVoice(Speakers.Text, "测试语音", tts.Set.NoiseScale, tts.Set.NoiseScale, tts.Set.LengthScale);
        }
    }
}
