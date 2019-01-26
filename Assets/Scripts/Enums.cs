using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enums
{
    public enum eEntityType
    {
        ENTITY_PLAYER,
        ENTITY_MOM,
        ENTITY_DAD,
        ENTITY_TOTAL
    }


    public enum eSoundType
    {
		SOUND_CLICK,
		SOUND_PLAYER_WALK,
		SOUND_MOM_WALK,
		SOUND_FLOWER_WASE_BREAK,
		SOUND_MISSION_COMPLETE,
		SOUND_BUSTED,
		SOUND_MOM_HEY,
		SOUND_DAD_HEY,
		SOUND_TOTAL
    }

	public	enum	eSoundSourceType
	{
		SOUND_SOURCE_GENERAL,
		SOUND_SOURCE_PLAYER,
		SOUND_SOURCE_MOM,
		SOUND_SOURCE_DAD,
		SOUND_SOURCE_TOTAL
	}

	public	enum	eLevel
	{
		LEVEL_1,
		LEVEL_2,
		LEVEL_TOTAL
	}

	public	enum	eHomeObject
	{
		HOME_OBJECT_FLOWER_WASE,
		HOME_OBJECT_TOTAL
	}
}
