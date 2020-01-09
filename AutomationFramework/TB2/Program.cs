using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Net.Mail;
using System.Diagnostics;
namespace TB2
{
    class Program
    {
        static void Main(string[] args)
        {
            RunManager runManager = new RunManager();
            runManager.RunTestCases();
            Console.WriteLine("Automation execution completed");
        }
    }
}