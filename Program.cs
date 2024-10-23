using System;
using System.Collections.Generic;

class Produk
{
    public string NamaProduk;
    public decimal HargaProduk;

    public Produk(string namaProduk, decimal hargaProduk)
    {
        this.NamaProduk = namaProduk;
        this.HargaProduk = hargaProduk;
    }

    public virtual decimal Ongkir()
    {
        return 0;
    }

    public virtual void InfoProduk()
    {
        Console.WriteLine($"Nama Produk: {NamaProduk}, Harga: {HargaProduk:C}");
    }
}

class Elektronik : Produk
{
    public double BeratProduk;

    public Elektronik(string namaProduk, decimal hargaProduk, double beratProduk) : base(namaProduk, hargaProduk)
    {
        this.BeratProduk = beratProduk;
    }

    public override decimal Ongkir()
    {
        return (decimal)(BeratProduk * 5000); //Ongkos kirim 5000 per kg
    }

    public override void InfoProduk()
    {
        base.InfoProduk();
        Console.WriteLine($"Berat: {BeratProduk} kg, Ongkos Kirim: {Ongkir():C}");
    }
}

class Baju : Produk
{
    public static readonly decimal OngkirTetap = 10000; 

    public Baju(string namaProduk, decimal hargaProduk) : base(namaProduk, hargaProduk)
    {
    }

    public override decimal Ongkir()
    {
        return OngkirTetap;
    }

    public override void InfoProduk()
    {
        base.InfoProduk();
        Console.WriteLine($"Ongkos Kirim: {OngkirTetap:C}");
    }
}

class Buku : Produk
{
    public static readonly decimal OngkirTetap = 5000; 

    public Buku(string namaProduk, decimal hargaProduk) : base(namaProduk, hargaProduk)
    {
    }

    public override decimal Ongkir()
    {
        return OngkirTetap;
    }

    public override void InfoProduk()
    {
        base.InfoProduk();
        Console.WriteLine($"Ongkos Kirim: {OngkirTetap:C}");
    }
}

class KeranjangBelanja
{
    private List<Produk> daftarProduk = new List<Produk>();

    public void TambahProduk(Produk produk)
    {
        daftarProduk.Add(produk);
    }

    public decimal TotalHarga()
    {
        decimal total = 0;
        foreach (var produk in daftarProduk)
        {
            total += produk.HargaProduk;
        }
        return total;
    }

    public decimal TotalHarga(bool diskon)
    {
        decimal total = TotalHarga();
        if (diskon)
        {
            total -= total * 0.10m; // Diskon 10%
        }
        return total;
    }

    public decimal TotalHarga(bool diskon, bool denganOngkir)
    {
        decimal total = TotalHarga(diskon);
        if (denganOngkir)
        {
            foreach (var produk in daftarProduk)
            {
                total += produk.Ongkir();
            }
        }
        return total;
    }

    public void DaftarKeranjang()
    {
        Console.WriteLine("Daftar Produk di Keranjang:");
        foreach (var produk in daftarProduk)
        {
            produk.InfoProduk();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        KeranjangBelanja keranjang = new KeranjangBelanja();

        keranjang.TambahProduk(new Elektronik("Motherboard ROG Strix", 15000000, 3)); // Berat 3 kg
        keranjang.TambahProduk(new Baju("Balenciaga Cotton Jersey", 14900000));
        keranjang.TambahProduk(new Buku("Ensiklopedia IPTEK", 100000));

        keranjang.DaftarKeranjang();

        Console.WriteLine($"\nTotal Harga: {keranjang.TotalHarga():C}");

        Console.WriteLine($"Total Harga Setelah Diskon 10%: {keranjang.TotalHarga(true):C}");

        Console.WriteLine($"Total Harga Dengan Ongkos Kirim: {keranjang.TotalHarga(true, true):C}");
    }
}

