#pragma once

#include <iostream>

class NullReferenceException : public std::exception {
public:
	NullReferenceException() : exception("NullReferenceException") {}
};

class ZeroDivisionException : public std::exception {
public:
	ZeroDivisionException() :  exception("ZeroDivisionException") {}
};