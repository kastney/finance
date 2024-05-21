namespace Finance.Structures;

public struct DetailStruct {
    public float Price;
    public int Count;
    public bool IsPrefixed;

    public DetailStruct(float price, int count = 1) {
        Price = price;
        Count = count;
    }

    public DetailStruct(float price, bool isPrefixed) {
        Price = price;
        IsPrefixed = isPrefixed;
    }
}