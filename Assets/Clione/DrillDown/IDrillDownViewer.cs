namespace Clione.DrillDown
{
    public interface IDrillDownViewer
    {
        DrillDownViewerManager DrillDownManager { get; }
        void Initialize(object param, DrillDownViewerManager manager);
        void Show();
        void Next(bool isDig);
    }
}