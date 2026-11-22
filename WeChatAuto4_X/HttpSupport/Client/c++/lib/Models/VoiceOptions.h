
/*
 * VoiceOptions.h
 *
 * 
 */

#ifndef TINY_CPP_CLIENT_VoiceOptions_H_
#define TINY_CPP_CLIENT_VoiceOptions_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 
 *
 *  \ingroup Models
 *
 */

class VoiceOptions{
public:

    /*! \brief Constructor.
	 */
    VoiceOptions();
    VoiceOptions(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~VoiceOptions();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 音色  系统音色，或者 自定义音色（克隆的音色或者自定义生成的音色）
	 */
	std::string getVoice();

	/*! \brief Set 音色  系统音色，或者 自定义音色（克隆的音色或者自定义生成的音色）
	 */
	void setVoice(std::string voice);
	/*! \brief Get 语音合成模型。Qwen-Audio-TTS 系列均支持指令控制、声音复刻与流式输出。  可选值：qwen-audio-3.1-tts-flash（默认，系统音色最丰富）、qwen-audio-3.0-tts-flash、qwen-audio-3.0-tts-plus（官方推荐，适合克隆/设计音色）。
	 */
	std::string getModel();

	/*! \brief Set 语音合成模型。Qwen-Audio-TTS 系列均支持指令控制、声音复刻与流式输出。  可选值：qwen-audio-3.1-tts-flash（默认，系统音色最丰富）、qwen-audio-3.0-tts-flash、qwen-audio-3.0-tts-plus（官方推荐，适合克隆/设计音色）。
	 */
	void setModel(std::string model);
	/*! \brief Get 语言类型。Qwen-Audio-TTS 已不再使用该参数（由音色自身或指令控制决定），保留仅为向后兼容。
	 */
	std::string getLanguageType();

	/*! \brief Set 语言类型。Qwen-Audio-TTS 已不再使用该参数（由音色自身或指令控制决定），保留仅为向后兼容。
	 */
	void setLanguageType(std::string languageType);
	/*! \brief Get 指令控制文本。Qwen-Audio-TTS 通过 instruction 参数以自然语言控制方言、情感、语速、音色等合成效果。  系统音色与声音复刻音色均可输入任意指令。
	 */
	std::string getInstructions();

	/*! \brief Set 指令控制文本。Qwen-Audio-TTS 通过 instruction 参数以自然语言控制方言、情感、语速、音色等合成效果。  系统音色与声音复刻音色均可输入任意指令。
	 */
	void setInstructions(std::string instructions);
	/*! \brief Get 音量。可选，默认 50，范围 [0, 100]。为 null 时不发送，由 API 使用默认值。
	 */
	int getVolume();

	/*! \brief Set 音量。可选，默认 50，范围 [0, 100]。为 null 时不发送，由 API 使用默认值。
	 */
	void setVolume(int volume);
	/*! \brief Get 语速。可选，默认 1.0，范围 [0.5, 2.0]。为 null 时不发送，交由 instruction 控制，  避免与指令中的语速描述冲突。
	 */
	float getRate();

	/*! \brief Set 语速。可选，默认 1.0，范围 [0.5, 2.0]。为 null 时不发送，交由 instruction 控制，  避免与指令中的语速描述冲突。
	 */
	void setRate(float rate);
	/*! \brief Get 音调。可选，默认 1.0，范围 [0.5, 2.0]。为 null 时不发送。
	 */
	float getPitch();

	/*! \brief Set 音调。可选，默认 1.0，范围 [0.5, 2.0]。为 null 时不发送。
	 */
	void setPitch(float pitch);
	/*! \brief Get 随机种子。可选，范围 [0, 65535]。相同文本、音色与参数下，相同 seed 可复现相同合成结果。
	 */
	int getSeed();

	/*! \brief Set 随机种子。可选，范围 [0, 65535]。相同文本、音色与参数下，相同 seed 可复现相同合成结果。
	 */
	void setSeed(int seed);
	/*! \brief Get 是否在发送前用LLM进行口语化校核
	 */
	bool isOptimizeInstructions();

	/*! \brief Set 是否在发送前用LLM进行口语化校核
	 */
	void setOptimizeInstructions(bool optimizeInstructions);
	/*! \brief Get 
	 */
	bool isIsStream();

	/*! \brief Set 
	 */
	void setIsStream(bool isStream);


    private:
    std::string voice{};
    std::string model{};
    std::string languageType{};
    std::string instructions{};
    int volume{};
    float rate{};
    float pitch{};
    int seed{};
    bool optimizeInstructions{};
    bool isStream{};
};
}

#endif /* TINY_CPP_CLIENT_VoiceOptions_H_ */
