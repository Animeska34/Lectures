#include <iostream>
#include "Demo.hpp"

using namespace std;


class 学生 {
public:
    string 名前;
    学生* 次の;
};

void テスト() {
    学生 第一, 第二, 第三;
    第一.次の = &第二;
    第二.次の = &第三;
    第一.名前 = "第一学生";
    第二.名前 = "第二学生";
    第三.名前 = "第三学生";

    学生* この = &第一;
    while (この->次の != NULL)
        この = この->次の;

    std::cout << "Last Student: " << この->名前 << endl;
}

#include "D:\repos\CUI\Ports\C++\CUIC.h"

int main()
{
    return Demo::entry();
}