
/*
 * Color.h
 *
 * 
 */

#ifndef TINY_CPP_CLIENT_Color_H_
#define TINY_CPP_CLIENT_Color_H_


#include <string>
#include "bourne/json.hpp"
#include "Helpers.h"

namespace Tiny {


/*! \brief 
 *
 *  \ingroup Models
 *
 */

class Color{
public:

    /*! \brief Constructor.
	 */
    Color();
    Color(std::string jsonString);


    /*! \brief Destructor.
	 */
    virtual ~Color();


    /*! \brief Retrieve a bourne JSON representation of this class.
	 */
    bourne::json toJson();


    /*! \brief Fills in members of this class from bourne JSON object representing it.
	 */
    void fromJson(std::string jsonObj);

	/*! \brief Get 
	 */
	int getR();

	/*! \brief Set 
	 */
	void setR(int r);
	/*! \brief Get 
	 */
	int getG();

	/*! \brief Set 
	 */
	void setG(int g);
	/*! \brief Get 
	 */
	int getB();

	/*! \brief Set 
	 */
	void setB(int b);
	/*! \brief Get 
	 */
	int getA();

	/*! \brief Set 
	 */
	void setA(int a);
	/*! \brief Get 
	 */
	bool isIsKnownColor();

	/*! \brief Set 
	 */
	void setIsKnownColor(bool isKnownColor);
	/*! \brief Get 
	 */
	bool isIsEmpty();

	/*! \brief Set 
	 */
	void setIsEmpty(bool isEmpty);
	/*! \brief Get 
	 */
	bool isIsNamedColor();

	/*! \brief Set 
	 */
	void setIsNamedColor(bool isNamedColor);
	/*! \brief Get 
	 */
	bool isIsSystemColor();

	/*! \brief Set 
	 */
	void setIsSystemColor(bool isSystemColor);
	/*! \brief Get 
	 */
	std::string getName();

	/*! \brief Set 
	 */
	void setName(std::string name);


    private:
    int r{};
    int g{};
    int b{};
    int a{};
    bool isKnownColor{};
    bool isEmpty{};
    bool isNamedColor{};
    bool isSystemColor{};
    std::string name{};
};
}

#endif /* TINY_CPP_CLIENT_Color_H_ */
