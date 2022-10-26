#include "pch.h"
#include "StateMachine.h"
#if __has_include("StateMachine.g.cpp")
#include "StateMachine.g.cpp"
#endif

using namespace winrt;
using namespace Windows::UI::Xaml;

namespace winrt::HW01::implementation
{
    StateMachine::StateMachine()
    {
        InitializeComponent();
    }

    int32_t StateMachine::MyProperty()
    {
        throw hresult_not_implemented();
    }

    void StateMachine::MyProperty(int32_t /* value */)
    {
        throw hresult_not_implemented();
    }

    void StateMachine::ClickHandler(IInspectable const&, RoutedEventArgs const&)
    {
        Button().Content(box_value(L"Clicked"));
    }
}
