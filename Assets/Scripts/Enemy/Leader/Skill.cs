using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

// 基础技能类
public abstract class Skill : MonoBehaviour
{
    public abstract void UseSkill();
}

// 第一阶段技能一：发射飞弹并生成仆从
public class LeaderSkill1 : Skill
{
    public GameObject missilePrefab; // 导弹的预制件
    public GameObject minionPrefab; // 仆从的预制件
    public Transform target; // 目标（通常是玩家）
    public Transform spawnPoint; // 仆从生成点
    public int maxMinions = 5; // 最大仆从数量
    public GameObject orbPrefab; // 法球的预制件
    private List<GameObject> minions = new List<GameObject>(); // 当前场上的仆从列表
    private List<GameObject> orbs = new List<GameObject>(); // 当前场上的法球列表

    public override void UseSkill()
    {
        // 发射3个飞弹
        for (int i = 0; i < 3; i++)
        {
            // 实例化导弹，并设置其目标为玩家
            GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
            missile.GetComponent<Missile>().SetTarget(target);
        }

        // 生成仆从
        if (minions.Count < maxMinions)
        {
            // 实例化仆从并添加到仆从列表中
            GameObject minion = Instantiate(minionPrefab, spawnPoint.position, Quaternion.identity);
            minions.Add(minion);
            // 订阅仆从死亡事件
            minion.GetComponent<Minion>().OnMinionKilled += HandleMinionKilled;
        }
    }

    private void HandleMinionKilled(GameObject minion)
    {
        // 从仆从列表中移除已死亡的仆从
        minions.Remove(minion);
        // 生成法球
        if (orbs.Count < 5)
        {
            // 实例化法球并添加到法球列表中
            GameObject orb = Instantiate(orbPrefab, transform.position, Quaternion.identity);
            orbs.Add(orb);
        }
    }
}

// 第一阶段技能二：献祭仆从生成护盾
public class LeaderSkill2 : Skill
{
    public GameObject shieldPrefab; // 护盾的预制件
    public Transform leaderTransform; // 领袖的位置
    private List<GameObject> minions = new List<GameObject>(); // 当前场上的仆从列表

    public override void UseSkill()
    {
        // 获取所有带有"Minion"标签的仆从
        minions = new List<GameObject>(GameObject.FindGameObjectsWithTag("Minion"));
        // 计算护盾值，每个仆从增加0.1的护盾值
        float shieldValue = minions.Count * 0.1f;

        // 献祭所有仆从
        foreach (GameObject minion in minions)
        {
            // 销毁仆从
            Destroy(minion);
        }
        // 清空仆从列表
        minions.Clear();

        // 实例化护盾并设置护盾值
        GameObject shield = Instantiate(shieldPrefab, leaderTransform.position, Quaternion.identity);
        shield.GetComponent<Shield>().SetShieldValue(shieldValue);
    }
}

// 第二阶段技能一：强化仆从生成技能
public class EnhancedLeaderSkill1 : LeaderSkill1
{
    public override void UseSkill()
    {
        // 发射更多的飞弹
        for (int i = 0; i < 5; i++)
        {
            // 实例化导弹，并设置其目标为玩家
            GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
            missile.GetComponent<Missile>().SetTarget(target);
        }

        // 生成更强的仆从
        if (minions.Count < maxMinions)
        {
            // 实例化仆从并添加到仆从列表中
            GameObject minion = Instantiate(minionPrefab, spawnPoint.position, Quaternion.identity);
            // 增强仆从属性
            minion.GetComponent<Minion>().Enhance();
            minions.Add(minion);
            // 订阅仆从死亡事件
            minion.GetComponent<Minion>().OnMinionKilled += HandleMinionKilled;
        }
    }
}

// 第二阶段技能二：释放冲击波
public class LeaderSkill4 : Skill
{
    public float waveDamage = 30f; // 冲击波伤害
    public float manaDrain = 1f; // 技力消耗
    public float waveRange = 10f; // 冲击波范围

    public override void UseSkill()
    {
        // 水平释放冲击波
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, waveRange);
        foreach (Collider2D enemy in hitEnemies)
        {
            // 如果碰撞到的是玩家
            if (enemy.CompareTag("Player"))
            {
                // 对玩家造成伤害
                enemy.GetComponent<Player>().TakeDamage(waveDamage);
                // 减少玩家的技力值
                enemy.GetComponent<Player>().UseMana(manaDrain);
            }
        }
    }
}

// 第二阶段技能三：近战爆炸技能
public class LeaderSkill3 : Skill
{
    public float explosionRange = 5f; // 爆炸范围
    public float explosionDamage = 50f; // 爆炸伤害

    public override void UseSkill()
    {
        // 检查范围内的敌人
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRange);
        foreach (Collider2D enemy in hitEnemies)
        {
            // 如果碰撞到的是玩家
            if (enemy.CompareTag("Player"))
            {
                // 对玩家造成爆炸伤害
                enemy.GetComponent<Character>().TakeDamage(explosionDamage);
            }
        }
    }
}

