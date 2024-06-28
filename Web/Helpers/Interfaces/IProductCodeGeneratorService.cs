namespace Web.Helpers.Interfaces
{
    public interface IProductCodeGeneratorService
    {
        string GenerateBarcode(string productCode, string barCodeText);
        bool RemoveBarcode(string barcodeImgPath);

        string GenerateQRCode(string productCode, string qrCodeText);
        bool RemoveQRCode(string qrcodeImgPath);
    }
}
