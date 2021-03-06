using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Raspador.Data;
using Raspador.Models;

namespace Raspador.Services
{
    public class SnapshotInfoService : ISnapshotInfoService
    {
        private readonly ApplicationDbContext _context;

        public SnapshotInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SnapshotInfo[]> GetSnapshotAsync(IdentityUser user)
        {
            var snapshots = await _context.Snapshots
                    .Where(x => x.UserId == user.Id)
                    .ToArrayAsync();
            return snapshots;
        }
        
        public async Task<bool> AddSnapshotAsync(SnapshotInfo newSnapshotInfo, IdentityUser user)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--headless");

//            Initialize Driver
            ChromeDriver driver = new ChromeDriver(options);

//            Navigate to Yahoo Login
            driver.Navigate()
                .GoToUrl(
                    "https://login.yahoo.com/?.src=finance&.intl=us&.done=https%3A%2F%2Ffinance.yahoo.com%2Fportfolios&add=1");

//            Maximize Browser Window
            try
            {
                driver.Manage().Window.Maximize();

            }
            catch (Exception)
            {
                Console.WriteLine("Window did not maximize");
            }

//            Wait 3 Seconds
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

//            Enter username
            driver.FindElement(By.Id("login-username")).SendKeys("testfinance@yahoo.com" + Keys.Enter);

//            Wait 3 Seconds
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

//            Enter password
            driver.FindElement(By.Name("password")).SendKeys("3eggWhites6Almonds" + Keys.Enter);

//            Close popup
            try
            {
                driver.FindElement(By.XPath("//*[@id=\"__dialog\"]/section/button")).Click();
            }
            catch (Exception)
            {
                Console.WriteLine("No pop up window to close.");
            }

//            Click on "My Watchlist"
            driver.FindElement(By.XPath("//*[@id=\"main\"]/section/section/div[2]/table/tbody/tr/td[1]/a")).Click();

//            Click on "My Holdings"
            driver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[1]/ul/li[2]/a")).Click();

//            Get Portfolio Info
            var netWorth = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/header/div/div[1]/div/div[2]/p[1]"))
                .Text;
            var dgdgp = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/header/div/div[1]/div/div[2]/p[2]/span"))
                .Text;
            var dgdgpSplit = dgdgp.Split(" ");
            var dayGain = dgdgpSplit[0];
            var dayGainPercent = dgdgpSplit[1];
            var tgtgp = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/header/div/div[1]/div/div[2]/p[3]/span"))
                .Text;
            var tgtgpSplit = tgtgp.Split(" ");
            var totalGain = tgtgpSplit[0];
            var totalGainPercent = tgtgpSplit[1];

//                 Creates the database if it does not exist
                _context.Database.EnsureCreated();

//                Adds a portfolio

            newSnapshotInfo.NetWorth = netWorth;
            newSnapshotInfo.DayGainChange = dayGain;
            newSnapshotInfo.DayGainPercent = dayGainPercent;
            newSnapshotInfo.TotalGain = totalGain;
            newSnapshotInfo.TotalGainPercent = totalGainPercent;
            newSnapshotInfo.Date = DateTime.Now;
            newSnapshotInfo.UserId = user.Id;

//            _context.Snapshots.Add(newSnapshotInfo);

            var SnapshotStocks = new List<Stock>();

//            List<Stock> stock = new List<Stock>();
                List<String> stockRowData = new List<string>();

//            Find table
                var stockTable = driver.FindElement(By.ClassName("tJDbU "));

//            Find Table Rows
                var stockTableRows = new List<IWebElement>(stockTable.FindElements(By.TagName("tr")));

//            Go over each stockTableRow
                foreach (var stockTableRow in stockTableRows)
                {
//                Get the columns in a stockTableRow
                    var stockTableColumns = new List<IWebElement>(stockTableRow.FindElements(By.TagName("td")));

                    if (stockTableColumns.Count > 0)
                    {
//                    Go over each column adding data to stockRowData
                        foreach (var stockTableColumn in stockTableColumns)
                        {
                            stockRowData.Add(stockTableColumn.Text);
                        }

                        var stockRowColumn0 = stockRowData[0].Split("\n");
                        var stockRowColumn1 = stockRowData[1].Split("\n");
                        var stockRowColumn5 = stockRowData[5].Split("\n");
                        var stockRowColumn6 = stockRowData[6].Split("\n");

                        var stockSymbol = stockRowColumn0[0];
                        var lastStockPrice = stockRowColumn0[1];
                        var stockPriceChange = stockRowColumn1[1];
                        var stockPricePercentageChange = stockRowColumn1[0];
                        var shares = stockRowData[2];
                        var costBasis = stockRowData[3];
                        var marketValue = stockRowData[4];
                        var stockDayGain = stockRowColumn5[1];
                        var stockDayGainPercentage = stockRowColumn5[0];
                        var stockTotalGain = stockRowColumn6[1];
                        var stockTotalGainPercentage = stockRowColumn6[0];
                        var stockNumofLots = stockRowData[7];
                        var stockNotes = stockRowData[8];

                        // Adds a stock
                        
                        SnapshotStocks.Add(new Stock()
                        {
                            Symbol = stockSymbol,
                            LastPrice = lastStockPrice,
                            PriceChange = stockPriceChange,
                            PricePercentChange = stockPricePercentageChange,
                            Shares = shares,
                            CostBasis = costBasis,
                            MarketValue = marketValue,
                            DayGainChange = stockDayGain,
                            DayGainPercentChange = stockDayGainPercentage,
                            TotalGainChange = stockTotalGain,
                            TotalGainPercentChange = stockTotalGainPercentage,
                            NumOfLots = stockNumofLots,
                            Notes = stockNotes,
                            Date = DateTime.Now,
                            UserId = user.Id
                        });

                    }

                    stockRowData.Clear();
                }
            
            
            driver.Quit();
            newSnapshotInfo.Stocks = SnapshotStocks;
            
            _context.Snapshots.Add(newSnapshotInfo);

            
            var saveResult = await _context.SaveChangesAsync();            
            return saveResult == 1;

        }
    }
}