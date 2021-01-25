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

        public override void MoveByPath(Dictionary<Vector2, MapInfo> pathDic)
        {
            if (pathDic.Count == 0)
            {
                return;
            }

            foreach (var item in pathDic)
            {
                StartCoroutine(yieldMove(item.Key));
            }
        }

        private IEnumerator yieldMove(Vector2 info)
        {
            int x = (int)info.x - (int)transform.position.x;
            int y = (int)info.y - (int)transform.position.y;
            yield return new WaitForSeconds(0f);
            AttempMove<Player>(x, y);
        }

        public void Move(int x,int y)
        {
            AttempMove<Player>(x, y);
        }
    }
}
