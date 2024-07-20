using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

// ����������
public abstract class Skill : MonoBehaviour
{
    public abstract void UseSkill();
}

// ��һ�׶μ���һ������ɵ��������ʹ�
public class LeaderSkill1 : Skill
{
    public GameObject missilePrefab; // ������Ԥ�Ƽ�
    public GameObject minionPrefab; // �ʹӵ�Ԥ�Ƽ�
    public Transform target; // Ŀ�꣨ͨ������ң�
    public Transform spawnPoint; // �ʹ����ɵ�
    public int maxMinions = 5; // ����ʹ�����
    public GameObject orbPrefab; // �����Ԥ�Ƽ�
    private List<GameObject> minions = new List<GameObject>(); // ��ǰ���ϵ��ʹ��б�
    private List<GameObject> orbs = new List<GameObject>(); // ��ǰ���ϵķ����б�

    public override void UseSkill()
    {
        // ����3���ɵ�
        for (int i = 0; i < 3; i++)
        {
            // ʵ������������������Ŀ��Ϊ���
            GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
            missile.GetComponent<Missile>().SetTarget(target);
        }

        // �����ʹ�
        if (minions.Count < maxMinions)
        {
            // ʵ�����ʹӲ���ӵ��ʹ��б���
            GameObject minion = Instantiate(minionPrefab, spawnPoint.position, Quaternion.identity);
            minions.Add(minion);
            // �����ʹ������¼�
            minion.GetComponent<Minion>().OnMinionKilled += HandleMinionKilled;
        }
    }

    private void HandleMinionKilled(GameObject minion)
    {
        // ���ʹ��б����Ƴ����������ʹ�
        minions.Remove(minion);
        // ���ɷ���
        if (orbs.Count < 5)
        {
            // ʵ����������ӵ������б���
            GameObject orb = Instantiate(orbPrefab, transform.position, Quaternion.identity);
            orbs.Add(orb);
        }
    }
}

// ��һ�׶μ��ܶ����׼��ʹ����ɻ���
public class LeaderSkill2 : Skill
{
    public GameObject shieldPrefab; // ���ܵ�Ԥ�Ƽ�
    public Transform leaderTransform; // �����λ��
    private List<GameObject> minions = new List<GameObject>(); // ��ǰ���ϵ��ʹ��б�

    public override void UseSkill()
    {
        // ��ȡ���д���"Minion"��ǩ���ʹ�
        minions = new List<GameObject>(GameObject.FindGameObjectsWithTag("Minion"));
        // ���㻤��ֵ��ÿ���ʹ�����0.1�Ļ���ֵ
        float shieldValue = minions.Count * 0.1f;

        // �׼������ʹ�
        foreach (GameObject minion in minions)
        {
            // �����ʹ�
            Destroy(minion);
        }
        // ����ʹ��б�
        minions.Clear();

        // ʵ�������ܲ����û���ֵ
        GameObject shield = Instantiate(shieldPrefab, leaderTransform.position, Quaternion.identity);
        shield.GetComponent<Shield>().SetShieldValue(shieldValue);
    }
}

// �ڶ��׶μ���һ��ǿ���ʹ����ɼ���
public class EnhancedLeaderSkill1 : LeaderSkill1
{
    public override void UseSkill()
    {
        // �������ķɵ�
        for (int i = 0; i < 5; i++)
        {
            // ʵ������������������Ŀ��Ϊ���
            GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
            missile.GetComponent<Missile>().SetTarget(target);
        }

        // ���ɸ�ǿ���ʹ�
        if (minions.Count < maxMinions)
        {
            // ʵ�����ʹӲ���ӵ��ʹ��б���
            GameObject minion = Instantiate(minionPrefab, spawnPoint.position, Quaternion.identity);
            // ��ǿ�ʹ�����
            minion.GetComponent<Minion>().Enhance();
            minions.Add(minion);
            // �����ʹ������¼�
            minion.GetComponent<Minion>().OnMinionKilled += HandleMinionKilled;
        }
    }
}

// �ڶ��׶μ��ܶ����ͷų����
public class LeaderSkill4 : Skill
{
    public float waveDamage = 30f; // ������˺�
    public float manaDrain = 1f; // ��������
    public float waveRange = 10f; // �������Χ

    public override void UseSkill()
    {
        // ˮƽ�ͷų����
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, waveRange);
        foreach (Collider2D enemy in hitEnemies)
        {
            // �����ײ���������
            if (enemy.CompareTag("Player"))
            {
                // ���������˺�
                enemy.GetComponent<Player>().TakeDamage(waveDamage);
                // ������ҵļ���ֵ
                enemy.GetComponent<Player>().UseMana(manaDrain);
            }
        }
    }
}

// �ڶ��׶μ���������ս��ը����
public class LeaderSkill3 : Skill
{
    public float explosionRange = 5f; // ��ը��Χ
    public float explosionDamage = 50f; // ��ը�˺�

    public override void UseSkill()
    {
        // ��鷶Χ�ڵĵ���
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRange);
        foreach (Collider2D enemy in hitEnemies)
        {
            // �����ײ���������
            if (enemy.CompareTag("Player"))
            {
                // �������ɱ�ը�˺�
                enemy.GetComponent<Character>().TakeDamage(explosionDamage);
            }
        }
    }
}

