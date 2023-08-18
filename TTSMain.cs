using LinePutScript.Converter;
using LinePutScript.Localization.WPF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using VPet_Simulator.Core;
using VPet_Simulator.Windows.Interface;
using LinePutScript;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using System.Threading;

namespace VPet.Plugin.MoreTTS
{
    public class TTSMain : MainPlugin
    {
        public string CacheDir = GraphCore.CachePath + @"\moretts_voice";
        public string ttsUrl = "https://bwnotfound-vits-uma-genshin-honkai.hf.space";
        public Setting Set;
        public TTSMain(IMainWindow mainwin) : base(mainwin)
        {
        }

        public override void LoadPlugin()
        {
            var line = MW.Set.FindLine("MoreTTS");
            if (line == null)
            {
                Set = new Setting();
            }
            else
            {
                Set = LPSConvert.DeserializeObject<Setting>(line);
            }
            if (!Directory.Exists(CacheDir))
                Directory.CreateDirectory(CacheDir);
            if (Set.Enable)
            {
                MW.Main.OnSay += MainOnSay;
            }

            MenuItem modset = MW.Main.ToolBar.MenuMODConfig;
            modset.Visibility = Visibility.Visible;
            var menuItem = new MenuItem()
            {
                Header = "MoreTTS",
                HorizontalContentAlignment = HorizontalAlignment.Center,
            };
            menuItem.Click += (s, e) => { Setting(); };
            modset.Items.Add(menuItem);
        }

        public void MainOnSay_Parameter(string saythings, string Language,
                                string Speaker, Double NoiseScale,
                                Double NoiseScaleW, Double LengthScale)
        {//说话语音
            var path = CacheDir + $"\\{Sub.GetHashCode($"{Language}_{Speaker}_{NoiseScale}_{NoiseScaleW}_{LengthScale}_{saythings}"):X}.mp3";
            if (!File.Exists(path))
            {
                JObject req_data = new JObject();
                JArray data = new JArray
                {
                    saythings,
                    Language,
                    Speaker,
                    NoiseScale,
                    NoiseScaleW,
                    LengthScale,
                };
                req_data["data"] = data;
                string sendData = JsonConvert.SerializeObject(req_data);
                bool isSuccess = false;
                for (int i = 0; i < 3 && !isSuccess; i++)
                {
                    try
                    {
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{ttsUrl}/api/generate");
                        byte[] buf = Encoding.GetEncoding("UTF-8").GetBytes(sendData);
                        request.Method = "POST";
                        request.ContentType = "application/json";
                        request.ContentLength = buf.Length;
                        Stream writer = request.GetRequestStream();
                        writer.Write(buf, 0, buf.Length);
                        writer.Close();

                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Stream reader = response.GetResponseStream();
                        StreamReader sr = new StreamReader(reader, Encoding.UTF8);
                        string res = sr.ReadToEnd();
                        sr.Close();
                        reader.Close();
                        response.Close();
                        JObject res_data = JObject.Parse(res);
                        string url = $"{ttsUrl}/file={res_data["data"][1]["name"]}";
                        WebClient webClient = new WebClient();
                        webClient.DownloadFile(url, path);
                        isSuccess = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }
                }
                if (!isSuccess)
                {
                    AssertMessage("三次尝试生成语音失败，请重试或者排查网络原因等，也可能是bug");
                }
                //#               // 将wav文件转换为mp3文件
                //                string mp3Path = path.Replace(".wav", ".mp3");
                //                string command = $"-i {path} -acodec libmp3lame -y {mp3Path}";
                //                System.Diagnostics.Process p = new System.Diagnostics.Process();
                //                p.StartInfo.FileName = "ffmpeg.exe";

            }
            MW.Main.PlayVoice(new Uri(path));
        }

        public void MainOnSay(string saythings)
        {//说话语音
            MainOnSay_Parameter(saythings,
                Set.Language,
                Set.Speaker,
                Set.NoiseScale,
                Set.NoiseScaleW,
                Set.LengthScale);
        }

        public AssertWindow assertWindow;

        public void AssertMessage(string message)
        {
            
            if (assertWindow == null)
            {
                Thread thread = new Thread(() =>
                {
                    assertWindow = new AssertWindow(message, this);
                    assertWindow.Show();
                    System.Windows.Threading.Dispatcher.Run();
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
            else
            {
                assertWindow.Topmost = true;
            }
            
        }

        public winSetting winSetting;
        public override void Setting()
        {
            if (winSetting == null)
            {
                winSetting = new winSetting(this);
                winSetting.Show();
            }
            else
            {
                winSetting.Topmost = true;
            }
        }
        public override string PluginName => "MoreTTS";
    }
}
