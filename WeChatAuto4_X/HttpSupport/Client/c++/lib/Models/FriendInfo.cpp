

#include "FriendInfo.h"

using namespace Tiny;

FriendInfo::FriendInfo()
{
	nickName = std::string();
	memoName = std::string();
	area = std::string();
	lable = std::list<std::string>();
	sameGroupNumber = std::string();
	signature = std::string();
	source = std::string();
	wxId = std::string();
	avatarPath = std::string();
	avatarImage = Image();
	avatarImageBase64 = std::string();
	friendSearchResult = FriendSearchResultEnums();
	chatType = ChatType();
	addDateTime = std::string();
	name = std::string();
}

FriendInfo::FriendInfo(std::string jsonString)
{
	this->fromJson(jsonString);
}

FriendInfo::~FriendInfo()
{

}

void
FriendInfo::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *nickNameKey = "nickName";

    if(object.has_key(nickNameKey))
    {
        bourne::json value = object[nickNameKey];



        jsonToValue(&nickName, value, "std::string");


    }

    const char *memoNameKey = "memoName";

    if(object.has_key(memoNameKey))
    {
        bourne::json value = object[memoNameKey];



        jsonToValue(&memoName, value, "std::string");


    }

    const char *areaKey = "area";

    if(object.has_key(areaKey))
    {
        bourne::json value = object[areaKey];



        jsonToValue(&area, value, "std::string");


    }

    const char *lableKey = "lable";

    if(object.has_key(lableKey))
    {
        bourne::json value = object[lableKey];


        std::list<std::string> lable_list;
        std::string element;
        for(auto& var : value.array_range())
        {

            jsonToValue(&element, var, "std::string");


            lable_list.push_back(element);
        }
        lable = lable_list;


    }

    const char *sameGroupNumberKey = "sameGroupNumber";

    if(object.has_key(sameGroupNumberKey))
    {
        bourne::json value = object[sameGroupNumberKey];



        jsonToValue(&sameGroupNumber, value, "std::string");


    }

    const char *signatureKey = "signature";

    if(object.has_key(signatureKey))
    {
        bourne::json value = object[signatureKey];



        jsonToValue(&signature, value, "std::string");


    }

    const char *sourceKey = "source";

    if(object.has_key(sourceKey))
    {
        bourne::json value = object[sourceKey];



        jsonToValue(&source, value, "std::string");


    }

    const char *wxIdKey = "wxId";

    if(object.has_key(wxIdKey))
    {
        bourne::json value = object[wxIdKey];



        jsonToValue(&wxId, value, "std::string");


    }

    const char *avatarPathKey = "avatarPath";

    if(object.has_key(avatarPathKey))
    {
        bourne::json value = object[avatarPathKey];



        jsonToValue(&avatarPath, value, "std::string");


    }

    const char *avatarImageKey = "avatarImage";

    if(object.has_key(avatarImageKey))
    {
        bourne::json value = object[avatarImageKey];




        Image* obj = &avatarImage;
		obj->fromJson(value.dump());

    }

    const char *avatarImageBase64Key = "avatarImageBase64";

    if(object.has_key(avatarImageBase64Key))
    {
        bourne::json value = object[avatarImageBase64Key];



        jsonToValue(&avatarImageBase64, value, "std::string");


    }

    const char *friendSearchResultKey = "friendSearchResult";

    if(object.has_key(friendSearchResultKey))
    {
        bourne::json value = object[friendSearchResultKey];




        FriendSearchResultEnums* obj = &friendSearchResult;
		obj->fromJson(value.dump());

    }

    const char *chatTypeKey = "chatType";

    if(object.has_key(chatTypeKey))
    {
        bourne::json value = object[chatTypeKey];




        ChatType* obj = &chatType;
		obj->fromJson(value.dump());

    }

    const char *addDateTimeKey = "addDateTime";

    if(object.has_key(addDateTimeKey))
    {
        bourne::json value = object[addDateTimeKey];



        jsonToValue(&addDateTime, value, "std::string");


    }

    const char *nameKey = "name";

    if(object.has_key(nameKey))
    {
        bourne::json value = object[nameKey];



        jsonToValue(&name, value, "std::string");


    }


}

