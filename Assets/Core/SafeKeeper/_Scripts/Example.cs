using System;
using UnityEngine;
using UnityEngine.UI;

namespace SafeKepper
{
    public class Example : MonoBehaviour
    {
        [SerializeField] private InputFields _inputInt;
        [SerializeField] private InputFields _inputFloat;
        [SerializeField] private InputFields _inputBool;
        [SerializeField] private InputFields _inputText;
        [SerializeField] private InputFields _inputObject;
        [SerializeField] private InputModel _inputModel;

        private void Start()
        {
            InitInt();
            InitFloat();
            InitBool();
            InitText();
            InitObject();
        }

        private void InitInt()
        {
            _inputInt.SaveData.onClick.AddListener(() =>
            {
                if (int.TryParse(_inputInt.InputData.text, out var data))
                {
                    Saver.SetInt(_inputInt.InputKey.text, data);
                }
                else throw new ArgumentException("Enter int type to field");
            });
        }

        private void InitFloat()
        {
            _inputFloat.SaveData.onClick.AddListener(() =>
            {
                if (float.TryParse(_inputFloat.InputData.text, out var data))
                {
                    Saver.SetFloat(_inputFloat.InputKey.text, data);
                }
                else throw new ArgumentException("Enter float type to field");
            });
        }

        private void InitBool()
        {
            _inputBool.SaveData.onClick.AddListener(() =>
            {
                if (bool.TryParse(_inputBool.InputData.text, out var data))
                {
                    Saver.SetBool(_inputBool.InputKey.text, data);
                }
                else throw new ArgumentException("Enter bool type to field");
            });
        }

        private void InitText()
        {
            _inputText.SaveData.onClick.AddListener(() =>
            {
                Saver.SetString(_inputText.InputKey.text, _inputText.InputData.text);
            });
        }

        private void InitObject()
        {
            _inputObject.SaveData.onClick.AddListener(() =>
            {
                var saveObject = new ModelForSave(_inputModel.TextInput.text, int.Parse(_inputModel.IntInput.text), float.Parse(_inputModel.FloatInput.text));
                Saver.SetObject(_inputObject.InputKey.text, saveObject);
                Debug.Log(Saver.GetObject<ModelForSave>(_inputObject.InputKey.text));
            });
        }
        
        [Serializable]
        class InputFields
        {
            public InputField InputKey;
            public InputField InputData;
            public Button SaveData;
        }
        
        [Serializable]
        class InputModel
        {
            public InputField TextInput;
            public InputField IntInput;
            public InputField FloatInput;
        }
        
        [Serializable]
        class ModelForSave
        {
            public string TextData;
            public int IntData;
            public float FloatData;

            public ModelForSave(string textData, int intData, float floatData)
            {
                TextData = textData;
                IntData = intData;
                FloatData = floatData;
            }

            public override string ToString()
            {
                return $"{TextData} {IntData} {FloatData}";
            }
        }
        
        
    }


    

}
