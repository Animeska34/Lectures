#pragma once

#include "StateMachine.g.h"

namespace winrt::HW01::implementation
{
    struct StateMachine : StateMachineT<StateMachine>
    {
        StateMachine();

        int32_t MyProperty();
        void MyProperty(int32_t value);

        void ClickHandler(Windows::Foundation::IInspectable const& sender, Windows::UI::Xaml::RoutedEventArgs const& args);
    };
}

namespace winrt::HW01::factory_implementation
{
    struct StateMachine : StateMachineT<StateMachine, implementation::StateMachine>
    {
    };
}
