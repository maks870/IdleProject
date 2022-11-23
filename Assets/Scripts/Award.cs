using UnityEngine;
using YG;

public abstract class Award : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private LanguageYG nameLang;
    [SerializeField] private LanguageYG descriptionLang;
    protected bool accessibility = true;

    public Sprite Sprite => sprite; 
    public string Name => nameLang.currentTranslation; 
    public virtual string Description => descriptionLang.currentTranslation; 
    public bool Accessibility => accessibility;
    public abstract void AwardAction();
}
