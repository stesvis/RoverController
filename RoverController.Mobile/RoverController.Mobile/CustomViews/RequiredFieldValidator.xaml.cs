using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RoverController.Mobile.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RequiredFieldValidator : ContentView
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            propertyName: nameof(Text),
            returnType: typeof(string),
            declaringType: typeof(RequiredFieldValidator),
            defaultValue: "Required");

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty ShowErrorProperty = BindableProperty.Create(
           propertyName: nameof(ShowError),
           returnType: typeof(bool),
           declaringType: typeof(RequiredFieldValidator),
           defaultValue: false);

        public bool ShowError
        {
            get { return (bool)GetValue(ShowErrorProperty); }
            set { SetValue(ShowErrorProperty, value); }
        }

        public RequiredFieldValidator()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            textLabel.BindingContext = this;
        }
    }
}