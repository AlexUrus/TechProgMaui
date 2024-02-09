using Microcharts;
using SkiaSharp;
using TechProgMaui.ViewModels;

namespace TechProgMaui.Views;

public partial class ParallelSortView : ContentPage
{
	private ParallelSortViewModel _viewModel;
	public List<ChartEntry> CharEntries { get; set; }

	public ParallelSortView()
	{
		InitializeComponent();
		_viewModel = new ParallelSortViewModel();
		_viewModel.GenerateTestCases();
		CollectEntries();
	}

	private void CollectEntries()
	{
		CharEntries =
        [
            new ChartEntry(0)
            {
                Color = SKColor.Parse("#FF1493"),
                Label = "0",
                ValueLabel = "0"
            },
        ];

		_viewModel.SortResultsList.OrderBy(x => x.NameSort);

        foreach (var item in _viewModel.SortResultsList)
		{
			CharEntries.Add(new ChartEntry(item.TicksSorting)
			{
				Color = SKColor.Parse("#FF1493"),
				Label = item.NameSort + " " + item.LengthArray,
				ValueLabel = $"{item.TicksSorting}"
			});
		}

		ChartView.Chart = new LineChart()
		{
			Entries = CharEntries
		};
    }

}