namespace LaJuana.Application.Models.ViewModels
{
    public class InspectionTrainPieChartFullVm
    {
        public string[]? Labels { get; set; }
        public Datasets[] Datasets { get; set; }
        
    }
    public class Datasets
    {
        public string Label { get; set; } = string.Empty;
        public int[]? data { get; set; }
        public string BackgroundColor { get; set; } = string.Empty;
    }
}
