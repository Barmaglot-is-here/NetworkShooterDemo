using System;
using System.Collections.Generic;

namespace Assets.Game.Scripts.Utils
{
    [Serializable]
    public class SerializableList<T>
    {
        public List<T> Value;

        public SerializableList(List<T> value) => Value = value;
    }
}
