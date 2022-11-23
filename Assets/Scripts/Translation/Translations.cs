using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Translations
{
    public Language currentLanguage;
    public List<Language> languages;
    public Translations(int fieldsToTranslate)
    {
        languages = new List<Language>() { new Language("en", fieldsToTranslate), new Language("ru", fieldsToTranslate), new Language("tr", fieldsToTranslate) };
    }
}
