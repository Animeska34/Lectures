#include <iostream>
#include <windows.h>
#include <conio.h>
#include "StateMachine.hpp"
#include "Vector2.hpp"
#include "Random.hpp"

using namespace std;
using namespace FSM;

//Vector2 worldLimits = Vector2(30, -30);

class Entity {
	Vector2 currentPosition;
public:
	StateMachine* ai;
	Vector2 position;
	char icon;

	Entity(char i) {
		ai = 0;
		position = Vector2();
		icon = i;
	}
	Entity(char i, Vector2 p) {
		ai = 0;
		icon = i;
		position = p;
	}
	Entity(char i, Vector2 p, StateMachine* s) {
		icon = i;
		position = p;
		ai = s;
	}

	void Update() {
		if (ai)
			ai->Update()->ExecuteAll();
		/*
		if (position != currentPosition && position.x >= 0 && position.x < worldLimits.x && position.y <= 0 && position.y > worldLimits.y)
		{
			printf("\033[s\033[<%i>;<%i>H \033[<%i>;<%i>H%c\033[u", (int)currentPosition.y, (int)currentPosition.x, (int)position.y, (int)position.x, icon);
			currentPosition = position;
		}
		else
			position = currentPosition;
		*/
	}
};

class Attack : public Action {
	Entity* ref;
public:
	virtual void Act() {
		Beep(500, 100);
		if (next)
			next->Act();
	}
	Attack(Entity* e) {
		ref = e;
	}
};

class Patrol : public Action {
	Entity* ref;
public:
	virtual void Act() {
		int dir = RandomRange(0, 4);
		switch (dir) {
		case 0: 
			ref->position += Vector2(0, 1);
			break;
		case 1:
			ref->position += Vector2(0, -1);
			break;
		case 2:
			ref->position += Vector2(1, 0);
			break;
		case 3:
			ref->position += Vector2(-1, 0);
			break;
		}
		if (next)
			next->Act();
	}
	Patrol(Entity* e) {
		ref = e;
	}
};

Entity player = Entity('O', Vector2(10, 10));
Entity enemy = Entity('X', Vector2(0, 0));

class PlayerNear : public Condition {
	Entity* ref;
public:
	virtual bool Test() {
		float dist = (ref->position - player.position).Magnitude();
		return dist < 10.0f;
	}
	PlayerNear(Entity* e) {
		ref = e;
	}
};

void Input() {
	while (true) {
		int key = _getch();

		switch (key)
		{
		case 72:
			player.position.y += 1;
			return;
		case 80:
			player.position.y -= 1;
			return;
		case 77:
			player.position.x += 1;
			return;
		case 75:
			player.position.x -= 1;
			return;
		}
	}
}
int main()
{
	State patrol = State("Patrol");
	patrol.actions = new Patrol(&enemy);

	State attack = State("Attack");
	attack.entryActions = new Attack(&enemy);

	auto playerNear = ConditionalTransition(&attack, new PlayerNear(&enemy));
	auto playerFar = NotConditionalTransition(&patrol, new PlayerNear(&enemy));

	attack.transition = &playerFar;
	patrol.transition = &playerNear;

	StateMachine sm = StateMachine(&patrol);

	enemy.ai = &sm;

	while (true) {
		enemy.Update();
		player.Update();

		Vector2 rel = enemy.position - player.position;
		float magnitude = rel.Magnitude();
		Vector2 normalized = rel.Normalize();
		cout << "Player pos: " << player.position << endl << "Enemy pos: " << enemy.position << endl << "Distance between: " << magnitude << endl << "Direction to player: " << normalized << endl << "Current state: " << sm.current->name << endl;
		Input();
		system("CLS");
	}
}