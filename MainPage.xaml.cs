namespace maui_bmi;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCalculateClicked(object? sender, EventArgs e)
    {
        var isHeightValid = double.TryParse(HeightEntry.Text, out var heightCm);
        var isWeightValid = double.TryParse(WeightEntry.Text, out var weightKg);

        if (!isHeightValid || !isWeightValid || heightCm <= 0 || weightKg <= 0)
        {
            ResultLabel.Text = "BMI: -";
            CategoryLabel.Text = "Category: Please enter valid positive numbers.";
            SemanticScreenReader.Announce(CategoryLabel.Text);
            return;
        }

        var heightMeters = heightCm / 100.0;
        var bmi = weightKg / (heightMeters * heightMeters);

        ResultLabel.Text = $"BMI: {bmi:F1}";
        CategoryLabel.Text = $"Category: {GetBmiCategory(bmi)}";
        SemanticScreenReader.Announce($"{ResultLabel.Text}. {CategoryLabel.Text}");
    }

    private static string GetBmiCategory(double bmi)
    {
        if (bmi < 18.5)
            return "Underweight";
        if (bmi < 25)
            return "Normal weight";
        if (bmi < 30)
            return "Overweight";

        return "Obesity";
    }
}