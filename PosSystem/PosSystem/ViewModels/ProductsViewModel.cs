using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PosSystem.Models;
using PosSystem.Services.Interfaces;

namespace PosSystem.ViewModels
{
    public class ProductsViewModel : INotifyPropertyChanged
    {
        private readonly IProductRepository _productRepo;
        private List<Product> _allProducts = new();
        private Client _currentClient;
        private string _currentFilter = string.Empty;

        public ObservableCollection<Product> Products { get; } = new();
        public bool IsLoading { get; private set; }
        public ICommand LoadProductsCommand { get; }
        public ICommand AddToCartCommand { get; }

        public ProductsViewModel(IProductRepository productRepository)
        {
            _productRepo = productRepository;
            LoadProductsCommand = new Command(async () => await LoadProductsAsync());
            AddToCartCommand = new Command<Product>(AddToCart);
        }

        public async Task InitializeAsync()
        {
            await LoadProductsAsync();
            if (_currentClient != null)
            {
                ApplyClientPreferences();
            }
        }

        public async Task LoadProductsAsync()
        {
            IsLoading = true;
            OnPropertyChanged(nameof(IsLoading));

            try
            {
                _allProducts = await _productRepo.GetProductsAsync();
                ApplyFilters();
            }
            finally
            {
                IsLoading = false;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public void FilterProducts(string filterText)
        {
            _currentFilter = filterText;
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            Products.Clear();

            var filtered = string.IsNullOrWhiteSpace(_currentFilter)
                ? _allProducts
                : _allProducts.Where(p =>
                    p.Title.Contains(_currentFilter, StringComparison.OrdinalIgnoreCase) ||
                    p.Category.Contains(_currentFilter, StringComparison.OrdinalIgnoreCase));

            foreach (var product in filtered)
            {
                Products.Add(product);
            }
        }

        private void ApplyClientPreferences()
        {
            if (!string.IsNullOrEmpty(_currentClient.PreferredCategory))
            {
                foreach (var product in Products)
                {
                    product.IsRecommended = product.Category.Equals(
                        _currentClient.PreferredCategory,
                        StringComparison.OrdinalIgnoreCase);
                }
            }
        }

        public void InitializeForClient(Client client)
        {
            _currentClient = client;
            _currentFilter = string.Empty;
            ApplyFilters();
            ApplyClientPreferences();
        }

        private void AddToCart(Product product)
        {
            if (product == null) return;
            Console.WriteLine($"Producto agregado: {product.Title}");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
