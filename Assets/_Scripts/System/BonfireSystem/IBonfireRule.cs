using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBonfireRule
{
    int NeedSeconds { get; }
    string Key { get; }
    bool Unlocked { get; }
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
    
    void Save();
    IBonfireRule Load();
}
