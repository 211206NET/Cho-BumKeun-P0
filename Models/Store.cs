using CustomExceptions;
using System.Text.RegularExpressions;
using System.Data;

namespace Models;

public class Store {
    public Store()
    {
        this.Orders = new List<Order>();
    }
    public Store(string name)
    {
        this.Orders = new List<Order>();
        this._name = name;
    }

    /// <summary>
    /// Converting Restaurant table's data row into Restaurant Object
    /// </summary>
    /// <param name="row">a data row from Restaurant object, must have id, name, city, state columns</param>
    public Store(DataRow row)
    {
        this.Id = (int) row["Id"];
        this.Name = row["Name"].ToString() ?? "";
        this.City = row["City"].ToString() ?? "";
        this.State = row["State"].ToString() ?? "";
    }

    public int Id { get; set; }

    private string _name;
    public string Name {
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

    public string City { get; set; }
    public string State { get; set; }
    public List<Order> Orders { get; set; }

    public override string ToString()
    {
        return $"Id: {this.Id} \nName: {this.Name} \nCity: {this.City} \nState: {this.State} \n==================================";
    }

    /// <summary>
    /// Takes in Restaurant Table's DataRow and fills the columns with the Restaurant Instance's info
    /// </summary>
    /// <param name="row">Restaurant Table's DataRow pass by ref</param>
    public void ToDataRow(ref DataRow row)
    {
        row["Name"] = this.Name;
        row["City"] = this.City;
        row["State"] = this.State;
    }
}