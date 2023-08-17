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

namespace VPet.Plugin.MoreTTS
{
    public class TTSMain : MainPlugin
    {
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
            if (!Directory.Exists(GraphCore.CachePath + @"\moretts_voice"))
                Directory.CreateDirectory(GraphCore.CachePath + @"\moretts_voice");
            if (Set.Enable)
                MW.Main.OnSay += Main_OnSay;

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

        public void Main_OnSay(string saythings)
        {//说话语音
            var path = GraphCore.CachePath + $"\\moretts_voice\\{Sub.GetHashCode($"{Set.FormatString()}_{saythings}"):X}.mp3";
            if (!File.Exists(path))
            {
                JObject req_data = new JObject();
                JArray data = new JArray
                {
                    saythings,
                    Set.Language,
                    Set.Speaker,
                    Set.NoiseScale,
                    Set.NoiseScaleW,
                    Set.LengthScale
                };
                req_data["data"] = data;
                string sendData = JsonConvert.SerializeObject(req_data);
                bool isSuccess = false;
                for (int i = 0; i < 3 || !isSuccess; i++)
                {
                    try
                    {
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ikechan8370-vits-uma-genshin-honkai.hf.space/api/generate");
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
                        string url = $"https://ikechan8370-vits-uma-genshin-honkai.hf.space/file={res_data["data"][1]["name"]}";
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
                    throw new Exception("语音合成失败，请检查网络");
                }
//#               // 将wav文件转换为mp3文件
//                string mp3Path = path.Replace(".wav", ".mp3");
//                string command = $"-i {path} -acodec libmp3lame -y {mp3Path}";
//                System.Diagnostics.Process p = new System.Diagnostics.Process();
//                p.StartInfo.FileName = "ffmpeg.exe";

            }
            MW.Main.PlayVoice(new Uri(path));
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
        public override string PluginName => "Atri";
    }
}
