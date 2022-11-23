using UnityEngine;
using YG;

public abstract class Award : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private LanguageYG nameLang;
    [SerializeField] private LanguageYG descriptionLang;
    private bool accessibility;

    public Sprite Sprite => sprite; 
    public string Name => nameLang.currentTranslation; 
    public virtual string Description => descriptionLang.currentTranslation; 
    public virtual bool Accessibility => true;
    public abstract void AwardAction();
}
