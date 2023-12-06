#pragma once

namespace FSM
{

	class Action
	{
	public:
		float priority;
		Action *next;

		Action *GetLast()
		{
			return next == 0 ? this : next->GetLast();
		}

		void DeleteList()
		{
			if (next != 0)
				next->DeleteList();
			delete this;
		}

		virtual void Act() = 0;

		void ExecuteAll()
		{
			if (this)
			{
				Act();
				if (next)
					next->ExecuteAll();
			}
		}
	};

	class Condition
	{
	public:
		virtual bool Test() { return false; }
	};

	class State;

	class Transition
	{
	public:
		Transition *next;
		virtual bool IsTriggered() { return false; }
		// virtual Action* GetActions() { return 0; }
		// virtual State* GetTargetState() { return 0; }

		State *target;
		Action *actions;

		Transition(State *t)
		{
			target = t;
		}
	};

	class State
	{
	public:
		Transition *transition;
		Action *entryActions;
		Action *actions;
		Action *exitActions;
		const char *name;
		// virtual Action* GetActions() = 0;
		// virtual Action* GetEntryActions() = 0;
		// virtual Action* GetExitActions() = 0;

		class State(const char *n = "Unnamed State")
		{
			name = n;
		}

		class State(Transition *t)
		{
			transition = t;
			name = "Unnamed State";
		}

		class State(Transition *t, const char *n)
		{
			transition = t;
			name = n;
		}
	};

	class ConditionalTransition : public Transition
	{
	public:
		Condition *condition;
		virtual bool IsTriggered()
		{
			return condition->Test();
		}
		ConditionalTransition(State *t, Condition *c) : Transition(t)
		{
			condition = c;
		}
	};

	class NotConditionalTransition : public ConditionalTransition
	{
	public:
		virtual bool IsTriggered()
		{
			return !condition->Test();
		}
		NotConditionalTransition(State *t, Condition *c) : ConditionalTransition(t, c) {}
	};

	class StateMachine
	{
	public:
		State *initial;
		State *current;

		Action *Update()
		{
			Action *actions = 0;
			if (!current)
			{
				if (initial)
				{
					current = initial;
					actions = current->actions;
				}
			}
			else
			{
				Transition *transition = 0;

				Transition *test = current->transition;
				while (test)
				{
					if (test->IsTriggered())
					{
						transition = test;
						break;
					}

					test = test->next;
				}

				if (transition != 0)
				{
					State *nextState = transition->target;
					Action *temp = 0, *last = 0;

					if (current->exitActions != 0)
					{
						actions = current->exitActions;
						last = actions->GetLast();
					}

					temp = transition->actions;
					if (temp != 0)
					{
						if (last != 0)
							last->next = temp;
						else
							actions = temp;
						last = temp->GetLast();
					}

					temp = nextState->entryActions;
					if (temp != 0)
					{
						if (last != 0)
							last->next = temp;
						else
							actions = temp;
					}
					current = nextState;
				}
				else
					actions = current->actions;
				return actions;
			}
		}

		StateMachine(State *i)
		{
			initial = i;
		}
	};
}