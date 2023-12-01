using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sound List", menuName = "SoundList")]
public class SoundList : ScriptableObject {
    public List<Sound> sounds = new List<Sound>();
}
