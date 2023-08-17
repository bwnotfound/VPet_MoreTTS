# VPet插件: 更多的TTS(MoreTTS)

## 介绍

平台为Steam虚拟桌宠模拟器(VPet_Simulator)

本插件依托于 https://huggingface.co/spaces/ikechan8370/vits-uma-genshin-honkai ，使用了其中的TTS模型，侵权删。

本插件属于练手，本来是想做个Atri的TTS，无奈技术不够，没有合适满意的Atri TTS模型，所以做了个这个，算是提前练个手。

如果你有Atri的TTS模型，非常欢迎私信我的B站账号[蓝白bw](https://space.bilibili.com/107433411)，我将速速将其移植到VPet中。

如果你有什么别的想法，可以在Issue里面提，我会酌情添加功能（当然比较懒）

目前没有制作Mod的其他语言翻译，建议在中文环境下使用（当然产生的文本你要看是中日混合还是纯净的中文/日语，如果是后者，直接Mod设置选中对应语言即可）

## 使用方法

在Steam上搜索MoreTTS并订阅，然后在VPet的插件设置里面启用MoreTTS插件。在启动插件并重启后，VPet会提示代码风险，这时候去Mod管理把MoreTTS下面的红框点了就行。

Mod设置中，你可以选择使用什么语言，注意，中日混用时需要用类似[ZH]亚托莉[ZH][JA]孤独な心を抱える少年とロボットの少女が出会[JA]的格式输出文字，具体可以到开发控制台里面试一下。

人物选择时，你可以滚轮慢慢看，也可以直接在选择框中输入对应的汉字，然后再点开选择框就可以看到匹配的人物了。

所有音频都会在根目录下的cache\moretts_voice文件夹里面，如果大了，直接全部删掉就行。

合成速度有点慢，一句10字左右的话需要3s以上，具体可以到开发控制台里面尝试延迟，不过由于有缓存，所以说过了一次就不会再卡了。
建议打开cache\moretts_voice文件夹然后合成一句话看有无文件生成。

语音播放上暂时没怎么测试，能跑就行。

注意语言合成最多会进行3次网络请求，请求失败后我也不知道会发生什么，再加上HuggingFace可能得玄学网络，所以本插件目前见仁见智，可能日常使用易出bug

待续...

## Acknowledgement

https://github.com/LorisYounger/VPet

https://github.com/LorisYounger/VPet.Plugin.Demo
