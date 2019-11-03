using Syncfusion.SfNumericUpDown.XForms;
using Xamarin.Forms;

namespace RoverController.Mobile.Behaviors
{
    public class RequiredNumericValidationBehavior : Behavior<SfNumericUpDown>
    {
        #region Properties

        private static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly(
            nameof(IsValid),
            typeof(bool),
            typeof(RequiredNumericValidationBehavior),
            false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }

        #endregion Properties

        protected override void OnAttachedTo(SfNumericUpDown bindable)
        {
            //bindable.ValueChanged += OnValueChanged;
            bindable.PropertyChanged += OnPropertyChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(SfNumericUpDown bindable)
        {
            //bindable.ValueChanged -= OnValueChanged;
            bindable.PropertyChanged -= OnPropertyChanged;
            base.OnDetachingFrom(bindable);
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var numericUpDown = sender as SfNumericUpDown;
            if (e.PropertyName == nameof(numericUpDown.Value))
            {
                Validate(sender, numericUpDown.Value);
            }
        }

        private void OnValueChanged(object sender, ValueEventArgs args)
        {
            Validate(sender, args.Value);
        }

        private void Validate(object sender, object value)
        {
            var numericUpDown = sender as SfNumericUpDown;

            if (value == null)
            {
                IsValid = false;
                return;
            }

            IsValid = false;

            if (double.TryParse(value.ToString(), out double numericValue))
            {
                IsValid = numericValue != 0;

                // Now check if the max value is valid
                if (numericUpDown.Maximum != 0)
                {
                    if (numericValue > numericUpDown.Maximum)
                    {
                        IsValid = false;
                    }
                }

                // Now check if the min value is valid
                if (numericUpDown.Minimum != 0)
                {
                    if (numericValue < numericUpDown.Minimum)
                    {
                        IsValid = false;
                    }
                }
            }

            //numericUpDown.TextColor = IsValid ? Color.Black : Color.Red;
        }
    }
}