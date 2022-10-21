#pragma once

#include <iostream>

	static int RandomRange(int min, int max) {
		return min + (std::rand() % (max - min));
	}