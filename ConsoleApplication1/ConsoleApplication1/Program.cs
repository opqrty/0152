using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var waters = Findwaters();

            Showwater(waters);

            Console.WriteLine("按下任一鍵進行新增資料庫");
            Console.ReadKey();
            Insertwater(waters);
        }

        public static List<water> Findwaters()
        {
            /*   -< twed:TaiwanWaterExchangingData xmlns:twed = "http://twed.wra.gov.tw/twedml/opendata" xmlns: gml = "http://www.opengis.net/gml/3.2" xmlns = "http://twed.wra.gov.tw/twedml/opendata" >


                -< twed:StatisticofWaterResourcesClass >


                 -< twed:TheDomesticConsumptionOfWater >*/

            List<water> waters = new List<water>();
            var xml = XElement.Load(@"D:\thisway\water.xml");
            XNamespace gml = @"http://www.opengis.net/gml/3.2";
            XNamespace twed = @"http://twed.wra.gov.tw/twedml/opendata";
            var watersNode = xml.Descendants(twed + "StatisticofWaterResourcesClass").ToList();



            for (var i = 0; i < watersNode.Count; i++)
            {
                var waterNode = watersNode[i];
            }
            watersNode.Where(x => !x.IsEmpty).ToList().ForEach(waterNode =>
            {
                var TheDomesticConsumptionOfWater = xml.Descendants(twed + "TheDomesticConsumptionOfWater").ToList();
                var Year = waterNode.Element(twed + "Year").Value.Trim();
                var ExecutingUnit = waterNode.Element(twed + "ExecutingUnit").Value.Trim();
                var ConsumptionOfWater = waterNode.Element(twed + "ConsumptionOfWater").Value.Trim();
                var PopulationServed = waterNode.Element(twed + "PopulationServed").Value.Trim();
                var TheDailyDomesticConsumptionOfWaterPerPerson = waterNode.Element(twed + "TheDailyDomesticConsumptionOfWaterPerPerson").Value.Trim();
                var Remarks = waterNode.Element(twed + "Remarks").Value.Trim();

                water waterData = new water();
                waterData.Year = Year;
                waterData.ExecutingUnit = ExecutingUnit;
                waterData.ConsumptionOfWater = ConsumptionOfWater;
                waterData.PopulationServed = PopulationServed;
                waterData.TheDailyDomesticConsumptionOfWaterPerPerson = TheDailyDomesticConsumptionOfWaterPerPerson;
                waterData.Remarks = Remarks;

                waters.Add(waterData);
            });

            return waters;
        }
        public static void Showwater(List<water> waters)
        {

            Console.WriteLine(string.Format("共收到{0}筆監測站的資料", waters.Count));
            waters.ForEach(x =>
            {
                Console.WriteLine(string.Format("Year：{0}\n ExecutingUnit:{1}\n ConsumptionOfWater：{2}\n PopulationServed：{3}\n TheDailyDomesticConsumptionOfWaterPerPerson：{4}\n Remarks：", x.Year, x.ExecutingUnit, x.ConsumptionOfWater, x.PopulationServed, x.TheDailyDomesticConsumptionOfWaterPerPerson, x.Remarks));


            });


        }


        public static void Insertwater(List<water> waters)
        {

            Repository.water db = new Repository.water();


            Console.WriteLine(string.Format("新增{0}筆優良廠商的資料開始", waters.Count));
            waters.ForEach(x =>
            {

                db.Createwater(x);


            });
            Console.WriteLine(string.Format("新增優良廠商的資料結束"));
            Console.ReadKey();
        
    }
    }
}
