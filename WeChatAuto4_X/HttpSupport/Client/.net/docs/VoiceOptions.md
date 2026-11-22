# WeChatAuto.Client.Model.VoiceOptions

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Voice** | **string** | 音色  系统音色，或者 自定义音色（克隆的音色或者自定义生成的音色） | [optional] 
**Model** | **string** | 语音合成模型。Qwen-Audio-TTS 系列均支持指令控制、声音复刻与流式输出。  可选值：qwen-audio-3.1-tts-flash（默认，系统音色最丰富）、qwen-audio-3.0-tts-flash、qwen-audio-3.0-tts-plus（官方推荐，适合克隆/设计音色）。 | [optional] 
**LanguageType** | **string** | 语言类型。Qwen-Audio-TTS 已不再使用该参数（由音色自身或指令控制决定），保留仅为向后兼容。 | [optional] 
**Instructions** | **string** | 指令控制文本。Qwen-Audio-TTS 通过 instruction 参数以自然语言控制方言、情感、语速、音色等合成效果。  系统音色与声音复刻音色均可输入任意指令。 | [optional] 
**Volume** | **int?** | 音量。可选，默认 50，范围 [0, 100]。为 null 时不发送，由 API 使用默认值。 | [optional] 
**Rate** | **float?** | 语速。可选，默认 1.0，范围 [0.5, 2.0]。为 null 时不发送，交由 instruction 控制，  避免与指令中的语速描述冲突。 | [optional] 
**Pitch** | **float?** | 音调。可选，默认 1.0，范围 [0.5, 2.0]。为 null 时不发送。 | [optional] 
**Seed** | **int?** | 随机种子。可选，范围 [0, 65535]。相同文本、音色与参数下，相同 seed 可复现相同合成结果。 | [optional] 
**OptimizeInstructions** | **bool** | 是否在发送前用LLM进行口语化校核 | [optional] 
**IsStream** | **bool** |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

