using System.Windows.Controls;
using System.Windows.Data;
using ViewModels;
using CORE.ColourConverters;
using Interfaces;
using System.Windows.Input;

namespace UI
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : UserControl
    {
        private GenerationViewModel generationViewModel;

        public View()
        {
            InitializeComponent();
            generationViewModel = new GenerationViewModel(UI.Properties.Settings.Default.BoardSize);
            GenerateBoard(generationViewModel);
            DataContext = generationViewModel;
        }

        private void GenerateBoard(GenerationViewModel vm)
        {
            for (int y = 0; y < vm.UniverseSize; y++)
            {
                Board.RowDefinitions.Add(new RowDefinition());
                for (int x = 0; x < vm.UniverseSize; x++)
                {
                    if (y == 0) Board.ColumnDefinitions.Add(new ColumnDefinition());
                    Label cellLabel = CreateCell(vm.GetCell(y, x));

                    Grid.SetRow(cellLabel, y);
                    Grid.SetColumn(cellLabel, x);

                    Board.Children.Add(cellLabel);
                }
            }
        }

        private Label CreateCell(ICell cell)
        {
            Label cellLabel = new Label();
            cellLabel.DataContext = cell;
            cellLabel.InputBindings.Add(ToggleLiveClickBinding(cell));
            cellLabel.SetBinding(Label.BackgroundProperty, IsAliveBackgroundBinding());
            cellLabel.Height = 20;
            cellLabel.Width = 20;
            return cellLabel;
        }

        private Binding IsAliveBackgroundBinding()
        {
            Binding binding = new Binding();
            binding.Path = new System.Windows.PropertyPath("IsAlive");
            binding.Mode = BindingMode.TwoWay;
            binding.Converter = new StandardColourConverter();

            return binding;
        }

        private InputBinding ToggleLiveClickBinding(ICell cell)
        {
            InputBinding inputBinding = new InputBinding(
                generationViewModel.ToggleLifeCommand,
                new MouseGesture(MouseAction.LeftClick)
                );

            if (cell != null)
                inputBinding.CommandParameter = string.Format("{0},{1}", cell.Y, cell.X);

            return inputBinding;
        }
    }
}
