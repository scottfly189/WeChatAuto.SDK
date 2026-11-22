
/*
 * DatabaseQueryRequest.h
 *
 * 数据库只读查询。仅支持 SELECT（含 WITH ... SELECT），不会自动追加当前微信号条件。
 */

#ifndef TINY_CPP_CLIENT_DatabaseQueryRequest_H_
#define TINY_CPP_CLIENT_DatabaseQueryRequest_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 数据库只读查询。仅支持 SELECT（含 WITH ... SELECT），不会自动追加当前微信号条件。
 *
 *  \ingroup Models
 *
 */

class DatabaseQueryRequest{
public:

    /*! \brief Constructor.
	 */
    DatabaseQueryRequest();
    DatabaseQueryRequest(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~DatabaseQueryRequest();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 
	 */
	std::string getSql();

	/*! \brief Set 
	 */
	void setSql(std::string sql);


    private:
    std::string sql{};
};
}

#endif /* TINY_CPP_CLIENT_DatabaseQueryRequest_H_ */
