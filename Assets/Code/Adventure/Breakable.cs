using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure {
    public class Breakable : MonoBehaviour {
        public void Break() {
            Destroy(gameObject);
        }
    }
}
