using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flightpath
{
    public class FlightpathBuilder
    {
        private static FlightpathBuilder builder;

        public enum Objects
        {
            Sun, Earth, Mars, Psyche, Satellite
        }
        
        public static FlightpathBuilder GetBuilder()
        {
            if (builder == null) 
            {
                builder = new FlightpathBuilder();
            }
            return builder;
        }
        
    }
}