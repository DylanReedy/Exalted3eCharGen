using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.IO;

public class CascadeLoaderTest {

	[Test]
	public void CascadeLoaderTestSimplePasses() {
//		string abilityToLoad = Ability.Archery.ToString();// ((Ability)i).ToString ();
//		string jsonCharms = File.ReadAllText ("Assets/Resources/Data/" + abilityToLoad + "Charms.txt");
//		CharmCascade charms = JsonUtility.FromJson<CharmCascade> (jsonCharms);

		string testString = "complex charm name";
		string otherString = "different charm name";
		Assert.AreNotEqual (testString, otherString);
		// Use the Assert class to test conditions.
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
//	[UnityTest]
//	public IEnumerator CascadeLoaderTestWithEnumeratorPasses() {
//		// Use the Assert class to test conditions.
//		// yield to skip a frame
//		yield return null;
//	}
}
