using Microsoft.AspNetCore.Mvc;
using NetCoreMvc.Extensions;
using NetCoreMvc.Models;
using NetCoreMvc.Services;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace NetCoreMvc.Controllers;

public class HomeController : Controller
{
    private readonly IEmailSender _emailSender;
    private readonly IPdfGenerator _pdfGenerator;
    private readonly IFileDownloader _fileDownloader;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IConfiguration _configuration;
    private readonly ILogger<HomeController> _logger;
    private readonly string[] permittedExtensions = { ".gif", ".jpg", ".jpeg", ".png" };

    public HomeController(IEmailSender emailSender, IPdfGenerator pdfGenerator, IFileDownloader fileDownloader, IHostEnvironment hostEnvironment,
        IConfiguration configuration, ILogger<HomeController> logger)
    {
        _emailSender = emailSender;
        _pdfGenerator = pdfGenerator;
        _fileDownloader = fileDownloader;
        _hostEnvironment = hostEnvironment;
        _configuration = configuration;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ListDownloadFiles()
    {
        List<DownloadFileModel> model = new();

        var uploadFolder = "wwwroot/files/";

        if (!Directory.Exists(uploadFolder))
        {
            return View(new List<DownloadFileModel>());
        }
        else
        {
            string[] fileEntries = Directory.GetFiles(uploadFolder);
            foreach (string fileName in fileEntries)
            {
                FileInfo file = new(fileName);

                model.Add(new DownloadFileModel
                {
                    FileName = file.Name,
                    Size = file.Length,
                    LastAccess = file.LastAccessTime
                });
            }
        }
            
        return View(model.OrderByDescending(m => m.LastAccess));
    }

    public IActionResult DownloadFiles(string fileName)
    {
        string physicalPath = $"wwwroot/files/{fileName}";

        var (ms, contentType, errorMessage) = _fileDownloader.DownloadFile(physicalPath);
        if (errorMessage is not null)
        {
            return Content(errorMessage);
        }
        else
        {
            if (ms is not null && contentType is not null)
            {
                return File(ms, "application/force-download", physicalPath);
            }

            return Content("File not found.");
        }
    }

    public IActionResult UploadFiles()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UploadFiles(UploadFileModel model)
    {
        if (ModelState.IsValid)
        {
            var formFiles = model.Files;

            if (formFiles == null || formFiles.Count == 0)
            {
                ModelState.AddModelError("Files", "Uploaded files is empty or null.");
            }
            else
            {
                var uploadFolder = "wwwroot/files/";
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                long size = formFiles.Sum(f => f.Length);
                long limitSizeFile = _configuration.GetValue<long>("FileSizeLimit");

                if (size > limitSizeFile)
                {
                    ModelState.AddModelError("Files", $"Maximum allowed file sizes is {limitSizeFile} bytes.");
                }
                else
                {
                    List<string> filesNotPermitted = new();
                    foreach (var formFile in formFiles)
                    {
                        var extension = Path.GetExtension(formFile.FileName).ToLowerInvariant();
                        if (!string.IsNullOrWhiteSpace(extension) && permittedExtensions.Contains(extension))
                        {
                            if (formFile.Length > 0)
                            {
                                var fileName = $"{Guid.NewGuid().ToString().Replace("-", "")[..16].ToLowerInvariant()}{extension}";
                                var filePath = $"wwwroot/files/{fileName}";

                                using var stream = System.IO.File.Create(filePath);
                                await formFile.CopyToAsync(stream);
                            }
                            else
                            {
                                filesNotPermitted.Add($"Empty file not permitted: \"{formFile.FileName}\"");
                            }
                        }
                        else
                        {
                            filesNotPermitted.Add($"File extension not permitted: \"{formFile.FileName}\"");
                        }                        
                    }

                    if (filesNotPermitted.Any())
                    {
                        TempData["FilesNotPermitted"] = JsonSerializer.Serialize(filesNotPermitted);
                    }

                    return RedirectToAction(nameof(ListDownloadFiles));
                }
            }
        }

        return View(model);
    }

    public IActionResult GenerateQrCode()
    {
        return View(new QRCodeModel());
    }

    [HttpPost]
    public IActionResult GenerateQrCode([FromServices] QRCodeService qrCodeService, QRCodeModel model)
    {
        if (ModelState.IsValid)
        {
            string plaintText = model.Url;
            int pixelsPerModule = 7;

            string result = qrCodeService.GetQRCodeAsBase64(pixelsPerModule, plaintText);

            return View(new QRCodeModel
            {
                QrCode = result
            });
        }

        return View(model);
    }

    public IActionResult GeneratePdf()
    {
        PdfModel model = new();

        string viewPath = "Views/Home/ExamplePage.cshtml";
        if (System.IO.File.Exists(viewPath))
        {
            model.RazorContent = System.IO.File.ReadAllText(viewPath);
        }

        string viewBackupPath = "Views/_Backup/ExamplePage.bak.cshtml";
        ViewData["ShowRestoreBackup"] = System.IO.File.Exists(viewBackupPath);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateRazorView(PdfModel model)
    {
        string viewPath = "Views/Home/ExamplePage.cshtml";
        string viewBackupPath = "Views/_Backup/ExamplePage.bak.cshtml";

        if (System.IO.File.Exists(viewPath))
        {
            System.IO.File.Copy(viewPath, viewBackupPath, true);

            using StreamWriter sw = new(viewPath, false, Encoding.UTF8);
            await sw.WriteAsync(model.RazorContent);

            TempData["SuccessMessage"] = "Razor view was updated successfully";
        }
        else
        {
            TempData["ErrorMessage"] = "Razor view not found";
        }

        return RedirectToAction(nameof(GeneratePdf));
    }

    [HttpPost]
    public IActionResult RestoreBackupRazorView()
    {
        string viewPath = "Views/Home/ExamplePage.cshtml";
        string viewBackupPath = "Views/_Backup/ExamplePage.bak.cshtml";

        if (System.IO.File.Exists(viewPath) && System.IO.File.Exists(viewBackupPath))
        {
            System.IO.File.Copy(viewBackupPath, viewPath, true);
            TempData["SuccessMessage"] = "Last backup was restored successfully";
        }
        else
        {
            TempData["ErrorMessage"] = "Razor view not found";
        }

        return RedirectToAction(nameof(GeneratePdf));
    }

    [HttpPost]
    public async Task<IActionResult> GeneratePdf(PdfModel model)
    {
        if (ModelState.IsValid)
        {
            var exampleMode = new List<ExampleModel>
            {
                new ExampleModel { Id = 1, FirstName = "Mark", LastName = "Otto", Age = 30 },
                new ExampleModel { Id = 2, FirstName = "Jacob", LastName = "Thornton", Age = 40 },
                new ExampleModel { Id = 3, FirstName = "Larry the Bird", LastName = "", Age = 50 }
            };

            var html = await this.RenderViewAsync("ExamplePage", exampleMode);
            var fileContents = _pdfGenerator.GeneratePdf(html);

            if (fileContents != null)
            {
                return File(new MemoryStream(fileContents), "application/force-download", "Content.pdf");
            }
        }

        return View(model);
    }

    public IActionResult SendEmail()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SendEmail(SendEmailModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _emailSender.SendEmailAsync(model.FullName, model.Address, model.Subject, model.Message);
                ViewData["SuccessMessage"] = "E-mail was sent succesfully";

                return View();
            }
        }
        catch (Exception ex)
        {
            ViewData["ErrorMessage"] = $"An error occurred while sending the e-mail message: {ex.Message}";
        }

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}