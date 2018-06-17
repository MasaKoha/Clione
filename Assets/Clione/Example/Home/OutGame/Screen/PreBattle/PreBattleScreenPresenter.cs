namespace Clione.Example
{
    public class PreBattleScreenPresenter : OutGameScreenPresenterBase<PreBattleScreenView>
    {
        public override void Initialize()
        {
            base.Initialize();
            SetEvent();
        }

        private void SetEvent()
        {
            View.GameStartButtonClickedEvent.AddListener(() => { SceneLoader.Instance.LoadScene("InGame"); });
        }
    }
}