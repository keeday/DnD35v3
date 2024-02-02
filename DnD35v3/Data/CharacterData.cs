namespace DnD35v3.Data
{
    public class CharacterList
    {
        public int CharacterID { get; set; }
        public int Owner { get; set; }
        public int GroupID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Race { get; set; } = string.Empty;
        public string Alignment { get; set; } = string.Empty;
        public string Deity { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Height { get; set; } = string.Empty;
        public string Weight { get; set; } = string.Empty;
        public string Eyes { get; set; } = string.Empty;
        public string Hair { get; set; } = string.Empty;
        public string Skin { get; set; } = string.Empty;
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
        public int MaxHitPoints { get; set; }
        public int CurrentHitPoints { get; set; }
        public int Speed { get; set; }
        public int BaseAttackBonus1 { get; set; }
        public int BaseAttackBonus2 { get; set; }
        public int BaseAttackBonus3 { get; set; }
        public int BaseAttackBonus4 { get; set; }
        public int BaseAttackBonus5 { get; set; }
        public int SpellResistance { get; set; }
        public int BaseFortitudeSave { get; set; }
        public int FortitudeMagicMod { get; set; }
        public int FortitudeMiscMod { get; set; }
        public int BaseReflexSave { get; set; }
        public int ReflexMagicMod { get; set; }
        public int ReflexMiscMod { get; set; }
        public int BaseWillSave { get; set; }
        public int WillMagicMod { get; set; }
        public int WillMiscMod { get; set; }
        public int WalletCP { get; set; }
        public int WalletSP { get; set; }
        public int WalletGP { get; set; }
        public int WalletPP { get; set; }
        public int BankCP { get; set; }
        public int BankSP { get; set; }
        public int BankGP { get; set; }
        public int BankPP { get; set; }
        public bool IsAlive { get; set; }
        public DateTime DateCreated { get; set; }
    }
}