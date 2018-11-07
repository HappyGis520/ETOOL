using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EllaMakerTool
{
    public class MapCommon
    {
        private const double EARTH_RADIUS = 6378137;//地球半径，单位米

        /// <summary>
        /// 计算两点位置的距离，返回两点的距离，单位 米
        /// 该公式为GOOGLE提供，误差小于0.2米
        /// </summary>
        /// <param name="lat1">第一点纬度</param>
        /// <param name="lng1">第一点经度</param>
        /// <param name="lat2">第二点纬度</param>
        /// <param name="lng2">第二点经度</param>
        /// <returns></returns>
        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = Rad(lat1);
            double radLng1 = Rad(lng1);
            double radLat2 = Rad(lat2);
            double radLng2 = Rad(lng2);
            double a = radLat1 - radLat2;
            double b = radLng1 - radLng2;
            double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;
            return result;
        }

        /// <summary>
        /// 经纬度转化成弧度
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static double Rad(double d)
        {
            return (double)d * Math.PI / 180d;
        }

        /// <summary>
        /// 计算距离
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lng1"></param>
        /// <param name="lat2"></param>
        /// <param name="lng2"></param>
        /// <returns></returns>
        public static float GetRadie(double lat1, double lng1, double lat2, double lng2)
        {
            if (lat1 == lat2 && lng1 == lng2)
            {
                return 0;
            }
            var res = 6378137 * Math.Acos(Math.Sin(lat1 / 180 * Math.PI) *
                              Math.Sin(lat2 / 180 * Math.PI)
                              +
                              Math.Cos(lat1 / 180 * Math.PI) *
                              Math.Cos(lat2 / 180 * Math.PI) *
                              Math.Cos(
                                  (lng1 - lng2) / 180 * Math.PI)
            );
            return (float)res;
        }
    }
}
