using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Calculator.Controllers
{
    public class CalculatorController : Controller
    {
        double num1, num2;
        string oprPrevious;
        bool isPreviousOperationEquals;
        //
        // GET: /Calculator/

        public ActionResult Index()
        {
            ViewBag.Result = "0";
            return View();
        }

        public ActionResult Calculate(string operation, string inputValue, string reset)
        {
            if (operation.Equals("AC"))
            {
                num1 = 0;
                num2 = 0;
                ViewBag.Result = "0";
                Session.Clear();
            }
            else
            {

                num1 = Convert.ToDouble(Session["num1"]);
                num2 = Convert.ToDouble(Session["num2"]);
                oprPrevious = Convert.ToString(Session["oprPrevious"]);
                isPreviousOperationEquals = Convert.ToBoolean(Session["isPreviousOperationEquals"]);

                if (num1 == 0)
                {
                    num1 = Convert.ToDouble(inputValue);
                    ViewBag.Result = num1.ToString();
                }
                else if(operation.Equals("="))
                {
                    isPreviousOperationEquals = true;
                    if (reset.Equals("true"))
                    {
                        num1 = Convert.ToDouble(inputValue);
                    }
                    else
                    {
                        num2 = Convert.ToDouble(inputValue);
                    }
                    num1 = getCalculatedValue(oprPrevious, num1, num2);
                    ViewBag.Result = num1.ToString();

                }
                else
                {
                    if (isPreviousOperationEquals)
                    {
                        num1 = Convert.ToDouble(inputValue);
                        isPreviousOperationEquals = false;
                    }
                    else
                    {
                        num2 = Convert.ToDouble(inputValue);
                        num1 = getCalculatedValue(oprPrevious, num1, num2);
                    }
                    ViewBag.Result = num1.ToString();
                }
                if (!operation.Equals("="))
                {
                    oprPrevious = operation;
                }
                ViewBag.Reset = "true";
                Session["num1"] = num1;
                Session["num2"] = num2;
                Session["oprPrevious"] = oprPrevious;
                Session["isPreviousOperationEquals"] = isPreviousOperationEquals;
            }
            return View("index");
        }

        private double getCalculatedValue(string operation, double num1, double num2)
        {
            switch (operation)
            {
                case "+":
                    num1 = num1 + num2;
                    break;
                case "-":
                    num1 = num1 - num2;
                    break;
                case "*":
                    num1 = num1 * num2;
                    break;
                case "/":
                    num1 = num1 / num2;
                    break;
                default:
                    break;
            }
            return num1;
        }

    }
}
