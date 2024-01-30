using System.ComponentModel;

namespace BaseShop.Models ;

 
    public class Product: INotifyPropertyChanged
    {
        public string ProductName { get; set; }
        public string ProductUnitDesc {get; set; }
        public string ProductDesc {get; set; }
        public double EndUserPrice {get; set; }
        public string BrandTitle {get; set; }
        public int TsCodeID {get; set; }
        public double Reduction {get; set; }
        public bool isActive {get; set; }
        public bool isSaleable {get; set; }
        public int BarcodeID {get; set; }
        public string KBarCode {get; set; }
        public string Color {get; set; }
        public string Size {get; set; }
        public string Picture { get; set; }
        public int ProductID { get; set; }
        public int GroupID {get; set; }
        public double SalePrice { get; set; }
        private int quantity;
        public int Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }
        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ProductsRootObject
    {
        public List<Product> Value { get; set; }
    
        public void RemoveDuplicateProducts()
        {
            if (Value == null || Value.Count == 0)
                return;

            Value = Value.GroupBy(p => p.ProductID)
                .Select(g => g.First())
                .ToList();
        }
    }
