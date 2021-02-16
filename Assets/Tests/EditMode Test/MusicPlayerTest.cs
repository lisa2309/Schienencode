using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class MusicPlayerTest
    {
        //Test if the variable musicVolume is never greather than >1.0
        [Test]
        public void MusicVolumeMaxValue()
        {
            GameObject gameObject = GameObject.Find("SliderMusic");
            double test = gameObject.GetComponent<Slider>().maxValue;
            Assert.AreEqual(1.0d, test);
        }

        // Test if the variable musicVolume is never less than <0
        [Test]
        public void MusicVolumeMinValue()
        {
            GameObject gameObject = GameObject.Find("SliderMusic");
            double test = gameObject.GetComponent<Slider>().minValue;
            Assert.AreEqual(0.0d, test);
        }

        // Test if the variable musicVolume has at start the value 0.5
        [Test]
        public void MusicVolumeStartValue()
        {
            GameObject gameObject = GameObject.Find("SliderMusic");
            Assert.AreEqual(0.5, gameObject.GetComponent<Slider>().value);
        }

    }
}