bourne::json
FriendInfo::toJson()
{
    bourne::json object = bourne::json::object();





    object["nickName"] = getNickName();






    object["memoName"] = getMemoName();






    object["area"] = getArea();





    std::list<std::string> lable_list = getLable();
    bourne::json lable_arr = bourne::json::array();

    for(auto& var : lable_list)
    {
        lable_arr.append(var);
    }
    object["lable"] = lable_arr;









    object["sameGroupNumber"] = getSameGroupNumber();






    object["signature"] = getSignature();






    object["source"] = getSource();






    object["wxId"] = getWxId();






    object["avatarPath"] = getAvatarPath();







	object["avatarImage"] = getAvatarImage().toJson();





    object["avatarImageBase64"] = getAvatarImageBase64();







	object["friendSearchResult"] = getFriendSearchResult().toJson();






	object["chatType"] = getChatType().toJson();





    object["addDateTime"] = getAddDateTime();






    object["name"] = getName();



    return object;

}

std::string
FriendInfo::getNickName()
{
	return nickName;
}

void
FriendInfo::setNickName(std::string nickName)
{
	this->nickName = nickName;
}

std::string
FriendInfo::getMemoName()
{
	return memoName;
}

void
FriendInfo::setMemoName(std::string memoName)
{
	this->memoName = memoName;
}

std::string
FriendInfo::getArea()
{
	return area;
}

void
FriendInfo::setArea(std::string area)
{
	this->area = area;
}

std::list<std::string>
FriendInfo::getLable()
{
	return lable;
}

void
FriendInfo::setLable(std::list<std::string> lable)
{
	this->lable = lable;
}

std::string
FriendInfo::getSameGroupNumber()
{
	return sameGroupNumber;
}

void
FriendInfo::setSameGroupNumber(std::string sameGroupNumber)
{
	this->sameGroupNumber = sameGroupNumber;
}

std::string
FriendInfo::getSignature()
{
	return signature;
}

void
FriendInfo::setSignature(std::string signature)
{
	this->signature = signature;
}

std::string
FriendInfo::getSource()
{
	return source;
}

void
FriendInfo::setSource(std::string source)
{
	this->source = source;
}

std::string
FriendInfo::getWxId()
{
	return wxId;
}

void
FriendInfo::setWxId(std::string wxId)
{
	this->wxId = wxId;
}

std::string
FriendInfo::getAvatarPath()
{
	return avatarPath;
}

void
FriendInfo::setAvatarPath(std::string avatarPath)
{
	this->avatarPath = avatarPath;
}

Image
FriendInfo::getAvatarImage()
{
	return avatarImage;
}

void
FriendInfo::setAvatarImage(Image avatarImage)
{
	this->avatarImage = avatarImage;
}

std::string
FriendInfo::getAvatarImageBase64()
{
	return avatarImageBase64;
}

void
FriendInfo::setAvatarImageBase64(std::string avatarImageBase64)
{
	this->avatarImageBase64 = avatarImageBase64;
}

FriendSearchResultEnums
FriendInfo::getFriendSearchResult()
{
	return friendSearchResult;
}

void
FriendInfo::setFriendSearchResult(FriendSearchResultEnums friendSearchResult)
{
	this->friendSearchResult = friendSearchResult;
}

ChatType
FriendInfo::getChatType()
{
	return chatType;
}

void
FriendInfo::setChatType(ChatType chatType)
{
	this->chatType = chatType;
}

std::string
FriendInfo::getAddDateTime()
{
	return addDateTime;
}

void
FriendInfo::setAddDateTime(std::string addDateTime)
{
	this->addDateTime = addDateTime;
}

std::string
FriendInfo::getName()
{
	return name;
}

void
FriendInfo::setName(std::string name)
{
	this->name = name;
}



