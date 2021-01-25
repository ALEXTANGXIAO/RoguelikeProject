using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGame
{
    public class Player : MovingManager
    {
        private void Update()
        {
            if (!GameController.Instance.playerTurn)
                return;
            int horizontal = 0;
            int vertical = 0;
            horizontal = (int)(Input.GetAxisRaw("Horizontal"));
            vertical = (int)(Input.GetAxisRaw("Vertical"));
            if (horizontal != 0)
                vertical = 0;
            if (horizontal != 0 || vertical != 0)
            {
                GameController.Instance.playerTurn = false;
                AttempMove<Player>(horizontal, vertical);
            }
        }


        protected override void OnCantMove<T>(T component)
        {
            throw new System.NotImplementedException();
        }
    }
}
