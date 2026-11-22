
/*
 * RemoveMomentsRequest.h
 *
 * 删除自己发的朋友圈（按文字内容）。
 */

#ifndef TINY_CPP_CLIENT_RemoveMomentsRequest_H_
#define TINY_CPP_CLIENT_RemoveMomentsRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 删除自己发的朋友圈（按文字内容）。
 *
 *  \ingroup Models
 *
 */

class RemoveMomentsRequest{
public:

    /*! \brief Constructor.
	 */
    RemoveMomentsRequest();
    RemoveMomentsRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~RemoveMomentsRequest();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 
	 */
	std::string getContent();

	/*! \brief Set 
	 */
	void setContent(std::string content);


    private:
    std::string content{};
};
}

#endif /* TINY_CPP_CLIENT_RemoveMomentsRequest_H_ */
