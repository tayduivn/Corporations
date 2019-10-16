﻿using Entitas;
using System;
using System.Collections.Generic;

namespace Assets.Utils
{
    public static class NotificationUtils
    {
        private static GameEntity GetNotificationsComponent(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Notifications)[0];
        }

        public static List<NotificationMessage> GetNotifications(GameContext gameContext)
        {
            return GetNotificationsComponent(gameContext).notifications.Notifications;
        }

        internal static void Subscribe(GameContext gameContext, IAnyNotificationsListener listener)
        {
            var c = GetNotificationsComponent(gameContext);

            c.AddAnyNotificationsListener(listener);
        }

        internal static void UnSubscribe(GameContext gameContext, IAnyNotificationsListener listener)
        {
            var c = GetNotificationsComponent(gameContext);

            c.RemoveAnyNotificationsListener(listener);
        }

        public static void AddNotification(GameContext gameContext, NotificationMessage notificationMessage)
        {
            var n = GetNotificationsComponent(gameContext);

            var notys = n.notifications.Notifications;
            notys.Add(notificationMessage);

            n.ReplaceNotifications(notys);
        }

        internal static void ClearNotification(GameContext gameContext, int notificationId)
        {
            var n = GetNotificationsComponent(gameContext);

            var l = n.notifications.Notifications;
            l.RemoveAt(notificationId);

            n.ReplaceNotifications(l);
        }

        public static void ClearNotifications(GameContext gameContext)
        {
            var n = GetNotificationsComponent(gameContext);

            var l = n.notifications.Notifications;
            l.Clear();

            n.ReplaceNotifications(l);
        }

        // TODO move to separate file?
        /// Popups
        /// 

        internal static void AddPopup(GameContext gameContext, PopupMessage popup)
        {
            var container = GetPopupContainer(gameContext);
            var messages = container.popup.PopupMessages;

            messages.Add(popup);

            container.ReplacePopup(messages);
            ScreenUtils.TriggerScreenUpdate(gameContext);
        }

        internal static void ClosePopup(GameContext gameContext)
        {
            if (!IsHasActivePopups(gameContext))
                return;

            var container = GetPopupContainer(gameContext);
            var messages = container.popup.PopupMessages;

            messages.RemoveAt(0);

            container.ReplacePopup(messages);
            ScreenUtils.TriggerScreenUpdate(gameContext);
        }

        public static GameEntity GetPopupContainer(GameContext gameContext)
        {
            return GetNotificationsComponent(gameContext);
        }

        public static bool IsHasActivePopups(GameContext gameContext)
        {
            return GetNotificationsComponent(gameContext).popup.PopupMessages.Count > 0;
        }

        public static List<PopupMessage> GetPopups(GameContext gameContext)
        {
            var container = GetPopupContainer(gameContext);

            return container.popup.PopupMessages;
        }

        public static PopupMessage GetPopupMessage(GameContext gameContext)
        {
            var messages = GetPopups(gameContext);

            return messages[0];
        }
    }
}
