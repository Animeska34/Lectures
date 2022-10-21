#include <iostream>
#include "Demo.hpp"

using namespace std;


class 学生 {
public:
    string 名前;
    学生* 次;
};

void テスト() {
    学生 第一, 第二, 第三;
    第一.次 = &第二;
    第二.次 = &第三;
    第一.名前 = "第一学生";
    第二.名前 = "第二学生";
    第三.名前 = "第三学生";

    学生* この = &第一;
    while (この->次 != NULL)
        この = この->次;

    std::cout << "Last Student: " << この->名前 << endl;
}

int main()
{
    return Demo::entry();
}