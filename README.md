# VPet插件: 更多的TTS(MoreTTS)

## 介绍

平台为Steam虚拟桌宠模拟器(VPet_Simulator)

本插件依托于 https://huggingface.co/spaces/ikechan8370/vits-uma-genshin-honkai ，使用了其中的TTS模型，侵权删。

本插件属于练手，本来是想做个Atri的TTS，无奈技术不够，没有合适满意的Atri TTS模型，所以做了个这个，算是提前练个手。

如果你有Atri的TTS模型，非常欢迎私信我的B站账号[蓝白bw](https://space.bilibili.com/107433411)，我将速速将其移植到VPet中。

如果你有什么别的想法，可以在Issue里面提，我会酌情添加功能（当然比较懒）

目前没有制作Mod的其他语言翻译，建议在中文环境下使用（当然产生的文本你要看是中日混合还是纯净的中文/日语，如果是后者，直接Mod设置选中对应语言即可）

## 使用方法

注意(以下提到的将会在以后进行改进)：
	1.	语音合成由于调用的是HuggingFace的API，所以延迟可能会很大，请耐心等候，也可以到cache\moretts_audio里面看有无新增音频
	2.	文字上限是100
	3.	合成延迟目前约为4字符/s


在Steam上搜索MoreTTS并订阅，然后在VPet的插件设置里面启用MoreTTS插件。在启动插件并重启后，VPet会提示代码风险，这时候去Mod管理把MoreTTS下面的红框点了就行。

Mod设置中，你可以选择使用什么语言，注意，中日混用时需要用类似[ZH]亚托莉[ZH][JA]孤独な心を抱える少年とロボットの少女が出会[JA]的格式输出文字，具体可以到开发控制台里面试一下。

人物选择时，你可以滚轮慢慢看，也可以直接在选择框中输入对应的汉字，然后再点开选择框就可以看到匹配的人物了。

所有音频都会在根目录下的cache\moretts_voice文件夹里面，如果大了，直接全部删掉就行。

待续...

## Acknowledgement

https://github.com/LorisYounger/VPet

https://github.com/LorisYounger/VPet.Plugin.Demo
