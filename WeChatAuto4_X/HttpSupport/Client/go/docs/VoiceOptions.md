# VoiceOptions

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Voice** | Pointer to **NullableString** | 音色  系统音色，或者 自定义音色（克隆的音色或者自定义生成的音色） | [optional] 
**Model** | Pointer to **NullableString** | 语音合成模型。Qwen-Audio-TTS 系列均支持指令控制、声音复刻与流式输出。  可选值：qwen-audio-3.1-tts-flash（默认，系统音色最丰富）、qwen-audio-3.0-tts-flash、qwen-audio-3.0-tts-plus（官方推荐，适合克隆/设计音色）。 | [optional] 
**LanguageType** | Pointer to **NullableString** | 语言类型。Qwen-Audio-TTS 已不再使用该参数（由音色自身或指令控制决定），保留仅为向后兼容。 | [optional] 
**Instructions** | Pointer to **NullableString** | 指令控制文本。Qwen-Audio-TTS 通过 instruction 参数以自然语言控制方言、情感、语速、音色等合成效果。  系统音色与声音复刻音色均可输入任意指令。 | [optional] 
**Volume** | Pointer to **NullableInt32** | 音量。可选，默认 50，范围 [0, 100]。为 null 时不发送，由 API 使用默认值。 | [optional] 
**Rate** | Pointer to **NullableFloat32** | 语速。可选，默认 1.0，范围 [0.5, 2.0]。为 null 时不发送，交由 instruction 控制，  避免与指令中的语速描述冲突。 | [optional] 
**Pitch** | Pointer to **NullableFloat32** | 音调。可选，默认 1.0，范围 [0.5, 2.0]。为 null 时不发送。 | [optional] 
**Seed** | Pointer to **NullableInt32** | 随机种子。可选，范围 [0, 65535]。相同文本、音色与参数下，相同 seed 可复现相同合成结果。 | [optional] 
**OptimizeInstructions** | Pointer to **bool** | 是否在发送前用LLM进行口语化校核 | [optional] 
**IsStream** | Pointer to **bool** |  | [optional] 

## Methods

### NewVoiceOptions

`func NewVoiceOptions() *VoiceOptions`

NewVoiceOptions instantiates a new VoiceOptions object
This constructor will assign default values to properties that have it defined,
and makes sure properties required by API are set, but the set of arguments
will change when the set of required properties is changed

### NewVoiceOptionsWithDefaults

`func NewVoiceOptionsWithDefaults() *VoiceOptions`

NewVoiceOptionsWithDefaults instantiates a new VoiceOptions object
This constructor will only assign default values to properties that have it defined,
but it doesn't guarantee that properties required by API are set

### GetVoice

`func (o *VoiceOptions) GetVoice() string`

GetVoice returns the Voice field if non-nil, zero value otherwise.

### GetVoiceOk

`func (o *VoiceOptions) GetVoiceOk() (*string, bool)`

GetVoiceOk returns a tuple with the Voice field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetVoice

`func (o *VoiceOptions) SetVoice(v string)`

SetVoice sets Voice field to given value.

### HasVoice

`func (o *VoiceOptions) HasVoice() bool`

HasVoice returns a boolean if a field has been set.

### SetVoiceNil

`func (o *VoiceOptions) SetVoiceNil(b bool)`

 SetVoiceNil sets the value for Voice to be an explicit nil

### UnsetVoice
`func (o *VoiceOptions) UnsetVoice()`

UnsetVoice ensures that no value is present for Voice, not even an explicit nil
### GetModel

`func (o *VoiceOptions) GetModel() string`

GetModel returns the Model field if non-nil, zero value otherwise.

### GetModelOk

`func (o *VoiceOptions) GetModelOk() (*string, bool)`

GetModelOk returns a tuple with the Model field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetModel

`func (o *VoiceOptions) SetModel(v string)`

SetModel sets Model field to given value.

### HasModel

`func (o *VoiceOptions) HasModel() bool`

HasModel returns a boolean if a field has been set.

### SetModelNil

`func (o *VoiceOptions) SetModelNil(b bool)`

 SetModelNil sets the value for Model to be an explicit nil

### UnsetModel
`func (o *VoiceOptions) UnsetModel()`

UnsetModel ensures that no value is present for Model, not even an explicit nil
### GetLanguageType

`func (o *VoiceOptions) GetLanguageType() string`

GetLanguageType returns the LanguageType field if non-nil, zero value otherwise.

### GetLanguageTypeOk

`func (o *VoiceOptions) GetLanguageTypeOk() (*string, bool)`

GetLanguageTypeOk returns a tuple with the LanguageType field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetLanguageType

`func (o *VoiceOptions) SetLanguageType(v string)`

SetLanguageType sets LanguageType field to given value.

### HasLanguageType

`func (o *VoiceOptions) HasLanguageType() bool`

HasLanguageType returns a boolean if a field has been set.

### SetLanguageTypeNil

`func (o *VoiceOptions) SetLanguageTypeNil(b bool)`

 SetLanguageTypeNil sets the value for LanguageType to be an explicit nil

### UnsetLanguageType
`func (o *VoiceOptions) UnsetLanguageType()`

UnsetLanguageType ensures that no value is present for LanguageType, not even an explicit nil
### GetInstructions

`func (o *VoiceOptions) GetInstructions() string`

GetInstructions returns the Instructions field if non-nil, zero value otherwise.

### GetInstructionsOk

