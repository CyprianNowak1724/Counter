namespace Counter;

public partial class CounterView : ContentView
{
    public CounterData Data;
    MainPage mainPage;

    public CounterView(CounterData counterData, MainPage page)
    {
        InitializeComponent();

        Data = counterData;
        mainPage = page;

        NameLabel.Text = Data.Name;
        ValueLabel.Text = Data.Value.ToString();
        CardBorder.BackgroundColor = Color.FromArgb(Data.Color);
    }

    private void PlusClicked(object sender, EventArgs e)
    {
        Data.Value++;
        ValueLabel.Text = Data.Value.ToString();
        mainPage.SaveCounters();
    }

    private void MinusClicked(object sender, EventArgs e)
    {
        Data.Value--;
        ValueLabel.Text = Data.Value.ToString();
        mainPage.SaveCounters();
    }

    private void ResetClicked(object sender, EventArgs e)
    {
        Data.Value = Data.StartValue;
        ValueLabel.Text = Data.Value.ToString();
        mainPage.SaveCounters();
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        mainPage.DeleteCounter(this);
    }
}