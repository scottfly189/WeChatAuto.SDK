

#include "SendVoiceChatsRequest.h"

using namespace Tiny;

SendVoiceChatsRequest::SendVoiceChatsRequest()
{
	who = std::string();
	partner = std::list<std::string>();
}

SendVoiceChatsRequest::SendVoiceChatsRequest(std::string jsonString)
{
	this->fromJson(jsonString);
}

SendVoiceChatsRequest::~SendVoiceChatsRequest()
{

}

void
SendVoiceChatsRequest::fromJson(std::string jsonObj)
{
    bourne::json object = bourne::json::parse(jsonObj);

    const char *whoKey = "who";

    if(object.has_key(whoKey))
    {
        bourne::json value = object[whoKey];



        jsonToValue(&who, value, "std::string");


    }

    const char *partnerKey = "partner";

    if(object.has_key(partnerKey))
    {
        bourne::json value = object[partnerKey];


        std::list<std::string> partner_list;
        std::string element;
        for(auto& var : value.array_range())
        {

            jsonToValue(&element, var, "std::string");


            partner_list.push_back(element);
        }
        partner = partner_list;


    }


}

bourne::json
SendVoiceChatsRequest::toJson()
{
    bourne::json object = bourne::json::object();





    object["who"] = getWho();





    std::list<std::string> partner_list = getPartner();
    bourne::json partner_arr = bourne::json::array();

    for(auto& var : partner_list)
    {
        partner_arr.append(var);
    }
    object["partner"] = partner_arr;






    return object;

}

std::string
SendVoiceChatsRequest::getWho()
{
	return who;
}

void
SendVoiceChatsRequest::setWho(std::string who)
{
	this->who = who;
}

std::list<std::string>
SendVoiceChatsRequest::getPartner()
{
	return partner;
}

void
SendVoiceChatsRequest::setPartner(std::list<std::string> partner)
{
	this->partner = partner;
}



