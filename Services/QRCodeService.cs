using QRCoder;

namespace NetCoreMvc.Services;

public class QRCodeService
{
    private readonly QRCodeGenerator _generator;

    public QRCodeService(QRCodeGenerator generator)
    {
        _generator = generator;
    }

    public string GetQRCodeAsBase64(int pixelsPerModule, string plaintText)
    {
        QRCodeData qrCodeData = _generator.CreateQrCode(plaintText, QRCodeGenerator.ECCLevel.Q);
        PngByteQRCode qrCode = new(qrCodeData);

        byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(pixelsPerModule);

        return Convert.ToBase64String(qrCodeAsPngByteArr);
    }
}
