public interface IUpgradeble
{
    public void SetDataVariables();
    public int GetStatLvl(string statName);
    public void Upgrade(string statName);

}
