#pragma once
#include "SMBase.hpp"

namespace SM {

	class Transition;

	class State {
	public:
		Transition* firstTransition;

		virtual Action* GetActions() 
		{
			return 0;
		}
		virtual Action* GetEntryActions() 
		{
			return 0;
		}
		virtual Action* GetExitActions() 
		{
			return 0;
		}
	};

	class Transition : public BaseTransition {
	public:
		virtual State* GetTargetState() = 0;
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
			if (current == 0) {
				if (initial != 0) {
					current = initial;
					actions = current->GetEntryActions();
				}
				else {
					actions = 0;
				}
			} 
			else 
			{
				// Turime esama busena
		// pradedam be perejimo
					Transition* transition = NULL;

					// Tikrinam esamoje busenoje visus perejimus, pradedant nuo pirmo.
					//Pirmas rastas issauktas, bus ivykdytas
					BaseTransition* testTransition = current->firstTransition;
					while (testTransition != NULL) {
						if (testTransition->IsTriggered()) {
							transition = (Transition*)testTransition;
							break;
						}
						testTransition = testTransition->next;
					}

					// Tikrinam ar radom perejima
					if (transition) {
						// Randam busena i kuria pereisim
						State* nextState = transition->GetTargetState();

						// Kaupiam veiksmu sarasa
						Action* tempList = NULL;
						Action* last = NULL;

						// Paimam isejimo is busenos veiksmus
						actions = current->GetExitActions();
						last = actions->GetLast();//Paimam paskutini isejimo veiksma, kad prie prilipdyt sekancius


						tempList = transition->GetAction();//paimam perejimo veiksmus
						last->next = tempList;//prie pask. isejimo veiksmo prilipdom perejimo veiksmus
						last = tempList->GetLast();//bus paimtas paskutinis veiksmas (siuo atveju perejimo)

						tempList = nextState->GetActions();//Paimam iejimo i nauja busena veiksmus
						last->next = tempList;//prie perejimo pask. veiksmo prilipdom iejimo i nauja busena veiksmus

						// Atnaujinam busenos pasikeitima
						current = nextState;
					}
					else { // Priesingu atveju visi veiksmai yra sios busenos
						actions = current->GetActions();
					}
				return actions;
			}
		}
	};
}