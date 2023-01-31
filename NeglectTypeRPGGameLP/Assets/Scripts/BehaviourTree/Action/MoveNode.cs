using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeglectTypeRPG
{
    public enum PositionType
    {
        OriginPos,
        TargetPos,
    }
    public enum PositionDirection
    {
        None,
        Front,
        Back,
        Left,
        Right,
    }
    public enum MoveType
    {
        None,
        MoveTowards,
        Teleportation,
    }
    public class MoveNode : ActionNode,ICharacterNode
    {
        HeroContext context;
        public PositionType positionType;
        public PositionDirection targetPosDirection;
        public MoveType moveType;
        bool success;
        float distance;
        public float distanceFromTarget;
        public float speed = 10;
        Team myTeam;
        protected override void OnStart()
        {
            if (context == null)
            {
                distance = distanceFromTarget;
                context = blackBoard.context as HeroContext;
                if (myTeam == Team.None)
                {
                    myTeam = blackBoard.data.TeamCheck(blackBoard);
                    if (myTeam == Team.RED)
                        distanceFromTarget *= -1;
                }
            }
            TargetPosAdd();
        }

        protected override void OnStop()
        {
            if (positionType == PositionType.OriginPos)
            {
                blackBoard.gameObject.transform.rotation = Quaternion.Euler(context.originRotation.x, context.originRotation.y, context.originRotation.z);
            }
        }

        protected override State OnUpdate()
        {
            if(success)
                return State.Success;
            return Action();

        }

        public State Action()
        {
            context.gameObject.transform.LookAt(new Vector3(context.targetPos.x, context.gameObject.transform.position.y, context.targetPos.z));

            Move();

            context.gameObject.transform.LookAt(new Vector3(context.targetPos.x, context.gameObject.transform.position.y, context.targetPos.z));

            if (Vector3.Distance(context.gameObject.transform.position, context.targetPos) <= distance)
            {
                return State.Success;
            }
            return State.Running;
        }

        public void Move()
        {
            context.gameObject.transform.LookAt(new Vector3(context.targetPos.x, context.gameObject.transform.position.y, context.targetPos.z));

            switch (moveType)
            {
                case MoveType.MoveTowards:
                    context.gameObject.transform.position += speed * context.gameObject.transform.forward * Time.deltaTime;
                    break;
                case MoveType.Teleportation:
                    context.gameObject.transform.position = context.targetPos;
                    break;
            }
        }

        public void TargetPosAdd()
        {
            float x = 0, z = 0;
            int count = context.targets.Count;

            switch (positionType)
            {
                case PositionType.OriginPos:
                    context.targetPos = context.originPos;
                    break;
                case PositionType.TargetPos:
                    for (int i = 0; i < count; ++i)
                    {
                        x += context.targets[i].transform.position.x;
                        z += context.targets[i].transform.position.z;
                    }
                    context.targetPos = new Vector3(x / count, context.gameObject.transform.position.y, z / count);
                    break;
            }

          
            switch (targetPosDirection)
            {
                case PositionDirection.Front:
                    context.targetPos = new Vector3(context.targetPos.x, context.targetPos.y, context.targetPos.z- distanceFromTarget);
                    break;
                case PositionDirection.Back:
                    context.targetPos = new Vector3(context.targetPos.x, context.targetPos.y, context.targetPos.z + distanceFromTarget);
                    break;
                case PositionDirection.Left:
                    context.targetPos = new Vector3(context.targetPos.x+ distanceFromTarget, context.targetPos.y, context.targetPos.z);
                    break;
                case PositionDirection.Right:
                    context.targetPos = new Vector3(context.targetPos.x- distanceFromTarget, context.targetPos.y, context.targetPos.z);
                    break;
            }
         
        }
    }
}