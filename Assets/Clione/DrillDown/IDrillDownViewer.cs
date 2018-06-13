namespace Clione.DrillDown
{
    public interface IDrillDownViewer
    {
        DrillDownViewerManager Manager { get; }
        void Initialize(object param, DrillDownViewerManager manager);
        void Show();
        void Next(bool isDig);
    }
}