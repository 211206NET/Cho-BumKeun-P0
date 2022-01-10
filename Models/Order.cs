using CustomExceptions;
using System.Text.RegularExpressions;
using System.Data;

namespace Models;

public class Order {
    public Order() { }

    public Order(int quantity)
    {
        this.Quantity = quantity;
    }

    public Order(DataRow row)
    {
        this.Id = (int) row["Id"];
        this.StoreId = (int) row["StoreId"];
        this.StoreName = row["StoreName"].ToString() ?? "";
        this.ProductId = (int) row["ProductId"];
        this.ProductName = row["ProductName"].ToString() ?? "";
        this.Quantity = (int) row["Quantity"];
        this.TotalPrice = (decimal) row["TotalPrice"];
        this.UserId = (int) row["UserId"];
    }

    public int Id { get; set; }
    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal TotalPrice { get; set; }
    public int UserId { get; set; }

    private int _quantity;
    public int Quantity
    {
        get => _quantity;
        set
        {
            if(value <= 0 || value > 10)
            {
                throw new InputInvalidException("You can only purchase a max of 10 copies per order");
            }
            this._quantity = value;
        }
    }

    public override string ToString()
    {
        return $"Id: {this.Id} \nStore: {this.StoreName} \nProduct: {this.ProductName} \nQuantity: {this.Quantity} \nPrice: {this.TotalPrice} \n==================================";
    }

    public void ToDataRow(ref DataRow row)
    {
        row["Store"] = this.StoreName;
        row["Product"] = this.ProductName;
        row["Quantity"] = this.Quantity;
        row["TotalPrice"] = this.TotalPrice;
    }
}