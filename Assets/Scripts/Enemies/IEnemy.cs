using UnityEngine;
using UnityEngine.Events;

namespace Enemies
{
    public interface IEnemy
    {
        public void ChangeWaypoint();
        public void Move();
        public void Jump();
    }
}