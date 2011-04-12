using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

namespace CodeSoda.ObjectProxy.Tests
{
	[TestFixture]
	public class LazyObjectProxyTests
	{
		bool loaded = false;

		[Test]
		public void test1()
		{

			var car = LazyProxy<Car>.Make(
				() => MakeActualCar()
			);
			
			DoNothingWithACar(car);
			Assert.IsFalse(loaded);

			DoSomethingWithACar(car);
			Assert.IsTrue(loaded);
			
			car.DoNothing();

			Assert.AreEqual("Ford", car.Make);
		}

		private Car MakeActualCar()
		{
			loaded = true;
			return new Car { Make = "Ford", Model = "Escort" };
		}

		private static void DoNothingWithACar(Car car)
		{
			// do absolutely nothing with the car
		}

		private static void DoSomethingWithACar(Car car)
		{
			if (car.Make == car.Model);
		}

		[Test]
		public void ListTest()
		{

			var cars = LazyProxy<LazyList<Car>>.Make(
				() => MakeCars()
			);

			Debug.WriteLine(cars.Count);

			foreach(var car in (IEnumerable)cars)
			{
				if (car != null)
				{
					Debug.WriteLine(((Car)car).Make + " " + ((Car)car).Model);
				}
			}

		}

		public LazyList<Car> MakeCars()
		{
			return new LazyList<Car> {
				new Car{ Make = "Make1", Model = "Model1"},
				new Car{ Make = "Make2", Model = "Model2"}
			};
		}

		[Test]
		public void GenericLazyDictionaryTest()
		{
			LazyDictionary<string, Car> lazy = new LazyDictionary<string, Car>(LoadCar);

			Assert.AreEqual("abc", lazy["abc"].Make);
			Assert.AreEqual(null, lazy["xyz"]);
		}

		public Car LoadCar(string key)
		{
			if (key.StartsWith("a"))
				return new Car { Make = key, Model = key };

			return null;
		}

		[Test]
		public void LazyDictionaryTest()
		{
			int callcount = 0;
			LazyDictionary lazy = new LazyDictionary(
				o => {
					callcount++;
					string key = o.ToString();
					if (key.StartsWith("a"))
						return new Car { Make = key, Model = key };
					return null;
				}
			);

			Assert.AreEqual("abc", ((Car)lazy["abc"]).Make);
			Assert.AreEqual("abc", ((Car)lazy["abc"]).Model);
			Assert.AreEqual(null, lazy["xyz"]);
			Assert.AreEqual(2, callcount);
		}
	}

	public class Car
	{
		virtual public string Make { get; set; }
		virtual public string Model { get; set; }

		virtual public void DoNothing() {
			// do nothing
		}
	}
}