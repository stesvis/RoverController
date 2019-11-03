using RoverController.Lib;
using Xamarin.Forms;

namespace RoverController.Mobile.Behaviors
{
    public class RequiredEntryValidationBehavior : Behavior<Entry>
    {
        #region Properties

        private static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly(
            nameof(IsValid),
            typeof(bool),
            typeof(RequiredEntryValidationBehavior),
            false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }

        #endregion Properties

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += OnTextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= OnTextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs args)
        {
            if (args.NewTextValue == null)
            {
                IsValid = false;
                return;
            }

            IsValid = !args.NewTextValue.IsEmpty();
        }
    }
}