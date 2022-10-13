#pragma once

#include <iostream>
#include "SMState.hpp"

using namespace std;
using namespace SM;

namespace Demo {
	class DemoAction : public Action 
	{
	public:
		string actionText;
		virtual void Act() {
			cout << actionText << endl;
		}
	};

	DemoAction* CreateActions(const char **items, int count) {
		DemoAction *first = 0, *last = 0, *current = 0;
		for (int i = 0; i < count; i++) {
			current = new DemoAction();
			current->actionText = *items[i];
			if (i == 0)
				first = current;
			if (last)
				last->next = current;
			last = current;
		}
		return first;
	}

	class DemoTransition : public Transition, public ConditionalTransitionMixin, public FixedTargetTransitionMixin
	{
	public:
		const char ** text;
		int textCount;

		virtual Action* GetAction() {
			return CreateActions(text, textCount);
		}

        State* GetTargetState()
        {
            return FixedTargetTransitionMixin::GetTargetState();
        }

		bool IsTriggered()
		{
			cout << "Tikrinamas perejimas -" << ((IntegerMatchCondition*)condition)->target << endl;

			bool result = ConditionalTransitionMixin::IsTriggered();
			if (result) {
				cout << "Issaukia\n";
			}
			else {
				cout << "Neissaukia\n";
			}
			return result;
		}
	};

    /*
 * Klase paveldi StateMachineState ir turi "tekstinius" veiksmus
 */
    class DemoState : public State
    {
    public:
        const char** text;
        unsigned textCount;

        const char** entryText;
        unsigned entryTextCount;

        const char** exitText;
        unsigned exitTextCount;

        virtual Action* GetActions();
        virtual Action* GetEntryActions();
        virtual Action* GetExitActions();
    };

    Action* DemoState::GetActions()
    {
        return CreateActions(text, textCount);
    }

    Action* DemoState::GetEntryActions()
    {
        return CreateActions(entryText, entryTextCount);
    }

    Action* DemoState::GetExitActions()
    {
        return CreateActions(exitText, exitTextCount);
    }



    int entry()
    {
        int option;
        // string masyvai, naudojami visose busenose
        const char* allText[] = {
            // Busenu tekstas
            "Ieiname i busena A", "Busenoje A", "Iseiname is busenos A",
            "Ieiname i busena B", "Busenoje B", "Iseiname is busenos B",
            "Ieiname i busena C", "Busenoje C", "Iseiname is busenos C",
            "Ieiname i busena D", "Busenoje D", "Iseiname is busenos D",
            "Ieiname i busena E", "Busenoje E", "Iseiname is busenos E",
            "Ieiname i busena F", "Busenoje F", "Iseiname is busenos F",
            "Ieiname i busena G", "Busenoje G", "Iseiname is busenos G",

            // Perejimu tekstas
            "Perejimas 1",
            "Perejimas 2",
            "Perejimas 3",
            "Perejimas 4",
            "Perejimas 5",
            "Perejimas 6",
            "Perejimas 7",
            "Perejimas 8",
            "Perejimas 9",
            "Perejimas 10",
            "Perejimas 11",
            "Perejimas 12",
            "Perejimas 13",
            "Perejimas 14",
            "Perejimas 15"
        };

        // Sukuriam savo busenu automata
        DemoState states[7];
        DemoTransition transitions[15];

        // kiekvienam atvejui nustatome teksta
        for (int i = 0; i < 7; i++)
        {
            states[i].entryText = allText + (i * 3);
            states[i].entryTextCount = 1;
            states[i].text = allText + (i * 3 + 1);
            states[i].textCount = 1;
            states[i].exitText = allText + (i * 3 + 2);
            states[i].exitTextCount = 1;
        }
        for (int i = 0; i < 15; i++)
        {
            transitions[i].text = allText + (21 + i);
            transitions[i].textCount = 1;

            transitions[i].next = NULL;

            IntegerMatchCondition* condition = new IntegerMatchCondition;
            condition->watch = &option;
            condition->target = (i + 1);
            transitions[i].condition = condition;
        }

        // Sujungiam perejimus su ju naujom busenom
        transitions[0].target = states + 1;
        transitions[1].target = states + 1;
        transitions[2].target = states + 2;
        transitions[3].target = states + 3;
        transitions[4].target = states + 4;
        transitions[5].target = states + 0;
        transitions[6].target = states + 5;
        transitions[7].target = states + 6;
        transitions[8].target = states + 5;
        transitions[9].target = states + 2;
        transitions[10].target = states + 6;
        transitions[11].target = states + 4;
        transitions[12].target = states + 6;
        transitions[13].target = states + 4;
        transitions[14].target = states + 6;

        // Talpiname perejimus i ju busenas
        states[0].firstTransition = transitions + 0;
        transitions[0].next = transitions + 4;

        states[1].firstTransition = transitions + 1;
        transitions[1].next = transitions + 2;
        transitions[2].next = transitions + 6;
        transitions[6].next = transitions + 7;

        states[2].firstTransition = transitions + 3;
        transitions[3].next = transitions + 8;

        states[3].firstTransition = transitions + 10;

        states[4].firstTransition = transitions + 13;
        transitions[13].next = transitions + 14;

        states[5].firstTransition = transitions + 5;
        transitions[5].next = transitions + 11;
        transitions[11].next = transitions + 12;

        states[6].firstTransition = transitions + 9;

        // Nustatom busenu automata
        StateMachine sm;
        sm.initial = states + 0;
        sm.current = NULL;

        // Paleidziam demo
        cout << "Busenu automatu Demo (Ctrl+C to exit)\n";
        char buffer[4];

        while (true)
        {
            // Rodom esama situacija
            if (sm.current != NULL) {
                cout << "\n\nEsama busena:\n" << ((DemoState*)sm.current)->text[0] << endl;
                cout << "Perejimai is sios busenos:\n";

                // isvedame visus galimus perejimus
                BaseTransition* next = sm.current->firstTransition;

                while (next != NULL)
                {
                    DemoTransition* nextDT = (DemoTransition*)(next);
                    DemoState* to = (DemoState*)nextDT->target;
                    cout << nextDT->text[0] << " buti " << to->text[0] << endl;

                    // Imam sekanti perejima
                    next = next->next;
                }
            }
            else {
                cout << "\n\nNera dabartines busenos\nNera dabartiniu perejimu\n";
            }

            // Prasom vartotojo ivesti duomenis
            cout << "Kuri perejima issaukti (0[nieko]-15)\n";

            // gaunam vartotojo ivedima
            cin >> buffer;
            option = atoi(buffer);//verciam i skaiciu

            // Paleidziam SM viena iteracija
            Action* actions = sm.Update();

            // Isvedam teksta (ivykdom veiksma)
            Action* next = actions;
            while (next != NULL)
            {
                next->Act();
                next = next->next;
            }

            // Trinam veiksmus
            if (actions != NULL)
                actions->DeleteList();
        }

        return 0;
    }
}