#pragma once

#include "SMBase.hpp"

namespace SM {
	class State {
	public:
		Transition *firstTransition;

		virtual Action* GetAction() 
		{
			return 0;
		}
		virtual Action* GetEntryActions() 
		{
			return 0;
		}
		virtual Action* GetExitAction() 
		{
			return 0;
		}
	};

	class Transition : public BaseTransition {
		virtual State* GetTargetState() {};
	};

	class FixedTargetTransitionMixin {
	public:
		State* target;
		virtual State* GetTargetState() {
			return target;
		}
	};

	class StateMachine {
	public:
		State* initial;
		State* current;
		Action* Update() {
			Action* actions = 0;
			if (current == 0 && initial != 0) {
				Transition* transition = 0;
				BaseTransition* test = current->firstTransition;

				while (test != 0) {
					if (test->IsTriggered()) {
						transition = (Transition*)test;
						break;
					}
					test = test->next;
				}
			}
		}
	};
}