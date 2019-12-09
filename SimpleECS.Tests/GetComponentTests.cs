using NUnit.Framework;
using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Tests {
	public class GetComponentTests {
		class Component1 : IComponent {}
		class Component2 : IComponent {}

		EntitySet _entities = null;

		[SetUp]
		public void Setup() {
			_entities = new EntitySet();
			_entities.Add().AddComponent(new Component1());
			_entities.Add().AddComponent(new Component2());
			_entities.Add().AddComponent(new Component1());
			var lastEntity = _entities.Add();
			lastEntity.AddComponent(new Component1());
			lastEntity.AddComponent(new Component2());
		}

		[Test]
		[Timeout(1000)]
		public void GetComponentsValid() {
			var count = 0;
			foreach ( var _ in _entities.GetComponent<Component1>() ) {
				count++;
			}
			Assert.AreEqual(3, count);
		}

		[Test]
		public void GetEntitiesValid() {
			var count = 0;
			foreach ( var _ in _entities.Get<Component2>() ) {
				count++;
			}
			Assert.AreEqual(2, count);
		}
	}
}