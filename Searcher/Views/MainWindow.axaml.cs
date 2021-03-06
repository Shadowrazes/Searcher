using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Searcher.ViewModels;
using Avalonia.Interactivity;

namespace Searcher.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.FindControl<Button>("OpenFileBtn").Click += async delegate
            {
                var taskPath = new OpenFileDialog()
                {
                    Title = "Search File",
                    Filters = null
                }.ShowAsync((Window)this.VisualRoot);

                string[]? filePath = await taskPath;

                if (filePath != null)
                {
                    var context = this.DataContext as MainWindowViewModel;
                    context.Path = string.Join(@"\", filePath);
                }
            };

            this.FindControl<Button>("SaveFileBtn").Click += async delegate
            {
                var taskPath = new OpenFileDialog()
                {
                    Title = "Search File",
                    Filters = null
                }.ShowAsync((Window)this.VisualRoot);

                string[]? filePath = await taskPath;

                if (filePath != null)
                {
                    var context = this.DataContext as MainWindowViewModel;
                    context.saveFile(string.Join(@"\", filePath), this.FindControl<TextBlock>("Output").Text);
                }
            };

            this.FindControl<TextBox>("Input").KeyUp += delegate
            {
                var context = this.DataContext as MainWindowViewModel;
                context.InputContent = this.FindControl<TextBox>("Input").Text;
                context.Filter = context.Filter;
            };
        }

        private async void OpenSetRegex(object control, RoutedEventArgs arg)
        {
            string currentFilter = "";
            var context = this.DataContext as MainWindowViewModel;
            if(context != null)
                currentFilter = context.Filter;
            string? filter = await new SearchFilter(currentFilter).ShowDialog<string?>((Window)this.VisualRoot);
            if (filter == null)
                filter = "";
            if(filter != "Cancel" && context != null)
            {
                context.Filter = filter;
            }
        }
    }
}
