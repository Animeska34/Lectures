#include <iostream>
#include <conio.h>
#include <Windows.h>
#include "DecisionTree.hpp"

using namespace std;
using namespace DecisionTree;

int playerPosition;
int enemyPosition;


class Distance10 : public Decision {
	virtual bool Decide() {
		auto d = enemyPosition - playerPosition;
		return d >= 10;
	}
};

class Distance5 : public Decision {
	virtual bool Decide() {
		auto d = enemyPosition - playerPosition;
		return d <= 5;
	}
};

class Approach : public Action {
	virtual Node* Act() {
		enemyPosition -= 1;
		return this;
	}
};

class Retreat : public Action {
	virtual Node* Act() {
		Beep(500, 100);
		enemyPosition += 1;
		return this;
	}
};

void Input() {
	while (true) {
		int key = _getch();

		switch (key)
		{
		case 72:
			playerPosition += 1;
			return;
		case 80:
			playerPosition -= 1;
			return;
		}
	}
}

int main()
{
	Distance5* d5 = new Distance5();
	Distance10* d10 = new Distance10();

	d10->falseNode = d5;
	d10->trueNode = new Approach();

	d5->trueNode = new Retreat();
	d5->falseNode = new Action();

	enemyPosition = 20;
	playerPosition = 0;

	while (true) {
		cout << "playerPosition: 0," << playerPosition << "\nenemyPosition: 0," << enemyPosition << "\ndistance: " << enemyPosition - playerPosition << endl;
		Input();
		d10->Act();
		system("CLS");
	}
	cout << "Hello World!\n";
}