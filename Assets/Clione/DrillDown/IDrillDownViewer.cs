namespace Clione.DrillDown
{
    public interface IDrillDownViewer
    {
        void Initialize(object param);
        void Show();
        void Next(bool isDig);
    }
}