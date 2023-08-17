using LinePutScript.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPet.Plugin.MoreTTS
{
    public class Setting
    {

        /// <summary>
        /// 讲述人
        /// </summary>
        [Line]
        public string Speaker { get; set; } = "派蒙";
        /// <summary>
        /// 语言
        /// </summary>
        [Line]
        public string Language { get; set; } = "中文";
        /// <summary>
        /// 启用MoreTTS
        /// </summary>
        [Line]
        public bool Enable { get; set; } = true;

        /// <summary>
        /// 控制感情变化程度
        /// </summary>
        [Line]
        public Double NoiseScale { get; set; } = 0.6;

        /// <summary>
        /// 控制音素发音长度
        /// </summary>
        [Line]
        public Double NoiseScaleW { get; set; } = 0.668;

        /// <summary>
        /// 控制整体语速
        /// </summary>
        [Line]
        public Double LengthScale { get; set; } = 1.1;

        public String FormatString()
        {
            return $"{Speaker}_{Language}_{NoiseScale}_{NoiseScaleW}_{LengthScale}";
        }
    }
}
