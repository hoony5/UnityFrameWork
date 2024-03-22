namespace Share
{
    public static class SharedValue
    {
        public const int DefaultZero = 0;
        public const int DefaultOne = 1;

        public static class Signatures
        {
            public static readonly string Percent = "%";
            public static readonly string Plus = "+";
            public static readonly string Minus = "-";
            public static readonly string LeftCurlBracket = "{";
            public static readonly string RightCurlBracket = "}";
            public static readonly string LeftSquareBracket = "[";
            public static readonly string RightSquareBracket = "]";
            public static readonly string LeftParenthesis = "(";
            public static readonly string RightParenthesis = ")";
            public static readonly string Comma = ",";
            public static readonly string Colon = ":";
            public static readonly string Dot = ".";
            public static readonly string Slash = "/";
            public static readonly string UnderBar = "_";
            public static readonly string Sharp = "#";
            public static readonly string Tilde = "~";
            public static readonly string ParentOneWayBranch = "────";
            public static readonly string ParentTwoWayBranch = "──┤";
            public static readonly string ParentThreeWayBranch = "──┼──";
            public static readonly string ChildRoofBranch = "┌──";
            public static readonly string ChildVerticalBranch = "|";
            public static readonly string ChildMiddleBranch  = "├──";
            public static readonly string ChildFloorBranch  = "└──";
        }

        public static class ExcelReader
        {
            public static readonly string BreakingSignature_Sharp = "##";
            public static readonly string IgnoreSignature_Slash = "//";
            public static readonly string IgnoreSignature_UnderBar = "_";
            public static readonly string IgnoreSignature_Prefix = "Column";
            public static readonly string Bool_TRUE = "TRUE";
            public static readonly string Bool_FALSE = "FALSE";
        }

        public static class DataInfo
        {
            public static readonly int NamePartIndex = 0;
            public static readonly int ValuePartIndex = 1;
            public static readonly int MinPartIndex = 0;
            public static readonly int MaxPartIndex = 1;

            public const string TopCategory = "TopCategory";
            public const string MiddleCategory = "MiddleCategory";
            public const string BottomCategory = "BottomCategory";
            public const string ObjectName = "ObjectName";
            public const string ObjectID = "ObjectID";
            public const string DisplayName = "DisplayName";

            public const string MaxLevel = "MaxLevel";
            public const string DefaultValue = "DefaultValue";
            public const string IntervalValue = "IntervalValue";
            public const string JumpingLevel = "JumpingLevel";
            public const string JumpingPoint = "JumpingPoint";
            public const string CustomSteps = "CustomSteps";

            public const string EnhancementInfo = "EnhancementInfo";
            public const string EnhancementInfos = "EnhancementInfos";
            public const string Grade = "Grade";
            public const string ElementalType = "ElementalType";
            public const string MainResourceName = "MainResourceName";
            public const string RewardObjectName = "RewardObjectName";
            public const string Amount = "Amount";
            public const string AmountRange = "AmountRange";
            public const string ItemName = "ItemName";

            public const string StatusPairs = "StatusPairs";
        }

        public static class Ability
        {
            public static readonly string Total_Statuses_Keyword = "TotalStatus";

            public static readonly string Original_Status_Keyword = "OriginalStatus";
            public static readonly string Passive_Status_Keyword = "PassiveStatus";
            public static readonly string Buff_Debuff_Status_Keyword = "BuffOrDebuffStatus";
            public static readonly string Equipment_Status_Keyword = "EquipmentStatus";
            public static readonly string Achievement_Status_Keyword = "AchievementStatus";
            public static readonly string Collection_Status_Keyword = "CollectionStatus";
            public static readonly string Pet_Bonus_Status_Keyword = "PetBonusStatus";
            public static readonly string Enhancement_Status_Keyword = "EquipmentStatus";


            public static readonly string Level = "Level";
            public static readonly string Exp = "Exp";
            public static readonly string CurrentHealth = "Hp";
            public static readonly string Health = "Health";
            public static readonly string HealthRegen = "HealthRegen";
            public static readonly string HealthRegenPoint = "HealthRegenPoint";
            public static readonly string CurrentMana = "Mp";
            public static readonly string Mana = "Mana";
            public static readonly string ManaRegen = "ManaRegen";
            public static readonly int RecoveryManaPoint = 50;
            public static readonly int MaxManaPoint = 100;
            public static readonly string ManaRegenPoint = "ManaRegenPoint";
            public static readonly string CurrentStamina = "Sp";
            public static readonly string Stamina = "Stamina";
            public static readonly string StaminaRegen = "StaminaRegen";
            public static readonly string StaminaRegenPoint = "StaminaRegenPoint";
            public static readonly string Strength = "Strength";
            public static readonly string Dexterity = "Dexterity";
            public static readonly string Intelligence = "Intelligence";
            public static readonly string Wisdom = "Wisdom";
            public static readonly string Charisma = "Charisma";
            public static readonly string Managing = "Managing";
            public static readonly string PhysicsAttack = "PhysicsAttack";
            public static readonly string MagicAttack = "MagicAttack";
            public static readonly string PhysicsDefense = "PhysicsDefense";
            public static readonly string MagicDefense = "MagicDefense";
            public static readonly string MovingSpeed = "MovingSpeed";
            public static readonly string AttackSpeed = "AttackSpeed";
            public static readonly string CastingSpeed = "CastingSpeed";
            public static readonly string PhysicsAccuracy = "PhysicsAccuracy";
            public static readonly string PhysicsDodge = "PhysicsDodge";
            public static readonly string MagicAccuracy = "MagicAccuracy";
            public static readonly string MagicDodge = "MagicDodge";
            public static readonly string PhysicsCritical = "PhysicsCritical";
            public static readonly string PhysicsCriticalRate = "PhysicsCriticalRate";
            public static readonly string MagicCritical = "MagicCritical";
            public static readonly string MagicCriticalRate = "MagicCriticalRate";
            public static readonly string PhysicsPenetration = "PhysicsPenetration";
            public static readonly string MagicPenetration = "MagicPenetration";
            public static readonly string IgnorePhysics = "IgnorePhysics";
            public static readonly string IgnoreMagic = "IgnoreMagic";
            public static readonly string BlockPhysics = "BlockPhysics";
            public static readonly string BlockMagic = "BlockMagic";
            public static readonly string ReflectPhysics = "ReflectPhysics";
            public static readonly string ReflectMagic = "ReflectMagic";
            public static readonly string Barrier = "Barrier";
            public static readonly string MagicBarrier = "MagicBarrier";
            public static readonly string DrainHp = "DrainHp";
            public static readonly string DrainMp = "DrainMp";
            public static readonly string DrainSp = "DrainSp";
            public static readonly string Poison = "Poison";
            public static readonly string PoisonRegistance = "PoisonRegistance";
            public static readonly string Fire = "Fire";
            public static readonly string FireRegistance = "FireRegistance";
            public static readonly string Water = "Water";
            public static readonly string WaterRegistance = "WaterRegistance";
            public static readonly string Ice = "Ice";
            public static readonly string IceRegistance = "IceRegistance";
            public static readonly string Hungry = "Hungry";
            public static readonly string HungryRegistance = "HungryRegistance";
            public static readonly string Chaos = "Chaos";
            public static readonly string ChaosRegistance = "ChaosRegistance";
            public static readonly string Fear = "Fear";
            public static readonly string FearRegistance = "FearRegistance";
            public static readonly string Fatigue = "Fatigue";
            public static readonly string FatigueRegistance = "FatigueRegistance";
            public static readonly string Training = "Training";
        }

        public static class Battle
        {
            public static readonly string Animation_NormalAttack_Trigger_Keyword = "NormalAttack";
            public static readonly string Animation_SkillAttack_Trigger_Keyword = "BattleSkillAction";
            public static readonly string Animation_Hit_Trigger_Keyword = "Hit";
            public static readonly string Animation_Idle_Bool_Keyword = "IsIdle";
            public static readonly string Animation_Movement_Float_Keyword = "Movement";
            public static readonly float Animation_Idle_Float = 0.0f;
            public static readonly float Animation_Walk_Float = 1.0f;
            public static readonly float Animation_Run_Float = 2.0f;
            public static readonly string Animation_Dead_Bool_Keyword = "IsDead";
            public static readonly string Animation_Victory_Bool_Keyword = "IsWin";
            public static readonly string Animation_Defeat_Bool_Keyword = "IsLose";
            public static readonly string Animation_AttackSpeed_Float_Keyword = "AttackSpeed";
            public static readonly string VFX_Hit_ObjectName = "Hit";
        }

        public static class Enhancement
        {
            public static readonly string Total_Statuses_Keyword = "TotalStatus";
            public static readonly string Original_Statuses_Keyword = "OriginalStatus";
            public static readonly string Enhancement_Statuses_Keyword = "EnhancementStatus";
        } 
    }
}