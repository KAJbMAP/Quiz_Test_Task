using System;

namespace Interfaces.View
{
    public interface IScreenFaderView
    {
        void FadeInOut(Action fullFadedCallback = null);
    }
}