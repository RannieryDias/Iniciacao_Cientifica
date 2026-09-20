using LegisVisao.Http;
using NLog;
using System.Collections;
using System.ComponentModel;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows;

namespace LegisVisao
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            IsFetchButtonEnabled = true;

            Loaded += MainWindow_Loaded;
        }

        private bool _isFetchButtonEnabled;

        public bool IsFetchButtonEnabled
        {
            get => _isFetchButtonEnabled;
            set
            {
                if (_isFetchButtonEnabled != value)
                {
                    _isFetchButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Dynamically discover model class names
            var currentAssembly = Assembly.GetExecutingAssembly();

            var modelNames = currentAssembly.GetTypes()
                .Where(t => t.IsClass && t.Namespace != null && t.Namespace.StartsWith("LegisVisao.Models"))
                .Select(t => t.Name)
                .OrderBy(name => name);

            foreach (var name in modelNames)
            {
                RequestTypeComboBox.Items.Add(name);
            }
        }

        private async void FetchButton_Click(object sender, RoutedEventArgs e)
        {
            IsFetchButtonEnabled = false;

            try
            {
                var requestType = RequestTypeComboBox.SelectedItem as string;
                if (string.IsNullOrWhiteSpace(requestType))
                {
                    MessageBox.Show("Selecione um tipo de requisição.");
                    return;
                }

                if (!int.TryParse(ResultsCountTextBox.Text, out int resultsCount) || resultsCount <= 0)
                {
                    MessageBox.Show("Quantidade de resultados inválida.");
                    return;
                }

                if (!int.TryParse(PageIndexTextBox.Text, out int pageIndex) || pageIndex < 1)
                {
                    MessageBox.Show("Índice da página inválido.");
                    return;
                }

                // Convert class name to API endpoint
                string endpoint = requestType.ToLower();
                if (!endpoint.EndsWith("s")) endpoint += "s";

                string url = $"https://dadosabertos.camara.leg.br/api/v2/{endpoint}?pagina={pageIndex}&itens={resultsCount}&ordem=ASC&ordenarPor=id";

                // Use the shared ApiClient
                Logger logger = LogManager.GetCurrentClassLogger();
                using var http = new HttpClient();
                var apiClient = new ApiClient(http, logger);

                // Resolve the DTO Root type
                string dtoTypeName = $"LegisVisao.DTOs.{requestType}DTO.Root";
                Type? dtoType = Type.GetType(dtoTypeName);

                ResultsListBox.Items.Clear();

                if (dtoType != null)
                {
                    // Call GetAsync<Root> via reflection
                    var method = typeof(ApiClient).GetMethod("GetAsync")!.MakeGenericMethod(dtoType);
                    var task = (Task)method.Invoke(apiClient, [url, CancellationToken.None])!;
                    await task.ConfigureAwait(true);

                    var resultProperty = task.GetType().GetProperty("Result");
                    var dtoResult = resultProperty!.GetValue(task);

                    if (dtoResult == null)
                    {
                        MessageBox.Show("Resposta vazia da API.");
                        return;
                    }

                    // Extract the "dados" property (List<Dado>, List<Deputado>, ...)
                    var dadosProperty = dtoType.GetProperty("dados");
                    if (dadosProperty == null)
                    {
                        MessageBox.Show($"O tipo {requestType} não possui uma propriedade 'dados'.");
                        return;
                    }

                    var dadosValue = dadosProperty.GetValue(dtoResult) as IEnumerable;

                    if (dadosValue != null)
                    {
                        foreach (var item in dadosValue)
                        {
                            if (item == null) continue;

                            // Format each DTO as "Prop: Value | Prop: Value | ..."
                            var texto = string.Join(" | ",
                                item.GetType().GetProperties()
                                    .Where(p => p.GetValue(item) != null)
                                    .Select(p => $"{p.Name}: {p.GetValue(item)}"));

                            ResultsListBox.Items.Add(texto);
                        }
                    }

                    if (ResultsListBox.Items.Count == 0)
                    {
                        ResultsListBox.Items.Add("Nenhum resultado encontrado.");
                    }
                }
                else
                {
                    // Fallback to raw JsonElement
                    var jsonElement = await apiClient.GetAsync<JsonElement>(url);
                    var json = jsonElement.GetRawText();

                    ResultsListBox.Items.Add(json);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar dados: {ex.Message}");
            }
            finally
            {
                IsFetchButtonEnabled = true;
            }
        }
    }
}