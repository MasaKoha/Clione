using System.Collections;
using System.Collections.Generic;
using Clione.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Clione.Example
{
    public class ButtonTestMain : MonoBehaviour
    {
        [SerializeField] private ClioneButton _button;

        private Text _test;

        // Use this for initialization
        private void Start()
        {
            _button.ButtonEvent.AddListener(state => { Debug.Log($"{state.ToString()} : {Time.frameCount}"); });
        }
    }
}