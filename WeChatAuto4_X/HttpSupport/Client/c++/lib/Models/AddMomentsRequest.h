
/*
 * AddMomentsRequest.h
 *
 * 发朋友圈。
 */

#ifndef TINY_CPP_CLIENT_AddMomentsRequest_H_
#define TINY_CPP_CLIENT_AddMomentsRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"
#include "MomentsOptions.h"
#include <list>

namespace Tiny {


/*! \brief 发朋友圈。
 *
 *  \ingroup Models
 *
 */

class AddMomentsRequest{
public:

    /*! \brief Constructor.
	 */
    AddMomentsRequest();
    AddMomentsRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~AddMomentsRequest();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 
	 */
	std::list<std::string> getImageFiles();

	/*! \brief Set 
	 */
	void setImageFiles(std::list<std::string> image_files);
	/*! \brief Get 
	 */
	std::string getContent();

	/*! \brief Set 
	 */
	void setContent(std::string content);
	/*! \brief Get 
	 */
	MomentsOptions getOptions();

	/*! \brief Set 
	 */
	void setOptions(MomentsOptions options);


    private:
    std::list<std::string> image_files;
    std::string content{};
    MomentsOptions options;
};
}

#endif /* TINY_CPP_CLIENT_AddMomentsRequest_H_ */
