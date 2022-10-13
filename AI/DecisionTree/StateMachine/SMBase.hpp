#pragma once

namespace SM {
	class Action {
	public:
		float priority;
		Action* next;
		Action* GetLast() {
			return next == 0 ? this : next->GetLast();
		}

		void DeleteList() {
			if (next != 0)
				next->DeleteList();
			delete this;
		}

		virtual void Act() = 0;
	};

	class BaseTransition {
	public:
		virtual bool IsTriggered() = 0;
		virtual Action* GetAction() = 0;
		BaseTransition* next;

	};

	class Condition {
	public:
		virtual bool Test() = 0;
	};

	class ConditionalTransitionMixin : public Condition
	{
	public:
		Condition* condition;
		virtual bool IsTriggered() {
			return condition->Test();
		}
		virtual bool Test() {};
	};

	//Test classes
	class IntegerMatchCondition : public Condition
	{
	public:
		//Rodykle i stebima skaiciu
		int* watch;

		//reiksme, su kuria lyginamas stebimas skaicius
		int target;

		//tikrinama ar stebimas skaicius toks, kokio reikia
		virtual bool Test()
		{
			return (*watch == target);
		}
	};
}