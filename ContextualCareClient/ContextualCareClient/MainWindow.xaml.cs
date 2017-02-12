using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace ContextualCareClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void textBox1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private async void submit_Click(object sender, RoutedEventArgs e)
        {
            InputValues inputValues = new InputValues();
            inputValues.Age = Int32.Parse(age.Text);
            inputValues.BlurredVision = GetIntValueFromChoices(visionYes);
            inputValues.Diabetic = GetIntValueFromChoices(diabeticYes);
            inputValues.DopplerHBeatCount = GetIntValueFromChoices(countYes);
            inputValues.FatigueLevel = ((ComboBoxItem)fatigueLevels.SelectedItem).Name;
            inputValues.FeelThirsty = GetIntValueFromChoices(thirstyYes);
            inputValues.FrequentFetalMovements = GetIntValueFromChoices(movementsYes);
            inputValues.FrequentUrination = GetIntValueFromChoices(urinateYes);
            inputValues.FundusHeightBeyondRange = GetIntValueFromChoices(rangeYes);
            inputValues.HcGLevel = ((ComboBoxItem)hcGValues.SelectedItem).Name;
            inputValues.Hypertension = GetIntValueFromChoices(highBPYes);
            inputValues.PastHypertensionHistory = GetIntValueFromChoices(hyperHistoryYes);
            inputValues.SugarLevelInUrine = float.Parse(urinesugar.Text);
            inputValues.UltrasoundConfirmation = ((ComboBoxItem)usConfirmations.SelectedItem).Name;
            inputValues.WeightGain = GetIntValueFromChoices(weightYes);

            var result = await ContextualCareServiceProxy.InvokeRequestResponseService(inputValues);
            var jsonResult = JsonConvert.DeserializeObject(result);
            Pathway pathway = ExtractAndConvertToPathway(jsonResult);
        }

        private Pathway ExtractAndConvertToPathway(object jsonResult)
        {
            throw new NotImplementedException();
        }

        private int GetIntValueFromChoices(RadioButton radioButton)
        {
            if ((bool)radioButton.IsChecked)
                return 1;
            else
                return 0;
        }
    }
}
