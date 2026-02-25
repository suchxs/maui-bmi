namespace maui_bmi;

public partial class MainPage : ContentPage
{
    private const int MetricUnitIndex = 0;
    private const int ImperialUnitIndex = 1;

    public MainPage()
    {
        InitializeComponent();
        ApplyUnitLabels();
    }

    private void OnCalculateClicked(object? sender, EventArgs e)
    {
        var isHeightValid = double.TryParse(HeightEntry.Text, out var heightCm);
        var isWeightValid = double.TryParse(WeightEntry.Text, out var weightKg);

        if (!isHeightValid || !isWeightValid || heightCm <= 0 || weightKg <= 0)
        {
            ResultLabel.Text = "BMI: -";
            CategoryLabel.Text = "Category: Please enter valid positive numbers.";
            CategoryBadge.BackgroundColor = GetDefaultBadgeColor();
            SemanticScreenReader.Announce(CategoryLabel.Text);
            return;
        }

        double bmi;
        if (UnitPicker.SelectedIndex == ImperialUnitIndex)
        {
            var heightInches = heightCm;
            var weightPounds = weightKg;
            bmi = 703.0 * weightPounds / (heightInches * heightInches);
        }
        else
        {
            var heightMeters = heightCm / 100.0;
            bmi = weightKg / (heightMeters * heightMeters);
        }

        var category = GetBmiCategory(bmi);

        ResultLabel.Text = $"BMI: {bmi:F1}";
        CategoryLabel.Text = $"Category: {category}";
        CategoryBadge.BackgroundColor = GetCategoryColor(category);
        SemanticScreenReader.Announce($"{ResultLabel.Text}. {CategoryLabel.Text}");
    }

    private void OnUnitChanged(object? sender, EventArgs e)
    {
        ApplyUnitLabels();
        HeightEntry.Text = string.Empty;
        WeightEntry.Text = string.Empty;
        ResultLabel.Text = "BMI: -";
        CategoryLabel.Text = "Category: -";
        CategoryBadge.BackgroundColor = GetDefaultBadgeColor();
    }

    private void ApplyUnitLabels()
    {
        if (UnitPicker.SelectedIndex == ImperialUnitIndex)
        {
            HeightLabel.Text = "Height (in)";
            HeightEntry.Placeholder = "e.g. 69";
            WeightLabel.Text = "Weight (lb)";
            WeightEntry.Placeholder = "e.g. 154";
            return;
        }

        HeightLabel.Text = "Height (cm)";
        HeightEntry.Placeholder = "e.g. 175";
        WeightLabel.Text = "Weight (kg)";
        WeightEntry.Placeholder = "e.g. 70";
    }

    private static string GetBmiCategory(double bmi)
    {
        if (bmi < 18.5)
            return "Underweight";
        if (bmi < 25)
            return "Normal weight";
        if (bmi < 30)
            return "Overweight";

        return "Obese";
    }

    private static Color GetDefaultBadgeColor()
    {
        return GetColorResource("Gray200", Colors.LightGray);
    }

    private static Color GetCategoryColor(string category)
    {
        var colorKey = category switch
        {
            "Underweight" => "BmiUnderweight",
            "Normal weight" => "BmiNormal",
            "Overweight" => "BmiOverweight",
            "Obese" => "BmiObese",
            _ => "Gray200"
        };

        return GetColorResource(colorKey, Colors.LightGray);
    }

    private static Color GetColorResource(string key, Color fallback)
    {
        if (Application.Current?.Resources.TryGetValue(key, out var value) == true && value is Color color)
            return color;

        return fallback;
    }
}