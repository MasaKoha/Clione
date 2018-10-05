using System.Collections;
using UnityEngine;

namespace Clione.Utility
{
    public class ClioneDispatcher : MonoBehaviour
    {
        public void Run(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
    }
}