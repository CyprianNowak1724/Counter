using System.Xml.Linq;

namespace Counter;

public partial class MainPage : ContentPage
{
    List<CounterData> counters = new List<CounterData>();

    string path = Path.Combine(FileSystem.AppDataDirectory, "counters.xml");

    public MainPage()
    {
        InitializeComponent();

        ColorPicker.Items.Add("Biały");
        ColorPicker.Items.Add("Czerwony");
        ColorPicker.Items.Add("Zielony");
        ColorPicker.Items.Add("Niebieski");
        ColorPicker.Items.Add("Żółty");
        ColorPicker.SelectedIndex = 0;

        LoadCounters();
    }

    private void AddCounterClicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text;
        if(string.IsNullOrWhiteSpace(name))
        {
            name = "Licznik " + (counters.Count + 1);
        }

        int startValue = 0;
        int.TryParse(ValueEntry.Text, out startValue);

        string color;
        switch (ColorPicker.SelectedIndex)
        {
            case 1:
                color = "#F3E3E0";   
                break;
            case 2:
                color = "#E3EBE4"; 
                break;
            case 3:
                color = "#E1E7EE";  
                break;
            case 4:
                color = "#F3EBD8";  
                break;
            default:
                color = "#FFFFFF";  
                break;
        }

        CounterData data = new CounterData();
        data.Name = name;
        data.Value = startValue;
        data.StartValue = startValue;
        data.Color = color;

        counters.Add(data);
        AddCounterView(data);
        SaveCounters();

        NameEntry.Text = "";
        ValueEntry.Text = "";
    }

    private void AddCounterView(CounterData data)
    {
        CounterView view = new CounterView(data, this);
        CountersContainer.Children.Add(view);
    }

    public void DeleteCounter(CounterView view)
    {
        counters.Remove(view.Data);
        CountersContainer.Children.Remove(view);
        SaveCounters();
    }

    public void SaveCounters()
    {
        XElement root = new XElement("Counters");

        foreach(CounterData c in counters)
        {
            XElement counter = new XElement("Counter");
            counter.Add(new XElement("Name", c.Name));
            counter.Add(new XElement("Value", c.Value));
            counter.Add(new XElement("StartValue", c.StartValue));
            counter.Add(new XElement("Color", c.Color));

            root.Add(counter);
        }

        XDocument doc = new XDocument(root);
        doc.Save(path);
    }

    private void LoadCounters()
    {
        if(File.Exists(path) == false)
        {
            return;
        }

        XDocument doc = XDocument.Load(path);

        foreach(XElement el in doc.Descendants("Counter"))
        {
            CounterData data = new CounterData();
            data.Name = el.Element("Name").Value;
            data.Value = int.Parse(el.Element("Value").Value);
            data.StartValue = int.Parse(el.Element("StartValue").Value);
            data.Color = el.Element("Color").Value;

            counters.Add(data);
            AddCounterView(data);
        }
    }
}
