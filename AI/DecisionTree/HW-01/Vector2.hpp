#pragma once
#include <iostream>
#include "Exceptions.hpp"

using namespace std;
class Vector2 {

public:

	float x, y;
	Vector2() {
		x = 0;
		y = 0;
	}
	Vector2(Vector2* v) {
		x = v->x;
		y = v->y;
	}
	Vector2(float x, float y) {
		this->x = x;
		this->y = y;
	}

	bool operator==(Vector2 v)
	{
		return x == v.x && y == v.y;
	}

	bool operator!=(Vector2 v)
	{
		return !(this == &v);
	}

	Vector2 operator +(Vector2 v) {

		return Vector2(x + v.x, y + v.y);

	}
	Vector2 operator -(Vector2 v) {

		return Vector2(x - v.x, y - v.y);
	}
	Vector2 operator *(float f) {
		return Vector2(x * f, y * f);
	}
	Vector2 operator /(float f) {
		return Vector2(x / f, y / f);
	}

	void operator += (Vector2 v) {
		x += v.x;
		y += v.y;
	}
	void operator -= (Vector2 v) {
		x -= v.x;
		y -= v.y;
	}
	void operator *= (float i) {
		x *= i;
		y *= i;
	}
	void operator /= (float i) {
		try {
			if (i == 0)
				throw ZeroDivisionException();
			else
			{
				x /= i;
				y /= i;
			}
		}
		catch (...) {
			throw;
		}
	}

	Vector2 Normalize() {
		try {
			auto m = Magnitude();
			return m==0? Vector2(0,0) : Vector2(x / m, y / m);
		}
		catch (...) {
			throw;
		}
	}

	float Magnitude() {
		return sqrt(x * x + y * y);
	}
};

ostream& operator<<(ostream& os, Vector2 &v) {
	os << "(" << v.x << "," << v.y << ")";
	return os;
}