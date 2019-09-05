using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Helpers.TouchHelper
{
	/// <summary>
	/// Вспомогательный класс для имитации тачей в Unity editor-е.
	/// </summary>
	public static class TouchHelper
	{
		private static readonly TouchCreator LastFakeTouch = new TouchCreator();

		private static readonly TouchCreator ZoomGestureTouch1 = new TouchCreator();
		private static readonly TouchCreator ZoomGestureTouch2 = new TouchCreator();

		private static readonly Touch EmptyTouch = new Touch();

		private static int _lockerId;
		private static readonly List<int> Lockers = new List<int>();

		/// <summary>
		/// Блокировка тачей.
		/// </summary>
		/// <returns>Уникальный идентификатор блокировки.</returns>
		public static int Lock()
		{
			var id = ++_lockerId;
			Lockers.Add(id);
			return id;
		}

		/// <summary>
		/// Разблокировка тачей.
		/// </summary>
		/// <param name="id">Идентификатор снимаемой блокировки, полученный ранее из метода Lock().</param>
		public static void Unlock(int id)
		{
			Lockers.Remove(id);
		}

		/// <summary>
		/// Флаг, указывающий на наличие блокировки тачей.
		/// </summary>
		public static bool IsLocked => Lockers.Any();

		/// <summary>
		/// Получить все текущие тачи.
		/// </summary>
		/// <returns>Список текущих тачей.</returns>
		public static Touch[] GetTouches()
		{
			if (IsLocked)
			{
				return new Touch[0];
			}

			return MakeFakeTouch(out var isZoomGesture)
				? isZoomGesture
					? new[] {ZoomGestureTouch1.Create(), ZoomGestureTouch2.Create()}
					: new[] {LastFakeTouch.Create()}
				: Input.touches;
		}

		/// <summary>
		/// Получить текущий тач.
		/// </summary>
		/// <param name="touch">Возвращаемый тач.</param>
		/// <param name="touchNum">Номер тача (для мультитача).</param>
		/// <param name="ignoreLockers">Флаг, указывающий игнорировать блокировку.</param>
		/// <returns>Возвращает <code>true</code>, если тач имеется и возвращен в первом аргументе.</returns>
		public static bool GetTouch(out Touch touch, int touchNum = 0, bool ignoreLockers = false)
		{
			if (!ignoreLockers && IsLocked)
			{
				touch = EmptyTouch;
				return false;
			}

			if (Input.touchCount > touchNum)
			{
				touch = Input.GetTouch(touchNum);
				return true;
			}

			if (touchNum == 0 && MakeFakeTouch(out _))
			{
				touch = LastFakeTouch.Create();
				return true;
			}

			touch = EmptyTouch;
			return false;
		}

		/// <summary>
		/// Проверка на тач по элементу UI (не работает с UI в camera space).
		/// </summary>
		/// <returns>Возвращает <code>true</code>, если текущий тач приходится на элемент UI.</returns>
		public static bool IsPointerOverUiObject()
		{
#if UNITY_EDITOR
			return EventSystem.current.IsPointerOverGameObject();
#else
			var ped = new PointerEventData(EventSystem.current)
			{
				position = Input.touchCount > 0 ? Input.GetTouch(0).position : (Vector2) Input.mousePosition
			};
			Res.Clear();
			EventSystem.current.RaycastAll(ped, Res);
			return Res.Count > 0;
#endif
		}

		private static bool MakeFakeTouch(out bool isZoomGesture)
		{
			isZoomGesture = false;
			if (Input.touchSupported) return false;

			if (Input.GetMouseButtonDown(0))
			{
				LastFakeTouch.Phase = TouchPhase.Began;
				LastFakeTouch.DeltaPosition = Vector2.zero;
				LastFakeTouch.Position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				LastFakeTouch.FingerId = 0;
			}
			else if (Input.GetMouseButtonUp(0))
			{
				LastFakeTouch.Phase = TouchPhase.Ended;
				var newPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				LastFakeTouch.DeltaPosition = newPosition - LastFakeTouch.Position;
				LastFakeTouch.Position = newPosition;
				LastFakeTouch.FingerId = 0;
			}
			else if (Input.GetMouseButton(0))
			{
				LastFakeTouch.Phase = TouchPhase.Moved;
				var newPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				LastFakeTouch.DeltaPosition = newPosition - LastFakeTouch.Position;
				LastFakeTouch.Position = newPosition;
				LastFakeTouch.FingerId = 0;
			}
			else if (Input.GetMouseButtonDown(1))
			{
				isZoomGesture = true;

				var p = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				ZoomGestureTouch1.Phase = TouchPhase.Began;
				ZoomGestureTouch1.DeltaPosition = Vector2.zero;
				ZoomGestureTouch1.Position = p;
				ZoomGestureTouch1.FingerId = 0;

				ZoomGestureTouch2.Phase = TouchPhase.Began;
				ZoomGestureTouch2.DeltaPosition = Vector2.zero;
				ZoomGestureTouch2.Position = Mirror(p);
				ZoomGestureTouch2.FingerId = 1;
			}
			else if (Input.GetMouseButtonUp(1))
			{
				isZoomGesture = true;

				var p = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				ZoomGestureTouch1.Phase = TouchPhase.Ended;
				ZoomGestureTouch1.DeltaPosition = p - ZoomGestureTouch1.Position;
				ZoomGestureTouch1.Position = p;
				ZoomGestureTouch1.FingerId = 0;

				p = Mirror(p);
				ZoomGestureTouch2.Phase = TouchPhase.Ended;
				ZoomGestureTouch2.DeltaPosition = p - ZoomGestureTouch2.Position;
				ZoomGestureTouch2.Position = p;
				ZoomGestureTouch2.FingerId = 1;
			}
			else if (Input.GetMouseButton(1))
			{
				isZoomGesture = true;

				var p = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				ZoomGestureTouch1.Phase = TouchPhase.Moved;
				ZoomGestureTouch1.DeltaPosition = p - ZoomGestureTouch1.Position;
				ZoomGestureTouch1.Position = p;
				ZoomGestureTouch1.FingerId = 0;

				p = Mirror(p);
				ZoomGestureTouch2.Phase = TouchPhase.Moved;
				ZoomGestureTouch2.DeltaPosition = p - ZoomGestureTouch2.Position;
				ZoomGestureTouch2.Position = p;
				ZoomGestureTouch2.FingerId = 1;
			}
			else
			{
				return false;
			}

			return true;
		}

		private static Vector2 Mirror(Vector2 pos)
		{
			var center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
			return center + (pos - center) * -1f;
		}
	}
}