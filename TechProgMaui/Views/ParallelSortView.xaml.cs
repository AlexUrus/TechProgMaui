using Microcharts;
using SkiaSharp;
using TechProgMaui.ViewModels;

namespace TechProgMaui.Views;

public partial class ParallelSortView : ContentPage, IObserver<List<SortResults>>
{
	private ParallelSortViewModel _viewModel;
	public List<ChartEntry> CharEntries { get; set; }

	public ParallelSortView()
	{
		InitializeComponent();
        CharEntries = new();
        _viewModel = new();
        _viewModel.Subscribe(this);
	}

	private void CollectEntries(List<SortResults> listSortResults)
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

        listSortResults.OrderBy(x => x.NameSort);

        foreach (var item in listSortResults)
		{
			CharEntries.Add(new ChartEntry(item.TicksSorting)
			{
				Color = SKColor.Parse("#FF1493"),
				Label = item.NameSort + " " + item.LengthArray + " Correct = " + item.IsCorrectSorted,
				ValueLabel = $"{item.TicksSorting}"
			});
		}

		ChartView.Chart = new LineChart()
		{
			Entries = CharEntries
		};
    }

    public void OnCompleted()
    {
        throw new NotImplementedException();
    }

    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    public void OnNext(List<SortResults> listSortResults)
    {
        CollectEntries(listSortResults);
    }
}