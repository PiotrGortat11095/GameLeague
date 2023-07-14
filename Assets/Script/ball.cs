using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEditor.Build;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class ball : MonoBehaviour
{
    public Rigidbody fire;
    public Rigidbody magma;
    public Rigidbody meteor;
    public Transform spawnPoint;
    public Transform player;
    public GameObject Skill;
    public GameObject MeteorIcon;
    public Camera mainCamera;
    public float magmaLifetime = 5f;
    private Player playerScript;
    public LayerMask ThisLayers;
    public bool Enemy;
    public bool Dodano = false;
    public GameObject Skills;
    Skill skills;

    public int multipiler;
    public float maxMeteorDistance = 30f;

    private void Start()
    {
        playerScript = player.GetComponent<Player>();
    }
    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out _, maxMeteorDistance, ThisLayers))
        {
            Enemy = true;
        }
        else
        {
            Enemy = false;
        }
        if(playerScript.lvl >= 1 && !Dodano)
        {
            GameObject newSkill = Instantiate(Skill);
            Transform Skillposition = Skills.transform.Find("Image1/Image/Scroll/Panel");
            newSkill.transform.SetParent(Skillposition, false);
            GameObject SkillIcon = Instantiate(MeteorIcon);
            Transform SkillIconposition = Skills.transform.Find("Image1/Image/Scroll/Panel/Skill(Clone)/SkillIcon");
            SkillIcon.transform.SetParent(SkillIconposition, false);
            TextMeshProUGUI tekstComponent = Skills.transform.Find("Image1/Image/Scroll/Panel/Skill(Clone)/Text").GetComponentInChildren<TextMeshProUGUI>();
            tekstComponent.text = "Przywołanie Meteorytu";
            Dodano = true;
        }
        if(playerScript.lvl >= 1 && Dodano)
        {
            GameObject skill = Skills.transform.Find("Image1/Image/Scroll/Panel/Skill(Clone)/SkillIcon").gameObject;
            skills = skill.GetComponent<Skill>();
            if(skills.przedmiotWslocie == null)
            {
                GameObject SkillIcon = Instantiate(MeteorIcon);
                Transform SkillIconposition = Skills.transform.Find("Image1/Image/Scroll/Panel/Skill(Clone)/SkillIcon");
                SkillIcon.transform.SetParent(SkillIconposition, false);
            }
        }
    }
    public void FireBall()
    {
        Rigidbody fireInstance;
        fireInstance = Instantiate(fire, spawnPoint.position, fire.transform.rotation) as Rigidbody;

        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y + 10, mainCamera.transform.position.z));
        Vector3 direction = worldMousePosition - spawnPoint.position;

        fireInstance.AddForce(direction.normalized * multipiler);
        fireInstance.gameObject.AddComponent<FireDestroyer>();
    }
    public int mana;

    public void MagmaWall()
    {
        if (playerScript.PcurrentMana >= 10)
        {
            mana = 10;
        playerScript.Mana(mana);

            Rigidbody magmaInstance;
            Vector3 spawnPoint2 = player.position + player.forward * 2f;
            magmaInstance = Instantiate(magma, spawnPoint2, magma.transform.rotation) as Rigidbody;
            magmaInstance.transform.rotation = Quaternion.Euler(0f, mainCamera.transform.rotation.eulerAngles.y, 0f);
            Destroy(magmaInstance.gameObject, magmaLifetime);
        }
    }

    public void Meteor()
    {
        if (playerScript.PcurrentMana >= 30)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxMeteorDistance, ThisLayers))
            {
                mana = 30;
                playerScript.Mana(mana);
                Vector3 targetPosition = hit.point;
                targetPosition.y += 11f;
                if (Vector3.Distance(targetPosition, player.position) > maxMeteorDistance)
                {
                    targetPosition = player.position + (targetPosition - player.position).normalized * maxMeteorDistance;
                }
                Rigidbody meteorInstance;
                meteorInstance = Instantiate(meteor, targetPosition, meteor.transform.rotation) as Rigidbody;
                meteorInstance.gameObject.AddComponent<FireDestroyer2>();
                meteorInstance.AddForce(0, -1500000000, 0);
                Destroy(meteorInstance.gameObject, magmaLifetime);
            }
        }

    }
}

public class FireDestroyer : MonoBehaviour
{
    private Player playerScript;

    private void Start()
    {
        playerScript = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        AiMobs enemy = other.gameObject.GetComponent<AiMobs>();
        if (enemy != null)
        {
            if (Random.value <= playerScript.CriticalHitChance)
            {
                enemy.TakeDamage(Mathf.Round(playerScript.damage * playerScript.CriticalHitStrength));
                Destroy(gameObject);
            }
            else
            {
                enemy.TakeDamage(playerScript.damage);
                Destroy(gameObject);
            }
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            Destroy(gameObject);
        }
    }
}

public class FireDestroyer2 : MonoBehaviour
{
    private Player playerScript;
    public float ultimatedamage;
    private void Start()
    {
        playerScript = FindObjectOfType<Player>();
        ultimatedamage = 25 * playerScript.damage/10;
    }

    private void OnTriggerEnter(Collider other)
    {
        AiMobs enemy = other.gameObject.GetComponent<AiMobs>();
        if (enemy != null)
        {
            if (Random.value <= playerScript.CriticalHitChance)
            {
                enemy.TakeDamage(Mathf.Round(ultimatedamage * playerScript.CriticalHitStrength));

            }
            else
            {
                enemy.TakeDamage(ultimatedamage);

            }
        }
    }
}


