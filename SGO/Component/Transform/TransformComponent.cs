﻿using System;
using GameObject;
using SS13_Shared;
using SS13_Shared.GO;
using SS13_Shared.GO.Component.Transform;
using ServerInterfaces.GOC;

namespace SGO
{
    internal class TransformComponent : Component, ITransformComponent
    {
        private Vector2 _position = Vector2.Zero;

        public TransformComponent()
        {
            Family = ComponentFamily.Transform;
        }

        public float X
        {
            get { return Position.X; }
            set { Position = new Vector2(value, Position.Y); }
        }

        public float Y
        {
            get { return Position.Y; }
            set { Position = new Vector2(Position.X, value); }
        }

        #region ITransformComponent Members

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                Vector2 oldPosition = _position;
                _position = value;

                if (OnMove != null) OnMove(this, new VectorEventArgs(oldPosition, _position));
            }
        }

        public void TranslateTo(Vector2 toPosition)
        {
            Position = toPosition;
        }

        public void TranslateByOffset(Vector2 offset)
        {
            Position = Position + offset;
        }

        #endregion

        public event EventHandler<VectorEventArgs> OnMove;

        public override void Shutdown()
        {
            Position = Vector2.Zero;
        }

        public override ComponentState GetComponentState()
        {
            return new TransformComponentState(Position.X, Position.Y, false);
        }
    }
}