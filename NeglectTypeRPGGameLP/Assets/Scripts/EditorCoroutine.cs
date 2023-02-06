using System.Collections;
using UnityEditor;

namespace NeglectTypeRPG
{
#if UNITY_EDITOR
	public class EditorCoroutine
	{
        public static EditorCoroutine StartCoroutine(IEnumerator _routine)
        {
            EditorCoroutine coroutine = new EditorCoroutine(_routine);
            coroutine.Start();
            return coroutine;
        }

        readonly IEnumerator routine;
        private EditorCoroutine(IEnumerator _routine) => routine = _routine;

        private void Start()
        {
            EditorApplication.update += Update;
        }
        private void Stop()
        {
            EditorApplication.update -= Update;
        }
        private void Update()
        {
            if (!routine.MoveNext())
                Stop();
        }
    }
#endif
}

