using MauiBundler.Components;

namespace VueSampleApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new MauiBundlerView();
	}
}