`func (o *VoiceOptions) GetInstructionsOk() (*string, bool)`

GetInstructionsOk returns a tuple with the Instructions field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetInstructions

`func (o *VoiceOptions) SetInstructions(v string)`

SetInstructions sets Instructions field to given value.

### HasInstructions

`func (o *VoiceOptions) HasInstructions() bool`

HasInstructions returns a boolean if a field has been set.

### SetInstructionsNil

`func (o *VoiceOptions) SetInstructionsNil(b bool)`

 SetInstructionsNil sets the value for Instructions to be an explicit nil

### UnsetInstructions
`func (o *VoiceOptions) UnsetInstructions()`

UnsetInstructions ensures that no value is present for Instructions, not even an explicit nil
### GetVolume

`func (o *VoiceOptions) GetVolume() int32`

GetVolume returns the Volume field if non-nil, zero value otherwise.

### GetVolumeOk

`func (o *VoiceOptions) GetVolumeOk() (*int32, bool)`

GetVolumeOk returns a tuple with the Volume field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetVolume

`func (o *VoiceOptions) SetVolume(v int32)`

SetVolume sets Volume field to given value.

### HasVolume

`func (o *VoiceOptions) HasVolume() bool`

HasVolume returns a boolean if a field has been set.

### SetVolumeNil

`func (o *VoiceOptions) SetVolumeNil(b bool)`

 SetVolumeNil sets the value for Volume to be an explicit nil

### UnsetVolume
`func (o *VoiceOptions) UnsetVolume()`

UnsetVolume ensures that no value is present for Volume, not even an explicit nil
### GetRate

`func (o *VoiceOptions) GetRate() float32`

GetRate returns the Rate field if non-nil, zero value otherwise.

### GetRateOk

`func (o *VoiceOptions) GetRateOk() (*float32, bool)`

GetRateOk returns a tuple with the Rate field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetRate

`func (o *VoiceOptions) SetRate(v float32)`

SetRate sets Rate field to given value.

### HasRate

`func (o *VoiceOptions) HasRate() bool`

HasRate returns a boolean if a field has been set.

### SetRateNil

`func (o *VoiceOptions) SetRateNil(b bool)`

 SetRateNil sets the value for Rate to be an explicit nil

### UnsetRate
`func (o *VoiceOptions) UnsetRate()`

UnsetRate ensures that no value is present for Rate, not even an explicit nil
### GetPitch

`func (o *VoiceOptions) GetPitch() float32`

GetPitch returns the Pitch field if non-nil, zero value otherwise.

### GetPitchOk

`func (o *VoiceOptions) GetPitchOk() (*float32, bool)`

GetPitchOk returns a tuple with the Pitch field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetPitch

`func (o *VoiceOptions) SetPitch(v float32)`

SetPitch sets Pitch field to given value.

### HasPitch

`func (o *VoiceOptions) HasPitch() bool`

HasPitch returns a boolean if a field has been set.

### SetPitchNil

`func (o *VoiceOptions) SetPitchNil(b bool)`

 SetPitchNil sets the value for Pitch to be an explicit nil

### UnsetPitch
`func (o *VoiceOptions) UnsetPitch()`

UnsetPitch ensures that no value is present for Pitch, not even an explicit nil
### GetSeed

`func (o *VoiceOptions) GetSeed() int32`

GetSeed returns the Seed field if non-nil, zero value otherwise.

### GetSeedOk

`func (o *VoiceOptions) GetSeedOk() (*int32, bool)`

GetSeedOk returns a tuple with the Seed field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetSeed

`func (o *VoiceOptions) SetSeed(v int32)`

SetSeed sets Seed field to given value.

### HasSeed

`func (o *VoiceOptions) HasSeed() bool`

HasSeed returns a boolean if a field has been set.

### SetSeedNil

`func (o *VoiceOptions) SetSeedNil(b bool)`

 SetSeedNil sets the value for Seed to be an explicit nil

### UnsetSeed
`func (o *VoiceOptions) UnsetSeed()`

UnsetSeed ensures that no value is present for Seed, not even an explicit nil
### GetOptimizeInstructions

`func (o *VoiceOptions) GetOptimizeInstructions() bool`

GetOptimizeInstructions returns the OptimizeInstructions field if non-nil, zero value otherwise.

### GetOptimizeInstructionsOk

`func (o *VoiceOptions) GetOptimizeInstructionsOk() (*bool, bool)`

GetOptimizeInstructionsOk returns a tuple with the OptimizeInstructions field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetOptimizeInstructions

`func (o *VoiceOptions) SetOptimizeInstructions(v bool)`

SetOptimizeInstructions sets OptimizeInstructions field to given value.

### HasOptimizeInstructions

`func (o *VoiceOptions) HasOptimizeInstructions() bool`

HasOptimizeInstructions returns a boolean if a field has been set.

### GetIsStream

`func (o *VoiceOptions) GetIsStream() bool`

GetIsStream returns the IsStream field if non-nil, zero value otherwise.

### GetIsStreamOk

`func (o *VoiceOptions) GetIsStreamOk() (*bool, bool)`

GetIsStreamOk returns a tuple with the IsStream field if it's non-nil, zero value otherwise
and a boolean to check if the value has been set.

### SetIsStream

`func (o *VoiceOptions) SetIsStream(v bool)`

SetIsStream sets IsStream field to given value.

### HasIsStream

`func (o *VoiceOptions) HasIsStream() bool`

HasIsStream returns a boolean if a field has been set.


[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


