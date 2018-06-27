using System.Collections;
using Clione.Home;

namespace Clione.Example
{
    public class PreBattleScreenPresenterPresenter : OutGameScreenPresenterBase<PreBattleScreenView>
    {
        public override IEnumerator InitializeEnumerator()
        {
            SetEvent();
            yield break;
        }

        private void SetEvent()
        {
            View.GameStartButtonClickedEvent.AddListener(() => { SceneLoader.LoadScene("InGame"); });
        }
    }
}