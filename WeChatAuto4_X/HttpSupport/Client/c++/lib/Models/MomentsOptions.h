
/*
 * MomentsOptions.h
 *
 * 朋友圈选项
 */

#ifndef TINY_CPP_CLIENT_MomentsOptions_H_
#define TINY_CPP_CLIENT_MomentsOptions_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"
#include <list>

namespace Tiny {


/*! \brief 朋友圈选项
 *
 *  \ingroup Models
 *
 */

class MomentsOptions{
public:

    /*! \brief Constructor.
	 */
    MomentsOptions();
    MomentsOptions(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~MomentsOptions();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 朋友圈被@的好友
	 */
	std::list<std::string> getAtUsrs();

	/*! \brief Set 朋友圈被@的好友
	 */
	void setAtUsrs(std::list<std::string> atUsrs);
	/*! \brief Get 朋友圈哪些设定的标签可以看，如果没有设置标签，则全部可见.
	 */
	std::list<std::string> getLabels();

	/*! \brief Set 朋友圈哪些设定的标签可以看，如果没有设置标签，则全部可见.
	 */
	void setLabels(std::list<std::string> labels);
	/*! \brief Get 是否执行操作后关闭朋友圈,默认关闭，也可以设置为False,然后使用者可以手动关闭
	 */
	bool isIsCloseMoments();

	/*! \brief Set 是否执行操作后关闭朋友圈,默认关闭，也可以设置为False,然后使用者可以手动关闭
	 */
	void setIsCloseMoments(bool isCloseMoments);


    private:
    std::list<std::string> atUsrs;
    std::list<std::string> labels;
    bool isCloseMoments{};
};
}

#endif /* TINY_CPP_CLIENT_MomentsOptions_H_ */
