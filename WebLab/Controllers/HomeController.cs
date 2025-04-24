using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

public class HomeController : Controller
{
    private readonly List<ListDemo> _listData;

    public HomeController()
    {
        _listData = new List<ListDemo>
        {
            new ListDemo {Id=1, Name="Item 1"},
            new ListDemo {Id=2, Name="Item 2"},
            new ListDemo {Id=3, Name="Item 3"}
        };
    }

    public IActionResult Index()
    {
        ViewData["Title"] = "Index";
        ViewData["Text"] = "Лабораторная работа №2";
        return View(new SelectList(_listData, "Id", "Name"));
    }
}

public class ListDemo
{
    public int Id { get; set; }
    public string Name { get; set; }
}