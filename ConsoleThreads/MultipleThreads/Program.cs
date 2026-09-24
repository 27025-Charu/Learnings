using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    // Shared application state
    private static Dictionary<string, double> portfolio = new Dictionary<string, double> { { "BTC", 1.0 }, { "USD", 10000.0 } };
    private static Dictionary<string, double> marketPrices = new Dictionary<string, double> { { "BTC", 60000.0 } };

    // Automation trigger settings
    private static double stopLossTriggerPrice = 59500.0;
    private static bool stopLossExecuted = false;

    // Mutex locks
    private static readonly object stateLock = new object();
    private static readonly object consoleLock = new object(); // Prevents threads from messing up cursor placements

    static void Main(string[] args)
    {
        Console.Clear();
        InitializeLayout();

        // Thread 1: Background Market & Alert Engine
        Task.Run(() => NotificationThread());

        // Thread 2: Automatic Stop-Loss Risk Monitor
        Task.Run(() => RiskAutomationThread());

        // Thread 3: Active User Menu (Runs on the Main thread)
        DashboardThread();
    }

    private static void InitializeLayout()
    {
        lock (consoleLock)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("=================== CRYPTO-TERMINAL PRO (3-THREADS) ===================");
            Console.WriteLine(" Menu Options:                                       | System Log:");
            Console.WriteLine(" 1. View Portfolio                                   |");
            Console.WriteLine(" 2. Buy 0.1 BTC                                      |");
            Console.WriteLine(" 3. Sell 0.1 BTC                                     |");
            Console.WriteLine(" 4. Exit                                             |");
            Console.WriteLine("-----------------------------------------------------|");
            Console.SetCursorPosition(0, 13);
            Console.WriteLine("=======================================================================");
            Console.WriteLine("📢 LIVE NOTIFICATIONS & MARKET ALERTS:");
            Console.WriteLine("-----------------------------------------------------------------------");
        }
    }

    /// <summary>
    /// THREAD 1: Background Notification & Price Ticker
    /// </summary>
    static void NotificationThread()
    {
        Random rand = new Random();
        string[] alerts = {
            "🔥 WHALE ALERT: 10,000 BTC moved to an exchange!",
            "📰 NEWS: Regulatory updates announced in the EU.",
            "🚀 BREAKING: Major tech company adopts BTC for payments.",
            "📉 MARKET DIP: Liquidation cascade triggers liquidations."
        };

        int notificationRow = 16;

        while (true)
        {
            Thread.Sleep(rand.Next(3000, 5000)); // Update every 3-5 seconds

            double currentPrice;
            string? alertText = null;

            lock (stateLock)
            {
                // Simulate volatility: -1.5% to +1.5% change
                double changePercent = (rand.NextDouble() * 0.03) - 0.015;
                marketPrices["BTC"] *= (1 + changePercent);
                currentPrice = marketPrices["BTC"];

                if (rand.NextDouble() > 0.4)
                {
                    alertText = alerts[rand.Next(alerts.Length)];
                }
            }

            // Clean, non-disruptive printing via structural coordinates
            lock (consoleLock)
            {
                // Store where the user was currently typing
                int originalX = Console.CursorLeft;
                int originalY = Console.CursorTop;

                // Print Price Alert
                Console.SetCursorPosition(0, notificationRow);
                ClearCurrentConsoleLine();
                Console.Write($"🔔 [ALERT] BTC Price updated: ${currentPrice:N2}");

                // Print News Alert if generated
                if (alertText != null)
                {
                    notificationRow = notificationRow == 16 ? 17 : 16; // Alternate between two rows
                    Console.SetCursorPosition(0, notificationRow);
                    ClearCurrentConsoleLine();
                    Console.Write(alertText);
                }

                // Instantly restore user cursor position
                Console.SetCursorPosition(originalX, originalY);
            }
        }
    }

    /// <summary>
    /// THREAD 2: Automated Risk Monitor (Stop-Loss Trigger)
    /// </summary>
    static void RiskAutomationThread()
    {
        while (true)
        {
            Thread.Sleep(1000); // Check market rules every 1 second

            lock (stateLock)
            {
                if (stopLossExecuted) continue;

                double currentPrice = marketPrices["BTC"];

                // If price falls below protection trigger, panic sell everything automatically!
                if (currentPrice <= stopLossTriggerPrice && portfolio["BTC"] >= 0.1)
                {
                    double amountToSell = portfolio["BTC"];
                    double revenue = currentPrice * amountToSell;

                    portfolio["USD"] += revenue;
                    portfolio["BTC"] = 0;
                    stopLossExecuted = true;

                    // Log action safely to the side panel
                    lock (consoleLock)
                    {
                        int originalX = Console.CursorLeft;
                        int originalY = Console.CursorTop;

                        Console.SetCursorPosition(54, 2);
                        Console.Write("⚠️ [STOP-LOSS] TRIGGERED!");
                        Console.SetCursorPosition(54, 3);
                        Console.Write($"Sold all BTC at ${currentPrice:N2}");
                        Console.SetCursorPosition(54, 4);
                        Console.Write($"Recovered: ${revenue:N2}");

                        Console.SetCursorPosition(originalX, originalY);
                    }
                }
            }
        }
    }

    /// <summary>
    /// THREAD 3: Active User Menu & Interaction Dashboard
    /// </summary>
    static void DashboardThread()
    {
        int logRow = 5;

        while (true)
        {
            // Position user input consistently on line 8
            lock (consoleLock)
            {
                Console.SetCursorPosition(0, 8);
                ClearCurrentConsoleLine();
                Console.Write("👉 Select option (1-4): ");
            }

            string? choice = Console.ReadLine()?.Trim();
            string outputLog = "";

            lock (stateLock)
            {
                double btcPrice = marketPrices["BTC"];

                switch (choice)
                {
                    case "1":
                        outputLog = $"💼 Portfolio: {portfolio["BTC"]:F2} BTC | Cash: ${portfolio["USD"]:N2}";
                        break;

                    case "2":
                        double cost = btcPrice * 0.1;
                        if (portfolio["USD"] >= cost)
                        {
                            portfolio["USD"] -= cost;
                            portfolio["BTC"] += 0.1;
                            outputLog = $"✅ Bought 0.1 BTC for ${cost:N2}";
                        }
                        else
                        {
                            outputLog = "❌ Error: Insufficient cash balance.";
                        }
                        break;

                    case "3":
                        if (portfolio["BTC"] >= 0.1)
                        {
                            double revenue = btcPrice * 0.1;
                            portfolio["USD"] += revenue;
                            portfolio["BTC"] -= 0.1;
                            outputLog = $"✅ Sold 0.1 BTC for ${revenue:N2}";
                        }
                        else
                        {
                            outputLog = "❌ Error: No asset tokens to sell.";
                        }
                        break;

                    case "4":
                        lock (consoleLock)
                        {
                            Console.SetCursorPosition(0, 20);
                            Console.WriteLine("\n👋 Program Exited cleanly. Goodbye!");
                        }
                        Environment.Exit(0);
                        return;

                    default:
                        outputLog = "⚠️ Invalid configuration key chosen.";
                        break;
                }
            }

            // Write operation logs onto the side window panel asynchronously
            lock (consoleLock)
            {
                Console.SetCursorPosition(54, logRow);
                Console.Write(new string(' ', Console.WindowWidth - 54)); // clear log segment
                Console.SetCursorPosition(54, logRow);
                Console.Write(outputLog);

                logRow++;
                if (logRow > 11) logRow = 5; // Reset side logs row tracking cyclic rotation
            }
        }
    }

    private static void ClearCurrentConsoleLine()
    {
        Console.Write(new string(' ', Console.WindowWidth - 1));
        Console.SetCursorPosition(0, Console.CursorTop);
    }
}
