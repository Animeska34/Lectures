#pragma once
#include <iostream>

using namespace std;

namespace DecisionTree {
	class Node {
	public:
		virtual Node* Act() = 0;
	};

	class Action : public Node {
	public:
		virtual Node* Act() {
			return this;
		}
	};

	class Decision : public Node {
	public:
		Node* trueNode;
		Node* falseNode;

		virtual bool Decide() { return false; };

		virtual Node* Act() {
			if (Decide())
				return trueNode == NULL ? NULL : trueNode->Act();
			else
				return falseNode == NULL ? NULL : falseNode->Act();
		}
	};
}