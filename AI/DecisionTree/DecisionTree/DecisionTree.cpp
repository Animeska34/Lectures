#include <iostream>

using namespace std;

namespace DecisionTree {
	class DecisionTreeNode {
	public:
		virtual DecisionTreeNode* MakeDecision() = 0;
	};


	class DecisionTreeAction : public DecisionTreeNode {
	public:
		virtual DecisionTreeNode* MakeDecision() {
			return this;
		}
		virtual void PerformAction() = 0;
	};

	class Decision : public DecisionTreeNode {
	public:
		DecisionTreeNode* trueBranch;
		DecisionTreeNode* falseBranch;

		virtual bool GetBranch() = 0;

		virtual DecisionTreeNode* MakeDecision() {
			if (GetBranch())
				return trueBranch == NULL ? NULL : trueBranch->MakeDecision();
			else
				return falseBranch == NULL ? NULL : falseBranch->MakeDecision();
		}
	};

	class InteractiveDecision : public Decision {
	public:
		const char* question;
		virtual bool GetBranch() {
			char buff[3];
			cout << question << "? [Y/N]" << endl;
			cin >> buff;
			return buff[0] == 'y' ? true : false;
		};
	};

	class DemoAction : public DecisionTreeAction {
	public:
		const char* act;
		virtual void PerformAction() {
			cout << act << endl;
		}
	};
	class BeepAction : public DecisionTreeAction {
	public:
		virtual void PerformAction() {
			
		}
	};
}

using namespace DecisionTree;
int main()
{
	InteractiveDecision decisions[7];
	DemoAction actions[6];

	decisions[0].question = "healthy";
	decisions[0].trueBranch = &decisions[1];
	decisions[0].falseBranch = &actions[3];

	decisions[1].question = "near";
	decisions[1].trueBranch = &decisions[2];
	decisions[1].falseBranch = &actions[0];
	
	decisions[2].question = "strong";
	decisions[2].trueBranch = &actions[2];
	decisions[2].falseBranch = &decisions[4];

	decisions[3].question = "cover";
	decisions[3].trueBranch = &actions[2];
	decisions[3].falseBranch = &actions[4];

	decisions[4].question = "visible";
	decisions[4].trueBranch = &decisions[3];
	decisions[4].falseBranch = &actions[5];

	decisions[5].question = "Q6";
	decisions[5].trueBranch = &actions[0];
	decisions[5].falseBranch = &actions[4];

	decisions[6].question = "Q7";
	decisions[6].trueBranch = &actions[3];
	decisions[6].falseBranch = &actions[2];

	actions[0].act = "stay";
	actions[1].act = "attack";
	actions[2].act = "retreat";
	actions[3].act = "heal";
	actions[4].act = "cover";
	actions[5].act = "approach";


	while (true) {
		DecisionTreeNode* node = decisions[0].MakeDecision();
		((DecisionTreeAction*)node)->PerformAction();
		system("PAUSE");
	}
}