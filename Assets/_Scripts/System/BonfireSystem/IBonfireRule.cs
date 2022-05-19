using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBonfireRule
{
    /// <summary>
    /// 解锁需要的寿命
    /// </summary>
    int NeedSeconds { get; }
    /// <summary>
    /// 区别特定规则的标志
    /// </summary>
    string Key { get; }
    /// <summary>
    /// 是否已解锁，通常存档只需要知道此项
    /// </summary>
    bool Unlocked { get; set; }
    void UnLock();
    /// <summary>
    /// 重置规则
    /// </summary>
    void Reset();
    /// <summary>
    /// 火堆界面中显示的选项
    /// </summary>
    void OnBonfireGUI();
    /// <summary>
    /// 解锁规则后出现的UI或获得的能力
    /// </summary>
    void OnTopRightGUI();
    void OnGUI();
    /// <summary>
    /// 存档功能
    /// </summary>
    void Save();
    IBonfireRule Load();
}
