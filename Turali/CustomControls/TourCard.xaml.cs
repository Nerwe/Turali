using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Turali.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для TourCard.xaml
    /// </summary>
    public partial class TourCard : UserControl
    {
        public TourCard()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty CommandProperty =
          DependencyProperty.Register(
              nameof(Command),
              typeof(ICommand),
              typeof(TourCard),
              new PropertyMetadata(null));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(
                nameof(CommandParameter),
                typeof(object),
                typeof(TourCard),
                new PropertyMetadata(null));

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
    }
}
