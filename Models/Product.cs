using CustomExceptions;
using System.Text.RegularExpressions;
using System.Data;

namespace Models;

public class Product {
    public Product()
    {
        this.Orders = new List<Order>();
    }
    public Product(string name)
    {
        this.Orders = new List<Order>();
        this._name = name;
    }

    public Product(DataRow row)
    {
        this.Id = (int) row["Id"];
        this.Title = row["Title"].ToString() ?? "";
        this.Price = (decimal) row["Price"];
        this.Developer = row["Developer"].ToString() ?? "";
        this.Inventory = (int) row["Inventory"];
    }

    public int Id { get; set; }

    private string _name;
    public string Title {
        get => _name;
        set
        {
            Regex pattern = new Regex("^[a-zA-Z0-9 !?']+$");
            if(string.IsNullOrWhiteSpace(value))
            {
                throw new InputInvalidException("Name can't be empty");
            }
            else if(!pattern.IsMatch(value))
            {
                throw new InputInvalidException("Restaurant name can only have alphanumeric characters, white space, !, ?, and '.");
            }
            else
            {
                this._name = value;
            }
        } 
    }

    public decimal Price { get; set; }
    public string Developer { get; set; }
    public int Inventory { get; set; }
    public List<Order> Orders { get; set; }

    public override string ToString()
    {
        return $"Id: {this.Id} \nTitle: {this.Title} \nDeveloper: {this.Developer} \nPrice: {this.Price} \nInventory: {this.Inventory} \n==================================";
    }

    public void ToDataRow(ref DataRow row)
    {
        row["Title"] = this.Title;
        row["Developer"] = this.Developer;
        row["Price"] = this.Price;
        row["Inventory"] = this.Inventory;
    }
}