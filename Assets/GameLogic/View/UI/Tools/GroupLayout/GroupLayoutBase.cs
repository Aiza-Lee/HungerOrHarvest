using System.Collections.Generic;
using GameLogic.View.UI.WorldVillPanel;
using UnityEngine;

namespace GameLogic.View.UI
{
	public abstract class GroupLayoutBase : MonoBehaviour {
		[SerializeField] private RectTransform _eleContainer;
		[SerializeField] protected float _space;
		public enum Direction {
			LeftToRight,
			UpToDown,
		}
		[SerializeField] private Direction _direction;

		public float EleSize { get; private set; }

		protected RectTransform _rectTrans;
		protected virtual void Awake() {
			_rectTrans = GetComponent<RectTransform>();
		}

		protected readonly List<IGroupLayoutEle> _eles = new();


		#region PublicMethods
		public virtual void Clear() {
			foreach (var ele in _eles) {
				ele.OnDirty -= RearrangeEle;
				ele.BelongedGroup = null;
			}
			_eles.Clear();
		}
		public virtual void SetWidth(float width) {
			EleSize = width;
			if (_direction == Direction.LeftToRight) {
				_eleContainer.offsetMax = new(_eleContainer.offsetMin.x + width, _eleContainer.offsetMax.y);
			} else if (_direction == Direction.UpToDown) {
				_eleContainer.offsetMin = new(_eleContainer.offsetMin.x, _eleContainer.offsetMax.y - width);
			}
		}
		public virtual void RearrangeEle() {
			float pos = 0f;
			foreach (var ele in _eles) {
				ele.SetPos(pos);
				pos += ele.EleSize + _space;
			}
			SetWidth(pos + _space);
		}
		public void AddEle(IGroupLayoutEle ele) {
			ele.RectTrans.SetParent(_eleContainer);
			_eles.Add(ele);
			ele.OnDirty += RearrangeEle;
			ele.OnAddedToGroup();
			ele.BelongedGroup = this;
			RearrangeEle();
		}
		public void RemoveEle(IGroupLayoutEle ele) {
			_eles.Remove(ele);
			ele.OnDirty -= RearrangeEle;
			ele.BelongedGroup = null;
			RearrangeEle();
		}
		#endregion
	}
}