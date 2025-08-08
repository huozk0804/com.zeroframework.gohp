//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Keystone.Goap.Agent
{
    /// <summary>
    /// MonoBehaviour接口 - 定义Unity组件的常用属性和方法
    /// </summary>
    public interface IMonoBehaviour
    {
        /// <summary>
        /// 判断对象是否相等
        /// </summary>
        bool Equals(object other);
        /// <summary>
        /// 获取哈希码
        /// </summary>
        int GetHashCode();
        /// <summary>
        /// 转换为字符串
        /// </summary>
        string ToString();
        /// <summary>
        /// 获取实例ID
        /// </summary>
        int GetInstanceID();
        /// <summary>
        /// 名称
        /// </summary>
        string name { get; set; }
        /// <summary>
        /// 隐藏标志
        /// </summary>
        HideFlags hideFlags { get; set; }
        /// <summary>
        /// 变换组件
        /// </summary>
        Transform transform { get; }
        /// <summary>
        /// 游戏对象
        /// </summary>
        GameObject gameObject { get; }
        /// <summary>
        /// 标签
        /// </summary>
        string tag { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        bool enabled { get; set; }
        /// <summary>
        /// 是否激活且启用
        /// </summary>
        bool isActiveAndEnabled { get; }
        /// <summary>
        /// 是否使用GUILayout
        /// </summary>
        bool useGUILayout { get; set; }
        /// <summary>
        /// 获取指定类型的组件
        /// </summary>
        Component GetComponent(Type type);
        /// <summary>
        /// 获取指定类型的组件（泛型）
        /// </summary>
        T GetComponent<T>();
        /// <summary>
        /// 通过类型名获取组件
        /// </summary>
        Component GetComponent(string type);
        /// <summary>
        /// 尝试获取指定类型的组件
        /// </summary>
        bool TryGetComponent(Type type, out Component component);
        /// <summary>
        /// 尝试获取指定类型的组件（泛型）
        /// </summary>
        bool TryGetComponent<T>(out T component);
        /// <summary>
        /// 获取子物体中的组件
        /// </summary>
        Component GetComponentInChildren(Type t, bool includeInactive);
        Component GetComponentInChildren(Type t);
        T GetComponentInChildren<T>(bool includeInactive);
        T GetComponentInChildren<T>();
        Component[] GetComponentsInChildren(Type t, bool includeInactive);
        Component[] GetComponentsInChildren(Type t);
        T[] GetComponentsInChildren<T>(bool includeInactive);
        void GetComponentsInChildren<T>(bool includeInactive, List<T> result);
        T[] GetComponentsInChildren<T>();
        void GetComponentsInChildren<T>(List<T> results);
        /// <summary>
        /// 获取父物体中的组件
        /// </summary>
        Component GetComponentInParent(Type t, bool includeInactive);
        Component GetComponentInParent(Type t);
        T GetComponentInParent<T>(bool includeInactive);
        T GetComponentInParent<T>();
        Component[] GetComponentsInParent(Type t, bool includeInactive);
        Component[] GetComponentsInParent(Type t);
        T[] GetComponentsInParent<T>(bool includeInactive);
        void GetComponentsInParent<T>(bool includeInactive, List<T> results);
        T[] GetComponentsInParent<T>();
        /// <summary>
        /// 获取所有指定类型的组件
        /// </summary>
        Component[] GetComponents(Type type);
        void GetComponents(Type type, List<Component> results);
        void GetComponents<T>(List<T> results);
        T[] GetComponents<T>();
        /// <summary>
        /// 比较标签
        /// </summary>
        bool CompareTag(string tag);
        /// <summary>
        /// 向上传递消息
        /// </summary>
        void SendMessageUpwards(string methodName, object value, SendMessageOptions options);
        void SendMessageUpwards(string methodName, object value);
        void SendMessageUpwards(string methodName);
        void SendMessageUpwards(string methodName, SendMessageOptions options);
        /// <summary>
        /// 发送消息
        /// </summary>
        void SendMessage(string methodName, object value);
        void SendMessage(string methodName);
        void SendMessage(string methodName, object value, SendMessageOptions options);
        void SendMessage(string methodName, SendMessageOptions options);
        /// <summary>
        /// 广播消息
        /// </summary>
        void BroadcastMessage(string methodName, object parameter, SendMessageOptions options);
        void BroadcastMessage(string methodName, object parameter);
        void BroadcastMessage(string methodName);
        void BroadcastMessage(string methodName, SendMessageOptions options);
        /// <summary>
        /// 是否正在调用
        /// </summary>
        bool IsInvoking();
        bool IsInvoking(string methodName);
        /// <summary>
        /// 取消调用
        /// </summary>
        void CancelInvoke();
        void CancelInvoke(string methodName);
        /// <summary>
        /// 延迟调用
        /// </summary>
        void Invoke(string methodName, float time);
        void InvokeRepeating(string methodName, float time, float repeatRate);
        /// <summary>
        /// 启动协程
        /// </summary>
        Coroutine StartCoroutine(string methodName);
        Coroutine StartCoroutine(string methodName, object value);
        Coroutine StartCoroutine(IEnumerator routine);
        Coroutine StartCoroutine_Auto(IEnumerator routine);
        /// <summary>
        /// 停止协程
        /// </summary>
        void StopCoroutine(IEnumerator routine);
        void StopCoroutine(Coroutine routine);
        void StopCoroutine(string methodName);
        void StopAllCoroutines();
    }
}