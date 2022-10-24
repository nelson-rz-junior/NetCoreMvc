using Microsoft.AspNetCore.Mvc;

namespace NetCoreMvc.Controllers;

public class PlotlyController : Controller
{
    public IActionResult ScatterPlots()
    {
        return View();
    }

    public IActionResult LineGraphs()
    {
        return View();
    }

    public IActionResult LinearGraphs()
    {
        return View();
    }

    public IActionResult MultipleLines()
    {
        return View();
    }

    public IActionResult BarCharts()
    {
        return View();
    }

    public IActionResult HorizontalBarCharts()
    {
        return View();
    }

    public IActionResult PieCharts()
    {
        return View();
    }

    public IActionResult DonutCharts()
    {
        return View();
    }

    public JsonResult GetHousePriceBySize()
    {
        var data = new[]
        {
            new { SquareMeters = 50, PriceInMillions = 7 },
            new { SquareMeters = 60, PriceInMillions = 8 },
            new { SquareMeters = 70, PriceInMillions = 8 },
            new { SquareMeters = 80, PriceInMillions = 9 },
            new { SquareMeters = 90, PriceInMillions = 9 },
            new { SquareMeters = 100, PriceInMillions = 9 },
            new { SquareMeters = 110, PriceInMillions = 10 },
            new { SquareMeters = 120, PriceInMillions = 11 },
            new { SquareMeters = 130, PriceInMillions = 14 },
            new { SquareMeters = 140, PriceInMillions = 14 },
            new { SquareMeters = 150, PriceInMillions = 15 }
        };

        return Json(data);
    }

    public JsonResult GetWorldWineProduction()
    {
        var data = new[]
        {
            new { Country = "Italy", ProductionInMhl = 55 },
            new { Country = "France", ProductionInMhl = 49 },
            new { Country = "Spain", ProductionInMhl = 44 },
            new { Country = "USA", ProductionInMhl = 24 },
            new { Country = "Argentina", ProductionInMhl = 15 }
        };

        return Json(data);
    }
}
