using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Interfaces.Model.Systems;
using Interfaces.Presenters;
using Interfaces.View;

namespace Presenters
{
    public class ScreenSystemPresenter : IScreenSystemPresenter, IDisposable
    {
        private readonly IScreenSystem _screenSystem;
        private readonly IScreenFaderView _screenFaderView;
        private readonly IReadOnlyDictionary<QuizScreen, IScreenPresenter> _screenPresenters;

        public ScreenSystemPresenter(IScreenSystem screenSystem, IScreenFaderView screenFaderView, IEnumerable<IScreenPresenter> screenPresenters)
        {
            _screenSystem = screenSystem;
            _screenFaderView = screenFaderView;
            _screenPresenters = screenPresenters.ToDictionary(presenter => presenter.QuizScreen, presenter => presenter);
            _screenSystem.ScreenChanged += ScreenSystemOnScreenChanged;
        }

        public void Dispose()
        {
            _screenSystem.ScreenChanged -= ScreenSystemOnScreenChanged;
        }

        private void ScreenSystemOnScreenChanged(QuizScreen screen)
        {
            _screenFaderView.FadeInOut(() =>
            {
                foreach (var screenPresenter in _screenPresenters.Values)
                    screenPresenter.Hide();
                
                _screenPresenters[screen].Present();
            });
        }
    }
}