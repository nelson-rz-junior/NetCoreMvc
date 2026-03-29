using Microsoft.AspNetCore.Mvc;

namespace NetCoreMvc.Controllers;

public class ModalController : Controller
{
    public IActionResult JqueryConfirmIndex()
    {
        return View();
    }

    public IActionResult JqueryConfirmSuccess()
    {
        ViewData["Success"] = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.";

        return View("JqueryConfirmIndex");
    }

    public IActionResult JqueryConfirmError()
    {
        ViewData["Error"] = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.";

        return View("JqueryConfirmIndex");
    }

    public IActionResult JqueryConfirmInfo()
    {
        ViewData["Info"] = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.";

        return View("JqueryConfirmIndex");
    }

    public IActionResult JqueryConfirmWarning()
    {
        ViewData["Warning"] = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.";

        return View("JqueryConfirmIndex");
    }

    public IActionResult BootboxJsIndex()
    {
        return View();
    }

    public IActionResult BootboxJsSuccess()
    {
        ViewData["Success"] = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.";

        return View("BootboxJsIndex");
    }

    public IActionResult BootboxJsError()
    {
        ViewData["Error"] = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.";

        return View("BootboxJsIndex");
    }

    public IActionResult BootboxJsInfo()
    {
        ViewData["Info"] = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.";

        return View("BootboxJsIndex");
    }

    public IActionResult BootboxJsWarning()
    {
        ViewData["Warning"] = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.";

        return View("BootboxJsIndex");
    }
}
