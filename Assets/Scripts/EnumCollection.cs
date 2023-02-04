
//The EnumCollections just needs to be in the project, no GameObject needed
//Can be freely change to suit the project, calls need to be changed
namespace EnumCollection
{   
    public enum GameState
    {
        MainMenu,
        Credits,
        Settings,
        HighscoreMenu,
        Starting,
        GameOver,
        NewGame,
        Quit,
    }

    public enum Fade
    {
        In,
        Out,
    }

    //Tracks and SFX will be in alphabetical order depending on the name
    //in the Assets/Resources Folder
    public enum Track
    {
        Track_001_Tree_of_Peace,
        Track_002_Tree_of_War,
    }

    public enum SFX
    {
        SFX_001_ButtonClick,
        SFX_002_Character_Footstep1,
        SFX_003_Character_Footstep2,
        SFX_004_Character_Footstep3,
        SFX_005_Enemy_Appear1,
        SFX_006_Enemy_Appear2,
        SFX_007_Enemy_Appear3,
        SFX_008_Enemy_Appear4,
        SFX_009_Enemy_Appear5,
        SFX_010_Enemy_Attack1,
        SFX_011_Enemy_Attack2,
        SFX_012_Enemy_Attack3,
        SFX_013_Enemy_Attack4,
        SFX_014_Enemy_Attack5,
        SFX_015_Enemy_Death1,
        SFX_016_Enemy_Death2,
        SFX_017_Enemy_Death3,
        SFX_018_Enemy_Death4,
        SFX_019_Enemy_Death5,
        SFX_020_Tree_Damage1,
        SFX_021_Tree_Damage2,
        SFX_022_Tree_Damage3,
        SFX_023_Tree_Damage4,
        SFX_024_Tree_Damage5,
        SFX_025_Elevator_Move,
    }
}