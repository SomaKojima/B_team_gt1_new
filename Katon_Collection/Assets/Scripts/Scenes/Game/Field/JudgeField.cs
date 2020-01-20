using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeField : MonoBehaviour
{
    [SerializeField, EnumListLabel(typeof(Type))]
    Transform[] areaCenterPosition = new Transform[(int)Type.Max];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public Type ChangePositionToPlaceType(Vector3 _position)
    {
        float length = 999999999999.0f;
        Type _placeType = Type.none;
        for(int i = 0; i < (int)Type.Max; i++)
        {
            if (areaCenterPosition[i] == null) continue;
            Vector3 vec = _position - areaCenterPosition[i].position;
            if (vec.sqrMagnitude < length)
            {
                length = vec.sqrMagnitude;
                _placeType = (Type)i;
            }
        }

        return _placeType;
    }

    public Vector3 GetAreaCenterPosition(Type _placeType)
    {
        return areaCenterPosition[(int)_placeType].position;
    }
}
