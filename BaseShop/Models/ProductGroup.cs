namespace BaseShop.Models ;

    
    public class ProductGroup
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public int GroupLevel { get; set; }
        public double GroupDiscount { get; set; }
        public string Tags { get; set; }
        public int TsCodeID { get; set; }
        public int OnlinePriority { get; set; }
        public bool IsOnlineActive { get; set; }


    }
    public class ProductGroupRootObject
    {
        public List<ProductGroup> Value { get; set; }
    }
