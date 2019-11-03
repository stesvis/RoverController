using RoverController.Lib;
using System;
using Xamarin.Forms;

namespace RoverController.Mobile.Behaviors
{
    public class RequiredPickerValidationBehavior : Behavior<Picker>
    {
        #region Properties

        private static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly(
            nameof(IsValid),
            typeof(bool),
            typeof(RequiredPickerValidationBehavior),
            false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }

        #endregion Properties

        protected override void OnAttachedTo(Picker bindable)
        {
            bindable.SelectedIndexChanged += OnSelectedIndexChanged;
            base.OnAttachedTo(bindable);
        }

        private void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(sender is Picker bindable))
                {
                    IsValid = false;
                    return;
                }

                if (bindable.SelectedIndex < 0)
                {
                    IsValid = false;
                    return;
                }

                IsValid = !bindable.SelectedItem.ToString().IsEmpty();
            }
            catch (Exception)
            {
                IsValid = false;
            }
        }

        protected override void OnDetachingFrom(Picker bindable)
        {
            bindable.SelectedIndexChanged -= OnSelectedIndexChanged;
            base.OnDetachingFrom(bindable);
        }
    }
}