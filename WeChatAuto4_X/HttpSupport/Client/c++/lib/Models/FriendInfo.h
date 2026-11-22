
/*
 * FriendInfo.h
 *
 * 个人信息
 */

#ifndef TINY_CPP_CLIENT_FriendInfo_H_
#define TINY_CPP_CLIENT_FriendInfo_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"
#include "ChatType.h"
#include "FriendSearchResultEnums.h"
#include "Image.h"
#include <list>

namespace Tiny {


/*! \brief 个人信息
 *
 *  \ingroup Models
 *
 */

class FriendInfo{
public:

    /*! \brief Constructor.
	 */
    FriendInfo();
    FriendInfo(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~FriendInfo();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 昵称
	 */
	std::string getNickName();

	/*! \brief Set 昵称
	 */
	void setNickName(std::string nickName);
	/*! \brief Get 备注名
	 */
	std::string getMemoName();

	/*! \brief Set 备注名
	 */
	void setMemoName(std::string memoName);
	/*! \brief Get 地区,建议仅供参考
	 */
	std::string getArea();

	/*! \brief Set 地区,建议仅供参考
	 */
	void setArea(std::string area);
	/*! \brief Get 标签
	 */
	std::list<std::string> getLable();

	/*! \brief Set 标签
	 */
	void setLable(std::list<std::string> lable);
	/*! \brief Get 共同群数量
	 */
	std::string getSameGroupNumber();

	/*! \brief Set 共同群数量
	 */
	void setSameGroupNumber(std::string sameGroupNumber);
	/*! \brief Get 个性签名
	 */
	std::string getSignature();

	/*! \brief Set 个性签名
	 */
	void setSignature(std::string signature);
	/*! \brief Get 来源
	 */
	std::string getSource();

	/*! \brief Set 来源
	 */
	void setSource(std::string source);
	/*! \brief Get 微信ID
	 */
	std::string getWxId();

	/*! \brief Set 微信ID
	 */
	void setWxId(std::string wxId);
	/*! \brief Get 头像路径
	 */
	std::string getAvatarPath();

	/*! \brief Set 头像路径
	 */
	void setAvatarPath(std::string avatarPath);
	/*! \brief Get 
	 */
	Image getAvatarImage();

	/*! \brief Set 
	 */
	void setAvatarImage(Image avatarImage);
	/*! \brief Get 
	 */
	std::string getAvatarImageBase64();

	/*! \brief Set 
	 */
	void setAvatarImageBase64(std::string avatarImageBase64);
	/*! \brief Get 
	 */
	FriendSearchResultEnums getFriendSearchResult();

	/*! \brief Set 
	 */
	void setFriendSearchResult(FriendSearchResultEnums friendSearchResult);
	/*! \brief Get 
	 */
	ChatType getChatType();

	/*! \brief Set 
	 */
	void setChatType(ChatType chatType);
	/*! \brief Get 添加好友时间，>4.x版本才有此属性
	 */
	std::string getAddDateTime();

	/*! \brief Set 添加好友时间，>4.x版本才有此属性
	 */
	void setAddDateTime(std::string addDateTime);
	/*! \brief Get 微信中使用的名称  本质是: 如果昵称不等于备注名，则以备注名不为主.
	 */
	std::string getName();

	/*! \brief Set 微信中使用的名称  本质是: 如果昵称不等于备注名，则以备注名不为主.
	 */
	void setName(std::string name);


    private:
    std::string nickName{};
    std::string memoName{};
    std::string area{};
    std::list<std::string> lable;
    std::string sameGroupNumber{};
    std::string signature{};
    std::string source{};
    std::string wxId{};
    std::string avatarPath{};
    Image avatarImage;
    std::string avatarImageBase64{};
    FriendSearchResultEnums friendSearchResult;
    ChatType chatType;
    std::string addDateTime{};
    std::string name{};
};
}

#endif /* TINY_CPP_CLIENT_FriendInfo_H_ */
