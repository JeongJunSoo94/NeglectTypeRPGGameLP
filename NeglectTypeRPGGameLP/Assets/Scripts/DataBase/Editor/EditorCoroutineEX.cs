using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace NeglectTypeRPG
{
#if UNITY_EDITOR
	public static class EditorCoroutineExtensions
	{
		public static EditorCoroutinesEX.EditorCoroutineEX StartCoroutine(this EditorWindow thisRef, IEnumerator coroutine)
		{
			return EditorCoroutinesEX.StartCoroutine(coroutine, thisRef);
		}

		public static EditorCoroutinesEX.EditorCoroutineEX StartCoroutine(this EditorWindow thisRef, string methodName)
		{
			return EditorCoroutinesEX.StartCoroutine(methodName, thisRef);
		}

		public static EditorCoroutinesEX.EditorCoroutineEX StartCoroutine(this EditorWindow thisRef, string methodName, object value)
		{
			return EditorCoroutinesEX.StartCoroutine(methodName, value, thisRef);
		}

		public static void StopCoroutine(this EditorWindow thisRef, IEnumerator coroutine)
		{
			EditorCoroutinesEX.StopCoroutine(coroutine, thisRef);
		}

		public static void StopCoroutine(this EditorWindow thisRef, string methodName)
		{
			EditorCoroutinesEX.StopCoroutine(methodName, thisRef);
		}

		public static void StopAllCoroutines(this EditorWindow thisRef)
		{
			EditorCoroutinesEX.StopAllCoroutines(thisRef);
		}
	}

	public class EditorCoroutinesEX
	{
		public class EditorCoroutineEX
		{
			public ICoroutineYield currentYield = new YieldDefault();
			public IEnumerator routine;
			public string routineUniqueHash;
			public string ownerUniqueHash;
			public string MethodName = "";

			public int ownerHash;
			public string ownerType;

			public bool finished = false;

			public EditorCoroutineEX(IEnumerator routine, int ownerHash, string ownerType)
			{
				this.routine = routine;
				this.ownerHash = ownerHash;
				this.ownerType = ownerType;
				ownerUniqueHash = ownerHash + "_" + ownerType;

				if (routine != null)
				{
					string[] split = routine.ToString().Split('<', '>');
					if (split.Length == 3)
					{
						this.MethodName = split[1];
					}
				}

				routineUniqueHash = ownerHash + "_" + ownerType + "_" + MethodName;
			}

			public EditorCoroutineEX(string methodName, int ownerHash, string ownerType)
			{
				MethodName = methodName;
				this.ownerHash = ownerHash;
				this.ownerType = ownerType;
				ownerUniqueHash = ownerHash + "_" + ownerType;
				routineUniqueHash = ownerHash + "_" + ownerType + "_" + MethodName;
			}
		}

		public interface ICoroutineYield
		{
			bool IsDone(float deltaTime);
		}

		struct YieldDefault : ICoroutineYield
		{
			public bool IsDone(float deltaTime)
			{
				return true;
			}
		}

		struct YieldWaitForSeconds : ICoroutineYield
		{
			public float timeLeft;

			public bool IsDone(float deltaTime)
			{
				timeLeft -= deltaTime;
				return timeLeft < 0;
			}
		}

		struct YieldCustomYieldInstruction : ICoroutineYield
		{
			public CustomYieldInstruction customYield;

			public bool IsDone(float deltaTime)
			{
				return !customYield.keepWaiting;
			}
		}

		struct YieldWWW : ICoroutineYield
		{
			public WWW Www;

			public bool IsDone(float deltaTime)
			{
				return Www.isDone;
			}
		}

		struct YieldAsync : ICoroutineYield
		{
			public AsyncOperation asyncOperation;

			public bool IsDone(float deltaTime)
			{
				return asyncOperation.isDone;
			}
		}

		struct YieldNestedCoroutine : ICoroutineYield
		{
			public EditorCoroutineEX coroutine;

			public bool IsDone(float deltaTime)
			{
				return coroutine.finished;
			}
		}

		static EditorCoroutinesEX instance = null;

		Dictionary<string, List<EditorCoroutineEX>> coroutineDict = new Dictionary<string, List<EditorCoroutineEX>>();
		List<List<EditorCoroutineEX>> tempCoroutineList = new List<List<EditorCoroutineEX>>();

		Dictionary<string, Dictionary<string, EditorCoroutineEX>> coroutineOwnerDict =
			new Dictionary<string, Dictionary<string, EditorCoroutineEX>>();

		DateTime previousTimeSinceStartup;

		public static EditorCoroutineEX StartCoroutine(IEnumerator routine, object thisReference)
		{
			CreateInstanceIfNeeded();
			return instance.GoStartCoroutine(routine, thisReference);
		}

		public static EditorCoroutineEX StartCoroutine(string methodName, object thisReference)
		{
			return StartCoroutine(methodName, null, thisReference);
		}

		public static EditorCoroutineEX StartCoroutine(string methodName, object value, object thisReference)
		{
			MethodInfo methodInfo = thisReference.GetType()
				.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			if (methodInfo == null)
			{
				Debug.LogError("Coroutine '" + methodName + "' couldn't be started, the method doesn't exist!");
			}
			object returnValue;

			if (value == null)
			{
				returnValue = methodInfo.Invoke(thisReference, null);
			}
			else
			{
				returnValue = methodInfo.Invoke(thisReference, new object[] { value });
			}

			if (returnValue is IEnumerator)
			{
				CreateInstanceIfNeeded();
				return instance.GoStartCoroutine((IEnumerator)returnValue, thisReference);
			}
			else
			{
				Debug.LogError("Coroutine '" + methodName + "' couldn't be started, the method doesn't return an IEnumerator!");
			}

			return null;
		}

		public static void StopCoroutine(IEnumerator routine, object thisReference)
		{
			CreateInstanceIfNeeded();
			instance.GoStopCoroutine(routine, thisReference);
		}

		public static void StopCoroutine(string methodName, object thisReference)
		{
			CreateInstanceIfNeeded();
			instance.GoStopCoroutine(methodName, thisReference);
		}

		public static void StopAllCoroutines(object thisReference)
		{
			CreateInstanceIfNeeded();
			instance.GoStopAllCoroutines(thisReference);
		}

		static void CreateInstanceIfNeeded()
		{
			if (instance == null)
			{
				instance = new EditorCoroutinesEX();
				instance.Initialize();
			}
		}

		void Initialize()
		{
			previousTimeSinceStartup = DateTime.Now;
			EditorApplication.update += OnUpdate;
		}

		void GoStopCoroutine(IEnumerator routine, object thisReference)
		{
			GoStopActualRoutine(CreateCoroutine(routine, thisReference));
		}

		void GoStopCoroutine(string methodName, object thisReference)
		{
			GoStopActualRoutine(CreateCoroutineFromString(methodName, thisReference));
		}

		void GoStopActualRoutine(EditorCoroutineEX routine)
		{
			if (coroutineDict.ContainsKey(routine.routineUniqueHash))
			{
				coroutineOwnerDict[routine.ownerUniqueHash].Remove(routine.routineUniqueHash);
				coroutineDict.Remove(routine.routineUniqueHash);
			}
		}

		void GoStopAllCoroutines(object thisReference)
		{
			EditorCoroutineEX coroutine = CreateCoroutine(null, thisReference);
			if (coroutineOwnerDict.ContainsKey(coroutine.ownerUniqueHash))
			{
				foreach (var couple in coroutineOwnerDict[coroutine.ownerUniqueHash])
				{
					coroutineDict.Remove(couple.Value.routineUniqueHash);
				}
				coroutineOwnerDict.Remove(coroutine.ownerUniqueHash);
			}
		}

		EditorCoroutineEX GoStartCoroutine(IEnumerator routine, object thisReference)
		{
			if (routine == null)
			{
				Debug.LogException(new Exception("IEnumerator is null!"), null);
			}
			EditorCoroutineEX coroutine = CreateCoroutine(routine, thisReference);
			GoStartCoroutine(coroutine);
			return coroutine;
		}

		void GoStartCoroutine(EditorCoroutineEX coroutine)
		{
			if (!coroutineDict.ContainsKey(coroutine.routineUniqueHash))
			{
				List<EditorCoroutineEX> newCoroutineList = new List<EditorCoroutineEX>();
				coroutineDict.Add(coroutine.routineUniqueHash, newCoroutineList);
			}
			coroutineDict[coroutine.routineUniqueHash].Add(coroutine);

			if (!coroutineOwnerDict.ContainsKey(coroutine.ownerUniqueHash))
			{
				Dictionary<string, EditorCoroutineEX> newCoroutineDict = new Dictionary<string, EditorCoroutineEX>();
				coroutineOwnerDict.Add(coroutine.ownerUniqueHash, newCoroutineDict);
			}

			// If the method from the same owner has been stored before, it doesn't have to be stored anymore,
			// One reference is enough in order for "StopAllCoroutines" to work
			if (!coroutineOwnerDict[coroutine.ownerUniqueHash].ContainsKey(coroutine.routineUniqueHash))
			{
				coroutineOwnerDict[coroutine.ownerUniqueHash].Add(coroutine.routineUniqueHash, coroutine);
			}

			MoveNext(coroutine);
		}

		EditorCoroutineEX CreateCoroutine(IEnumerator routine, object thisReference)
		{
			return new EditorCoroutineEX(routine, thisReference.GetHashCode(), thisReference.GetType().ToString());
		}

		EditorCoroutineEX CreateCoroutineFromString(string methodName, object thisReference)
		{
			return new EditorCoroutineEX(methodName, thisReference.GetHashCode(), thisReference.GetType().ToString());
		}

		void OnUpdate()
		{
			float deltaTime = (float)(DateTime.Now.Subtract(previousTimeSinceStartup).TotalMilliseconds / 1000.0f);

			previousTimeSinceStartup = DateTime.Now;
			if (coroutineDict.Count == 0)
			{
				return;
			}

			tempCoroutineList.Clear();
			foreach (var pair in coroutineDict)
				tempCoroutineList.Add(pair.Value);

			for (var i = tempCoroutineList.Count - 1; i >= 0; i--)
			{
				List<EditorCoroutineEX> coroutines = tempCoroutineList[i];

				for (int j = coroutines.Count - 1; j >= 0; j--)
				{
					EditorCoroutineEX coroutine = coroutines[j];

					if (!coroutine.currentYield.IsDone(deltaTime))
					{
						continue;
					}

					if (!MoveNext(coroutine))
					{
						coroutines.RemoveAt(j);
						coroutine.currentYield = null;
						coroutine.finished = true;
					}

					if (coroutines.Count == 0)
					{
						coroutineDict.Remove(coroutine.routineUniqueHash);
					}
				}
			}
		}

		static bool MoveNext(EditorCoroutineEX coroutine)
		{
			if (coroutine.routine.MoveNext())
			{
				return Process(coroutine);
			}

			return false;
		}

		static bool Process(EditorCoroutineEX coroutine)
		{
			object current = coroutine.routine.Current;
			if (current == null)
			{
				coroutine.currentYield = new YieldDefault();
			}
			else if (current is WaitForSeconds)
			{
				float seconds = float.Parse(GetInstanceField(typeof(WaitForSeconds), current, "m_Seconds").ToString());
				coroutine.currentYield = new YieldWaitForSeconds() { timeLeft = seconds };
			}
			else if (current is CustomYieldInstruction)
			{
				coroutine.currentYield = new YieldCustomYieldInstruction()
				{
					customYield = current as CustomYieldInstruction
				};
			}
			else if (current is WWW)
			{
				coroutine.currentYield = new YieldWWW { Www = (WWW)current };
			}
			else if (current is WaitForFixedUpdate || current is WaitForEndOfFrame)
			{
				coroutine.currentYield = new YieldDefault();
			}
			else if (current is AsyncOperation)
			{
				coroutine.currentYield = new YieldAsync { asyncOperation = (AsyncOperation)current };
			}
			else if (current is EditorCoroutineEX)
			{
				coroutine.currentYield = new YieldNestedCoroutine { coroutine = (EditorCoroutineEX)current };
			}
			else
			{
				Debug.LogException(
					new Exception("<" + coroutine.MethodName + "> yielded an unknown or unsupported type! (" + current.GetType() + ")"),
					null);
				coroutine.currentYield = new YieldDefault();
			}
			return true;
		}

		static object GetInstanceField(Type type, object instance, string fieldName)
		{
			BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
			FieldInfo field = type.GetField(fieldName, bindFlags);
			return field.GetValue(instance);
		}
	}
#endif
}

