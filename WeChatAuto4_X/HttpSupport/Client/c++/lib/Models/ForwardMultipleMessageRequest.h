
/*
 * ForwardMultipleMessageRequest.h
 *
 * 转发多条消息。
 */

#ifndef TINY_CPP_CLIENT_ForwardMultipleMessageRequest_H_
#define TINY_CPP_CLIENT_ForwardMultipleMessageRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"
#include "ForwardMessageTypeEnums.h"
#include <list>

namespace Tiny {


/*! \brief 转发多条消息。
 *
 *  \ingroup Models
 *
 */

class ForwardMultipleMessageRequest{
public:

    /*! \brief Constructor.
	 */
    ForwardMultipleMessageRequest();
    ForwardMultipleMessageRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~ForwardMultipleMessageRequest();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 
	 */
	std::string getWho();

	/*! \brief Set 
	 */
	void setWho(std::string who);
	/*! \brief Get 
	 */
	std::list<std::string> getTo();

	/*! \brief Set 
	 */
	void setTo(std::list<std::string> to);
	/*! \brief Get 
	 */
	ForwardMessageTypeEnums getFType();

	/*! \brief Set 
	 */
	void setFType(ForwardMessageTypeEnums f_type);
	/*! \brief Get 
	 */
	int getRowCount();

	/*! \brief Set 
	 */
	void setRowCount(int row_count);


    private:
    std::string who{};
    std::list<std::string> to;
    ForwardMessageTypeEnums f_type;
    int row_count{};
};
}

#endif /* TINY_CPP_CLIENT_ForwardMultipleMessageRequest_H_ */
