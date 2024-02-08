namespace TechProgMaui.Views;

public partial class NewspaperPage : ContentPage
{
	public NewspaperPage()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		this.UpdateChildrenLayout();
    }
}